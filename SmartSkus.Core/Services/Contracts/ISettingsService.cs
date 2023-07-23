using SmartSkus.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSkus.Core.Services.Contracts
{
    public interface ISettingsService
    {
        Task<SettingsDto?> Create(SettingsDto settingsDto);
        Task<SettingsDto> Update(SettingsDto settingsDto);
        Task<SettingsDto> Delete(long id);
        Task<bool> DeleteAll();

        Task<IEnumerable<SettingsDto>> GetAll();
    }
}
