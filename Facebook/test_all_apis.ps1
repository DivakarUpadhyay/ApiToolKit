param()
$TOKEN  = "EAAO2NnWebZBcBR0BZBBFdhUM1sFm3eYGWMUkDYDq3McnM5PnF5ZCD17z5XtfhXZAKHDn6XCaFoJM1W5Rtknl3ChsZCrPEx3NvS7KEyAZAKVZCvwExyXCilBy4Ytn5Yvmff8QPrEVnZBjXZA7R2v2Nv4PWpmpXPZCfFB4ZCgQzC2vHXfew4CxP8Y6TcfGKO897vkzijsCvIPjHkoPQ2ZBN7X8KRfZCHYnyyT9jLple1A26Eypr"
$BASE   = "http://localhost:5150"
$UID    = "122108821923364890"
$BID    = "1084873647383329"
$APPID  = "1044769947938791"
$PAGE_ID    = "1248325291691587"
$utf8   = New-Object System.Text.UTF8Encoding $false

$pass = 0; $fail = 0; $results = @()
$module = ""

function jfile([string]$json) {
    $f = [System.IO.Path]::GetTempFileName()
    [System.IO.File]::WriteAllText($f, $json, $script:utf8)
    return $f
}

function T {
    param([string]$Label, [string]$Method, [string]$Url, [string]$Body = "")
    $ca = @("--silent", "--max-time", "15", "-w", "%{http_code}", "-o", "NUL", "-X", $Method.ToUpper(), $Url)
    $f = $null
    if ($Body) {
        $f = jfile $Body
        $ca += @("-H", "Content-Type: application/json", "-d", "@$f")
    }
    $code = & curl.exe @ca
    if ($f) { Remove-Item $f -Force -ErrorAction SilentlyContinue }
    $ok = ($code -ge 200 -and $code -lt 400)
    $icon = if ($ok) { "OK  " } else { "FAIL" }
    Write-Host ("  {0} [{1}] {2}" -f $icon, $code, $Label)
    $script:results += [PSCustomObject]@{ Module=$script:module; Code=$code; Label=$Label; OK=$ok }
    if ($ok) { $script:pass++ } else { $script:fail++ }
}

function TMultipart {
    param([string]$Label, [string]$Url, [string]$TempContent = "test feed content")
    $tmpFile = [System.IO.Path]::GetTempFileName() + ".csv"
    [System.IO.File]::WriteAllText($tmpFile, $TempContent, $script:utf8)
    $ca = @("--silent", "--max-time", "15", "-w", "%{http_code}", "-o", "NUL", "-X", "POST", $Url, "-F", "file=@$tmpFile")
    $code = & curl.exe @ca
    Remove-Item $tmpFile -Force -ErrorAction SilentlyContinue
    $ok = ($code -ge 200 -and $code -lt 400)
    $icon = if ($ok) { "OK  " } else { "FAIL" }
    Write-Host ("  {0} [{1}] {2}" -f $icon, $code, $Label)
    $script:results += [PSCustomObject]@{ Module=$script:module; Code=$code; Label=$Label; OK=$ok }
    if ($ok) { $script:pass++ } else { $script:fail++ }
}

# â”€â”€ 1. AUTH â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Auth"
Write-Host "`n=== 1. AUTH ==="
T "POST auth-url"          POST "$BASE/api/facebook/auth/auth-url"            '{"scope":["public_profile","business_management"],"state":"login1"}'
T "GET  app-token"         GET  "$BASE/api/facebook/auth/app-token"
T "POST token/debug"       POST "$BASE/api/facebook/auth/token/debug"         ('{"inputToken":"'+$TOKEN+'"}')
T "POST token/refresh"     POST "$BASE/api/facebook/auth/token/refresh"       ('{"accessToken":"'+$TOKEN+'"}')
T "POST token/long-lived"  POST "$BASE/api/facebook/auth/token/long-lived"    ('{"shortLivedToken":"'+$TOKEN+'"}')

# â”€â”€ 2. USER â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "User"
Write-Host "`n=== 2. USER ==="
T "GET profile"     GET "$BASE/api/facebook/user/$UID/profile?accessToken=$TOKEN"
T "GET friends"     GET "$BASE/api/facebook/user/$UID/friends?accessToken=$TOKEN"
T "GET permissions" GET "$BASE/api/facebook/user/$UID/permissions?accessToken=$TOKEN"
T "GET picture"     GET "$BASE/api/facebook/user/$UID/picture?accessToken=$TOKEN"
T "GET feed"        GET "$BASE/api/facebook/user/$UID/feed?accessToken=$TOKEN"
T "GET pages"       GET "$BASE/api/facebook/user/$UID/pages?accessToken=$TOKEN"
T "GET ad-accounts" GET "$BASE/api/facebook/user/$UID/ad-accounts?accessToken=$TOKEN"

# â”€â”€ 3. BUSINESS â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Business"
Write-Host "`n=== 3. BUSINESS ==="
T "GET  business"           GET    "$BASE/api/facebook/business/$BID`?accessToken=$TOKEN"
T "GET  pages"              GET    "$BASE/api/facebook/business/$BID/pages?accessToken=$TOKEN"
T "GET  ad-accounts"        GET    "$BASE/api/facebook/business/$BID/ad-accounts?accessToken=$TOKEN"
T "GET  instagram-accounts" GET    "$BASE/api/facebook/business/$BID/instagram-accounts?accessToken=$TOKEN"
T "GET  catalogs"           GET    "$BASE/api/facebook/business/$BID/catalogs?accessToken=$TOKEN"
T "GET  users"              GET    "$BASE/api/facebook/business/$BID/users?accessToken=$TOKEN"
T "GET  system-users"       GET    "$BASE/api/facebook/business/$BID/system-users?accessToken=$TOKEN"
T "GET  client-pages"       GET    "$BASE/api/facebook/business/$BID/client-pages?accessToken=$TOKEN"
T "POST users-add"          POST   "$BASE/api/facebook/business/$BID/users?accessToken=$TOKEN"       ('{"userId":"'+$UID+'","role":"EMPLOYEE","email":"user@example.com"}')
T "DEL  users"              DELETE "$BASE/api/facebook/business/$BID/users/$UID`?accessToken=$TOKEN"
T "POST claim-page"         POST   "$BASE/api/facebook/business/$BID/claim-page?accessToken=$TOKEN"  '{"pageId":"123456789"}'

