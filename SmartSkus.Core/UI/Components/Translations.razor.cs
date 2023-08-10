using Blazorise.Localization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using SmartSkus.Core.Local.Interface;

namespace SmartSkus.Core.UI.Components;

public partial class Translations
{
    #region Variables

    #region Parameter

    [Parameter]
    public EventCallback LanguageChanged { get; set; }

    #endregion

    #region CascadingParameter

    [CascadingParameter]
    Blazorise.Size Size { get; set; }

    #endregion

    #region Inject

    [Inject]
    IRepository Repository { get; set; } = null!;

    [Inject]
    ITextLocalizerService LocalizationService { get; set; } = null!;

    #endregion

    public static bool IsApple => OperatingSystem.IsIOS() || OperatingSystem.IsMacOS() || OperatingSystem.IsMacCatalyst();

    readonly Dictionary<string, CultureInfo> _cultures = new()
    {
        { "English", new CultureInfo("en") },
        { "Hindi", new CultureInfo("hi") }
    }; 

    #endregion

    async Task OnCultureChangeEvent(ChangeEventArgs e)
    {
        if (e.Value is string value)
            await OnCultureChanged(value);
    }

    async Task OnCultureChanged(string culture)
    {
        Repository.Settings.Culture = culture;
        await Repository.UpdateSettings(Repository.Settings.Id);

        LocalizationService.ChangeLanguage(culture);

        await LanguageChanged.InvokeAsync();
    }
}
