using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;

public partial class OrderPageManager : ContentPage
{
    private double _lastScale = -1;
    private Frame? _selectedShift;
    /// ///////////////

    public OrderPageManager()
    {
        InitializeComponent();
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


    // Sự kiện khi nhấn nút "Lọc"
    private void FilterButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Nút Lọc đã được nhấn!", "OK");
    }

    private void ViewOrderDetail_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Nút Xem chi tiết đã được nhấn!", "OK");
    }
    private void OnShiftClicked(object sender, EventArgs e)
    {

        _selectedShift = sender as Frame;
        FirstShift.BackgroundColor = Color.FromArgb("#FFEFD5");
        SecondShift.BackgroundColor = Color.FromArgb("#FFEFD5");
        ThirdShift.BackgroundColor = Color.FromArgb("#FFEFD5");
        // var frame = sender as Frame;
        if (_selectedShift == FirstShift)
        {
            FirstShift.BackgroundColor = Color.FromArgb("#C6E2FF");
        }
        if (_selectedShift == SecondShift)
        {
            SecondShift.BackgroundColor = Color.FromArgb("#C6E2FF");
        }
        if (_selectedShift == ThirdShift)
        {
            ThirdShift.BackgroundColor = Color.FromArgb("#C6E2FF");
        }
    }



    // Sự kiện khi nhấn nút "Lọc"
    private void OnFilterClicked(object sender, EventArgs e)
    {
        FilterPopupOverlay.IsVisible = true;
    }


    // Sự kiện khi nhấn nút "Áp dụng" trong popup lọc
    private void OnApplyFilterClicked(object sender, EventArgs e)
    {
        var selectedDate = FilterDatePicker.Date.ToString("dd/MM/yyyy");
        // Cập nhật ngày trên giao diện (ví dụ: Label trong Frame header)
        var dateLabel = this.FindByName<Label>("dateLabel"); // Giả định có Label tên "dateLabel"
        if (dateLabel != null)
        {
            dateLabel.Text = selectedDate;
        }
        FilterPopupOverlay.IsVisible = false;
    }

    // Sự kiện khi nhấn nút "Hủy" trong popup lọc
    private void OnCancelFilterClicked(object sender, EventArgs e)
    {
        FilterPopupOverlay.IsVisible = false;
    }

    // Sự kiện khi nhấn nút "Xem chi tiết"
    private void OnViewDetailClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            var grid = button.Parent as Grid;
            if (grid != null)
            {

                DetailPopupOverlay.IsVisible = true;

            }
        }
    }

    // Sự kiện khi nhấn nút "Đóng" trong popup chi tiết
    private void OnCloseDetailClicked(object sender, EventArgs e)
    {
        DetailPopupOverlay.IsVisible = false;
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

            DetailPopup.WidthRequest = scale * 500;
            DetailPopup.HeightRequest = scale * 600;

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