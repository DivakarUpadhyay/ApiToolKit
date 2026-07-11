using ApiToolKit.Domain.Entities.Facebook;
using ApiToolKit.Domain.Interfaces.Repositories.Facebook;
using ApiToolKit.Infrastructure.Data;
using Dapper;

namespace ApiToolKit.Infrastructure.Repositories.Facebook;

public class FacebookEventRepository : IFacebookEventRepository
{
    private readonly DapperContext _context;

    public FacebookEventRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> SaveEventAsync(FacebookEvent ev)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Event_Save",
            new
            {
                ev.EventId,
                ev.PageId,
                EventName = ev.Name,
                ev.Description,
                ev.StartTime,
                ev.EndTime,
                ev.Place,
                ev.AttendingCount,
                ev.InterestedCount,
                ev.DeclinedCount,
                ev.IsOnline,
                ev.Privacy
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<FacebookEvent?> GetEventByIdAsync(string eventId)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<FacebookEvent>(
            "sp_Facebook_Event_GetById",
            new { EventId = eventId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<FacebookEvent>> GetEventsByPageIdAsync(string pageId, int limit, int offset)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<FacebookEvent>(
            "sp_Facebook_Event_GetByPageId",
            new { PageId = pageId, Limit = limit, Offset = offset },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> UpdateEventAsync(FacebookEvent ev)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Event_Update",
            new
            {
                ev.EventId,
                EventName = ev.Name,
                ev.Description,
                ev.StartTime,
                ev.EndTime,
                ev.Place,
                ev.IsOnline,
                ev.Privacy
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> DeleteEventAsync(string eventId)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Event_Delete",
            new { EventId = eventId },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<int> SaveApiLogAsync(FacebookApiLog log)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "sp_Facebook_Event_SaveApiLog",
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
