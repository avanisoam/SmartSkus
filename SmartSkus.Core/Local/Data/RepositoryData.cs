
#region using

using System.Collections.Generic;
using SmartSkus.Core.Local.Models;
using SmartSkus.Shared.Dtos;

#endregion

namespace SmartSkus.Core.Local.Data;

internal class RepositoryData
{
    #region AppModel

    public List<AppModel> AppModelList { get; set; } = new();
    public Dictionary<long, AppModel> AppModelDictionary { get; set; } = new();

    #endregion

    #region SettingsModel

    public List<SettingsModel> SettingsModelList { get; set; } = new();
    public Dictionary<long, SettingsModel> SettingsModelDictionary { get; set; } = new();

    #endregion

    #region Dtos

    public List<SettingsDto> SettingsDtoList { get; set; } = new();
    public List<ItemDto> ItemDtoList { get; set; } = new();
    public List<ItemVariationDto> ItemVariationDtoList { get; set; } = new();
    public List<SkuModelDto> SkuModelDtoList { get; set; } = new();

    #endregion
}
