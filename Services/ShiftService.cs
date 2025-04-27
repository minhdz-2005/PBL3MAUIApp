using System.Net.Http.Json;
using System.Text.Json;

using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.Services;
public class ShiftService
{
    private readonly HttpClient _client;

    public ShiftService()
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
    public async Task<List<Shift>> GetShiftsAsync()
    {
        var url = $"{GetBaseUrl()}/api/Shift";

        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var listShift = JsonSerializer.Deserialize<List<Shift>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return listShift ?? new List<Shift>();
        }

        return new List<Shift>();
    }

    // CREATE
    public async Task<bool> AddShiftAsync(Shift a)
    {
        var url = $"{GetBaseUrl()}/api/Shift";
        var response = await _client.PostAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // UPDATE
    public async Task<bool> UpdateShiftAsync(int id, Shift a)
    {
        var url = $"{GetBaseUrl()}/api/Shift/{id}";
        a.Id = id;
        var response = await _client.PutAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> DeleteShiftAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Shift/{id}";
        var response = await _client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    // READ by ID
    public async Task<Shift?> GetShiftByIdAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Shift/{id}";
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<Shift>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return a;
        }

        return null;
    }
}