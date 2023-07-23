using SmartSkus.Shared.Dtos;
using SmartSkus.Core.Local.Models;
using SmartSkus.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSkus.Core.Local.Interface;

public interface IRepository : IDataModel
{
    event EventHandler? DataChanged;

    Task Initialize(IInventoryService InventoryService, IMasterService MasterService, ISettingsService SettingsService);
    Task DeleteAll(IInventoryService InventoryService, IMasterService MasterService, ISettingsService SettingsService);

    #region AppModel Init

    AppModel AppModelObject { get; }

    IReadOnlyDictionary<long, AppModel> AppModelDictionary { get; } 

    #endregion

    #region SettingsModel

    long NextSettingsId { get; }
    SettingsModel Settings { get; }
    Task AddSettings(SettingsModel settings);
    Task UpdateSettings(long id);
    Task DeleteSettings(long id);
    IReadOnlyDictionary<long, SettingsModel> AllSettings { get; }

    #endregion

    #region Support Import, Reset and Reload

    Task AddData(IDataModel data);
    Task ResetSettings();
    Task LoadExamples();

    #endregion

    #region Sku Add Single and Bulk

    Task<SkuModelDto> AddSkuModel(SkuModelDto skuModelDto);
    Task<List<SkuModelDto>> AddBulkSkus(BulkAddDto bulkAddDto);

    #endregion DueVej
}
