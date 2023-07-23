
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using SmartSkus.Core.Backup;
using SmartSkus.Core.Backup.Drive;
using SmartSkus.Core.Local;
using SmartSkus.Core.Local.Interface;
using SmartSkus.Core.Services;
using SmartSkus.Core.Services.Contracts;
using System;

namespace SmartSkus.Core.App;

public static class Services
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddBlazorise(options =>
            {
                options.Immediate = true; // ChangeTextOnKeyPress
            })
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();

        serviceCollection.AddSingleton<IErrorBoundaryLogger, ErrorBoundaryLogger>();

        serviceCollection.AddScoped<IPreRenderService, PreRenderService>();

        serviceCollection.AddScoped<JsInterop>();

        serviceCollection.AddScoped<JsonBackup>();
        serviceCollection.AddScoped<IDataExport>(x => x.GetRequiredService<JsonBackup>());
        serviceCollection.AddScoped<IFileImport>(x => x.GetRequiredService<JsonBackup>());

        serviceCollection.AddScoped<YamlBackup>();
        serviceCollection.AddScoped<IDataExport>(x => x.GetRequiredService<YamlBackup>());
        serviceCollection.AddScoped<IFileImport>(x => x.GetRequiredService<YamlBackup>());

        serviceCollection.AddScoped<IImportExport, ImportExport>();

        serviceCollection.AddScoped<IRepository, Repository>();
        //serviceCollection.AddScoped<IDatabaseAccess, DatabaseAccess>();

        serviceCollection.AddScoped<IMasterService, MasterService>();
        serviceCollection.AddScoped<IInventoryService, InventoryService>();

        serviceCollection.AddScoped<ISettingsService, SettingsService>();

        serviceCollection.AddScoped<IMyService, MyService>();

        return serviceCollection;
    }

    public static IServiceProvider UseServices(this IServiceProvider serviceProvider)
    {
        //IRepository repository = serviceProvider.GetRequiredService<IRepository>();

        // Works only in Wasm - doesn't work in WinForms or Wpf:

        //repository.Initialize();

        return serviceProvider;
    }
}
