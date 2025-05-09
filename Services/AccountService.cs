using System.Net.Http.Json;
using System.Text.Json;

using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.Services;
public class AccountService
{
    private readonly HttpClient _client;

    public AccountService ()
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
    public async Task<List<Account>> GetAccountsAsync()
    {
        var url = $"{GetBaseUrl()}/api/Account";

        var response = await _client.GetAsync(url);
        
        if(response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            
            var listAccount = JsonSerializer.Deserialize<List<Account>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            return listAccount ?? new List<Account>();
        }
        
        return new List<Account>();
    }
    public async Task<Account> GetAccountByUsername(string username)
    {
        var url = $"{GetBaseUrl()}/api/Account/Username/{username}";
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var account = JsonSerializer.Deserialize<Account>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return account ?? new Account();
        }

        return new Account();
    }

    // CREATE
    public async Task<bool> AddAccountAsync (Account a)
    {
        var url = $"{GetBaseUrl()}/api/Account";
        var response = await _client.PostAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // UPDATE
    public async Task<bool> UpdateAccountAsync(int id ,Account a)
    {
        var url = $"{GetBaseUrl()}/api/Account/{id}";
        a.Id = id;
        var response = await _client.PutAsJsonAsync(url, a);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> DeleteAccountAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Account/{id}";
        var response = await _client.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }

    // READ by ID
    public async Task<Account?> GetAccountByIdAsync(int id)
    {
        var url = $"{GetBaseUrl()}/api/Account/{id}";
        var response = await _client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var a = JsonSerializer.Deserialize<Account>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return a;
        }

        return null;
    }
}