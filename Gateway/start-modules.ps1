<#
.SYNOPSIS
  Start, stop or snapshot ApiToolKit modules for the Swagger gateway (http://localhost:5090/swagger).

.EXAMPLE
  .\Gateway\start-modules.ps1 facebook youtube        # start two modules in the background
  .\Gateway\start-modules.ps1 all                     # start every module
  .\Gateway\start-modules.ps1 -Status                 # which modules are running
  .\Gateway\start-modules.ps1 -Stop                   # stop every module this script started
  .\Gateway\start-modules.ps1 -RefreshSpecs           # start each module once, cache its swagger.json, stop it
  .\Gateway\start-modules.ps1 -Gateway                # start the gateway itself
#>
param(
    [Parameter(Position = 0, ValueFromRemainingArguments = $true)]
    [string[]]$Modules = @(),
    [switch]$Stop,
    [switch]$Status,
    [switch]$RefreshSpecs,
    [switch]$Gateway
)

$ErrorActionPreference = 'Stop'
$repo      = Split-Path -Parent $PSScriptRoot
$gwProject = Join-Path $PSScriptRoot 'ApiToolKit.Gateway'
$config    = Get-Content (Join-Path $gwProject 'appsettings.json') -Raw | ConvertFrom-Json
$all       = @($config.Gateway.Modules)
$pidFile   = Join-Path $PSScriptRoot '.modules.pids.json'
$logDir    = Join-Path $PSScriptRoot 'logs'
$specDir   = Join-Path $gwProject 'specs'
New-Item -ItemType Directory -Force $logDir, $specDir | Out-Null

function Read-Pids {
    $map = @{}
    if (Test-Path $pidFile) {
        (Get-Content $pidFile -Raw | ConvertFrom-Json).PSObject.Properties | ForEach-Object { $map[$_.Name] = [int]$_.Value }
    }
    return $map
}
function Save-Pids($map) { $map | ConvertTo-Json | Set-Content $pidFile -Encoding utf8 }

function Test-Up($m) {
    try { (Invoke-WebRequest "$($m.BaseUrl)/swagger/v1/swagger.json" -UseBasicParsing -TimeoutSec 3).StatusCode -eq 200 } catch { $false }
}

function Start-Module($m) {
    if (Test-Up $m) { Write-Host "  = $($m.Key) already running on $($m.BaseUrl)"; return $null }
    $project = Join-Path $repo $m.Project
    $log = Join-Path $logDir "$($m.Key).log"
    $env:ASPNETCORE_ENVIRONMENT = 'Development'   # several modules only expose Swagger in Development
    $p = Start-Process dotnet -ArgumentList @('run', '--project', "`"$project`"", '--no-launch-profile', '--urls', $m.BaseUrl) `
        -WindowStyle Hidden -RedirectStandardOutput $log -RedirectStandardError "$log.err" -PassThru
    Write-Host "  + $($m.Key) starting on $($m.BaseUrl) (pid $($p.Id), log $log)"
    return $p
}

function Wait-Up($m, [int]$seconds = 180) {
    $deadline = (Get-Date).AddSeconds($seconds)
    while ((Get-Date) -lt $deadline) { if (Test-Up $m) { return $true }; Start-Sleep -Seconds 2 }
    return $false
}

function Stop-Tree([int]$id) { & taskkill /PID $id /T /F 2>$null | Out-Null }

function Resolve-Modules([string[]]$keys) {
    if ($keys -contains 'all') { return $all }
    $found = foreach ($k in $keys) {
        $m = $all | Where-Object { $_.Key -eq $k }
        if (-not $m) { throw "Unknown module '$k'. Known: $($all.Key -join ', ')" }
        $m
    }
    return @($found)
}

if ($Status) {
    foreach ($m in $all) { '{0,-20} {1,-8} {2}' -f $m.Key, $(if (Test-Up $m) { 'running' } else { '-' }), $m.BaseUrl }
    return
}

if ($Stop) {
    $pids = Read-Pids
    foreach ($k in @($pids.Keys)) { Stop-Tree $pids[$k]; Write-Host "  - stopped $k" }
    Save-Pids @{}
    return
}

if ($Gateway) {
    $pids = Read-Pids
    $p = Start-Process dotnet -ArgumentList @('run', '--project', "`"$gwProject`"", '--launch-profile', 'http') `
        -WindowStyle Hidden -RedirectStandardOutput (Join-Path $logDir 'gateway.log') -RedirectStandardError (Join-Path $logDir 'gateway.log.err') -PassThru
    $pids['gateway'] = $p.Id; Save-Pids $pids
    Write-Host "Gateway starting: http://localhost:5090/swagger (pid $($p.Id))"
    return
}

if ($RefreshSpecs) {
    $targets = if ($Modules.Count) { Resolve-Modules $Modules } else { $all }
    $ok = 0; $failed = @()
    foreach ($m in $targets) {
        $started = Start-Module $m
        if (Wait-Up $m) {
            Invoke-WebRequest "$($m.BaseUrl)/swagger/v1/swagger.json" -UseBasicParsing -OutFile (Join-Path $specDir "$($m.Key).json")
            Write-Host "  OK cached $($m.Key)"; $ok++
        } else {
            Write-Warning "  FAIL $($m.Key) did not come up - see $logDir\$($m.Key).log"; $failed += $m.Key
        }
        if ($started) { Stop-Tree $started.Id }
    }
    Write-Host "Cached $ok of $($targets.Count) module specs in $specDir"
    if ($failed) { Write-Host "Failed: $($failed -join ', ')" }
    return
}

if (-not $Modules.Count) { Get-Help $MyInvocation.MyCommand.Path -Examples; return }

$pids = Read-Pids
foreach ($m in Resolve-Modules $Modules) {
    $p = Start-Module $m
    if ($p) { $pids[$m.Key] = $p.Id }
}
Save-Pids $pids
Write-Host "Stop them later with: .\Gateway\start-modules.ps1 -Stop"
