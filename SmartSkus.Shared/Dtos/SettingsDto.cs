using SmartSkus.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartSkus.Shared.Dtos
{
    //public class EFKeyValuePair<TKey, TValue>
    //{
    //    [Key]
    //    public TKey? Key { get; set; }
    //    public TValue? Value { get; set; }
    //}

    public class SettingsDto
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DataFormat SelectedBackupFormat { get; set; } = DataFormat.Json;

        public Blazorise.Size Size { get; set; }

        public string Culture { get; set; } = "en";

        public string Theme { get; set; } = "default";

        public string Background { get; set; } = "Default";

        public Screen Screen { get; set; } = Screen.Main;
    }
}
