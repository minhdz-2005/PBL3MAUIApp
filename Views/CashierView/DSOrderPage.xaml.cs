using System.Diagnostics;

using PBL3MAUIApp.Models;
using PBL3MAUIApp.ViewModels.CashierViewModels;

namespace PBL3MAUIApp.Views.CashierView;

public partial class DSOrderPage : ContentPage
{
    public CashierViewModel? mainViewModel;
    public DSOrderPage()
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
        if (mainViewModel != null) await mainViewModel.OrderVM.GetAllOrders();
    }
    
    // LOC DON HANG
    public void OnFilteringClicked(object sender, EventArgs e)
    {
        int? orderId = null;
        if (int.TryParse(OrderIdEntry.Text, out int parsedId))
        {
            orderId = parsedId;
        }
        DateTime fromDate = FromDatePicker.Date;              // Từ ngày
        DateTime toDate = ToDatePicker.Date;

        // TRUYEN GIA TRI DEN VIEWMODEL
        if(mainViewModel != null)
        {
            mainViewModel.OrderVM.OrderFiltering(orderId, fromDate, toDate);
        }
    }
    
    // XEM CHI TIET DON HANG
    private async void OnViewDetailClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var order = button?.BindingContext as Order;
        // if(order != null) Debug.WriteLine($"id: {order.Id}");
        if (button != null)
        {
            var grid = button.Parent as Grid;
            if (grid != null)
            {

                DetailPopupOverlay.IsVisible = true;
                if(mainViewModel != null && order != null) await mainViewModel.OrderDetailVM.ViewOrder(order.Id);

            }
        }
    }

    // Sự kiện khi nhấn nút "Đóng" trong popup chi tiết
    private void OnCloseDetailClicked(object sender, EventArgs e)
    {
        DetailPopupOverlay.IsVisible = false;
    }

}