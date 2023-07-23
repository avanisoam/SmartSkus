using SmartSkus.Shared.Enums;

namespace SmartSkus.Core.Local.Models;

public class SettingsModel
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DataFormat SelectedBackupFormat { get; set; } = DataFormat.Json;

    public Blazorise.Size Size { get; set; }

    public string Culture { get; set; } = "en";

    public string Theme { get; set; } = "default";

    public string Background { get; set; } = "Default";

    public Screen Screen { get; set; } = Screen.Main;
}