# â”€â”€ 4. PAGE â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Page"
Write-Host "`n=== 4. PAGE ==="
T "GET  categories"        GET  "$BASE/api/facebook/page/categories?accessToken=$TOKEN"
T "GET  user pages"        GET  "$BASE/api/facebook/page/user/$UID/pages?accessToken=$TOKEN"
T "GET  page info"         GET  "$BASE/api/facebook/page/123456789/info?accessToken=$TOKEN"
T "GET  page access-token" GET  "$BASE/api/facebook/page/123456789/access-token?userAccessToken=$TOKEN&accessToken=$TOKEN"
T "PUT  page settings"     PUT  "$BASE/api/facebook/page/123456789/settings?accessToken=$TOKEN"  '{"pageId":"123456789","setting":"MESSAGING","value":"true"}'
T "POST page post"         POST "$BASE/api/facebook/page/123456789/post?accessToken=$TOKEN"       '{"pageId":"123456789","message":"Test post from ApiToolKit"}'
T "POST page feed"         POST "$BASE/api/facebook/page/123456789/feed?accessToken=$TOKEN"       '{"pageId":"123456789","limit":10}'
T "POST page photos"       POST "$BASE/api/facebook/page/123456789/photos?accessToken=$TOKEN"     '{"pageId":"123456789","limit":10}'
T "POST page videos"       POST "$BASE/api/facebook/page/123456789/videos?accessToken=$TOKEN"     '{"pageId":"123456789","limit":10}'
T "GET  page events"       GET  "$BASE/api/facebook/page/123456789/events?accessToken=$TOKEN"
T "PUT  page picture"      PUT  "$BASE/api/facebook/page/123456789/picture?accessToken=$TOKEN"    '{"pageId":"123456789","picture":"https://example.com/img.jpg"}'
T "GET  page roles"        GET  "$BASE/api/facebook/page/123456789/roles?accessToken=$TOKEN"
T "GET  tabs"              GET  "$BASE/api/facebook/page/123456789/tabs?accessToken=$TOKEN"
T "POST add tab"           POST "$BASE/api/facebook/page/123456789/tabs?accessToken=$TOKEN"              '{"pageId":"123456789","appId":"987654321"}'
T "PUT  update tab"        PUT  "$BASE/api/facebook/page/123456789/tabs/photos?accessToken=$TOKEN"       '{"pageId":"123456789","tabId":"photos","customName":"Our Photos"}'
T "DEL  delete tab"        DELETE "$BASE/api/facebook/page/123456789/tabs/photos?accessToken=$TOKEN"
T "GET  milestones"        GET  "$BASE/api/facebook/page/123456789/milestones?accessToken=$TOKEN"
T "POST create milestone"  POST "$BASE/api/facebook/page/123456789/milestones?accessToken=$TOKEN"        '{"pageId":"123456789","title":"Founded","startTime":"2020-01-01"}'
T "DEL  delete milestone"  DELETE "$BASE/api/facebook/page/123456789/milestones/milestone1?accessToken=$TOKEN"
T "GET  blocked users"     GET  "$BASE/api/facebook/page/123456789/blocked?accessToken=$TOKEN&limit=10"
T "POST block user"        POST "$BASE/api/facebook/page/123456789/blocked?accessToken=$TOKEN"           '{"pageId":"123456789","userId":"987654321"}'
T "DEL  unblock user"      DELETE "$BASE/api/facebook/page/123456789/blocked/987654321?accessToken=$TOKEN"
T "GET  cta"               GET  "$BASE/api/facebook/page/123456789/cta?accessToken=$TOKEN"
T "POST create cta"        POST "$BASE/api/facebook/page/123456789/cta?accessToken=$TOKEN"               '{"pageId":"123456789","type":"LEARN_MORE","webUrl":"https://example.com"}'
T "DEL  delete cta"        DELETE "$BASE/api/facebook/page/123456789/cta/cta1?accessToken=$TOKEN"
T "POST visitor posts"     POST "$BASE/api/facebook/page/123456789/visitor-posts?accessToken=$TOKEN"     '{"pageId":"123456789","limit":10}'
T "GET  labels"            GET  "$BASE/api/facebook/page/123456789/labels?accessToken=$TOKEN"
T "POST create label"      POST "$BASE/api/facebook/page/123456789/labels?accessToken=$TOKEN"            '{"pageId":"123456789","name":"VIP Customer"}'
T "DEL  delete label"      DELETE "$BASE/api/facebook/page/123456789/labels/label1?accessToken=$TOKEN"
T "POST assign label"      POST "$BASE/api/facebook/page/123456789/labels/assign?accessToken=$TOKEN"     '{"pageId":"123456789","labelId":"label1","userId":"987654321"}'
T "POST published posts"   POST "$BASE/api/facebook/page/123456789/published-posts?accessToken=$TOKEN"   '{"pageId":"123456789","limit":10}'
T "POST scheduled posts"   POST "$BASE/api/facebook/page/123456789/scheduled-posts?accessToken=$TOKEN"   '{"pageId":"123456789","limit":10}'
T "POST promotable posts"  POST "$BASE/api/facebook/page/123456789/promotable-posts?accessToken=$TOKEN"  '{"pageId":"123456789","limit":10}'

# â”€â”€ 5. POST â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Post"
Write-Host "`n=== 5. POST ==="
$POSTID = "122108821923364890"
T "GET  post"          GET    "$BASE/api/facebook/post/$POSTID`?accessToken=$TOKEN"
T "PUT  post"          PUT    "$BASE/api/facebook/post/$POSTID`?accessToken=$TOKEN"   ('{"postId":"'+$POSTID+'","message":"updated message"}')
T "DEL  post"          DELETE "$BASE/api/facebook/post/$POSTID`?accessToken=$TOKEN"
T "POST feed"          POST   "$BASE/api/facebook/post/feed?accessToken=$TOKEN"       '{"pageId":"123456789","message":"Hello","link":""}'
T "POST comments list" POST   "$BASE/api/facebook/post/$POSTID/comments?accessToken=$TOKEN" ('{"objectId":"'+$POSTID+'","limit":10}')
T "POST reactions"     POST   "$BASE/api/facebook/post/$POSTID/reactions?accessToken=$TOKEN" ('{"objectId":"'+$POSTID+'","type":"LIKE"}')
T "POST publish"       POST   "$BASE/api/facebook/post/$POSTID/publish?accessToken=$TOKEN"   ('{"postId":"'+$POSTID+'"}')

# â”€â”€ 6. COMMENT â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Comment"
Write-Host "`n=== 6. COMMENT ==="
$CMTID = "123456789"
T "GET  comment" GET    "$BASE/api/facebook/comment/$CMTID`?accessToken=$TOKEN"
T "POST list"    POST   "$BASE/api/facebook/comment/list?accessToken=$TOKEN"         ('{"objectId":"'+$POSTID+'","limit":10}')
T "PUT  comment" PUT    "$BASE/api/facebook/comment/$CMTID`?accessToken=$TOKEN"      ('{"commentId":"'+$CMTID+'","message":"updated comment"}')
T "DEL  comment" DELETE "$BASE/api/facebook/comment/$CMTID`?accessToken=$TOKEN"
T "POST hide"    POST   "$BASE/api/facebook/comment/$CMTID/hide?accessToken=$TOKEN" ('{"commentId":"'+$CMTID+'","isHidden":true}')
T "GET  replies" GET    "$BASE/api/facebook/comment/$CMTID/replies?accessToken=$TOKEN"
T "POST like"    POST   "$BASE/api/facebook/comment/$CMTID/like?accessToken=$TOKEN"

# â”€â”€ 7. PHOTO â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Photo"
Write-Host "`n=== 7. PHOTO ==="
T "POST upload"       POST   "$BASE/api/facebook/photo/upload?accessToken=$TOKEN"        '{"pageId":"123456789","url":"https://example.com/img.jpg","caption":"test"}'
T "GET  photo"        GET    "$BASE/api/facebook/photo/123456789?accessToken=$TOKEN"
T "POST list"         POST   "$BASE/api/facebook/photo/list?accessToken=$TOKEN"          '{"pageId":"123456789","limit":10}'
T "DEL  photo"        DELETE "$BASE/api/facebook/photo/123456789?accessToken=$TOKEN"
T "GET  albums"       GET    "$BASE/api/facebook/photo/123456789/albums?accessToken=$TOKEN"
T "POST album"        POST   "$BASE/api/facebook/photo/album?accessToken=$TOKEN"         '{"pageId":"123456789","name":"Test Album","message":"desc"}'
T "GET  album photos" GET    "$BASE/api/facebook/photo/123456789/photos?accessToken=$TOKEN"

# â”€â”€ 8. VIDEO â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Video"
Write-Host "`n=== 8. VIDEO ==="
T "POST upload"     POST   "$BASE/api/facebook/video/upload?accessToken=$TOKEN"    '{"pageId":"123456789","fileUrl":"https://example.com/vid.mp4","title":"Test","description":"desc"}'
T "GET  video"      GET    "$BASE/api/facebook/video/123456789?accessToken=$TOKEN"
T "POST list"       POST   "$BASE/api/facebook/video/list?accessToken=$TOKEN"      '{"pageId":"123456789","limit":10}'
T "PUT  video"      PUT    "$BASE/api/facebook/video/123456789?accessToken=$TOKEN" '{"title":"Updated","description":"updated desc"}'
T "DEL  video"      DELETE "$BASE/api/facebook/video/123456789?accessToken=$TOKEN"
T "GET  status"     GET    "$BASE/api/facebook/video/123456789/status?accessToken=$TOKEN"
T "GET  thumbnails" GET    "$BASE/api/facebook/video/123456789/thumbnails?accessToken=$TOKEN"

