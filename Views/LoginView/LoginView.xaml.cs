using PBL3MAUIApp.ViewModels.LoginViewModels;

namespace PBL3MAUIApp.Views.LoginView;

public partial class LoginView : ContentPage
{
    private LoginViewModel? loginViewModel;
    public LoginView()
    {
        InitializeComponent();

        loginViewModel = BindingContext as LoginViewModel;

        this.SizeChanged += (s, e) =>
        {
            double height = this.Height;

            LoginLabel.FontSize = height / 20;

            AccountLabel.FontSize = height / 40;
            AccountEntry.FontSize = height / 40;

            AccountBorder.HeightRequest = height / 15;

            PasswordLabel.FontSize = height / 40;
            PasswordEntry.FontSize = height / 40;

            PasswordBorder.HeightRequest = height / 15;


            LoginButton.Margin = new Thickness(0, height / 20, 0, 0);
            LoginButton.FontSize = height / 40;
            LoginButton.HeightRequest = height / 15;
            LoginButton.WidthRequest = height / 4;


        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AccountEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
        PasswordEntry.IsPassword = true;
        PasswordEntry.Focus();
    }
    private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
    {
        PasswordEntry.IsPassword = true;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string account = AccountEntry.Text;
        string password = PasswordEntry.Text;
        if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Please enter your account and password.", "OK");
            return;
        }

        PasswordEntry.Unfocus();
        string passwordCopy = password;
        AccountEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
        PasswordEntry.IsPassword = true;

        if (loginViewModel != null) await loginViewModel.CheckAccount(account, passwordCopy);

    }
}
