using SmartSkus.Shared.Enums;
using System.Collections.Generic;

namespace SmartSkus.Core.Backup;

internal interface IImportExport
{
    IReadOnlyDictionary<string, IFileImport> FileImportByExtension { get; }
    IReadOnlyDictionary<DataFormat, IDataExport> DataExportByFormat { get; }
}