# â”€â”€ 9. EVENT â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Event"
Write-Host "`n=== 9. EVENT ==="
T "GET  event"       GET    "$BASE/api/facebook/event/123456789?accessToken=$TOKEN"
T "PUT  event"       PUT    "$BASE/api/facebook/event/123456789?accessToken=$TOKEN"           '{"eventId":"123456789","name":"Updated Event","startTime":"2026-08-01T10:00:00"}'
T "DEL  event"       DELETE "$BASE/api/facebook/event/123456789?accessToken=$TOKEN"
T "GET  page events" GET    "$BASE/api/facebook/event/123456789/events?accessToken=$TOKEN"
T "POST attendees"   POST   "$BASE/api/facebook/event/123456789/attendees?accessToken=$TOKEN" '{"eventId":"123456789","limit":25}'
T "POST rsvp"        POST   "$BASE/api/facebook/event/123456789/rsvp?accessToken=$TOKEN"      '{"eventId":"123456789","rsvp":"attending"}'

# â”€â”€ 10. GROUP â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Group"
Write-Host "`n=== 10. GROUP ==="
T "GET  group"       GET  "$BASE/api/facebook/group/123456789?accessToken=$TOKEN"
T "GET  user groups" GET  "$BASE/api/facebook/group/user/$UID/groups?accessToken=$TOKEN"
T "POST feed"        POST "$BASE/api/facebook/group/123456789/feed?accessToken=$TOKEN"    '{"groupId":"123456789","limit":25}'
T "POST members"     POST "$BASE/api/facebook/group/123456789/members?accessToken=$TOKEN" '{"groupId":"123456789","limit":25}'
T "POST post"        POST "$BASE/api/facebook/group/123456789/post?accessToken=$TOKEN"    '{"groupId":"123456789","message":"Hello group"}'
T "POST albums"      POST "$BASE/api/facebook/group/123456789/albums?accessToken=$TOKEN"  '{"groupId":"123456789"}'

# â”€â”€ 11. REACTION â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Reaction"
Write-Host "`n=== 11. REACTION ==="
T "POST list"     POST   "$BASE/api/facebook/reaction/list?accessToken=$TOKEN"        ('{"objectId":"'+$POSTID+'","limit":10}')
T "DEL  reaction" DELETE "$BASE/api/facebook/reaction/$POSTID`?accessToken=$TOKEN"

# â”€â”€ 12. INSIGHT â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Insight"
Write-Host "`n=== 12. INSIGHT ==="
T "POST page insight"     POST "$BASE/api/facebook/insight/page?accessToken=$TOKEN"              ('{"pageId":"'+$BID+'","metric":["page_fans"],"period":"day"}')
T "POST video insight"    POST "$BASE/api/facebook/insight/video?accessToken=$TOKEN"             '{"videoId":"123456789","metric":"total_video_views","period":"day"}'
T "POST ad insight"       POST "$BASE/api/facebook/insight/ad?accessToken=$TOKEN"                '{"adId":"123456789","accountId":"act_123456789","fields":["impressions","clicks"]}'
T "GET  campaign insight" GET  "$BASE/api/facebook/insight/campaign/123456789?accessToken=$TOKEN"
T "GET  adset insight"    GET  "$BASE/api/facebook/insight/adset/123456789?accessToken=$TOKEN"
T "GET  account insight"  GET  "$BASE/api/facebook/insight/account/act_123456789?accessToken=$TOKEN"

# â”€â”€ 13. AD ACCOUNT â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "AdAccount"
Write-Host "`n=== 13. AD ACCOUNT ==="
T "GET  account"       GET  "$BASE/api/facebook/ad-account/act_123456789?accessToken=$TOKEN"
T "POST user accounts" POST "$BASE/api/facebook/ad-account/user/ad-accounts?accessToken=$TOKEN"   ('{"userId":"'+$UID+'"}')
T "GET  billing"       GET  "$BASE/api/facebook/ad-account/act_123456789/billing?accessToken=$TOKEN"
T "POST add user"      POST "$BASE/api/facebook/ad-account/act_123456789/users?accessToken=$TOKEN" ('{"accountId":"act_123456789","userId":"'+$UID+'","role":"ADMIN"}')
T "POST campaigns"     POST "$BASE/api/facebook/ad-account/act_123456789/campaigns?accessToken=$TOKEN" '{"accountId":"act_123456789","limit":10}'
T "GET  creatives"     GET  "$BASE/api/facebook/ad-account/act_123456789/creatives?accessToken=$TOKEN"

# â”€â”€ 14. CAMPAIGN â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Campaign"
Write-Host "`n=== 14. CAMPAIGN ==="
T "POST create"          POST   "$BASE/api/facebook/campaign?accessToken=$TOKEN"           '{"accountId":"act_123456789","name":"Test Campaign","objective":"OUTCOME_AWARENESS","status":"PAUSED","dailyBudget":1000}'
T "GET  campaign"        GET    "$BASE/api/facebook/campaign/123456789?accessToken=$TOKEN"
T "POST list"            POST   "$BASE/api/facebook/campaign/list?accessToken=$TOKEN"      '{"accountId":"act_123456789","limit":10}'
T "PUT  campaign"        PUT    "$BASE/api/facebook/campaign/123456789?accessToken=$TOKEN" '{"campaignId":"123456789","name":"Updated Campaign","status":"ACTIVE"}'
T "DEL  campaign"        DELETE "$BASE/api/facebook/campaign/123456789?accessToken=$TOKEN"
T "GET  campaign adsets" GET    "$BASE/api/facebook/campaign/123456789/adsets?accessToken=$TOKEN"
T "GET  campaign ads"    GET    "$BASE/api/facebook/campaign/123456789/ads?accessToken=$TOKEN"

# â”€â”€ 15. AD SET â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "AdSet"
Write-Host "`n=== 15. AD SET ==="
T "POST create" POST   "$BASE/api/facebook/adset?accessToken=$TOKEN"           '{"campaignId":"123456789","accountId":"act_123456789","name":"Test AdSet","dailyBudget":500,"bidAmount":100,"billingEvent":"IMPRESSIONS","optimizationGoal":"REACH","targeting":{}}'
T "GET  adset"  GET    "$BASE/api/facebook/adset/123456789?accessToken=$TOKEN"
T "POST list"   POST   "$BASE/api/facebook/adset/list?accessToken=$TOKEN"      '{"campaignId":"123456789","limit":10}'
T "PUT  adset"  PUT    "$BASE/api/facebook/adset/123456789?accessToken=$TOKEN" '{"adSetId":"123456789","name":"Updated AdSet","status":"ACTIVE"}'
T "DEL  adset"  DELETE "$BASE/api/facebook/adset/123456789?accessToken=$TOKEN"
T "GET  ads"    GET    "$BASE/api/facebook/adset/123456789/ads?accessToken=$TOKEN"

# â”€â”€ 16. AD â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Ad"
Write-Host "`n=== 16. AD ==="
T "POST create"    POST   "$BASE/api/facebook/ad?accessToken=$TOKEN"                    '{"adSetId":"123456789","accountId":"act_123456789","name":"Test Ad","status":"PAUSED","creative":{}}'
T "GET  ad"        GET    "$BASE/api/facebook/ad/123456789?accessToken=$TOKEN"
T "POST list"      POST   "$BASE/api/facebook/ad/list?accessToken=$TOKEN"               '{"adSetId":"123456789","limit":10}'
T "PUT  ad"        PUT    "$BASE/api/facebook/ad/123456789?accessToken=$TOKEN"          '{"adId":"123456789","name":"Updated Ad","status":"ACTIVE"}'
T "DEL  ad"        DELETE "$BASE/api/facebook/ad/123456789?accessToken=$TOKEN"
T "POST creative"  POST   "$BASE/api/facebook/ad/creative?accessToken=$TOKEN"           '{"accountId":"act_123456789","name":"Test Creative","objectStorySpec":{}}'
T "GET  preview"   GET    "$BASE/api/facebook/ad/123456789/preview?adFormat=DESKTOP_FEED_STANDARD&accessToken=$TOKEN"
T "GET  creatives" GET    "$BASE/api/facebook/ad/account/act_123456789/creatives?accessToken=$TOKEN"

