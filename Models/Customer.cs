
namespace PBL3MAUIApp.Models;
public class Customer {
    public int Id { get; set;}
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public Customer () {}
    public Customer (string name, string phoneNumber) {
        Name = name;
        PhoneNumber = phoneNumber;
    }
}