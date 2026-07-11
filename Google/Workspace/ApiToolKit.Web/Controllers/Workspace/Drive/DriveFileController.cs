using ApiToolKit.Application.DTOs.Workspace.Drive;
using ApiToolKit.Application.Interfaces.Services.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ApiToolKit.Web.Controllers.Workspace.Drive;

/// <summary>Google Drive Files API</summary>
[ApiController]
[Route("api/workspace/drive/files")]
public class DriveFileController : ControllerBase
{
    private readonly IDriveFileService _service;

    public DriveFileController(IDriveFileService service)
    {
        _service = service;
    }

    /// <summary>List files in Drive</summary>
    [HttpGet("{userId}")]
    public async Task<IActionResult> ListFiles(string userId, [FromQuery] ListFilesRequest request)
    {
        var result = await _service.ListFilesAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Get a specific file</summary>
    [HttpGet("{userId}/{fileId}")]
    public async Task<IActionResult> GetFile(string userId, string fileId, [FromQuery] string? fields)
    {
        var result = await _service.GetFileAsync(userId, new GetFileRequest { FileId = fileId, Fields = fields });
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create a folder</summary>
    [HttpPost("{userId}/folder")]
    public async Task<IActionResult> CreateFolder(string userId, [FromBody] CreateFolderRequest request)
    {
        var result = await _service.CreateFolderAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Create file metadata</summary>
    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateFile(string userId, [FromBody] CreateFileMetadataRequest request)
    {
        var result = await _service.CreateFileMetadataAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Update a file</summary>
    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateFile(string userId, [FromBody] UpdateFileRequest request)
    {
        var result = await _service.UpdateFileAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Copy a file</summary>
    [HttpPost("{userId}/copy")]
    public async Task<IActionResult> CopyFile(string userId, [FromBody] CopyFileRequest request)
    {
        var result = await _service.CopyFileAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Move a file to a new parent</summary>
    [HttpPost("{userId}/move")]
    public async Task<IActionResult> MoveFile(string userId, [FromBody] MoveFileRequest request)
    {
        var result = await _service.MoveFileAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Trash a file</summary>
    [HttpPost("{userId}/trash")]
    public async Task<IActionResult> TrashFile(string userId, [FromBody] TrashFileRequest request)
    {
        var result = await _service.TrashFileAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Untrash a file</summary>
    [HttpPost("{userId}/untrash/{fileId}")]
    public async Task<IActionResult> UntrashFile(string userId, string fileId)
    {
        var result = await _service.UntrashFileAsync(userId, fileId);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Share a file</summary>
    [HttpPost("{userId}/share")]
    public async Task<IActionResult> ShareFile(string userId, [FromBody] ShareFileRequest request)
    {
        var result = await _service.ShareFileAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>Delete a file permanently</summary>
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteFile(string userId, [FromBody] DeleteFileRequest request)
    {
        var result = await _service.DeleteFileAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
