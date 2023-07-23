using Blazorise.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SmartSkus.Shared.Dtos;
using SmartSkus.Shared.Enums;
using SmartSkus.Core.Local.Interface;
using SmartSkus.Core.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSkus.Core.UI.Components.Admin
{
    public partial class SettingsComponent
    {
        #region Variables and Properties

        #region Inject

        [Inject]
        ITextLocalizer<Translations> Localizer { get; set; } = null!;

        [Inject]
        IRepository Repository { get; set; } = null!;

        [Inject]
        public ISettingsService? SettingsService { get; set; }

        #endregion

        public IEnumerable<SettingsDto> settingsDtos { get; set; } = new List<SettingsDto>();

        public SettingsDto? editItem { get; set; }

        public SettingsDto newSettingsObject { get; set; } = new(); 

        #endregion

        protected override async Task OnInitializedAsync()
        {
            settingsDtos = await SettingsService.GetAll();
        }

        async Task ShowMainScreen()
        {
            Repository.Settings.Screen = Screen.Main;
            await Repository.UpdateSettings(Repository.Settings.Id);
        }

        public async Task AddItem()
        {
            await SettingsService.Create(newSettingsObject);

            newSettingsObject = new();

            settingsDtos = await SettingsService.GetAll();

            //MyNavigationManager.NavigateTo("admin/category");

            Action = null;
            Id = null;
        }

        public async Task UpdateItem()
        {
            if (editItem is not null)
            {
                await SettingsService.Update(editItem);
            }

            settingsDtos = await SettingsService.GetAll();

            //MyNavigationManager.NavigateTo("admin/category");

            Action = null;
            Id = null;
        }

        public async Task DeleteItem(long id)
        {
            await SettingsService.Delete(id);

            settingsDtos = await SettingsService.GetAll();
            //MyNavigationManager.NavigateTo("admin/category");

            Action = null;
            Id = null;
        }

        //public async Task DeleteAll()
        //{
        //    bool confirmCancel = await jsRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to Delete All?");
        //    if (confirmCancel)
        //    {
        //        await SettingsService.DeleteAll();

        //        settingsDtos = await SettingsService.GetAll();

        //        Action = null;
        //        Id = null;
        //    }
        //}
    }
}
