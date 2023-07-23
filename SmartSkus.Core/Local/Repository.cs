using SmartSkus.Shared.Dtos;
using SmartSkus.Core.Local.Data;
using SmartSkus.Core.Local.Interface;
using SmartSkus.Core.Local.Models;
using SmartSkus.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSkus.Core.Local;

internal class Repository : DataModel, IRepository
{
    #region Changed event triggered from Child/Consumer

    public event EventHandler? DataChanged;

    private void OnDataChanged(object? sender, EventArgs e)
    {
        DataChanged?.Invoke(sender, e);
    }

    #endregion

    #region Variables and Init

    public IInventoryService? _inventoryService { get; set; }
    public IMasterService? _masterService { get; set; }
    public ISettingsService? _settingsService { get; set; }

    public async Task Initialize(IInventoryService InventoryService, IMasterService MasterService, ISettingsService SettingsService)
    {
        _inventoryService = InventoryService;
        _masterService = MasterService;

        _settingsService = SettingsService;

        await GetData();
    }

    private async Task GetData()
    {
        if ((_inventoryService != null) && (_masterService != null) && (_settingsService != null))
        {
            RepositoryData repositoryData = new()
            {
                ItemDtoList = (List<ItemDto>)await _inventoryService.GetAllItems(),

                ItemVariationDtoList = (List<ItemVariationDto>)await _inventoryService.GetAllItemVariations(),

                SkuModelDtoList = (List<SkuModelDto>)await _inventoryService.GetAll(),

                SettingsDtoList = (List<SettingsDto>)await _settingsService.GetAll()
            };

            foreach (SettingsDto settings in repositoryData.SettingsDtoList)
            {
                SettingsModel settingsModel = new()
                {
                    Id = settings.Id,
                    Name = settings.Name,
                    SelectedBackupFormat = settings.SelectedBackupFormat,
                    Size = settings.Size,
                    Culture = settings.Culture,
                    Theme = settings.Theme,
                    Background = settings.Background,
                    Screen = settings.Screen
                };

                _settingsDictDto[settings.Id] = settings;

                repositoryData.SettingsModelList.Add(settingsModel);

                repositoryData.SettingsModelDictionary.Add(settings.Id, settingsModel);
            }

            base.AppModelObject = AppModelObject;

            AppModelObject.ItemDtoList = repositoryData.ItemDtoList;
            AppModelObject.ItemVariationDtoList = repositoryData.ItemVariationDtoList;
            AppModelObject.SkuModelDtoList = repositoryData.SkuModelDtoList;

            AppModelObject.AppModelList = repositoryData.AppModelList;
            _newSkuDict = repositoryData.AppModelDictionary;

            SettingsList = repositoryData.SettingsModelList;
            _settingsDict = repositoryData.SettingsModelDictionary;

            if (!SettingsList.Any())
            {
                await AddDefaultSettings();
            }

            Settings = SettingsList.First();
        }
    }

    public async Task DeleteAll(IInventoryService InventoryService, IMasterService MasterService, ISettingsService SettingsService)
    {
        await Initialize(InventoryService, MasterService, SettingsService);
    }

    #endregion

    #region Variables and AppModel Init

    private Dictionary<long, AppModel> _newSkuDict = new();

    public long NextNewSkuId => _newSkuDict.Keys.DefaultIfEmpty().Max() + 1;

    public IReadOnlyDictionary<long, AppModel> AppModelDictionary => _newSkuDict;

    public AppModel AppModelObject { get; set; } = new();

    #endregion

    #region Variables and SettingsModel Init

    private List<SettingsDto> _settingsList = new();
    private readonly Dictionary<long, SettingsDto> _settingsDictDto = new();

    private Dictionary<long, SettingsModel> _settingsDict = new();

    public long NextSettingsId => _settingsDict.Keys.DefaultIfEmpty().Max() + 1;

    public SettingsModel Settings { get; set; } = new();

