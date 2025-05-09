using PBL3MAUIApp.ViewModels.CashierViewModels;

namespace PBL3MAUIApp.Views.CashierView;

public partial class UuDaiPage : ContentPage
{
    public CashierViewModel? mainViewModel;
    public UuDaiPage()
	{
		InitializeComponent();

        mainViewModel = BindingContext as CashierViewModel;

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
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (mainViewModel != null) await mainViewModel.VoucherVM.GetAllVouchers();
    }
}