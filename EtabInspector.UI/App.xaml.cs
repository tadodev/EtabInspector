using EtabInspector.UI.Contracts.Services;
using EtabInspector.UI.Contracts.Views;
using EtabInspector.UI.Services;
using EtabInspector.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace EtabInspector.UI;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IHost? _host;

    public App()
    {
    }

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        _host = Host.CreateDefaultBuilder(e.Args)
            .ConfigureServices(ConfigureServices)
            .Build();

        await _host.StartAsync();
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        // Hosted Service
        services.AddHostedService<ApplicationHostService>();

        // Services
        services.AddSingleton<IDocumentManagerService, DocumentManagerService>();

        // Views
        services.AddTransient<IShellWindow, ShellWindow>();

        // ViewModels
        services.AddTransient<ShellViewModel>();
        services.AddTransient<ModelDocumentViewModel>();
        services.AddTransient<DrawingDocumentViewModel>();
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        if (_host != null)
        {
            await _host.StopAsync();
            _host.Dispose();
            _host = null;
        }
    }
}


