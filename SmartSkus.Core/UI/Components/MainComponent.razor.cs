using Blazorise;
using Blazorise.Localization;
using SmartSkus.Core.App;
using SmartSkus.Core.Backup;
using SmartSkus.Core.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSkus.Core.Local.Models;
using SmartSkus.Shared.Dtos;
using SmartSkus.Shared.Enums;
using SmartSkus.Core.Local.Interface;

namespace SmartSkus.Core.UI.Components;

public partial class MainComponent
{
    #region Variables and Properties

    #region Parameter

    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    #endregion

    #region Inject

    [Inject]
    IInventoryService InventoryService { get; set; }

    [Inject]
    IMasterService MasterService { get; set; }

    [Inject]
    public ISettingsService SettingsService { get; set; }

    [Inject]
    ITextLocalizer<Translations> Localizer { get; set; } = null!;

    [Inject]
    IRepository Repository { get; set; } = null!;

    [Inject]
    protected IPreRenderService PreRenderService { get; set; } = null!;

    [Inject]
    ITextLocalizerService LocalizationService { get; set; } = null!;

    [Inject]
    IMyService MyService { get; set; } = null!;

    [Inject]
    IImportExport ImportExport { get; set; } = null!;

    #endregion

    #region Properties

    SettingsModel Settings => Repository.Settings;

    string Theme => Repository.Settings.Theme;

    Screen Screen => Repository.Settings.Screen;

    Blazorise.Size Size => Repository.Settings.Size;

    string? SidebarVisibilityCss => _sidebarVisible ? null : "sidebar-visible";

    bool UnsavedChanges => ImportExport.DataExportByFormat[Settings.SelectedBackupFormat].UnsavedChanges;

    Background _background
    {
        get
        {
            if (_backgrounds.ContainsKey(Repository.Settings.Background))
                return _backgrounds[Repository.Settings.Background];
            else
                return Background.Default;
        }
    }

    public static bool IsApple => OperatingSystem.IsIOS() || OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst();

    public static bool IsPersonalComputer => OperatingSystem.IsBrowser() || OperatingSystem.IsWindows() || OperatingSystem.IsLinux() || OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst();

    public static bool IsDebug
    {
        get
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }

    #endregion

    int _workaround;

    int _debugTheme = 0;

    int _debugBackground = 0;

    bool _showDebugControls;

    //bool _filtersVisible = true;

    bool _sidebarVisible = false;

    bool _bulkSkuVisible = false;

    bool _inventoryVisible = true;

    //bool _categoriesVisible = true;

    bool _bulkSkuNewVisible = false;

    bool _availableItemsVisible = true;

    //BulkAddDto _bulkAddDto = new();

    AppModel _appModelObject = new();

    readonly SortedList<string, Background> _backgrounds = new()
    {
        { "Default", Background.Default },
        { "Primary", Background.Primary },
        { "Secondary", Background.Secondary },
        { "Success", Background.Success },
        { "Danger", Background.Danger },
        { "Warning", Background.Warning },
        { "Info", Background.Info },
        { "Light", Background.Light },
        { "Dark", Background.Dark },
        { "White", Background.White },
        { "Transparent", Background.Transparent },
        { "Body", Background.Body },
    };

    readonly SortedList<string, string> _bootswatchThemes = new()
    {
        { "cerulean", "sha256-5FeWJNHtC2PdIw9W39dEuOyQN1BoL7BuHR3JdbpIzbw=" },
        { "cosmo", "sha256-iMtue7kH6spfqTbfU9XGcwtBF8bgiOnVTErTuh7LJr8=" },
        { "cyborg", "sha256-fO58jx4RDvdVgLJ4VWCNdWLLQF5cXb34EtdoGxlcJ68=" },
        { "darkly", "sha256-VZi/r/RC1MritcGE2Yyxb/ACi8WIOj1Y7BHuslF8+6I=" },
        { "flatly", "sha256-3LXKhyYmYxt+fGciLxN474K5Ycw5FXqQJDJpW54Q3XQ=" },
        { "journal", "sha256-Ont0XKIPs4G8F5vswFwCkN5t7KOSd8L8ifYhadEfEvk=" },
        { "litera", "sha256-oNAzP5red1ldRKkdYgkxkykk2qXm3hcosesq9UGvN4o=" },
        { "lumen", "sha256-27KHRZR9ihYCkP9BSm5cLLqJ4LsSZdLEYgKh5j2lrCs=" },
        { "lux", "sha256-WW13HpaaG94O2RHAP6ZIIEcijhqdeYjh3FkqE7zgMbY=" },
        { "materia", "sha256-I1/fNjCD26D0FEPPH2ox/AMQo4owDy1DsUkCJ5e/Ud4=" },
        { "minty", "sha256-X08VWhrLbfhaM0zE3n7Q7Mg9YVevZcIBFzpvSCWAWmo=" },
        { "morph", "sha256-1Wlk5rRLkqkcplEElHjnc+x3zrJ4qZRjzDxzAtI8H48=" }, /**/
        { "pulse", "sha256-d3j6nPvgdSos3dIAmJSHebf76C5yAALgWwcHIom40Mo=" },
        { "quartz", "sha256-GpjV2saTPcbYTZy+LZLbu2JpmSQfGJW7XE5V5EDdA/g=" }, /**/
        { "sandstone", "sha256-zWAnZkKmT2MYxdCMp506rQtnA9oE2w0/K/WVU7V08zw=" },
        { "simplex", "sha256-bFdwuvWKVAaFL6MZ6IlwACEx5uGox0TibRPTZstTN9o=" },
        { "sketchy", "sha256-H4KK1tCvREdvbtMG+OoveMdEkIsulg1bO5bJJpEBRyc=" },
        { "slate", "sha256-tuuKR9RAif6+FEcxArLfMAVcEfuamZw2J/dR9F5svcw=" },
        { "solar", "sha256-L3GhaXImktQTiaUA8LmRQ5W9/qn7eU2b+k7gSWn8U/A=" },
        { "spacelab", "sha256-gvYVyQ50XH5efNIn43UNuSOp7LhOePci95PJAAIfpek=" },
        { "superhero", "sha256-gox/GuMWCKC24lM1gRLnKpm/pgjHDI3u5bnjSxvC/QI=" },
        { "united", "sha256-rU1IToLlw3oMuNHSO29CP/HxYCcBqq9Vc73wVnN5prQ=" },
        { "vapor", "sha256-3s3k/1ZjojJ7E7SfJk0q/A4fe/weYY9gzvbE6C6JTI0=" }, /**/
        { "yeti", "sha256-g2YbprSKIubjkv6Pkd3RSeH9/6MW2MZcFrX/3eIu6vU=" },
        { "zephyr", "sha256-DWNfAPc9h5qI0aI4+9aPONXiE9jIz2nczDB3KvjiKlI=" } /**/
    }; 

