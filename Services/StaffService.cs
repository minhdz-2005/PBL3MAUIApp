using System.Net.Http.Json;
using System.Text.Json;

using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.Services;
public class StaffService
{
    private readonly HttpClient _client;

    public StaffService()
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
    public async Task<List<Staff>> GetStaffsAsync()
    {
        var url = $"{GetBaseUrl()}/api/Staff";

        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var listStaff = JsonSerializer.Deserialize<List<Staff>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return listStaff ?? new List<Staff>();
        }

        return new List<Staff>();
    }

    // CREATE
    public async Task<bool> AddStaffAsync(Staff a)
    {
        var url = $"{GetBaseUrl()}/api/Staff";
        var response = await _client.PostAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // UPDATE
    public async Task<bool> UpdateStaffAsync(int id, Staff a)
    {
        var url = $"{GetBaseUrl()}/api/Staff/{id}";
        a.Id = id;
        var response = await _client.PutAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> DeleteStaffAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Staff/{id}";
        var response = await _client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    // READ by ID
    public async Task<Staff?> GetStaffByIdAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Staff/{id}";
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<Staff>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return a;
        }

        return null;
    }
}