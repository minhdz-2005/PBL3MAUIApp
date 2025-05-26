using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using PBL3MAUIApp.Models;
using PBL3MAUIApp.Services;
using PBL3MAUIApp.ViewModels.LoginViewModels;

using static Microsoft.Maui.ApplicationModel.Permissions;

namespace PBL3MAUIApp.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private Account account = new Account();
        public Account Account
        {
            get => account;
            set
            {
                if (account != value)
                {
                    account = value;
                    OnPropertyChanged(nameof(Account));
                }
            }
        }

        private Staff staff = new Staff();
        public Staff Staff
        {
            get => staff;
            set
            {
                if (staff != value)
                {
                    staff = value;
                    OnPropertyChanged(nameof(Staff));
                }
            }
        }


        private AccountService accountService = new AccountService();
        private StaffService staffService = new StaffService();
        public async void LoadAccount()
        {
            Account = LoginViewModel.LoginAccount;
            var listStaff = await staffService.GetStaffsAsync();
            foreach (var item in listStaff)
            {
                if (item.Username == Account.Username)
                {
                    Staff = item;
                    break;
                }
            }
            Debug.WriteLine($"Account: {Account.Username}, {Account.Password}");
        }

        // LUU THONG TIN CA NHAN
        public async Task SaveInfo(string name, string phone)
        {
            var listStaff = await staffService.GetStaffsAsync();
            foreach (var item in listStaff)
            {
                if (item.PhoneNumber == phone && item.Username != Account.Username)
                {
                    await Shell.Current.DisplayAlert("Error", "Số điện thoại đã tồn tại !", "OK");
                    return;
                }
                if (item.Username == Account.Username)
                {
                    item.Name = name;
                    item.PhoneNumber = phone;
                    await staffService.UpdateStaffAsync(item.Id, item);
                    Debug.WriteLine($"Name: {item.Name}, Phone: {item.PhoneNumber}");
                    await Shell.Current.DisplayAlert("Success", "Thay dổi thông tin thành công !", "OK");
                    break;
                }
            }
        }
        // LUU MAT KHAU
        public async Task<bool> SavePassword(string oldPassword, string newPassword)
        {
            var account = await accountService.GetAccountByUsername(Account.Username);
            if (account != null)
            {
                if (account.Password == oldPassword)
                {
                    account.Password = newPassword;
                    await accountService.UpdateAccountAsync(account.Id, account);
                    await Shell.Current.DisplayAlert("Success", "Thay đổi mật khẩu thành công !", "OK");
                    return true;
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Sai mật khẩu !", "OK");
                    return false;
                }
            }
            return false;
        }


        // THEM TAI KHOAN (MANAGER)
        public async Task<bool> AddAccount(string username, string password, string role)
        {
            Account account = new Account(username, password, role);
            var result = await accountService.AddAccountAsync(account);
            if (result)
            {
                await Shell.Current.DisplayAlert("Success", "Account added successfully", "OK");
                return true;
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Failed to add account", "OK");
                return false;
            }
        }
        // CHINH SUA TAI KHOAN (MANAGER)
        public async Task UpdateAccount(string username, string password, string role)
        {
            Account account = new Account(username, password, role);
            var list = await accountService.GetAccountsAsync();
            foreach (var item in list)
            {
                if (item.Username == username)
                {
                    account.Id = item.Id;
                    await accountService.UpdateAccountAsync(item.Id, account);
                    break;
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
