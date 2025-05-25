using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;
using PBL3MAUIApp.ViewModels.CashierViewModels;
using PBL3MAUIApp.Models;
using System.Threading.Tasks;

public partial class RevenuePage : ContentPage
{
    private double _lastScale = -1;
    public CashierViewModel? mainViewModel;
    public RevenuePage()
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
        if (mainViewModel != null)
        {
            await mainViewModel.RevenueVM.CalcTotalAndBestDay();
            await mainViewModel.RevenueVM.CalcRevenue();
        }
    }
    public async void OnFilteringClicked(object sender, EventArgs e)
    {
        DateTime fromDate = FromDatePicker.Date;              // Từ ngày
        DateTime toDate = ToDatePicker.Date;

        // TRUYEN GIA TRI DEN VIEWMODEL
        if (mainViewModel != null)
        {
            await mainViewModel.RevenueVM.ByDayFiltering(fromDate, toDate);
        }
    }

    public void OnAllRevenueClicked(object sender, EventArgs e)
    {
        ProductRevenue.IsVisible = false;
        StaffRevenue.IsVisible = false;
        PromotionUsed.IsVisible = false;
        SummaryRevenue.IsVisible = true;
    }
    public void OnRevenueByStaffClicked(object sender, EventArgs e)
    {
        ProductRevenue.IsVisible = false;
        StaffRevenue.IsVisible = true;
        PromotionUsed.IsVisible = false;
        SummaryRevenue.IsVisible = false;
    }
    public void OnRevenueByProductClicked(object sender, EventArgs e)
    {
        ProductRevenue.IsVisible = true;
        StaffRevenue.IsVisible = false;
        PromotionUsed.IsVisible = false;
        SummaryRevenue.IsVisible = false;
    }
    public void OnPromotionUsedClicked(object sender, EventArgs e)
    {
        ProductRevenue.IsVisible = false;
        StaffRevenue.IsVisible = false;
        PromotionUsed.IsVisible = true;
        SummaryRevenue.IsVisible = false;
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
}