using System.Globalization;

namespace PBL3MAUIApp.Views.CashierView;

public partial class Templates : ResourceDictionary
{
    public Templates()
    {
        InitializeComponent();
    }

    private async void OnHomeTapped(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//MainPageCashier", animate: false);

    private async void OnOrderTapped(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//OrderPage", animate: false);

    private async void OnOrderListTapped(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//DSOrderPage", animate: false);

    private async void OnUuDaiTapped(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//UuDaiPage", animate: false);

    private async void OnDoanhThuTapped(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//DoanhThuPage", animate: false);

    private async void OnAccountTapped(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//AccountCashierPage", animate: false);
}
