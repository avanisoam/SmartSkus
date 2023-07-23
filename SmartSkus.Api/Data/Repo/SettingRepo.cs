using Microsoft.EntityFrameworkCore;
using SmartSkus.Api.Data.Interface;
using SmartSkus.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartSkus.Api.Data.Repo
{
    public class SettingRepo : ISettingRepo
    {
        private readonly InventoryContext _context;

        public SettingRepo(InventoryContext context)
        {
            _context = context;
        }

        public void Create(Settings setting)
        {
            _context.Settings.Add(setting);
        }

        public void Update(Settings setting)
        {
            _context.Settings.Update(setting);
        }

        public void Delete(Settings setting)
        {
            _context.Settings.Remove(setting);
        }

        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM dbo.Setting");
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public IEnumerable<Settings> GetAll()
        {
            return _context.Settings.ToList();
        }

        public Settings GetById(int id)
        {
            return _context.Settings.FirstOrDefault(v => v.Id == id);
        }
    }
}