# â”€â”€ 17. PIXEL â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Pixel"
Write-Host "`n=== 17. PIXEL ==="
T "POST create"      POST "$BASE/api/facebook/pixel/account/act_123456789?accessToken=$TOKEN" '{"name":"My Test Pixel"}'
T "GET  acct pixels" GET  "$BASE/api/facebook/pixel/account/act_123456789?accessToken=$TOKEN"
T "GET  pixel"       GET  "$BASE/api/facebook/pixel/123456789?accessToken=$TOKEN"
T "PUT  pixel"       PUT  "$BASE/api/facebook/pixel/123456789?accessToken=$TOKEN"              '{"name":"Updated Pixel"}'
T "GET  stats"       GET  "$BASE/api/facebook/pixel/123456789/stats?accessToken=$TOKEN"
T "GET  audiences"   GET  "$BASE/api/facebook/pixel/123456789/audiences?accessToken=$TOKEN"
T "POST share"       POST "$BASE/api/facebook/pixel/123456789/share?accessToken=$TOKEN"        '{"targetAdAccountId":"act_123456789"}'
T "GET  diagnostics" GET  "$BASE/api/facebook/pixel/123456789/diagnostics?accessToken=$TOKEN"

# â”€â”€ 18. CAPI â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "CAPI"
Write-Host "`n=== 18. CAPI ==="
T "POST events"      POST "$BASE/api/facebook/capi/events"             ('{"PixelId":"123456789","AccessToken":"'+$TOKEN+'","Data":[{"EventName":"Purchase","EventTime":1700000000}]}')
T "POST purchase"    POST "$BASE/api/facebook/capi/events/purchase"    ('{"PixelId":"123456789","AccessToken":"'+$TOKEN+'","EventTime":1700000000,"Value":29.99,"Currency":"USD"}')
T "POST add-to-cart" POST "$BASE/api/facebook/capi/events/add-to-cart" ('{"PixelId":"123456789","AccessToken":"'+$TOKEN+'","EventTime":1700000000,"Value":50.0,"Currency":"USD"}')
T "POST page-view"   POST "$BASE/api/facebook/capi/events/page-view"   ('{"PixelId":"123456789","AccessToken":"'+$TOKEN+'","EventTime":1700000000,"EventSourceUrl":"https://test.com"}')
T "POST lead"        POST "$BASE/api/facebook/capi/events/lead"        ('{"PixelId":"123456789","AccessToken":"'+$TOKEN+'","EventTime":1700000000,"Email":"test@test.com"}')

# â”€â”€ 19. CATALOG â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Catalog"
Write-Host "`n=== 19. CATALOG ==="
T "POST create"      POST   "$BASE/api/facebook/catalog?accessToken=$TOKEN"                    ('{"businessId":"'+$BID+'","name":"Test Catalog","verticalType":"commerce"}')
T "GET  catalog"     GET    "$BASE/api/facebook/catalog/123456789?accessToken=$TOKEN"
T "POST list"        POST   "$BASE/api/facebook/catalog/list?accessToken=$TOKEN"               ('{"businessId":"'+$BID+'"}')
T "DEL  catalog"     DELETE "$BASE/api/facebook/catalog/123456789?accessToken=$TOKEN"
T "POST add product" POST   "$BASE/api/facebook/catalog/123456789/product?accessToken=$TOKEN"  '{"catalogId":"123456789","retailerId":"SKU001","name":"Test Product","description":"desc","price":100,"currency":"USD","url":"https://example.com","imageUrl":"https://example.com/img.jpg","availability":"in stock","condition":"new","brand":"TestBrand"}'
T "GET  product"     GET    "$BASE/api/facebook/catalog/product/prod123?accessToken=$TOKEN"
T "POST list prods"  POST   "$BASE/api/facebook/catalog/123456789/products?accessToken=$TOKEN" '{"catalogId":"123456789","limit":25}'
T "PUT  product"     PUT    "$BASE/api/facebook/catalog/product/prod123?accessToken=$TOKEN"    '{"productId":"prod123","price":200,"availability":"out of stock"}'
T "DEL  product"     DELETE "$BASE/api/facebook/catalog/product/prod123?accessToken=$TOKEN"
T "POST prod-set"    POST   "$BASE/api/facebook/catalog/123456789/product-set?accessToken=$TOKEN" '{"catalogId":"123456789","name":"Test Set","filter":{}}'
T "GET  prod-sets"   GET    "$BASE/api/facebook/catalog/123456789/product-sets?accessToken=$TOKEN"

