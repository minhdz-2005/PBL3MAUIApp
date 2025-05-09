using System.Net.Http.Json;
using System.Text.Json;

using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.Services;
public class OrderService
{
    private readonly HttpClient _client;

    public OrderService()
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
    public async Task<List<Order>> GetOrdersAsync()
    {
        var url = $"{GetBaseUrl()}/api/Order";

        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var listOrder = JsonSerializer.Deserialize<List<Order>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return listOrder ?? new List<Order>();
        }

        return new List<Order>();
    }
    public async Task<List<Order>> GetOrdersByDateAsync(DateTime date)
    {
        var url = $"{GetBaseUrl()}/api/Order/Date/{date:yyyy-MM-dd}";
        var response = await _client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var listOrder = JsonSerializer.Deserialize<List<Order>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return listOrder ?? new List<Order>();
        }
        return new List<Order>();
    }

    // CREATE
    public async Task<bool> AddOrderAsync(Order a)
    {
        var url = $"{GetBaseUrl()}/api/Order";
        var response = await _client.PostAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // UPDATE
    public async Task<bool> UpdateOrderAsync(int id, Order a)
    {
        var url = $"{GetBaseUrl()}/api/Order/{id}";
        a.Id = id;
        var response = await _client.PutAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> DeleteOrderAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Order/{id}";
        var response = await _client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    // READ by ID
    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Order/{id}";
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<Order>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return a;
        }

        return null;
    }
}