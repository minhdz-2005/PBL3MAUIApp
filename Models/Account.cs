
namespace PBL3MAUIApp.Models;
public class Account {
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public Account () {}
    public Account (string username, string password, string role) {
        Username = username;
        Password = password;
        Role = role;
    }
}