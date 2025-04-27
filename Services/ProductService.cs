using System.Net.Http.Json;
using System.Text.Json;

using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.Services;
public class ProductService
{
    private readonly HttpClient _client;

    public ProductService()
    {
        _client = new HttpClient();
    }

    private string GetBaseUrl()
    {
        //return DeviceInfo.Platform == DevicePlatform.Android
        //? "https://10.0.2.2:7221" : "https://localhost:7221";

        return "https://localhost:7221";
    }

    // READ
    public async Task<List<Product>> GetProductsAsync()
    {
        var url = $"{GetBaseUrl()}/api/Product";

        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var listProduct = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return listProduct ?? new List<Product>();
        }

        return new List<Product>();
    }

    // CREATE
    public async Task<bool> AddProductAsync(Product a)
    {
        var url = $"{GetBaseUrl()}/api/Product";
        var response = await _client.PostAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // UPDATE
    public async Task<bool> UpdateProductAsync(int id, Product a)
    {
        var url = $"{GetBaseUrl()}/api/Product/{id}";
        a.Id = id;
        var response = await _client.PutAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> DeleteProductAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Product/{id}";
        var response = await _client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    // READ by ID
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Product/{id}";
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<Product>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return a;
        }

        return null;
    }
}