# â”€â”€ 20. INSTAGRAM â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Instagram"
$IGID = "123456789"
Write-Host "`n=== 20. INSTAGRAM ==="
T "GET  account"          GET    "$BASE/api/facebook/instagram/$IGID/account?accessToken=$TOKEN"
T "GET  page instagram"   GET    "$BASE/api/facebook/instagram/page/123456789/instagram?accessToken=$TOKEN"
T "POST media"            POST   "$BASE/api/facebook/instagram/media?accessToken=$TOKEN"                ('{"igUserId":"'+$IGID+'","imageUrl":"https://example.com/img.jpg","caption":"Test"}')
T "POST media/publish"    POST   "$BASE/api/facebook/instagram/media/publish?accessToken=$TOKEN"        ('{"igUserId":"'+$IGID+'","creationId":"123456789"}')
T "POST media/list"       POST   "$BASE/api/facebook/instagram/media/list?accessToken=$TOKEN"           ('{"igUserId":"'+$IGID+'","limit":10}')
T "POST media/insights"   POST   "$BASE/api/facebook/instagram/media/insights?accessToken=$TOKEN"       '{"mediaId":"123456789","metric":["impressions","reach"]}'
T "POST hashtag/search"   POST   "$BASE/api/facebook/instagram/hashtag/search?accessToken=$TOKEN"       ('{"igUserId":"'+$IGID+'","hashtagName":"sunset"}')
T "POST hashtag/media"    POST   "$BASE/api/facebook/instagram/hashtag/media?accessToken=$TOKEN"        ('{"igUserId":"'+$IGID+'","hashtagId":"123456789","type":"top_media"}')
T "GET  media comments"   GET    "$BASE/api/facebook/instagram/media/123456789/comments?accessToken=$TOKEN"
T "POST comment reply"    POST   "$BASE/api/facebook/instagram/comment/123456789/reply?message=Hello&accessToken=$TOKEN"
T "DEL  comment"          DELETE "$BASE/api/facebook/instagram/comment/123456789?accessToken=$TOKEN"
T "POST biz-discovery"    POST   "$BASE/api/facebook/instagram/business-discovery?accessToken=$TOKEN"   ('{"igUserId":"'+$IGID+'","username":"testuser","accessToken":"'+$TOKEN+'"}')
T "GET  insights"         GET    "$BASE/api/facebook/instagram/$IGID/insights?metrics=follower_count&period=day&accessToken=$TOKEN"
T "POST story"            POST   "$BASE/api/facebook/instagram/story"                                   ('{"igUserId":"'+$IGID+'","imageUrl":"https://example.com/img.jpg","accessToken":"'+$TOKEN+'"}')
T "POST reel"             POST   "$BASE/api/facebook/instagram/reel"                                    ('{"igUserId":"'+$IGID+'","videoUrl":"https://example.com/vid.mp4","caption":"Test reel","accessToken":"'+$TOKEN+'"}')
T "POST carousel"         POST   "$BASE/api/facebook/instagram/carousel"                                ('{"igUserId":"'+$IGID+'","items":[{"imageUrl":"https://example.com/img.jpg","isCarouselItem":true}],"caption":"Test","accessToken":"'+$TOKEN+'"}')
T "POST container status" POST   "$BASE/api/facebook/instagram/container/status"                        ('{"containerId":"123456789","accessToken":"'+$TOKEN+'"}')
T "POST publishing-limit" POST   "$BASE/api/facebook/instagram/publishing-limit"                        ('{"igUserId":"'+$IGID+'","accessToken":"'+$TOKEN+'"}')
T "POST mentions"         POST   "$BASE/api/facebook/instagram/mentions"                                ('{"igUserId":"'+$IGID+'","limit":10,"accessToken":"'+$TOKEN+'"}')
T "POST tagged-media"     POST   "$BASE/api/facebook/instagram/tagged-media"                            ('{"igUserId":"'+$IGID+'","limit":10,"accessToken":"'+$TOKEN+'"}')
T "POST dm/send"          POST   "$BASE/api/facebook/instagram/dm/send"                                 ('{"igUserId":"'+$IGID+'","recipientIgId":"987654321","message":"Hello","accessToken":"'+$TOKEN+'"}')
T "POST dm/convos"        POST   "$BASE/api/facebook/instagram/dm/conversations"                        ('{"igUserId":"'+$IGID+'","limit":10,"accessToken":"'+$TOKEN+'"}')
T "GET  stories"          GET    "$BASE/api/facebook/instagram/$IGID/stories?accessToken=$TOKEN"
T "GET  live-media"       GET    "$BASE/api/facebook/instagram/$IGID/live-media?accessToken=$TOKEN"
T "GET  media children"   GET    "$BASE/api/facebook/instagram/media/123456789/children?accessToken=$TOKEN"
T "DEL  media"            DELETE "$BASE/api/facebook/instagram/media/123456789?accessToken=$TOKEN"
T "POST toggle-comments"  POST   "$BASE/api/facebook/instagram/media/123456789/toggle-comments?commentsEnabled=true&accessToken=$TOKEN"
T "GET  recent hashtags"  GET    "$BASE/api/facebook/instagram/$IGID/recently-searched-hashtags?accessToken=$TOKEN"
T "GET  mentioned-comment" GET   "$BASE/api/facebook/instagram/$IGID/mentioned-comment?commentId=123&accessToken=$TOKEN"
T "GET  mentioned-media"  GET    "$BASE/api/facebook/instagram/$IGID/mentioned-media?mediaId=123&accessToken=$TOKEN"
T "GET  media detail"     GET    "$BASE/api/facebook/instagram/media/123456789/detail?accessToken=$TOKEN"
T "POST comment hide"     POST   "$BASE/api/facebook/instagram/comment/123456789/hide?hide=true&accessToken=$TOKEN"
T "GET  comment replies"  GET    "$BASE/api/facebook/instagram/comment/123456789/replies?accessToken=$TOKEN"
T "GET  comment"          GET    "$BASE/api/facebook/instagram/comment/123456789?accessToken=$TOKEN"
T "POST media comment"    POST   "$BASE/api/facebook/instagram/media/123456789/comment"                 ('{"mediaId":"123456789","message":"Great post!","accessToken":"'+$TOKEN+'"}')
T "POST biz-disc media"   POST   "$BASE/api/facebook/instagram/business-discovery/media"                ('{"igUserId":"'+$IGID+'","username":"testuser","limit":10,"accessToken":"'+$TOKEN+'"}')
T "POST webhook subscribe" POST  "$BASE/api/facebook/instagram/webhook/subscribe"                       ('{"appId":"'+$APPID+'","igUserId":"'+$IGID+'","callbackUrl":"https://example.com/webhook","verifyToken":"secret","accessToken":"'+$TOKEN+'"}')
T "POST conv messages"    POST   "$BASE/api/facebook/instagram/conversation/messages"                   ('{"igUserId":"'+$IGID+'","conversationId":"123456789","accessToken":"'+$TOKEN+'"}')

# â”€â”€ 21. INSTAGRAM SHOPPING â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "IGShopping"
Write-Host "`n=== 21. INSTAGRAM SHOPPING ==="
T "GET  catalog"          GET    "$BASE/api/facebook/instagram-shopping/$IGID/catalog?accessToken=$TOKEN"
T "POST product-tags"     POST   "$BASE/api/facebook/instagram-shopping/media/123456789/product-tags?accessToken=$TOKEN"  '{"tags":[{"productId":"123","x":0.5,"y":0.5}]}'
T "GET  product-tags"     GET    "$BASE/api/facebook/instagram-shopping/media/123456789/product-tags?accessToken=$TOKEN"
T "DEL  product-tags"     DELETE "$BASE/api/facebook/instagram-shopping/media/123456789/product-tags?accessToken=$TOKEN"
T "GET  tagged-media"     GET    "$BASE/api/facebook/instagram-shopping/$IGID/tagged-media?accessToken=$TOKEN"
T "POST appeal"           POST   "$BASE/api/facebook/instagram-shopping/$IGID/appeal?accessToken=$TOKEN"  '{"productId":"123","appealReason":"Product correctly represents my account"}'
T "GET  eligibility"      GET    "$BASE/api/facebook/instagram-shopping/$IGID/eligibility?accessToken=$TOKEN"
T "GET  catalog products" GET    "$BASE/api/facebook/instagram-shopping/catalog/123456789/products?accessToken=$TOKEN"
T "GET  shops"            GET    "$BASE/api/facebook/instagram-shopping/$IGID/shops?accessToken=$TOKEN"
T "GET  avail-catalogs"   GET    "$BASE/api/facebook/instagram-shopping/$IGID/available-catalogs?accessToken=$TOKEN"

# â”€â”€ 22. LIVE â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Live"
Write-Host "`n=== 22. LIVE ==="
T "POST create live"   POST   "$BASE/api/facebook/live/123456789?accessToken=$TOKEN"             '{"title":"Test Live","description":"desc","privacy":"EVERYONE"}'
T "GET  page videos"   GET    "$BASE/api/facebook/live/123456789/videos?accessToken=$TOKEN"
T "GET  live video"    GET    "$BASE/api/facebook/live/video/123456789?accessToken=$TOKEN"
T "PUT  live video"    PUT    "$BASE/api/facebook/live/video/123456789?accessToken=$TOKEN"       '{"title":"Updated Live"}'
T "POST end live"      POST   "$BASE/api/facebook/live/video/123456789/end?accessToken=$TOKEN"
T "DEL  live video"    DELETE "$BASE/api/facebook/live/video/123456789?accessToken=$TOKEN"
T "GET  errors"        GET    "$BASE/api/facebook/live/video/123456789/errors?accessToken=$TOKEN"
T "GET  blocked-users" GET    "$BASE/api/facebook/live/video/123456789/blocked-users?accessToken=$TOKEN"

# â”€â”€ 23. MESSENGER â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Messenger"
Write-Host "`n=== 23. MESSENGER ==="
T "POST send"          POST "$BASE/api/facebook/messenger/send?pageAccessToken=$TOKEN"          '{"recipientId":"PSID123","messageText":"Hello"}'
T "POST conversations" POST "$BASE/api/facebook/messenger/conversations?accessToken=$TOKEN"     '{"pageId":"123456789","limit":25}'
T "POST messages"      POST "$BASE/api/facebook/messenger/messages?accessToken=$TOKEN"          '{"conversationId":"123456789","limit":25}'
T "POST user/profile"  POST "$BASE/api/facebook/messenger/user/profile?pageAccessToken=$TOKEN" '{"psid":"PSID123","pageId":"123456789"}'
T "POST mark-seen"     POST "$BASE/api/facebook/messenger/mark-seen?pageAccessToken=$TOKEN"    '{"recipientId":"PSID123"}'
T "POST typing"        POST "$BASE/api/facebook/messenger/typing?pageAccessToken=$TOKEN"       '{"recipientId":"PSID123","action":"typing_on"}'
T "POST subscribe"     POST "$BASE/api/facebook/messenger/subscribe?accessToken=$TOKEN"         ('{"pageId":"123456789","pageAccessToken":"'+$TOKEN+'"}')
T "GET  subscriptions" GET  "$BASE/api/facebook/messenger/page/123456789/subscriptions?accessToken=$TOKEN"
T "POST page profile"  POST "$BASE/api/facebook/messenger/page/123456789/profile?accessToken=$TOKEN" '{"greeting":"Hello","getStartedPayload":"START"}'