    public async Task AddSettings(SettingsModel settings)
    {
        _settingsDict[settings.Id] = settings;

        SettingsDto settingsEntity = new()
        {
            Id = settings.Id,
            Name = settings.Name,
            SelectedBackupFormat = settings.SelectedBackupFormat,
            Size = settings.Size,
            Culture = settings.Culture,
            Theme = settings.Theme,
            Background = settings.Background,
            Screen = settings.Screen
        };

        _settingsList.Add(settingsEntity);

        _settingsDictDto[settingsEntity.Id] = settingsEntity;

        // Ef Core
        await _settingsService.Create(settingsEntity);
    }

    public async Task UpdateSettings(long id)
    {
        // TODO: Hack
        if (id == 0)
        {
            return;
        }

        if (_settingsDict.TryGetValue(id, out SettingsModel? settings))
        {
            if (_settingsDictDto.TryGetValue(settings.Id, out SettingsDto? settingsEntity))
            {
                settingsEntity.Name = settings.Name;
                settingsEntity.SelectedBackupFormat = settings.SelectedBackupFormat;
                settingsEntity.Size = settings.Size;
                settingsEntity.Culture = settings.Culture;
                settingsEntity.Theme = settings.Theme;
                settingsEntity.Background = settings.Background;
                settingsEntity.Screen = settings.Screen;

                // Ef Core
                await _settingsService.Update(settingsEntity);
            }
            else
            {
                throw new ArgumentException($"Settings {settings.Id} doesn't exist!");
            }
        }
        else
        {
            throw new ArgumentException($"Settings {id} doesn't exist!");
        }
    }

    public async Task DeleteSettings(long id)
    {
        SettingsList.Remove(_settingsDict[id]);

        _settingsDict.Remove(id);

        _settingsList.Remove(_settingsDictDto[id]);

        _settingsDictDto.Remove(id);

        // TODO: Index DB
        //await DeleteByKey<long, SettingsEntity>(id);

        // Ef core
        await _settingsService.Delete(id);
    }

    public IReadOnlyDictionary<long, SettingsModel> AllSettings => _settingsDict;

    #endregion

    #region Support Import, Reset and Reload

    /// <summary>
    /// Import
    /// </summary>
    public async Task AddData(IDataModel data)
    {
        foreach (SettingsModel settings in data.SettingsList)
        {
            SettingsList.RemoveAll(s => s.Id == settings.Id);
            SettingsList.Add(settings);

            _settingsDict[settings.Id] = settings;
        }

        await Task.Delay(0);
    }

    /// <summary>
    /// Reset or Clear
    /// </summary>
    public async Task ResetSettings()
    {
        await DeleteSettings(Settings.Id);

        await AddDefaultSettings();

        Settings = SettingsList.First();
    }

    private async Task AddDefaultSettings()
    {
        SettingsModel settings = new()
        {
            Id = NextSettingsId,
            Name = "Smart Sku's",
            Theme = "default"
        };

        SettingsList.Add(settings);

        await AddSettings(settings);
    }

    /// <summary>
    /// Reload or Reset Default data
    /// </summary>
    /// <returns></returns>
    public async Task LoadExamples()
    {
        // TODO:
    }

    #endregion

    #region Variables and add Single/Bulk Sku's

    private Dictionary<int, SkuModelDto> _skuModelDict = new();

    public async Task<SkuModelDto> AddSkuModel(SkuModelDto skuModelDto)
    {
        skuModelDto = await _inventoryService.Create(skuModelDto);

        _skuModelDict[skuModelDto.Id] = skuModelDto;

        return skuModelDto;
    }

    public async Task<List<SkuModelDto>> AddBulkSkus(BulkAddDto bulkAddDto)
    {
        await _inventoryService.Add(bulkAddDto);

        var skuModelDtos = (List<SkuModelDto>)await _inventoryService.GetAll();

        return skuModelDtos;
    }

    public IReadOnlyDictionary<int, SkuModelDto> AllSkuModel => _skuModelDict;

    #endregion
}