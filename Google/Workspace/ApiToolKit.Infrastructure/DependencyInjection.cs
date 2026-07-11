using ApiToolKit.Application.Interfaces.Services.Workspace;
using ApiToolKit.Application.Services.Workspace;
using ApiToolKit.Application.Services.Workspace.Calendar;
using ApiToolKit.Application.Services.Workspace.Chat;
using ApiToolKit.Application.Services.Workspace.Docs;
using ApiToolKit.Application.Services.Workspace.Drive;
using ApiToolKit.Application.Services.Workspace.Forms;
using ApiToolKit.Application.Services.Workspace.Gmail;
using ApiToolKit.Application.Services.Workspace.Meet;
using ApiToolKit.Application.Services.Workspace.Sheets;
using ApiToolKit.Application.Services.Workspace.Slides;
using ApiToolKit.Domain.Interfaces.Repositories.Workspace;
using ApiToolKit.Infrastructure.Data;
using ApiToolKit.Infrastructure.External.Workspace;
using ApiToolKit.Infrastructure.Repositories.Workspace;
using Microsoft.Extensions.DependencyInjection;

namespace ApiToolKit.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();

        // Repositories
        services.AddScoped<IWorkspaceAuthRepository, WorkspaceAuthRepository>();
        services.AddScoped<IGmailRepository, GmailRepository>();
        services.AddScoped<IDriveRepository, DriveRepository>();
        services.AddScoped<ICalendarRepository, CalendarRepository>();
        services.AddScoped<ISpreadsheetRepository, SpreadsheetRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IPresentationRepository, PresentationRepository>();
        services.AddScoped<IMeetRepository, MeetRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IFormRepository, FormRepository>();

        // HTTP Client
        services.AddHttpClient<WorkspaceApiClient>();
        services.AddScoped<IWorkspaceApiClient>(sp => sp.GetRequiredService<WorkspaceApiClient>());

        // Auth Service
        services.AddScoped<IWorkspaceAuthService, WorkspaceAuthService>();

        // Gmail Services
        services.AddScoped<IGmailMessageService, GmailMessageService>();
        services.AddScoped<IGmailLabelService, GmailLabelService>();
        services.AddScoped<IGmailDraftService, GmailDraftService>();
        services.AddScoped<IGmailThreadService, GmailThreadService>();
        services.AddScoped<IGmailSettingsService, GmailSettingsService>();

        // Drive Services
        services.AddScoped<IDriveFileService, DriveFileService>();
        services.AddScoped<IDrivePermissionService, DrivePermissionService>();
        services.AddScoped<IDriveRevisionService, DriveRevisionService>();
        services.AddScoped<IDriveSharedDriveService, DriveSharedDriveService>();

        // Calendar Services
        services.AddScoped<ICalendarService, CalendarService>();
        services.AddScoped<ICalendarEventService, CalendarEventService>();
        services.AddScoped<ICalendarAclService, CalendarAclService>();
        services.AddScoped<ICalendarFreeBusyService, CalendarFreeBusyService>();

        // Sheets Service
        services.AddScoped<ISheetsService, SheetsService>();

        // Docs Service
        services.AddScoped<IDocsService, DocsService>();

        // Slides Service
        services.AddScoped<ISlidesService, SlidesService>();

        // Meet Services
        services.AddScoped<IMeetSpaceService, MeetSpaceService>();
        services.AddScoped<IMeetRecordingService, MeetRecordingService>();
        services.AddScoped<IMeetParticipantService, MeetParticipantService>();

        // Chat Services
        services.AddScoped<IChatSpaceService, ChatSpaceService>();
        services.AddScoped<IChatMessageService, ChatMessageService>();
        services.AddScoped<IChatMemberService, ChatMemberService>();

        // Forms Services
        services.AddScoped<IFormsService, FormsService>();
        services.AddScoped<IFormResponseService, FormResponseService>();

        return services;
    }
}
