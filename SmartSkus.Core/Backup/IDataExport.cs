using SmartSkus.Shared.Enums;
using System.Threading.Tasks;

namespace SmartSkus.Core.Backup;

public interface IDataExport
{
    bool UnsavedChanges { get; }

    DataFormat DataFormat { get; }

    Task ExportData();
}