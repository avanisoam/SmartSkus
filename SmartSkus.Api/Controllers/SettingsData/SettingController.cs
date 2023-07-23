using Microsoft.AspNetCore.Mvc;
using SmartSkus.Api.Data.Interface;
using SmartSkus.Api.Models;
using System.Collections.Generic;

namespace SmartSkus.Api.Controllers.SettingsData
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingRepo _repo;

        public SettingController(ISettingRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Settings>> GetAll()
        {
            var settings = _repo.GetAll();
            return Ok(settings);
        }

        [HttpGet("{id}", Name = "GetSettingById")]
        public ActionResult<Settings> GetSettingById(int id)
        {
            var setting = _repo.GetById(id);
            if (setting == null)
            {
                return NotFound();
            }
            return Ok(setting);
        }

        [HttpPost]
        public ActionResult Create(Settings setting)
        {
            if (setting == null)
            {
                return BadRequest(ModelState);
            }
           
            _repo.Create(setting);
            _repo.SaveChanges();
            return CreatedAtRoute(routeName: "GetSettingById", routeValues: new { Id = setting.Id }, setting);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Settings setting)
        {
            if (id != setting.Id)
            {
                return NotFound();
            }
            
            _repo.Update(setting);
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var setting = _repo.GetById(id);
            if (setting == null)
            {
                return NotFound();
            }
            _repo.Delete(setting);
            _repo.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public ActionResult DeleteAll()
        {
            _repo.DeleteAll();
            _repo.SaveChanges();

            return NoContent();
        }
    }
}
