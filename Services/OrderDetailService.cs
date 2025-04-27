using System.Net.Http.Json;
using System.Text.Json;

using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.Services;
public class OrderDetailService
{
    private readonly HttpClient _client;

    public OrderDetailService()
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
    public async Task<List<OrderDetail>> GetOrderDetailsAsync()
    {
        var url = $"{GetBaseUrl()}/api/OrderDetail";

        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var listOrderDetail = JsonSerializer.Deserialize<List<OrderDetail>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return listOrderDetail ?? new List<OrderDetail>();
        }

        return new List<OrderDetail>();
    }

    // CREATE
    public async Task<bool> AddOrderDetailAsync(OrderDetail a)
    {
        var url = $"{GetBaseUrl()}/api/OrderDetail";
        var response = await _client.PostAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // UPDATE
    public async Task<bool> UpdateOrderDetailAsync(int id, OrderDetail a)
    {
        var url = $"{GetBaseUrl()}/api/OrderDetail/{id}";
        a.Id = id;
        var response = await _client.PutAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> DeleteOrderDetailAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/OrderDetail/{id}";
        var response = await _client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    // READ by ID
    public async Task<OrderDetail?> GetOrderDetailByIdAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/OrderDetail/{id}";
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<OrderDetail>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return a;
        }

        return null;
    }
}