# â”€â”€ 24. THREADS â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Threads"
Write-Host "`n=== 24. THREADS ==="
T "GET  profile"          GET    "$BASE/api/facebook/threads/profile?accessToken=$TOKEN"
T "GET  posts"            GET    "$BASE/api/facebook/threads/posts?accessToken=$TOKEN"
T "POST text post"        POST   "$BASE/api/facebook/threads/posts/text?accessToken=$TOKEN"      '{"text":"Hello Threads"}'
T "POST image post"       POST   "$BASE/api/facebook/threads/posts/image?accessToken=$TOKEN"     '{"mediaUrl":"https://example.com/img.jpg","mediaType":"IMAGE","text":"caption"}'
T "POST video post"       POST   "$BASE/api/facebook/threads/posts/video?accessToken=$TOKEN"     '{"mediaUrl":"https://example.com/vid.mp4","mediaType":"VIDEO","text":"caption"}'
T "POST carousel post"    POST   "$BASE/api/facebook/threads/posts/carousel?accessToken=$TOKEN"  '{"mediaUrls":["https://example.com/img.jpg"],"text":"caption"}'
T "POST publish"          POST   "$BASE/api/facebook/threads/posts/publish?accessToken=$TOKEN"   '{"creationId":"123456789"}'
T "GET  thread"           GET    "$BASE/api/facebook/threads/posts/123456789?accessToken=$TOKEN"
T "DEL  thread"           DELETE "$BASE/api/facebook/threads/posts/123456789?accessToken=$TOKEN"
T "POST reply"            POST   "$BASE/api/facebook/threads/posts/123456789/reply?accessToken=$TOKEN" '{"text":"my reply"}'
T "GET  replies"          GET    "$BASE/api/facebook/threads/posts/123456789/replies?accessToken=$TOKEN"
T "GET  conversation"     GET    "$BASE/api/facebook/threads/posts/123456789/conversation?accessToken=$TOKEN"
T "GET  insights"         GET    "$BASE/api/facebook/threads/insights?accessToken=$TOKEN"
T "GET  post insights"    GET    "$BASE/api/facebook/threads/posts/123456789/insights?accessToken=$TOKEN"
T "GET  publishing-limit" GET    "$BASE/api/facebook/threads/publishing-limit?accessToken=$TOKEN"

# â”€â”€ 25. WEBHOOK â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Webhook"
Write-Host "`n=== 25. WEBHOOK ==="
T "POST subscribe"     POST "$BASE/api/facebook/webhook/subscribe?accessToken=$TOKEN"     ('{"objectType":"page","callbackUrl":"https://example.com/webhook","verifyToken":"secret","fields":["feed"],"appId":"'+$APPID+'"}')
T "POST subscriptions" POST "$BASE/api/facebook/webhook/subscriptions?accessToken=$TOKEN" ('{"appId":"'+$APPID+'"}')
T "GET  verify"        GET  "$BASE/api/facebook/webhook/verify?hub.mode=subscribe&hub.verify_token=secret&hub.challenge=CHALLENGE123"
T "POST payload"       POST "$BASE/api/facebook/webhook/payload"                          '{"object":"page","entry":[]}'

# â”€â”€ 26. COMMERCE â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "Commerce"
Write-Host "`n=== 26. COMMERCE ==="
T "GET  shops"           GET  "$BASE/api/facebook/commerce/shops/123456789?accessToken=$TOKEN"
T "GET  shop detail"     GET  "$BASE/api/facebook/commerce/shops/detail/123456789?accessToken=$TOKEN"
T "GET  orders"          GET  "$BASE/api/facebook/commerce/orders/123456789?accessToken=$TOKEN"
T "GET  order detail"    GET  "$BASE/api/facebook/commerce/orders/detail/order123?accessToken=$TOKEN"
T "POST acknowledge"     POST "$BASE/api/facebook/commerce/orders/order123/acknowledge?accessToken=$TOKEN"  '{"merchantOrderReference":"REF-ORD-123456"}'
T "POST shipments"       POST "$BASE/api/facebook/commerce/orders/order123/shipments?accessToken=$TOKEN"    '{"trackingNumber":"TRK123","carrierName":"UPS","shippingMethod":"STANDARD"}'
T "POST cancel"          POST "$BASE/api/facebook/commerce/orders/order123/cancel?accessToken=$TOKEN"       '{"reasonCode":"CUSTOMER_REQUESTED"}'
T "POST refund"          POST "$BASE/api/facebook/commerce/orders/order123/refund?accessToken=$TOKEN"       '{"refundAmount":10.00,"currency":"USD","reasonText":"Customer return"}'
T "GET  fulfillments"    GET  "$BASE/api/facebook/commerce/orders/order123/fulfillments?accessToken=$TOKEN"
T "GET  returns"         GET  "$BASE/api/facebook/commerce/orders/order123/returns?accessToken=$TOKEN"
T "GET  account"         GET  "$BASE/api/facebook/commerce/account/123456789?accessToken=$TOKEN"
T "GET  acct settings"   GET  "$BASE/api/facebook/commerce/account/123456789/settings?accessToken=$TOKEN"
T "GET  tax-settings"    GET  "$BASE/api/facebook/commerce/account/123456789/tax-settings?accessToken=$TOKEN"
T "PUT  tax-settings"    PUT  "$BASE/api/facebook/commerce/account/123456789/tax-settings?accessToken=$TOKEN"    '{"taxRemitState":"ENABLED"}'
T "GET  return-settings" GET  "$BASE/api/facebook/commerce/account/123456789/return-settings?accessToken=$TOKEN"
T "PUT  return-settings" PUT  "$BASE/api/facebook/commerce/account/123456789/return-settings?accessToken=$TOKEN" '{"returnDays":30}'
T "GET  shipping"        GET  "$BASE/api/facebook/commerce/account/123456789/shipping-profiles?accessToken=$TOKEN"
T "GET  collections"     GET  "$BASE/api/facebook/commerce/shops/123456789/collections?accessToken=$TOKEN"
T "POST create shop"     POST   "$BASE/api/facebook/commerce/shops/create?accessToken=$TOKEN"                                    '{"pageId":"123456789","catalogId":"cat123","name":"Test Shop","description":"My shop"}'
T "PUT  update shop"     PUT    "$BASE/api/facebook/commerce/shops/shop123/update?accessToken=$TOKEN"                            '{"shopId":"shop123","name":"Updated Shop","isEnabled":true}'
T "GET  connected cats"  GET    "$BASE/api/facebook/commerce/account/acct123/catalogs?accessToken=$TOKEN"
T "POST connect catalog" POST   "$BASE/api/facebook/commerce/account/acct123/catalogs/connect?accessToken=$TOKEN"               '{"commerceAccountId":"acct123","catalogId":"cat123"}'
T "DEL  disconnect cat"  DELETE "$BASE/api/facebook/commerce/account/acct123/catalogs/cat123?accessToken=$TOKEN"
T "GET  acct collections" GET   "$BASE/api/facebook/commerce/account/acct123/collections?accessToken=$TOKEN"
T "POST create coll"     POST   "$BASE/api/facebook/commerce/account/acct123/collections?accessToken=$TOKEN"                    '{"commerceAccountId":"acct123","name":"Summer Sale","description":"Summer products"}'
T "PUT  update coll"     PUT    "$BASE/api/facebook/commerce/collections/coll123?accessToken=$TOKEN"                            '{"collectionId":"coll123","name":"Updated Collection"}'
T "DEL  delete coll"     DELETE "$BASE/api/facebook/commerce/collections/coll123?accessToken=$TOKEN"
T "GET  coll products"   GET    "$BASE/api/facebook/commerce/collections/coll123/products?accessToken=$TOKEN"
T "POST add product"     POST   "$BASE/api/facebook/commerce/collections/coll123/products?accessToken=$TOKEN"                   '{"collectionId":"coll123","productId":"prod123"}'
T "DEL  remove product"  DELETE "$BASE/api/facebook/commerce/collections/coll123/products?accessToken=$TOKEN"                   '{"collectionId":"coll123","productId":"prod123"}'

