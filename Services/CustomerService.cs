using System.Net.Http.Json;
using System.Text.Json;

using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.Services;
public class CustomerService
{
    private readonly HttpClient _client;

    public CustomerService()
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
    public async Task<List<Customer>> GetCustomersAsync()
    {
        var url = $"{GetBaseUrl()}/api/Customer";

        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var listCustomer = JsonSerializer.Deserialize<List<Customer>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return listCustomer ?? new List<Customer>();
        }

        return new List<Customer>();
    }

    // CREATE
    public async Task<bool> AddCustomerAsync(Customer a)
    {
        var url = $"{GetBaseUrl()}/api/Customer";
        var response = await _client.PostAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // UPDATE
    public async Task<bool> UpdateCustomerAsync(int id, Customer a)
    {
        var url = $"{GetBaseUrl()}/api/Customer/{id}";
        a.Id = id;
        var response = await _client.PutAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Customer/{id}";
        var response = await _client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    // READ by ID
    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Customer/{id}";
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<Customer>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return a;
        }

        return null;
    }
}