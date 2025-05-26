using System.Diagnostics;
using System.Threading.Tasks;

using PBL3MAUIApp.ViewModels;
using PBL3MAUIApp.ViewModels.LoginViewModels;

namespace PBL3MAUIApp.Views.CashierView;

public partial class AccountCashierPage : ContentPage
{
    private double _lastScale = -1;
    public CashierViewModel? mainViewModel;
    public AccountCashierPage()
    {
        InitializeComponent();

        mainViewModel = BindingContext as CashierViewModel;
        // Resources["IndexToBooleanConverter"] = new IndexToBooleanConverter();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        double baseWidth = 400;
        double baseHeight = 800;

        double widthScale = width / baseWidth;
        double heightScale = height / baseHeight;
        double scale = widthScale < heightScale ? widthScale : heightScale;

        scale = scale > 0.5 ? scale : 0.5;
        double horizontalScale = (width / baseWidth) > 0.5 ? (width / baseWidth) : 0.5;


        if (_lastScale < 0 || (scale > _lastScale + 0.01 || scale < _lastScale - 0.01))
        {
            Resources["DynamicFontSizeTitle"] = 30 * scale;
            Resources["DynamicFontSizeLarge"] = 20 * scale;
            Resources["DynamicFontSizeMedium"] = 16 * scale;
            Resources["DynamicFontSizeSmall"] = 12 * scale;
            Resources["DynamicPadding"] = 8 * scale;
            Resources["DynamicMargin"] = 5 * scale;
            Resources["DynamicSpacing"] = 10 * scale;
            Resources["DynamicButtonSize"] = 40 * scale;
            Resources["DynamicBorderThickness"] = 1 * scale;

            double cornerRadius = 10 * scale;
            Resources["DynamicCornerRadius"] = new CornerRadius(cornerRadius);

            Resources["NaviHeightRequest"] = 60 * scale;
            Resources["TabMenuHeightRequest"] = 25 * scale;
            Resources["TabMenuWidthRequest"] = 25 * scale;
            Resources["NaviTextFontSize"] = 25 * scale;
            Resources["NaviItemSpacing"] = 2 * horizontalScale;
            Resources["NaviMargin"] = 2 * horizontalScale; // Điều chỉnh Margin theo chiều ngang
            Resources["NaviPadding"] = 5 * horizontalScale;

            _lastScale = scale;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        mainViewModel?.AccountVM.LoadAccount();
    }
    private void OnChangePasswordClicked(object Sender, EventArgs e)
    {
        Information.IsVisible = false;
        ChangePassword.IsVisible = true;
    }
    private void OnInformationClicked(object Sender, EventArgs e)
    {
        Information.IsVisible = true;
        ChangePassword.IsVisible = false;
    }

    // BAM VAO NUT LUU THONG TIN
    private void OnSaveInforClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        string phone = PhoneNumberEntry.Text;

        // Debug.WriteLine($"Name: {name}, Phone: {phone}");

        mainViewModel?.AccountVM.SaveInfo(name, phone);
    }
    // BAM VAO NUT LUU MAT KHAU
    private async void OnSavePasswordClicked(object sender, EventArgs e)
    {
        string oldPassword = OldPassEntry.Text;
        string newPassword = NewPassEntry.Text;
        string rePassword = RePassEntry.Text;

        if (newPassword != rePassword)
        {
            await DisplayAlert("Error", "Mật khẩu mới không khớp nhau !", "OK");
            return;
        }
        else
        {
            if (mainViewModel != null)
            {
                bool isUpdated = await mainViewModel.AccountVM.SavePassword(oldPassword, newPassword);
                if (isUpdated)
                {
                    OldPassEntry.Text = string.Empty;
                    NewPassEntry.Text = string.Empty;
                    RePassEntry.Text = string.Empty;
                }
            }
        }
    }
    // BAM VAO NUT DANG XUAT
    private async void OnLogOutClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//LoginView", animate: false);

}