# â”€â”€ 27. PRODUCT FEED â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "ProductFeed"
Write-Host "`n=== 27. PRODUCT FEED ==="
T "POST create feed"   POST   "$BASE/api/facebook/product-feed/123456789/feeds?accessToken=$TOKEN"           '{"name":"Test Feed","url":"https://example.com/feed.csv","schedule":"DAILY"}'
T "GET  feeds"         GET    "$BASE/api/facebook/product-feed/123456789/feeds?accessToken=$TOKEN"
T "GET  feed"          GET    "$BASE/api/facebook/product-feed/feeds/feed123?accessToken=$TOKEN"
T "DEL  feed"          DELETE "$BASE/api/facebook/product-feed/feeds/feed123?accessToken=$TOKEN"
TMultipart "POST feed upload" "$BASE/api/facebook/product-feed/feeds/feed123/upload?accessToken=$TOKEN"
T "GET  feed uploads"  GET    "$BASE/api/facebook/product-feed/feeds/feed123/uploads?accessToken=$TOKEN"
T "GET  upload"        GET    "$BASE/api/facebook/product-feed/uploads/upload123?accessToken=$TOKEN"
T "GET  feed errors"   GET    "$BASE/api/facebook/product-feed/feeds/feed123/errors?accessToken=$TOKEN"

# â”€â”€ 28. WHATSAPP â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "WhatsApp"
Write-Host "`n=== 28. WHATSAPP ==="
T "POST text"          POST   "$BASE/api/facebook/whatsapp/messages/text"        '{"to":"919876543210","message":"Hello"}'
T "POST template"      POST   "$BASE/api/facebook/whatsapp/messages/template"    '{"to":"919876543210","templateName":"hello_world","languageCode":"en_US"}'
T "POST image"         POST   "$BASE/api/facebook/whatsapp/messages/image"       '{"to":"919876543210","imageUrl":"https://example.com/img.jpg"}'
T "POST video"         POST   "$BASE/api/facebook/whatsapp/messages/video"       '{"to":"919876543210","videoUrl":"https://example.com/vid.mp4"}'
T "POST audio"         POST   "$BASE/api/facebook/whatsapp/messages/audio"       '{"to":"919876543210","audioUrl":"https://example.com/aud.mp3"}'
T "POST document"      POST   "$BASE/api/facebook/whatsapp/messages/document"    '{"to":"919876543210","documentUrl":"https://example.com/doc.pdf","filename":"doc.pdf"}'
T "POST sticker"       POST   "$BASE/api/facebook/whatsapp/messages/sticker"     '{"to":"919876543210","stickerUrl":"https://example.com/sticker.webp"}'
T "POST location"      POST   "$BASE/api/facebook/whatsapp/messages/location"    '{"to":"919876543210","latitude":28.6,"longitude":77.2,"name":"Delhi","address":"India"}'
T "POST contact"       POST   "$BASE/api/facebook/whatsapp/messages/contact"     '{"to":"919876543210","contacts":[{"name":{"formattedName":"Test"},"phones":[{"phone":"123","type":"CELL"}]}]}'
T "POST reaction"      POST   "$BASE/api/facebook/whatsapp/messages/reaction"    '{"to":"919876543210","messageId":"msg123","emoji":"OK"}'
T "POST buttons"       POST   "$BASE/api/facebook/whatsapp/messages/interactive/buttons" '{"to":"919876543210","bodyText":"Choose:","buttons":[{"id":"b1","title":"Yes"}]}'
T "POST list"          POST   "$BASE/api/facebook/whatsapp/messages/interactive/list"    '{"to":"919876543210","headerText":"Pick","bodyText":"Choose:","buttonText":"Select","sections":[{"title":"Opts","rows":[{"id":"1","title":"Option 1"}]}]}'
T "POST product"       POST   "$BASE/api/facebook/whatsapp/messages/interactive/product" '{"to":"919876543210","catalogId":"cat1","productRetailerId":"prod1"}'
T "POST catalog"       POST   "$BASE/api/facebook/whatsapp/messages/interactive/catalog" '{"to":"919876543210","catalogId":"cat1","bodyText":"Browse","sections":[{"title":"Items","productRetailerIds":["p1"]}]}'
T "POST read"          POST   "$BASE/api/facebook/whatsapp/messages/read"                '{"messageId":"msg123"}'
T "POST loc-request"   POST   "$BASE/api/facebook/whatsapp/messages/interactive/location-request" '{"to":"919876543210","bodyText":"Share location"}'
T "POST cta-url"       POST   "$BASE/api/facebook/whatsapp/messages/interactive/cta-url" '{"to":"919876543210","headerText":"Visit","bodyText":"Click","buttonDisplayText":"Open","buttonUrl":"https://example.com"}'
T "POST flow"          POST   "$BASE/api/facebook/whatsapp/messages/interactive/flow"    '{"to":"919876543210","flowId":"flow1","flowToken":"token","bodyText":"Fill form"}'
T "POST media/upload"  POST   "$BASE/api/facebook/whatsapp/media/upload"                 '{"url":"https://example.com/img.jpg","type":"image/jpeg"}'
T "GET  media"         GET    "$BASE/api/facebook/whatsapp/media/media123?accessToken=TOKEN"
T "DEL  media"         DELETE "$BASE/api/facebook/whatsapp/media/media123?accessToken=TOKEN"
T "GET  phone-numbers" GET    "$BASE/api/facebook/whatsapp/phone-numbers?wabaId=waba123&accessToken=TOKEN"
T "GET  phone-number"  GET    "$BASE/api/facebook/whatsapp/phone-numbers/ph123?accessToken=TOKEN"
T "POST request-code"  POST   "$BASE/api/facebook/whatsapp/phone-numbers/ph123/request-code" '{"codeMethod":"SMS"}'
T "POST verify-code"   POST   "$BASE/api/facebook/whatsapp/phone-numbers/ph123/verify-code"  '{"code":"123456"}'
T "POST register"      POST   "$BASE/api/facebook/whatsapp/phone-numbers/ph123/register"     '{"pin":"123456"}'
T "POST deregister"    POST   "$BASE/api/facebook/whatsapp/phone-numbers/ph123/deregister"   '{}'
T "POST 2fa"           POST   "$BASE/api/facebook/whatsapp/phone-numbers/ph123/2fa"          '{"pin":"123456"}'
T "GET  biz-profile"   GET    "$BASE/api/facebook/whatsapp/business-profile?phoneNumberId=ph123&accessToken=TOKEN"
T "POST biz-profile"   POST   "$BASE/api/facebook/whatsapp/business-profile"                 '{"phoneNumberId":"ph123","about":"Test business"}'
T "POST templates"     POST   "$BASE/api/facebook/whatsapp/templates"                         '{"wabaId":"waba123","name":"test_template","language":"en_US","category":"UTILITY","components":[]}'
T "GET  templates"     GET    "$BASE/api/facebook/whatsapp/templates?wabaId=waba123&accessToken=TOKEN"
T "GET  template"      GET    "$BASE/api/facebook/whatsapp/templates/tmpl123?accessToken=TOKEN"
T "PUT  template"      PUT    "$BASE/api/facebook/whatsapp/templates/tmpl123"                '{"components":[]}'
T "DEL  template"      DELETE "$BASE/api/facebook/whatsapp/templates/tmpl123?accessToken=TOKEN"
T "POST qr-codes"      POST   "$BASE/api/facebook/whatsapp/qr-codes"                         '{"phoneNumberId":"ph123","prefilledMessage":"Hi","imageFormat":"PNG"}'
T "GET  qr-codes"      GET    "$BASE/api/facebook/whatsapp/qr-codes?phoneNumberId=ph123&accessToken=TOKEN"
T "GET  qr-code"       GET    "$BASE/api/facebook/whatsapp/qr-codes/qr123?phoneNumberId=ph123&accessToken=TOKEN"
T "PUT  qr-code"       PUT    "$BASE/api/facebook/whatsapp/qr-codes/qr123"                   '{"prefilledMessage":"Updated"}'
T "DEL  qr-code"       DELETE "$BASE/api/facebook/whatsapp/qr-codes/qr123?phoneNumberId=ph123&accessToken=TOKEN"
T "POST webhook sub"   POST   "$BASE/api/facebook/whatsapp/webhook/subscribe"                 ('{"wabaId":"waba123","callbackUrl":"https://example.com/wh","verifyToken":"secret","fields":["messages"],"accessToken":"'+$TOKEN+'"}')
T "GET  webhook verify" GET   "$BASE/api/facebook/whatsapp/webhook/verify?hub.mode=subscribe&hub.verify_token=secret&hub.challenge=CH123"
T "POST webhook payload" POST "$BASE/api/facebook/whatsapp/webhook/payload"                   '{"object":"whatsapp_business_account","entry":[]}'
T "GET  waba"          GET    "$BASE/api/facebook/whatsapp/waba/waba123?accessToken=TOKEN"
T "POST analytics"     POST   "$BASE/api/facebook/whatsapp/analytics"                         ('{"wabaId":"waba123","start":"1700000000","end":"1700086400","granularity":"DAY","accessToken":"'+$TOKEN+'"}')
T "POST conv-analytics" POST  "$BASE/api/facebook/whatsapp/conversation-analytics"            ('{"wabaId":"waba123","start":"1700000000","end":"1700086400","granularity":"DAY","accessToken":"'+$TOKEN+'"}')

