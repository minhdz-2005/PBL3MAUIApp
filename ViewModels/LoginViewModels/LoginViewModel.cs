using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

using PBL3MAUIApp.Models;
using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels.LoginViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    public static Account LoginAccount = new Account();
    public static int staffID = 0;

    private AccountService accountService = new AccountService();
    private StaffService staffService = new StaffService();
    private ShiftStaffsService shiftStaffsService = new ShiftStaffsService();
    private ShiftService shiftService = new ShiftService();

    public async Task CheckAccount(string username, string password)
    {
        Account account = await accountService.GetAccountByUsername(username);
        if (account != null)
        {
            if (account.Password == password)
            {
                LoginAccount = account;
                Debug.WriteLine($"us: {LoginAccount.Username}, {account.Username}");
                if (account.Role == "Quản lý")
                {
                    await Shell.Current.GoToAsync("//Manager_MainPage");
                    return;
                }
                if (account.Role == "Thu ngân")
                {
                    var staffs = await staffService.GetStaffsAsync();
                    if (staffs != null)
                    {
                        foreach (var st in staffs)
                        {
                            if (username == st.Username)
                            {
                                staffID = st.Id;

                                // KIEM TRA NHAN VIEN CO TRONG CA HIEN TAI HAY KHONG
                                var shifts = await shiftService.GetShiftsAsync();
                                if (shifts != null)
                                {
                                    //Debug.WriteLine("1");
                                    foreach (var shift in shifts)
                                    {
                                        //Debug.WriteLine("____");
                                        //Debug.WriteLine($"{shift.StartTime} vs {DateTime.Now}, {shift.EndTime} vs {DateTime.Now}");
                                        if (shift.StartTime <= DateTime.Now && shift.EndTime >= DateTime.Now)
                                        {
                                            var shiftStaffs = await shiftStaffsService.GetShiftStaffsByShiftIdAsync(shift.Id);
                                            if (shiftStaffs != null)
                                            {
                                                bool isInShift = shiftStaffs.Any(ss => ss.StaffId == staffID);
                                                if (!isInShift)
                                                {
                                                    // Nhan vien khong co trong ca hien tai
                                                    await Shell.Current.DisplayAlert("Thông báo", "Bạn không có ca làm việc hiện tại.", "OK");
                                                    return;
                                                }
                                                else
                                                {
                                                    await Shell.Current.GoToAsync("//MainPageCashier");
                                                    return;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            await Shell.Current.DisplayAlert("Thông báo", "Bạn không có ca làm việc hiện tại.", "OK");
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Lỗi", "Tài khoản hoặc mật khẩu không đúng", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Lỗi", "Tài khoản hoặc mật khẩu không đúng", "OK");
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

