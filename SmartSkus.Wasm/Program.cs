
#region using

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SmartSkus.Core.App;
using SmartSkus.Core.UI.Utils;
using SmartSkus.Wasm.UI;
using System;
using System.Net.Http;

#endregion

AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
{
	try
	{
		string? message = error.ExceptionObject.ToString();

		System.Diagnostics.Debug.WriteLine(message);

		string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HitNtry", "Error.log");
		System.IO.File.WriteAllText(path, message);
	}
	catch
	{

	}
};

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Main>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:53711/") });

builder.Services.AddHttpClient("WebAPI", client =>
    client.BaseAddress = new Uri("https://localhost:7192/"));

builder.Services.AddScoped<AuthenticationDataMemoryStorage>();
builder.Services.AddScoped<HitNtryAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<HitNtryAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();

builder.Services.AddServices();

WebAssemblyHost host = builder.Build();

host.Services.UseServices();

await host.RunAsync();
