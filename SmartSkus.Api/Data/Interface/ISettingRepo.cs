using SmartSkus.Api.Models;
using System.Collections.Generic;

namespace SmartSkus.Api.Data.Interface
{
    /// <summary>
    /// Settings CRUD
    /// </summary>
    public interface ISettingRepo
    {
        void Create(Settings setting);
        void Update(Settings setting);
        void Delete(Settings setting);
        void DeleteAll();

        bool SaveChanges();

        IEnumerable<Settings> GetAll();
        Settings GetById(int id);
    }
}
