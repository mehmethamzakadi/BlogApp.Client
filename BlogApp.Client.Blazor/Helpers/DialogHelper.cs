using Radzen;

namespace BlogApp.Client.Blazor.Helpers;

public static class DialogHelper
{
    public record DialogConfig(
        string Title, 
        string Width = "500px", 
        string Height = "auto",
        bool CloseOnEsc = true,
        bool ShowClose = true,
        bool CloseOnOverlayClick = false
    );

    public static DialogOptions CreateOptions(DialogConfig config)
    {
        return new DialogOptions
        {
            Width = config.Width,
            Height = config.Height,
            CloseDialogOnEsc = config.CloseOnEsc,
            CloseDialogOnOverlayClick = config.CloseOnOverlayClick,
            ShowClose = config.ShowClose
        };
    }

    public static ConfirmOptions CreateConfirmOptions(string okButtonText = "Evet", string cancelButtonText = "Hayır")
    {
        return new ConfirmOptions
        {
            OkButtonText = okButtonText,
            CancelButtonText = cancelButtonText
        };
    }
}