# â”€â”€ 29. WHATSAPP CATALOG â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "WACatalog"
Write-Host "`n=== 29. WHATSAPP CATALOG ==="
T "GET  catalogs"  GET    "$BASE/api/facebook/whatsapp/catalog/waba123/catalogs?accessToken=TOKEN"
T "POST catalog"   POST   "$BASE/api/facebook/whatsapp/catalog/waba123/catalogs?accessToken=TOKEN"  '{"name":"WA Catalog","vertical":"commerce"}'
T "GET  catalog"   GET    "$BASE/api/facebook/whatsapp/catalog/catalogs/cat123?accessToken=TOKEN"
T "GET  products"  GET    "$BASE/api/facebook/whatsapp/catalog/catalogs/cat123/products?accessToken=TOKEN"
T "POST product"   POST   "$BASE/api/facebook/whatsapp/catalog/catalogs/cat123/products?accessToken=TOKEN" '{"name":"Test Product","description":"A product","price":100,"currency":"USD","availability":"in stock","condition":"new"}'
T "GET  product"   GET    "$BASE/api/facebook/whatsapp/catalog/products/prod123?accessToken=TOKEN"
T "DEL  product"   DELETE "$BASE/api/facebook/whatsapp/catalog/products/prod123?accessToken=TOKEN"

# â”€â”€ 30. LEAD GEN â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "LeadGen"
Write-Host "`n=== 30. LEAD GEN ==="
T "POST list forms"      POST   "$BASE/api/facebook/leadgen/forms/list?accessToken=$TOKEN"  '{"pageId":"123456789","limit":10}'
T "GET  form"            GET    "$BASE/api/facebook/leadgen/forms/form123?accessToken=$TOKEN"
T "POST create form"     POST   "$BASE/api/facebook/leadgen/forms?accessToken=$TOKEN"       '{"pageId":"123456789","name":"Test Form","questions":[{"type":"EMAIL"},{"type":"FULL_NAME"}],"privacyPolicy":{"url":"https://example.com/privacy","linkType":"PRIVACY_POLICY"}}'
T "DEL  delete form"     DELETE "$BASE/api/facebook/leadgen/forms/form123?accessToken=$TOKEN"
T "POST list leads"      POST   "$BASE/api/facebook/leadgen/leads/list?accessToken=$TOKEN"  '{"formId":"form123","limit":10}'
T "GET  lead"            GET    "$BASE/api/facebook/leadgen/leads/lead123?accessToken=$TOKEN"
T "DEL  delete lead"     DELETE "$BASE/api/facebook/leadgen/leads/lead123?accessToken=$TOKEN"

# â”€â”€ 31. WHATSAPP FLOWS â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
$module = "WAFlows"
Write-Host "`n=== 31. WHATSAPP FLOWS ==="
T "POST list flows"      POST   "$BASE/api/facebook/whatsapp/flows/list?accessToken=$TOKEN" '{"wabaId":"waba123","limit":10}'
T "GET  flow"            GET    "$BASE/api/facebook/whatsapp/flows/flow123?accessToken=$TOKEN"
T "POST create flow"     POST   "$BASE/api/facebook/whatsapp/flows?accessToken=$TOKEN"      '{"wabaId":"waba123","name":"Test Flow","categories":["CUSTOMER_SERVICE"]}'
T "PUT  update flow"     PUT    "$BASE/api/facebook/whatsapp/flows/flow123?accessToken=$TOKEN" '{"flowId":"flow123","name":"Updated Flow"}'
T "DEL  delete flow"     DELETE "$BASE/api/facebook/whatsapp/flows/flow123?accessToken=$TOKEN"
T "POST publish flow"    POST   "$BASE/api/facebook/whatsapp/flows/flow123/publish?accessToken=$TOKEN"
T "POST deprecate flow"  POST   "$BASE/api/facebook/whatsapp/flows/flow123/deprecate?accessToken=$TOKEN"
T "GET  flow assets"     GET    "$BASE/api/facebook/whatsapp/flows/flow123/assets?accessToken=$TOKEN"
T "POST upload json"     POST   "$BASE/api/facebook/whatsapp/flows/flow123/assets?accessToken=$TOKEN" '{"flowId":"flow123","flowJson":"{\"version\":\"5.0\",\"screens\":[]}"}'
T "GET  flow preview"    GET    "$BASE/api/facebook/whatsapp/flows/flow123/preview?accessToken=$TOKEN"

# â”€â”€ SUMMARY â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€â”€
Write-Host "`n============================================================"
Write-Host "SUMMARY  (31 modules Â· 348 endpoints)"
Write-Host "============================================================"
$grouped = $results | Group-Object Module | Sort-Object Name
foreach ($g in $grouped) {
    $p = ($g.Group | Where-Object { $_.OK }).Count
    $t = $g.Count
    $icon = if ($p -eq $t) { "ALL OK " } elseif ($p -eq 0) { "ALL FAIL" } else { "PARTIAL " }
    Write-Host ("{0,-15} {1,2}/{2,-2}  {3}" -f $g.Name, $p, $t, $icon)
}
Write-Host "------------------------------------------------------------"
Write-Host ("TOTAL: {0} passed  {1} failed  {2} total" -f $pass, $fail, ($pass+$fail))
Write-Host "============================================================"

if ($fail -gt 0) {
    Write-Host "`nFailed endpoints:"
    $results | Where-Object { -not $_.OK } | ForEach-Object {
        Write-Host ("  [$($_.Code)] $($_.Module) - $($_.Label)")
    }
}

