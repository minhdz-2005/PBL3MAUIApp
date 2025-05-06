using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.CashierView;

public partial class MainPageCashier : ContentPage
{

    public MainPageCashier()
    {
        InitializeComponent();

        this.SizeChanged += (s, e) =>
        {
            double width = this.Width;
            MyLabel.FontSize = width / 25;

            double baseWidth = 1440; // chiều rộng chuẩn thiết kế
            double scale = this.Width / baseWidth;

            // Clamp để không quá nhỏ hoặc quá to
            // scale = Math.Max(0.5, Math.Min(scale, 1.5));
            if(Application.Current != null)
            {
                Application.Current.Resources["MenuFontSize"] = 20 * scale;
                Application.Current.Resources["MenuItemPadding"] = new Thickness(8 * scale, 4 * scale);
                Application.Current.Resources["MenuItemMargin"] = new Thickness(10 * scale, 0);
                Application.Current.Resources["NavIconSize"] = 30 * scale;
                Application.Current.Resources["NavBoxSize"] = 60 * scale;
            }
        };
    }
}
