using Blazorise.Localization;
using SmartSkus.Core.Backup;
using SmartSkus.Core.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SmartSkus.Core.Local.Models;
using SmartSkus.Shared.Enums;
using SmartSkus.Core.Local.Interface;

namespace SmartSkus.Core.UI.Components;

public partial class OptionsComponent
{
    #region Variables

    #region Inject

    [Inject]
    public IInventoryService InventoryService { get; set; }

    [Inject]
    IMasterService MasterService { get; set; }

    [Inject]
    public ISettingsService SettingsService { get; set; }

    [Inject]
    IImportExport ImportExport { get; set; } = null!;

    [Inject]
    IRepository Repository { get; set; } = null!;

    [Inject]
    ITextLocalizer<Translations> Localizer { get; set; } = null!;

    #endregion

    #region Parameter

    [Parameter]
    public AppModel SelectedCategory { get; set; } = null!;

    [Parameter]
    public EventCallback<AppModel> SelectedCategoryChanged { get; set; }

    [Parameter]
    public EventCallback LanguageChanged { get; set; }

    [Parameter]
    public Blazorise.Size Size { get; set; }

    [Parameter]
    public EventCallback<Blazorise.Size> SizeChanged { get; set; }

    [Parameter]
    public IList<string> Themes { get; set; } = null!;

    [Parameter]
    public string Theme { get; set; } = null!;

    [Parameter]
    public EventCallback<string> ThemeChanged { get; set; }

    [Parameter]
    public IList<string> Backgrounds { get; set; } = null!;

    [Parameter]
    public string Background { get; set; } = null!;

    [Parameter]
    public EventCallback<string> BackgroundChanged { get; set; }

    #endregion

    public static bool IsApple => OperatingSystem.IsIOS() || OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst();

    public static bool IsPersonalComputer => OperatingSystem.IsBrowser() || OperatingSystem.IsWindows() || OperatingSystem.IsLinux() || OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst(); 

    #endregion

    async Task LoadExamples()
    {
        await Repository.LoadExamples();

        await CloseOptions();
    }

    async Task DeleteAll()
    {
        await Repository.DeleteAll(InventoryService, MasterService, SettingsService);

        await OnSelectedCategoryChanged();

        await CloseOptions();
    }

    async Task ResetSettings()
    {
        await Repository.ResetSettings();

        await CloseOptions();
    }

    private async Task CloseOptions()
    {
        Repository.Settings.Screen = Screen.Main;
        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    private async Task OnSelectedCategoryChanged()
    {
        SelectedCategory = Repository.AppModelObject;

        await SelectedCategoryChanged.InvokeAsync(SelectedCategory);
    }

    async Task OnSizeChangeEvent(ChangeEventArgs e)
    {
        if (e.Value is string value)
            await OnSizeChanged(Enum.Parse<Blazorise.Size>(value));
    }

    async Task OnSizeChanged(Blazorise.Size size)
    {
        Size = size;
        await SizeChanged.InvokeAsync(Size);
    }

    async Task OnThemeChangeEvent(ChangeEventArgs e)
    {
        if (e.Value is string value)
            await OnThemeChanged(value);
    }

    async Task OnThemeChanged(string theme)
    {
        Theme = theme;
        await ThemeChanged.InvokeAsync(Theme);
    }

    async Task OnBackgroundChangeEvent(ChangeEventArgs e)
    {
        if (e.Value is string value)
            await OnBackgroundChanged(value);
    }

    async Task OnBackgroundChanged(string background)
    {
        Background = background;
        await BackgroundChanged.InvokeAsync(Background);
    }

    async Task Import(InputFileChangeEventArgs e)
    {
        if (ImportExport.FileImportByExtension.Where(pair => e.File.Name.EndsWith(pair.Key, StringComparison.OrdinalIgnoreCase)).Select(pair => pair.Value).FirstOrDefault() is IFileImport fileImport)
        {
            Stream stream = e.File.OpenReadStream(maxAllowedSize: 5242880);
            await fileImport.ImportData(stream);
            stream.Close();
        }

        await OnSelectedCategoryChanged();
    }

    async Task ExportData(DataFormat dataFormat)
    {
        await ImportExport.DataExportByFormat[dataFormat].ExportData();
    }
}
