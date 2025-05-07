
namespace PBL3MAUIApp.Views.ManagerView;

public partial class Templates_Manager : ResourceDictionary
{
    public Templates_Manager()
    {
        InitializeComponent();
    }

    private async void OnHomeClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("//Manager_MainPage", animate:false);
    private async void OnProductClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("ProductPage", animate: false);
    private async void OnStaffClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("StaffPage", animate: false);
    private async void OnShiftClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("ShiftPage", animate: false);
    private async void OnPromotionClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("PromotionPage", animate: false);
    private async void OnOrderClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("OrderPageManager", animate: false);
    private async void OnRevenueClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("RevenuePage", animate: false);
}