    #endregion

    protected override async Task OnInitializedAsync()
    {
        if (!PreRenderService.IsPreRendering)
            await Repository.Initialize(InventoryService,MasterService, SettingsService);

        Repository.DataChanged += (object? sender, EventArgs e) => StateHasChanged();

        _appModelObject = Repository.AppModelObject;

        _appModelObject.SelectedItemDtoList
            = new List<ItemDto>(_appModelObject.ItemDtoList);

        _appModelObject.SelectedItemVariationDtoList 
            = new List<ItemVariationDto>(_appModelObject.ItemVariationDtoList);

        _appModelObject.SelectedSkuModelDtoList
            = new List<SkuModelDto>(_appModelObject.SkuModelDtoList);

        LocalizationService.ChangeLanguage(Repository.Settings.Culture);

        StateHasChanged(); // refresh components with _repository.Settings

        //parent component
        MyService.RefreshRequested += RefreshMe;
    }

    void ToggleSidebar() => _sidebarVisible = !_sidebarVisible;

    void OnShowDebugChanged(bool? val) => _showDebugControls = val ?? false;

    async Task DebugBackground()
    {
        if (++_debugBackground == _backgrounds.Count)
            _debugBackground = 0;

        Repository.Settings.Background = _backgrounds.Keys[_debugBackground];
        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task DebugTheme()
    {
        ++_debugTheme;

        if (_debugTheme == _bootswatchThemes.Count)
        {
            await OnThemeChanged("default");
            return;
        }

        if (_debugTheme > _bootswatchThemes.Count)
        {
            _debugTheme = 0;
        }

        await OnThemeChanged(_bootswatchThemes.Keys[_debugTheme]);
    }

    private void RefreshMe()
    {
        StateHasChanged();
    }

    //https://stackoverflow.com/questions/55775060/blazor-component-refresh-parent-when-model-is-updated-from-child-component
    public void RefreshState()
    {
        this.StateHasChanged();
    }

    async Task Backup()
    {
        await ImportExport.DataExportByFormat[Settings.SelectedBackupFormat].ExportData();
    }

    async Task OnSizeChanged(Blazorise.Size size)
    {
        Repository.Settings.Size = size;
        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task OnThemeChanged(string theme)
    {
        _workaround = 1 - _workaround;

        Repository.Settings.Theme = theme;
        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task OnBackgroundChanged(string background)
    {
        Repository.Settings.Background = background;
        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task ToggleQrCodeTester()
    {
        if (Screen == Screen.QrCodeTester)
            Repository.Settings.Screen = Screen.Main;
        else
            Repository.Settings.Screen = Screen.QrCodeTester;

        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task ToggleOptions()
    {
        if (Screen == Screen.Options)
            Repository.Settings.Screen = Screen.Main;
        else
            Repository.Settings.Screen = Screen.Options;

        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task ToggleHelp()
    {
        if (Screen == Screen.Help)
            Repository.Settings.Screen = Screen.Main;
        else
            Repository.Settings.Screen = Screen.Help;

        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task ToggleInventory()
    {
        if (Screen == Screen.Inventory)
            Repository.Settings.Screen = Screen.Main;
        else
            Repository.Settings.Screen = Screen.Inventory;

        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task ToggleSkuCategory()
    {
        if (Screen == Screen.SkuCategory)
            Repository.Settings.Screen = Screen.Main;
        else
            Repository.Settings.Screen = Screen.SkuCategory;

        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task ToggleSkuOptions()
    {
        if (Screen == Screen.SkuOptions)
            Repository.Settings.Screen = Screen.Main;
        else
            Repository.Settings.Screen = Screen.SkuOptions;

        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task ToggleAdminSettings()
    {
        if (Screen == Screen.AdminSettings)
            Repository.Settings.Screen = Screen.Main;
        else
            Repository.Settings.Screen = Screen.AdminSettings;

        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task ToggleAbout()
    {
        if (Screen == Screen.About)
            Repository.Settings.Screen = Screen.Main;
        else
            Repository.Settings.Screen = Screen.About;

        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task ShowMainScreen()
    {
        Repository.Settings.Screen = Screen.Main;
        await Repository.UpdateSettings(Repository.Settings.Id);
    }

    async Task OnDataFormatChangeEvent(ChangeEventArgs e)
    {
        if (e.Value is string value)
            await OnDataFormatChanged(Enum.Parse<DataFormat>(value));
    }

    async Task OnDataFormatChanged(DataFormat dataFormat)
    {
        if (Settings.SelectedBackupFormat != dataFormat)
        {
            Settings.SelectedBackupFormat = dataFormat;

            await Repository.UpdateSettings(Settings.Id);
        }
    }
}