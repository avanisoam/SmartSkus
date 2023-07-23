using Blazorise.Localization;
using SmartSkus.Core.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartSkus.Core.Local.Models;
using SmartSkus.Shared.Dtos;
using SmartSkus.Core.Local.Interface;

namespace SmartSkus.Core.UI.Components
{
    public partial class AddBulkSkuComponent
    {
        #region Variables

        #region Parameter

        [Parameter]
        public AppModel AppModelObject { get; set; } = null!;

        [Parameter]
        public EventCallback<AppModel> AppModelObjectChanged { get; set; }

        [Parameter]
        public SettingsModel Settings { get; set; } = null!;

        [Parameter]
        public EventCallback<SettingsModel> SettingsChanged { get; set; }

        [Parameter]
        public IEnumerable<SkuModelDto>? Message { get; set; }

        #endregion

        #region Inject

        [Inject]
        ITextLocalizer<Translations> Localizer { get; set; } = null!;

        [Inject]
        IRepository Repository { get; set; } = null!;

        [Inject]
        public IInventoryService? InventoryService { get; set; }

        [Inject]
        public IMasterService? MasterService { get; set; }

        #endregion

        #region CascadingParameter

        [CascadingParameter]
        Blazorise.Size Size { get; set; }

        #endregion

        #region Enumerable & List

        public List<long> bulkAdd = new();

        public IEnumerable<SkuModelDto> Skus { get; set; } = new List<SkuModelDto>();

        public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

        public IEnumerable<OptionKeyDto> OptionKeys { get; set; } = new List<OptionKeyDto>();

        public IEnumerable<OptionValueDto> OptionValues { get; set; } = new List<OptionValueDto>();

        public IEnumerable<CategoryOptionKeyDto> CategoryOptionKeys { get; set; } = new List<CategoryOptionKeyDto>(); 

        #endregion

        public int OptionKeyID;
        public int CategoryID;

        public BulkAddDto newBulkAddObject { get; set; } = new();

        #endregion

        protected override async Task OnInitializedAsync()
        {
            if(MasterService != null)
            {
                CategoryOptionKeys = await MasterService.GetAllCategoryOptionKey();
                Categories = await MasterService.GetAllCategory();
                OptionValues = await MasterService.GetAllOptionValue();
            }

            if (InventoryService != null)
            {
                Skus = await InventoryService.GetAll();
            }
        }

        public async Task AddItem()
        {
            if (OptionKeyID != 0)
            {
                bulkAdd.Add(OptionKeyID);
            }
            newBulkAddObject.OptionKeyIds = bulkAdd;
            newBulkAddObject.CategoryId = CategoryID;
            await InventoryService.Add(newBulkAddObject);

            newBulkAddObject = new();

            Message = await InventoryService.GetAll();

            //MyNavigationManager.NavigateTo("/inventory");
            //await BlazoredModal.CloseAsync(ModalResult.Ok(Message));
        }

        bool CheckCustomValidations()
        {
            if ((AppModelObject.BulkAddDtoObject.CategoryId <= 0) || (bulkAdd.Count == 0))
            {
                HideLabel = false;

                return false;
            }
            else
            {
                HideLabel = true;

                return true;
            }
        }

        async Task AddBulkData()
        {
            if (await validations.ValidateAll())
            {
                if (!CheckCustomValidations())
                {
                    return;
                }

                if (OptionKeyID != 0)
                {
                    bulkAdd.Add(OptionKeyID);
                }

                BulkAddDto bulkAddDto = new()
                {
                    SKU = AppModelObject.BulkAddDtoObject.SKU,
                    Description = AppModelObject.BulkAddDtoObject.Description,
                    Quantity = AppModelObject.BulkAddDtoObject.Quantity,
                    OptionKeyIds = bulkAdd, //new List<long>() { 2,3, 4},
                    CategoryId = AppModelObject.BulkAddDtoObject.CategoryId

                };

                AppModelObject.SkuModelDtoList = await Repository.AddBulkSkus(bulkAddDto);

                AppModelObject.BulkAddDtoObject = new();
                bulkAdd = new();
                selectedValue = 0;
                OptionKeys = new List<OptionKeyDto>();
                ////OptionValues = new List<OptionValueDto>();
                validations?.ClearAll();

                AppModelObject.ItemDtoList = (List<ItemDto>)await InventoryService.GetAllItems();
                AppModelObject.ItemVariationDtoList
                    = (List<ItemVariationDto>)await InventoryService.GetAllItemVariations();

                AppModelObject.SelectedSkuModelDtoList = AppModelObject.SkuModelDtoList;
                AppModelObject.SelectedItemDtoList = AppModelObject.ItemDtoList;
                AppModelObject.SelectedItemVariationDtoList = AppModelObject.ItemVariationDtoList;

                await AppModelObjectChanged.InvokeAsync(AppModelObject);
            }
            else
            {
                CheckCustomValidations();
            }
        }
    }
}