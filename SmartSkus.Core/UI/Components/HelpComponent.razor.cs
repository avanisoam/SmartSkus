using Blazorise.Localization;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using SmartSkus.Shared.Enums;
using SmartSkus.Core.Local.Interface;

namespace SmartSkus.Core.UI.Components;

public partial class HelpComponent
{
    #region Inject

    [Inject]
    ITextLocalizer<Translations> Localizer { get; set; } = null!;

    [Inject]
    IRepository Repository { get; set; } = null!;

    [Inject]
    IMyService MyService { get; set; } = null!;

    #endregion


    async Task ShowMainScreen()
    {
        Repository.Settings.Screen = Screen.Main;
        await Repository.UpdateSettings(Repository.Settings.Id);

        //child component
        MyService.CallRequestRefresh();
    }
}
