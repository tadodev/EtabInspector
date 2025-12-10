using EtabInspector.UI.Contracts.Views;
using Microsoft.Extensions.Hosting;

namespace EtabInspector.UI.Services;

public class ApplicationHostService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private IShellWindow? _shellWindow;

    public ApplicationHostService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await InitializeAsync();
        await StartupAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    private async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    private async Task StartupAsync()
    {
        _shellWindow = _serviceProvider.GetService(typeof(IShellWindow)) as IShellWindow;
        _shellWindow?.ShowWindow();
        await Task.CompletedTask;
    }
}
