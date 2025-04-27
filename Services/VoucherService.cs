using System.Net.Http.Json;
using System.Text.Json;

using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.Services;
public class VoucherService
{
    private readonly HttpClient _client;

    public VoucherService()
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
    public async Task<List<Voucher>> GetVouchersAsync()
    {
        var url = $"{GetBaseUrl()}/api/Voucher";

        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var listVoucher = JsonSerializer.Deserialize<List<Voucher>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return listVoucher ?? new List<Voucher>();
        }

        return new List<Voucher>();
    }

    // CREATE
    public async Task<bool> AddVoucherAsync(Voucher a)
    {
        var url = $"{GetBaseUrl()}/api/Voucher";
        var response = await _client.PostAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // UPDATE
    public async Task<bool> UpdateVoucherAsync(int id, Voucher a)
    {
        var url = $"{GetBaseUrl()}/api/Voucher/{id}";
        a.Id = id;
        var response = await _client.PutAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> DeleteVoucherAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Voucher/{id}";
        var response = await _client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    // READ by ID
    public async Task<Voucher?> GetVoucherByIdAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Voucher/{id}";
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<Voucher>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return a;
        }

        return null;
    }
}