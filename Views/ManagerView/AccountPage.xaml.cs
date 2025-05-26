namespace PBL3MAUIApp.Views.ManagerView;
using PBL3MAUIApp.ViewModels;
public partial class AccountPage : ContentPage
{
    private double _lastScale = -1;
    public CashierViewModel? mainViewModel;
    public AccountPage()
    {
        InitializeComponent();
        mainViewModel = BindingContext as CashierViewModel;
        // Resources["IndexToBooleanConverter"] = new IndexToBooleanConverter();
        this.SizeChanged += (s, e) =>
        {
            double width = this.Width;

            double baseWidth = 1440; // chi?u r?ng chu?n thi?t k?
            double scale = this.Width / baseWidth;

            // Clamp ?? không quá nh? ho?c quá to
            // scale = Math.Max(0.5, Math.Min(scale, 1.5));
            if (Application.Current != null)
            {
                Application.Current.Resources["MenuFontSize"] = 20 * scale;
                Application.Current.Resources["MenuItemPadding"] = new Thickness(8 * scale, 4 * scale);
                Application.Current.Resources["MenuItemMargin"] = new Thickness(10 * scale, 0);
                Application.Current.Resources["NavIconSize"] = 30 * scale;
                Application.Current.Resources["NavBoxSize"] = 60 * scale;
            }
        };
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        mainViewModel?.AccountVM.LoadAccount();
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

            // FilterPopupOverlay.WidthRequest = scale * 500; // Chiều rộng linh hoạt
            // FilterPopupOverlay.HeightRequest = scale * 600; // Chiều cao linh hoạt

            // DetailPopup.WidthRequest = scale * 500;
            // DetailPopup.HeightRequest = scale * 600;

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
    private async void OnLogOutClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//LoginView", animate: false);
}
