
using System.IO;
using System.Threading.Tasks;

namespace SmartSkus.Core.Backup;

public interface IFileImport
{
    string FileExtension { get; }

    Task ImportData(Stream stream);
}