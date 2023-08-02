using Blazorise;
using Blazorise.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SmartSkus.Shared.Dtos;
using SmartSkus.Shared.Enums;
using SmartSkus.Core.Local.Interface;
using SmartSkus.Core.Local.Models;
using SmartSkus.Core.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSkus.Core.UI.Components.Admin
{
    public partial class SkusInventoryComponent
    {
        #region Variables

        #region Inject

        [Inject]
        ITextLocalizer<Translations> Localizer { get; set; } = null!;

        [Inject]
        IRepository Repository { get; set; } = null!;

        [Inject]
        public ISettingsService? SettingsService { get; set; }

        [Inject]
        public IInventoryService InventoryService { get; set; }

        [Inject] IMessageService MessageService { get; set; }

        #endregion

        #region CascadingParameter

        [CascadingParameter]
        public MainComponent _Parent { get; set; } = null!;

        [Parameter]
        public AppModel AppModelObject { get; set; } = null!;

        [Parameter]
        public EventCallback<AppModel> AppModelObjectChanged { get; set; }

        #endregion

        #region Enumerable / List / Array

        public SkuModelDto[]? skus;

        public IEnumerable<SkuModelDto> Skus { get; set; } = new List<SkuModelDto>();

        public IEnumerable<SettingsDto> settingsDtos { get; set; } = new List<SettingsDto>();

        #endregion


        public int Count;

        public SkuModelDto? editItem { get; set; }

        public SkuModelDto newSkuObject { get; set; } = new();

        public SettingsDto newSettingsObject { get; set; } = new();

        #endregion

        protected override async Task OnInitializedAsync()
        {
            settingsDtos = await SettingsService.GetAll();
            Skus = await InventoryService.GetAll();
            Count = Skus.Count();
        }

        async Task ShowMainScreen()
        {
            Repository.Settings.Screen = Screen.Main;
            await Repository.UpdateSettings(Repository.Settings.Id);
            _Parent.RefreshState();
        }

        public async Task AddItem()
        {
            if (await validations.ValidateAll())
            {
                await InventoryService.Create(newSkuObject);

                newSkuObject = new();

                Skus = await InventoryService.GetAll();
                skus = Skus.ToArray();
                Count = Count + 1;

                AppModelObject.SkuModelDtoList = (List<SkuModelDto>)await InventoryService.GetAll();

                AppModelObject.ItemDtoList = (List<ItemDto>)await InventoryService.GetAllItems();
                AppModelObject.ItemVariationDtoList
                    = (List<ItemVariationDto>)await InventoryService.GetAllItemVariations();

                AppModelObject.SelectedSkuModelDtoList = AppModelObject.SkuModelDtoList;
                AppModelObject.SelectedItemDtoList = AppModelObject.ItemDtoList;
                AppModelObject.SelectedItemVariationDtoList = AppModelObject.ItemVariationDtoList;

                await AppModelObjectChanged.InvokeAsync(AppModelObject);

                Action = null;
                Id = null;
            }
        }

        public async Task UpdateItem()
        {
            
            if (editItem is not null)
            {
                await InventoryService.Update(editItem);
            }

            Skus = await InventoryService.GetAll();

            AppModelObject.SkuModelDtoList = (List<SkuModelDto>)await InventoryService.GetAll();

            AppModelObject.ItemDtoList = (List<ItemDto>)await InventoryService.GetAllItems();
            AppModelObject.ItemVariationDtoList
                = (List<ItemVariationDto>)await InventoryService.GetAllItemVariations();

            AppModelObject.SelectedSkuModelDtoList = AppModelObject.SkuModelDtoList;
            AppModelObject.SelectedItemDtoList = AppModelObject.ItemDtoList;
            AppModelObject.SelectedItemVariationDtoList = AppModelObject.ItemVariationDtoList;

            await AppModelObjectChanged.InvokeAsync(AppModelObject);

            Action = null;
            Id = null;
        }

        public async Task DeleteItem(int id)
        {
            await InventoryService.Delete(id);

            Skus = await InventoryService.GetAll();

            Count = Count - 1;
            Action = null;
            Id = null;
        }

        public async Task DeleteAll()
        {
            //bool confirmCancel = await jsRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to Delete All?");
            //if (confirmCancel)
            //{
            //    await InventoryService.DeleteAll();

            //    Skus = await InventoryService.GetAll();
            //    Count = 0;

            //    Action = null;
            //    Id = null;
            //    _Parent.RefreshState();
            //}

            bool confirmDelete = await MessageService.Confirm("Are you sure you want to Delete All?", "Confirmation");
            if (confirmDelete)
            {
                await InventoryService.DeleteAll();

                Skus = new List<SkuModelDto>();
                Count = 0;
                AppModelObject.SkuModelDtoList = new();
                AppModelObject.SelectedSkuModelDtoList = new();
                AppModelObject.ItemDtoList = new();
                AppModelObject.SelectedItemDtoList = new();
                AppModelObject.ItemVariationDtoList = new();
                AppModelObject.SelectedItemVariationDtoList = new();

                //Action = null;
                //Id = null;
                await AppModelObjectChanged.InvokeAsync(AppModelObject);
            }
        }
    }
}
