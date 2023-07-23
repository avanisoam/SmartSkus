using SmartSkus.Shared.Dtos;
using System.Collections.Generic;

namespace SmartSkus.Core.Local.Models
{
    public class AppModel
    {
        public List<AppModel> AppModelList = new();

        #region SkuModelDto

        public SkuModelDto CreateSkuModel(string name)
        {
            SkuModelDto skuModelDto = new()
            {
                //Id = id,
                ItemName = name,
                Attribute1 = name,
                Attribute2 = name,
                Attribute3 = name
                //,
                //GenerateSku = name
            };

            SkuModelDtoList.Add(skuModelDto);

            return skuModelDto;
        }

        public SkuModelDto SkuModelDtoObject { get; set; } = new();

        public List<SkuModelDto> SelectedSkuModelDtoList = new();

        public List<SkuModelDto> SkuModelDtoList = new();

        #endregion

        public BulkAddDto BulkAddDtoObject { get; set; } = new();

        #region ItemDto

        public List<ItemDto> SelectedItemDtoList { get; set; } = new();

        public List<ItemDto> ItemDtoList = new();

        #endregion

        #region ItemVariationDto

        public List<ItemVariationDto> SelectedItemVariationDtoList = new();

        public List<ItemVariationDto> ItemVariationDtoList = new(); 

        #endregion
    }
}
