
namespace PBL3MAUIApp.Models;
public class Staff {
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Role { get; set;} = string.Empty;
    public decimal Salary { get; set; }

    public Staff () {}
    public Staff (string username, string name, string phoneNumber, string role, decimal salary) {
        Username = username;
        Name = name;
        PhoneNumber = phoneNumber;
        Role = role;
        Salary = salary;
    }
}