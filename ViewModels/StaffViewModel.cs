using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Models;
using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels
{
    public class StaffViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Staff> Staffs { get; set; } = new();
        public ObservableCollection<string> Roles { get; set; } = new();
        private int _count = 0;
        public int Count
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnPropertyChanged(nameof(Count));
                }
            }
        }

        private StaffService staffService = new StaffService();
        private AccountService accountService = new AccountService();
        // LOAD DANH SACH NHAN VIEN
        public async Task GetAllStaff()
        {
            List<Staff> listStaff = await staffService.GetStaffsAsync();
            Staffs.Clear();
            Roles.Clear();
            Count = 0;
            foreach (var item in listStaff)
            {
                Staffs.Add(item);
                if (!Roles.Contains(item.Role))
                {
                    Roles.Add(item.Role);
                }
                Count++;
            }
        }
        // LOC NHAN VIEN THEO CHUC VU
        public async Task GetStaffByRole(string role)
        {
            List<Staff> listStaff = await staffService.GetStaffsAsync();
            Staffs.Clear();
            Count = 0;
            foreach (var item in listStaff)
            {
                if (item.Role == role)
                {
                    Staffs.Add(item);
                    Count++;
                }
            }
        }
        // THEM NHAN VIEN
        public async Task<bool> AddStaff(string username, string password, string name, string phone, string role, string salaryText)
        {
            // KIEM TRA GIA TRI

            // kiem tra ten nhan vien
            if (string.IsNullOrWhiteSpace(name))
            {
                await Shell.Current.DisplayAlert("Error", "Employee name cannot be blank !", "OK");
                return false;
            }
            // kiem tra ten nhan vien da ton tai hay chua
            if (Staffs.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                await Shell.Current.DisplayAlert("Error", "Employee name already exists", "OK");
                return false;
            }
            // kiem tra sdt
            bool isNumeric = phone.All(char.IsDigit);
            if (!isNumeric)
            {
                await Shell.Current.DisplayAlert("Error", "Invalid phone number", "OK");
                return false;
            }
            // kiem tra luong
            decimal salary;
            bool isValidSalary = decimal.TryParse(
                salaryText,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out salary
            );
            if (!isValidSalary || salary < 0)
            {
                await Shell.Current.DisplayAlert("Error", "Invalid salary", "OK");
                return false;
            }
            // kiem tra so dien thoai va ten dang nhap
            var list = await staffService.GetStaffsAsync();
            bool existing = true;
            foreach (var item in list)
            {
                if ((item.Username == username && username != null) || item.PhoneNumber == phone)
                {
                    await Shell.Current.DisplayAlert("Error", "Username or phone number already exists", "OK");
                    existing = false;
                    return false;
                }
            }

            //kiem tra ten dang nhap va mat khau (thu ngan)
            bool isValidAccount = true;
            if ((username == null || password == null) && role == "Cashier")
            {
                isValidAccount = false;
                await Shell.Current.DisplayAlert("Error", "Invalid username or password", "OK");
                return false;
            }

            // Debug.WriteLine($"{isNumeric}, {isValidSalary}, {existing}, {isValidAccount}");

            // Them nhan vien vao db
            if (isNumeric && isValidSalary && existing && isValidAccount)
            {
                if (role == "Cashier" && username != null && password != null)
                {
                    Staff staff = new Staff(username, name, phone, role, salary);
                    await staffService.AddStaffAsync(staff);
                    Staffs.Add(staff);
                    Count++;

                    await accountService.AddAccountAsync(new Account(username, password, role));
                }
                if (role != "Cashier")
                {
                    Staff staff = new Staff("", name, phone, role, salary);
                    await staffService.AddStaffAsync(staff);
                    Staffs.Add(staff);
                    Count++;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Add employee failed", "OK");
                    return false;
                }
            }
            return true;
        }
        // CAP NHAT NHAN VIEN
        public async Task<bool> UpdateStaff(string username, string password, string name, string phone, string role, string salaryText, int id)
        {
            // KIEM TRA GIA TRI

            // kiem tra ten nhan vien
            if (string.IsNullOrWhiteSpace(name))
            {
                await Shell.Current.DisplayAlert("Error", "Employee name cannot be blank", "OK");
                return false;
            }
            // kiem tra ten nhan vien da ton tai hay chua
            if (Staffs.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && s.Id != id))
            {
                await Shell.Current.DisplayAlert("Error", "Employee name already exists", "OK");
                return false;
            }

            // kiem tra sdt
            bool isNumeric = phone.All(char.IsDigit);
            if (!isNumeric)
            {
                await Shell.Current.DisplayAlert("Error", "Invalid phone number", "OK");
                return false;
            }
            // kiem tra luong
            decimal salary;
            bool isValidSalary = decimal.TryParse(
                salaryText,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out salary
            );
            if (!isValidSalary || salary < 0)
            {
                await Shell.Current.DisplayAlert("Error", "Invalid salary", "OK");
                return false;
            }
            // kiem tra va ten dang nhap
            var list = await staffService.GetStaffsAsync();
            bool existing = true;
            foreach (var item in list)
            {
                if (item.Username == username && username != null && role == "Cashier")
                {
                    await Shell.Current.DisplayAlert("Error", "Username already exist", "OK");
                    existing = false;
                    return false;
                }
            }

            //kiem tra ten dang nhap va mat khau (thu ngan)
            bool isValidAccount = true;
            if ((username == null || password == null) && role == "Cashier")
            {
                isValidAccount = false;
                await Shell.Current.DisplayAlert("Error", "Invalid username or password", "OK");
                return false;
            }
            // Cap nhat nhan vien vao db
            if (isNumeric && isValidSalary && existing && isValidAccount)
            {
                if (role == "Cashier" && username != null && password != null)
                {
                    Staff staff = new Staff(username, name, phone, role, salary);
                    await staffService.UpdateStaffAsync(id, staff);

                    await accountService.AddAccountAsync(new Account(username, password, role));
                }
                if (role != "Cashier" && username != null)
                {
                    Staff staff = new Staff("", name, phone, role, salary);
                    await staffService.UpdateStaffAsync(id, staff);
                    var listAcc = await accountService.GetAccountsAsync();
                    foreach (var item in listAcc)
                    {
                        if (item.Username == username)
                        {
                            await accountService.DeleteAccountAsync(item.Id);
                            break;
                        }
                    }
                }
            }
            return true;
        }
        // XOA NHAN VIEN
        public async Task DeleteStaff(int id)
        {
            bool result = await staffService.DeleteStaffAsync(id);
            if (result)
            {
                var staff = Staffs.FirstOrDefault(s => s.Id == id);
                if (staff != null)
                {
                    Staffs.Remove(staff);
                    Count--;
                }
            }
        }
        // TIM KIEM NHAN VIEN
        public async Task SearchStaff(string name)
        {
            List<Staff> listStaff = await staffService.GetStaffsAsync();
            Staffs.Clear();
            Count = 0;
            foreach (var item in listStaff)
            {
                if (item.Name.ToLower().Contains(name.ToLower()))
                {
                    Staffs.Add(item);
                    Count++;
                }
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
