using Radzen;

namespace BlogApp.Client.Blazor.Services.Common;

public interface IExceptionHandlerService
{
    Task HandleExceptionAsync(Exception exception, NotificationService notificationService);
}