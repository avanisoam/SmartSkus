using System.Threading.Tasks;

namespace SmartSkus.Core.App;

public interface IUserDisplayName
{
    string DisplayName { get; set; }

    Task<string> GetUserDisplayName();
}
