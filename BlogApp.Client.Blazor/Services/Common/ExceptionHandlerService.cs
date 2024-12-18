using BlogApp.Client.Blazor.SharedKernel.Exceptions;
using Radzen;

namespace BlogApp.Client.Blazor.Services.Common;

public class ExceptionHandlerService : IExceptionHandlerService
{
    public async Task HandleExceptionAsync(Exception exception, NotificationService notificationService)
    {
        string title = "Hata";
        string message = "İşlem sırasında beklenmedik bir hata oluştu.";
        NotificationSeverity severity = NotificationSeverity.Error;

        switch (exception)
        {
            case ValidationException validationException:
                title = "Doğrulama Hatası";
                message = validationException.Message;
                severity = NotificationSeverity.Warning;
                break;

            case NotFoundException notFoundException:
                title = "Kayıt Bulunamadı";
                message = notFoundException.Message;
                severity = NotificationSeverity.Info;
                break;

            case UnauthorizedException unauthorizedException:
                title = "Yetkisiz İşlem";
                message = unauthorizedException.Message;
                severity = NotificationSeverity.Error;
                break;

            case BadRequestException badRequestException:
                title = "Geçersiz İstek";
                message = badRequestException.Message;
                severity = NotificationSeverity.Warning;
                break;
        }

        notificationService.Notify(severity, title, message);
    }
}