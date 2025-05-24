using System.Net.Http.Json;
using System.Text.Json;

using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.Services;
public class ShiftStaffsService
{
    private readonly HttpClient _client;

    public ShiftStaffsService()
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
    public async Task<List<ShiftStaffs>> GetShiftStaffssAsync()
    {
        var url = $"{GetBaseUrl()}/api/ShiftStaffs";

        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var listShiftStaffs = JsonSerializer.Deserialize<List<ShiftStaffs>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return listShiftStaffs ?? new List<ShiftStaffs>();
        }

        return new List<ShiftStaffs>();
    }
    public async Task<List<ShiftStaffs>> GetShiftStaffsByShiftIdAsync(int shiftId)
    {
        var url = $"{GetBaseUrl()}/api/ShiftStaffs/idShift/{shiftId}";
        var response = await _client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var listShiftStaffs = JsonSerializer.Deserialize<List<ShiftStaffs>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return listShiftStaffs ?? new List<ShiftStaffs>();
        }
        return new List<ShiftStaffs>();
    }
    // READ by ShiftId and StaffId
    public async Task<ShiftStaffs?> GetShiftStaffsByShiftIdAndStaffIdAsync(int shiftId, int staffId)
    {
        var url = $"{GetBaseUrl()}/api/ShiftStaffs/idShift/{shiftId}/idStaff/{staffId}";
        var response = await _client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<ShiftStaffs>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return a;
        }
        return null;
    }


    // CREATE
    public async Task<bool> AddShiftStaffsAsync(ShiftStaffs a)
    {
        var url = $"{GetBaseUrl()}/api/ShiftStaffs";
        var response = await _client.PostAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // UPDATE
    public async Task<bool> UpdateShiftStaffsAsync(int id, ShiftStaffs a)
    {
        var url = $"{GetBaseUrl()}/api/ShiftStaffs/{id}";
        a.Id = id;
        var response = await _client.PutAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> DeleteShiftStaffsAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/ShiftStaffs/{id}";
        var response = await _client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    // READ by ID
    public async Task<ShiftStaffs?> GetShiftStaffsByIdAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/ShiftStaffs/{id}";
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<ShiftStaffs>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return a;
        }

        return null;
    }
}