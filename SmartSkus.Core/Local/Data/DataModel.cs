using SmartSkus.Core.Local.Interface;
using SmartSkus.Core.Local.Models;
using System.Collections.Generic;

namespace SmartSkus.Core.Local.Data;

public class DataModel : IDataModel
{
    public AppModel AppModelObject { get; set; } = new();
    public List<SettingsModel> SettingsList { get; set; } = new();
}
