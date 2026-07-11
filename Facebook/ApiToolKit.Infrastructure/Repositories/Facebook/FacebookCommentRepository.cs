using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookCommentRepository : IFacebookCommentRepository
{
    private readonly DapperContext _context;

    public FacebookCommentRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveCommentAsync(FacebookComment comment)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Comment_Save",
            new
            {
                comment.CommentId,
                comment.ObjectId,
                comment.UserId,
                comment.FromId,
                comment.FromName,
                comment.Message,
                comment.CanRemove
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookComment?> GetCommentByIdAsync(string commentId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookComment>(
            "sp_Facebook_Comment_GetById",
            new { CommentId = commentId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookComment>> GetCommentsByObjectIdAsync(string objectId, int limit, int offset)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookComment>(
            "sp_Facebook_Comment_GetByObjectId",
            new { ObjectId = objectId, Limit = limit, Offset = offset },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteCommentAsync(string commentId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Comment_Delete",
            new { CommentId = commentId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Comment_SaveApiLog",
            new
            {
                log.UserId,
                log.PageId,
                log.ApiName,
                log.RequestBody,
                log.ResponseBody,
                log.IsSuccess,
                log.StatusCode
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
