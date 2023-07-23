using Blazorise.Localization;
using Microsoft.AspNetCore.Components;
using SmartSkus.Shared.Dtos;
using SmartSkus.Core.Local.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSkus.Core.UI.Components
{
    public partial class AvailableItemsComponent
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

        #endregion

        #region CascadingParameter

        [CascadingParameter]
        Blazorise.Size Size { get; set; }

        #endregion

        #region Inject

        [Inject]
        ITextLocalizer<Translations> Localizer { get; set; } = null!;

        #endregion 

        #endregion

        protected override async Task OnInitializedAsync()
        {
            await Task.Delay(0);
        }

        async Task LoadAllFromCache()
        {
            AppModelObject.SelectedItemDtoList = AppModelObject.ItemDtoList;

            AppModelObject.SelectedItemVariationDtoList = AppModelObject.ItemVariationDtoList;

            AppModelObject.SelectedSkuModelDtoList = AppModelObject.SkuModelDtoList;

            await AppModelObjectChanged.InvokeAsync(AppModelObject);
        }

        async Task ClickId(long ItemID)
        {
            var myItemsList = (from x in AppModelObject.ItemDtoList
                          where x.ItemID == ItemID
                          select x).ToList<ItemDto>();

            AppModelObject.SelectedItemDtoList = myItemsList;

            var myItemVariationsList = (from x in AppModelObject.ItemVariationDtoList
                           where x.ItemID == ItemID
                           select x).ToList<ItemVariationDto>();

            AppModelObject.SelectedItemVariationDtoList = myItemVariationsList;

            var mySkuModelList = (from x in AppModelObject.SkuModelDtoList
                          where x.ItemID == ItemID
                          select x).ToList<SkuModelDto>();

            AppModelObject.SelectedSkuModelDtoList = mySkuModelList;

            await AppModelObjectChanged.InvokeAsync(AppModelObject);
        }
    }
}
