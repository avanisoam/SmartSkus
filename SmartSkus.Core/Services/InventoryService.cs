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
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient _httpClient;

        public InventoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<SkuModelDto> Create(SkuModelDto skuModelDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<SkuModelDto>("api/sku", skuModelDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(SkuModelDto);
                    }

                    return await response.Content.ReadFromJsonAsync<SkuModelDto>();

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

        public async Task<BulkAddDto> Add(BulkAddDto bulkAdddto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<BulkAddDto>("api/inventory/skus/bulk-add", bulkAdddto);

                if (response.IsSuccessStatusCode)
                {
                    //if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    //{
                    //    return default(Object);
                    //}

                    //return await response.Content.ReadFromJsonAsync<Object>();
                    return default(BulkAddDto);

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // Console.WriteLine(ex.Message);
            }
        }

        public async Task<IEnumerable<ItemDto>> GetAllItems()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<ItemDto>>("api/inventory/items");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ItemVariationDto>> GetAllItemVariations()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<ItemVariationDto>>("api/inventory/getvariationsofallitems");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<SkuModelDto>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<SkuModelDto>>("api/sku");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SkuModelDto[]> GetAllSkuArray()
        {
            var response = await _httpClient.GetFromJsonAsync<SkuModelDto[]>("api/sku/getskuarray");
            return response;
        }

        public async Task<SkuModelDto> GetById(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<SkuModelDto>($"api/sku/{id}");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SkuModelDto> Update(SkuModelDto skuModelDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/sku/{skuModelDto.Id}", skuModelDto);

                return default(SkuModelDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SkuModelDto> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/sku/{id}");

                return default(SkuModelDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAll()
        {
            try
            {
                var response = await _httpClient.DeleteAsync("api/inventory/deleteall");
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
    }
}
