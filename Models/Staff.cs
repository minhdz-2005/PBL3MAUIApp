
using System.ComponentModel;

namespace PBL3MAUIApp.Models;
public class Staff : INotifyPropertyChanged
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    private string _name = string.Empty;
    public string Name
    {
        get { return _name; }
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
    private string _phoneNumber = string.Empty;
    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set
        {
            if (_phoneNumber != value)
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
    }
    private string _role = string.Empty;
    public string Role
    {
        get { return _role; }
        set
        {
            if (_role != value)
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }
    }
    private decimal _alary;
    public decimal Salary
    {
        get { return _alary; }
        set
        {
            if (_alary != value)
            {
                _alary = value;
                OnPropertyChanged(nameof(Salary));
            }
        }
    }

    public Staff () {}
    public Staff (string username, string name, string phoneNumber, string role, decimal salary) {
        Username = username;
        Name = name;
        PhoneNumber = phoneNumber;
        Role = role;
        Salary = salary;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}