using System.ComponentModel;

namespace SmartSkus.Shared.Enums
{
    public enum Screen
    {
        [Description("Main")]
        Main,

        [Description("Options")]
        Options,

        [Description("AdminSettings")]
        AdminSettings,

        [Description("Help")]
        Help,

        [Description("About")]
        About,

        [Description("QrCodeTester")]
        QrCodeTester,

        [Description("Inventory")]
        Inventory,

        [Description("SkuCategory")]
        SkuCategory,

        [Description("SkuOptions")]
        SkuOptions
    }
}
