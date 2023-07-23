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
    public class MasterService : IMasterService
    {
        private readonly HttpClient _httpClient;

        public MasterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CategoryDto> AddCategory(CategoryDto categoryDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<CategoryDto>("api/masterdata/categories/add", categoryDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CategoryDto);
                    }

                    return await response.Content.ReadFromJsonAsync<CategoryDto>();

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

        public async Task<CategoryDto> UpdateCategory(CategoryDto categoryDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/masterdata/updateCategory/{categoryDto.CategoryID}", categoryDto);
                return default(CategoryDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CategoryDto> DeleteCategory(long id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/masterdata/deletecategory/{id}");

                return default(CategoryDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategory()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDto>>("api/masterdata/categories");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<CategoryDto>($"api/masterdata/getcategorybyid/{id}");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OptionKeyDto> AddOptionKeys(OptionKeyDto optionKeyDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<OptionKeyDto>("api/masterdata/options/add", optionKeyDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(OptionKeyDto);
                    }

                    return await response.Content.ReadFromJsonAsync<OptionKeyDto>();

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

        public async Task<OptionKeyAndValueDto> DeleteOptionKeysWithValues(long id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/masterdata/deleteoptionkeywithvalues/{id}");

                return default(OptionKeyAndValueDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OptionKeyDto>> GetAllOptionKeys()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<OptionKeyDto>>("api/masterdata/options");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OptionValueDto> AddOptionValues(OptionValueDto optionValueDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<OptionValueDto>("api/masterdata/addoptionvalues", optionValueDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(OptionValueDto);
                    }

                    return await response.Content.ReadFromJsonAsync<OptionValueDto>();

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

        public async Task<OptionValueDto> DeleteOptionValues(long id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/masterdata/deleteoptionvalue/{id}");

                return default(OptionValueDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OptionValueDto>> GetAllOptionValue()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<OptionValueDto>>("api/masterdata/getoptionvalues");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateCategoryOptionKey(int categoryId, List<long> OptionKeyIds)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/masterdata/updateoptionkeyslist/{categoryId}", OptionKeyIds);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CategoryOptionKeyDto>> GetAllCategoryOptionKey()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryOptionKeyDto>>($"api/masterdata/getallcategorywithkeys");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CategoryOptionKeyDto>> GetByCategoryId(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryOptionKeyDto>>($"api/masterdata/getbycategoryid/{id}");
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
