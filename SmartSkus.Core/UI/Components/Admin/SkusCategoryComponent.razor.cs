using Blazorise.Localization;
using SmartSkus.Core.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazorise;
using SmartSkus.Shared.Enums;
using SmartSkus.Shared.Dtos;
using SmartSkus.Core.Local.Interface;

namespace SmartSkus.Core.UI.Components.Admin
{
    public partial class SkusCategoryComponent
    {
        #region Variables and Properties

        #region Inject

        [Inject]
        ITextLocalizer<Translations> Localizer { get; set; } = null!;

        [Inject]
        IRepository Repository { get; set; } = null!;

        [Inject]
        public ISettingsService? SettingsService { get; set; }

        [Inject]
        public IMasterService? MasterService { get; set; }

        [Inject] IMessageService MessageService { get; set; }

        #endregion

        Validations? validations;

        protected List<long> SelectedIds = new List<long>();
        public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public IEnumerable<OptionKeyDto> OptionKeys { get; set; } = new List<OptionKeyDto>();
        public IEnumerable<SettingsDto> settingsDtos { get; set; } = new List<SettingsDto>();

        public int CategoryId { get; set; }
        public CategoryDto? editItem { get; set; }
        public SettingsDto newSettingsObject { get; set; } = new();

        public CategoryDto newCategoryObject { get; set; } = new();

        public OptionKeyAndValueDto newOptionKeyAndValueObject { get; set; } = new(); 

        #endregion

        protected override async Task OnInitializedAsync()
        {
            settingsDtos = await SettingsService.GetAll();
            Categories = await MasterService.GetAllCategory();
            OptionKeys = await MasterService.GetAllOptionKeys();
        }

        async Task ShowMainScreen()
        {
            Repository.Settings.Screen = Screen.Main;
            await Repository.UpdateSettings(Repository.Settings.Id);
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

        public async Task DeleteItem(int id)
        {
            bool confirmDelete = await MessageService.Confirm("Are you sure you want to Delete this?", "Confirmation");
            if (confirmDelete)
            { 
                await MasterService.DeleteCategory(id);

                Categories = await MasterService.GetAllCategory();

                //MyNavigationManager.NavigateTo("/category");
                Action = null;
                CategoryID = null;
            }
        }

        public async Task ShowModal(int id)
        {
            CategoryId = id;
            newCategoryObject = await MasterService.GetCategoryById(id);
            foreach (var opt in newCategoryObject.CategoryOptionKeys)
            {
                SelectedIds.Add(opt.OptionKeyID);
            }
            await modalRef.Show();
        }

        public async Task AddItem()
        {
            if (await validations.ValidateAll())
            {
                await MasterService.AddCategory(newCategoryObject);

                newCategoryObject = new();

                Categories = await MasterService.GetAllCategory();

                //MyNavigationManager.NavigateTo("/category");

                Action = null;
                CategoryID = null;
            }
        }

        public async Task UpdateItem()
        {

            if (editItem is not null)
            {
                await MasterService.UpdateCategory(editItem);
            }

            Categories = await MasterService.GetAllCategory();

            //MyNavigationManager.NavigateTo("admin/category");

            Action = null;
            CategoryID = null;
        }

        protected void ShowSelectedValues()
        {
            OutPutValue = string.Join(",", SelectedIds.ToArray());
            MasterService.UpdateCategoryOptionKey(CategoryId, SelectedIds);
            StateHasChanged();
            SelectedIds = new List<long>();
            modalRef.Hide();
        }
    }
}
