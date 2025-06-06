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
        private ShiftStaffsService shiftStaffsService = new ShiftStaffsService();
        private OrderService orderService = new OrderService();
        private ShiftService shiftService = new ShiftService();
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
                await Shell.Current.DisplayAlert("Lỗi", "Hãy nhập tên nhân viên !", "OK");
                return false;
            }
            // kiem tra ten nhan vien da ton tai hay chua
            if (Staffs.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Tên nhân viên này đã tồn tại, hãy chọn tên khác hoặc biệt danh", "OK");
                return false;
            }
            // kiem tra sdt
            bool isNumeric = phone.All(char.IsDigit);
            if (!isNumeric && phone.Length <= 10)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Số điện thoại không hợp lệ", "OK");
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
                await Shell.Current.DisplayAlert("Lỗi", "Lương không hợp lệ", "OK");
                return false;
            }
            // kiem tra so dien thoai va ten dang nhap
            if (username != string.Empty) Debug.WriteLine($"Username: -{username}-");
            var list = await staffService.GetStaffsAsync();
            bool existing = true;
            foreach (var item in list)
            {
                if (username != string.Empty)
                {
                    if (item.Username == username)
                    {
                        await Shell.Current.DisplayAlert("Lỗi", "Tài khoản đã tồn tại", "OK");
                        existing = false;
                        return false;
                    }
                    if (item.PhoneNumber == phone)
                    {
                        await Shell.Current.DisplayAlert("Lỗi", "Số điện thoại đã tồn tại", "OK");
                        existing = false;
                        return false;
                    }
                }
            }

            //kiem tra ten dang nhap va mat khau (thu ngan)
            bool isValidAccount = true;
            if ((username == null || password == null) && role == "Thu ngân")
            {
                isValidAccount = false;
                await Shell.Current.DisplayAlert("Lỗi", "Tài khoản hoặc mật khẩu không hợp lệ", "OK");
                return false;
            }

            // Debug.WriteLine($"{isNumeric}, {isValidSalary}, {existing}, {isValidAccount}");

            // Them nhan vien vao db
            if (isNumeric && isValidSalary && existing && isValidAccount)
            {
                if (role == "Thu ngân" && username != null && password != null)
                {
                    Staff staff = new Staff(username, name, phone, role, salary);
                    await staffService.AddStaffAsync(staff);
                    Staffs.Add(staff);
                    await GetAllStaff();
                    Count++;

                    await accountService.AddAccountAsync(new Account(username, password, role));
                }
                if (role != "Thu ngân" && role != "Chức năng (Thu ngân/Pha chế/Phục vụ)")
                {
                    Staff staff = new Staff("", name, phone, role, salary);
                    await staffService.AddStaffAsync(staff);
                    Staffs.Add(staff);
                    await GetAllStaff();
                    Count++;
                }
                if (role == "Chức năng (Thu ngân/Pha chế/Phục vụ)")
                {
                    await Shell.Current.DisplayAlert("Lỗi", "Hãy chọn chức năng cho nhân viên", "OK");
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
                await Shell.Current.DisplayAlert("Lỗi", "Hãy nhập tên nhân viên !", "OK");
                return false;
            }
            // kiem tra ten nhan vien da ton tai hay chua
            if (Staffs.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && s.Id != id))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Tên nhân viên này đã tồn tại, hãy chọn tên khác hoặc biệt danh", "OK");
                return false;
            }

            // kiem tra sdt
            bool isNumeric = phone.All(char.IsDigit);
            if (!isNumeric && phone.Length <= 10)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Số điện thoại không hợp lệ !", "OK");
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
                await Shell.Current.DisplayAlert("Lỗi", "Lương không hợp lệ", "OK");
                return false;
            }
            // kiem tra va ten dang nhap
            var list = await staffService.GetStaffsAsync();
            bool existing = true;
            foreach (var item in list)
            {
                if (item.Username == username && username != null && role == "Thu ngân" && item.Id != id)
                {
                    await Shell.Current.DisplayAlert("Lỗi", "Tài khoản đã tồn tại", "OK");
                    existing = false;
                    return false;
                }
            }

            //kiem tra ten dang nhap va mat khau (thu ngan)
            bool isValidAccount = true;
            if ((username == null || password == null) && role == "Thu ngân")
            {
                isValidAccount = false;
                await Shell.Current.DisplayAlert("Lỗi", "Tài khoản và mật khẩu không hợp lệ", "OK");
                return false;
            }
            // Cap nhat nhan vien vao db
            if (isNumeric && isValidSalary && existing && isValidAccount)
            {
                if (role == "Thu ngân" && username != null && password != null)
                {
                    Staff staff = new Staff(username, name, phone, role, salary);
                    await staffService.UpdateStaffAsync(id, staff);

                    var listAcc = await accountService.GetAccountsAsync();
                    foreach (var item in listAcc)
                    {
                        if (item.Username == username)
                        {
                            await accountService.UpdateAccountAsync(item.Id, new Account(username, password, role));
                            break;
                        }
                    }
                }
                if (role != "Thu ngân" && username != null)
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
            await GetAllStaff();
            return true;
            
        }
        // XOA NHAN VIEN
        public async Task DeleteStaff(int id)
        {
            // Kiem tra nhan vien co trong ca hay khong
            var shiftStaffs = await shiftStaffsService.GetShiftStaffssAsync();
            var shifts = await shiftService.GetShiftsAsync();
           
            foreach (var ss in shiftStaffs)
            {
                foreach (var s in shifts)
                {
                    if (ss.ShiftId == s.Id && s.EndTime < DateTime.Now && ss.StaffId == id)
                    {
                        await Shell.Current.DisplayAlert("Lỗi", "Nhân viên đã từng làm việc trong quán, không thể xóa !", "OK");
                        return;
                    }
                }
            }
            
            // Kiem tra nhan vien co trong don hang hay khong
            var orders = await orderService.GetOrdersAsync();
            if (orders.Any(o => o.StaffId == id))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Nhân viên đã phục vụ đơn hàng, không thể xóa !", "OK");
                return;
            }

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
