using SmartSkus.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SmartSkus.Shared.Dtos;

namespace SmartSkus.Core.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly HttpClient _httpClient;

        public SettingsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<SettingsDto?> Create(SettingsDto settingsDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<SettingsDto>("api/setting", settingsDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(SettingsDto);
                    }

                    return await response.Content.ReadFromJsonAsync<SettingsDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<SettingsDto> Update(SettingsDto settingsDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/setting/{settingsDto.Id}", settingsDto);

                return settingsDto;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<SettingsDto?> Delete(long id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/setting/{id}");

                return default(SettingsDto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAll()
        {
            try
            {
                var response = await _httpClient.DeleteAsync("api/setting");
                //return response;
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return true;
                    }

                    return false;

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<SettingsDto>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<SettingsDto>>("api/setting");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
