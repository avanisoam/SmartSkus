using SmartSkus.Core.Local.Models;
using System.Collections.Generic;

namespace SmartSkus.Core.Local.Interface;

public interface IDataModel
{
    AppModel AppModelObject { get; set; }

    List<SettingsModel> SettingsList { get; set; }
}
