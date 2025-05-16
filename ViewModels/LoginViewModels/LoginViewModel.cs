using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

using PBL3MAUIApp.Models;
using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels.LoginViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Account> Accounts { get; set; } = new();
    public static int staffID = 0;

    private AccountService accountService = new AccountService();
    private StaffService staffService = new StaffService();
    public async Task GetAllAccount()
    {
        List<Account> listAccount = await accountService.GetAccountsAsync();
        Accounts.Clear();
        foreach (var item in listAccount)
            Accounts.Add(item);
    }
    public async Task CheckAccount(string username, string password)
    {
        Account account = await accountService.GetAccountByUsername(username);
        // Debug.WriteLine("Chay 1");
        if (account != null)
        {
            // Debug.WriteLine("Chay 2");
            if (account.Password == password)
            {
                // Debug.WriteLine("Chay 3");
                if (account.Role == "Manager")
                {
                    await Shell.Current.GoToAsync("//Manager_MainPage");
                }
                else
                {
                    var staffs = await staffService.GetStaffsAsync();
                    if (staffs != null)
                    {
                        //Debug.WriteLine("Chay 3");
                        foreach (var st in staffs)
                        {
                            //Debug.WriteLine($"Chay 4 {username}, {st.Username}");
                            if (username == st.Username)
                            {
                                //Debug.WriteLine("Chay 5");
                                staffID = st.Id;
                            }
                        }
                    }
                    await Shell.Current.GoToAsync("//MainPageCashier");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Incorrect username or password", "OK");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Incorrect username or password", "OK");
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;
}

