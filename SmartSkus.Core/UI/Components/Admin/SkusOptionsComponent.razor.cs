using Blazorise.Localization;
using Microsoft.AspNetCore.Components;
using SmartSkus.Shared.Dtos;
using SmartSkus.Shared.Enums;
using SmartSkus.Core.Local.Interface;
using SmartSkus.Core.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;

namespace SmartSkus.Core.UI.Components.Admin
{
    public partial class SkusOptionsComponent
    {
        #region Variables and Properties

        #region Inject

        [Inject]
        IRepository Repository { get; set; } = null!;

        [Inject]
        public IMasterService MasterService { get; set; }

        [Inject]
        public ISettingsService? SettingsService { get; set; }

        [Inject]
        NavigationManager MyNavigationManager { get; set; }

        [Inject]
        ITextLocalizer<Translations> Localizer { get; set; } = null!;

        [Inject] IMessageService MessageService { get; set; }

        #endregion

        public IEnumerable<SettingsDto> settingsDtos { get; set; } = new List<SettingsDto>();
        public IEnumerable<OptionKeyDto> OptionKeys { get; set; } = new List<OptionKeyDto>();
        public IEnumerable<OptionValueDto> OptionValues { get; set; } = new List<OptionValueDto>();

        public SettingsDto? editItem { get; set; }

        public SettingsDto newSettingsObject { get; set; } = new();

        public OptionKeyDto newOptionKeyObject { get; set; } = new();

        public OptionValueDto newOptionValueObject { get; set; } = new(); 

        #endregion

        protected override async Task OnInitializedAsync()
        {
            settingsDtos = await SettingsService.GetAll();

            OptionKeys = await MasterService.GetAllOptionKeys();
        }

        async Task ShowMainScreen()
        {
            Repository.Settings.Screen = Screen.Main;
            await Repository.UpdateSettings(Repository.Settings.Id);
        }

        public async Task AddOptionKey()
        {
            if (await validations.ValidateAll())
            {
                await MasterService.AddOptionKeys(newOptionKeyObject);

                newOptionKeyObject = new();

                OptionKeys = await MasterService.GetAllOptionKeys();

                //MyNavigationManager.NavigateTo("/addoptions");
                Action = null;
                OptionKeyID = null;
            }
        }

        public async Task AddOptionValue()
        {
            if (await validations.ValidateAll())
            {
                if (OptionKeyID != null)
                {
                    newOptionValueObject.OptionKeyID = (long)OptionKeyID;
                }
                await MasterService.AddOptionValues(newOptionValueObject);

                newOptionValueObject = new();

                OptionKeys = await MasterService.GetAllOptionKeys();

                //MyNavigationManager.NavigateTo("/addoptions");

                Action = null;
                OptionKeyID = null;
            }
        }

        public async Task DeleteItem(long id)
        {
            bool confirmDelete = await MessageService.Confirm("Are you sure you want to Delete this?", "Confirmation");
            if (confirmDelete)
            {
                await MasterService.DeleteOptionValues(id);

                OptionKeys = await MasterService.GetAllOptionKeys();

                OptionValues = await MasterService.GetAllOptionValue();

                MyNavigationManager.NavigateTo("/addoptions");
            }
        }

        public async Task DeleteOptionKey(long id)
        {
            bool confirmDelete = await MessageService.Confirm("Are you sure you want to Delete this?", "Confirmation");
            if (confirmDelete)
            {
                await MasterService.DeleteOptionKeysWithValues(id);

                OptionKeys = await MasterService.GetAllOptionKeys();

                //MyNavigationManager.NavigateTo("/addoptions");
                Action = null;
                OptionKeyID = null;
            }
        }

        //public async Task DeleteAll()
        //{
            //bool confirmCancel = await jsRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to Delete All?");
            //if (confirmCancel)
            //{
            //    await SettingsService.DeleteAll();

            //    settingsDtos = await SettingsService.GetAll();

            //    Action = null;
            //    Id = null;
            //}
        //}
    }
}