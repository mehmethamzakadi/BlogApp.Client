namespace BlogApp.Client.Blazor.States;

public abstract class BaseState : IDisposable
{
    private bool _disposed;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public bool IsLoading { get; protected set; }
    public bool IsSubmitting { get; protected set; }
    
    public event Action OnStateChanged;

    protected virtual void NotifyStateChanged() => OnStateChanged?.Invoke();

    protected async Task ExecuteWithLoadingAsync(Func<Task> action)
    {
        if (IsLoading) return;

        await _semaphore.WaitAsync();
        try
        {
            IsLoading = true;
            NotifyStateChanged();

            await action();
        }
        finally
        {
            IsLoading = false;
            NotifyStateChanged();
            _semaphore.Release();
        }
    }

    protected async Task ExecuteWithSubmittingAsync(Func<Task> action)
    {
        if (IsSubmitting) return;

        await _semaphore.WaitAsync();
        try
        {
            IsSubmitting = true;
            NotifyStateChanged();

            await action();
        }
        finally
        {
            IsSubmitting = false;
            NotifyStateChanged();
            _semaphore.Release();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                OnStateChanged = null;
                _semaphore.Dispose();
            }
            _disposed = true;
        }
    }
}