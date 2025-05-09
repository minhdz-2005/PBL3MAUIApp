using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;

public partial class OrderPageManager : ContentPage
{
    private double? _lastScale = -1;
    private Frame? _selectedShift; // Lưu ca làm được chọn
    private string? _selectedOrderId; // Lưu mã đơn hàng được chọn
    public OrderPageManager()
    {
        InitializeComponent();
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

    // Sự kiện khi nhấn vào ca làm
    // private void OnShiftClicked(object sender, EventArgs e)
    // {
    //     _selectedShift = sender as Frame;
    //     // Có thể thêm logic để cập nhật chi tiết ca làm ở đây
    // }

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
                var orderIdLabel = grid.Children[0] as Label; // Lấy Label "Mã đơn"
                if (orderIdLabel != null)
                {
                    _selectedOrderId = orderIdLabel.Text.Replace("Mã đơn: ", "");
                    UpdateDetailPopup();
                    DetailPopupOverlay.IsVisible = true;
                }
            }
        }
    }

    // Cập nhật nội dung popup chi tiết
    private void UpdateDetailPopup()
    {
        if (_selectedOrderId == "123456")
        {
            DetailOrderIdLabel.Text = "Mã đơn: 123456";
            DetailAmountLabel.Text = "Số tiền: 50.000 VNĐ";
            DetailTimeLabel.Text = "Thời gian: 10:00";
            DetailStatusLabel.Text = "Trạng thái: Đã thanh toán";
            DetailItemsLabel.Text = "Sản phẩm: Cà phê đen, Trà sữa";
        }
        else if (_selectedOrderId == "123457")
        {
            DetailOrderIdLabel.Text = "Mã đơn: 123457";
            DetailAmountLabel.Text = "Số tiền: 60.000 VNĐ";
            DetailTimeLabel.Text = "Thời gian: 10:30";
            DetailStatusLabel.Text = "Trạng thái: Đã thanh toán";
            DetailItemsLabel.Text = "Sản phẩm: Trà sữa, Bánh mì";
        }
        else if (_selectedOrderId == "123458")
        {
            DetailOrderIdLabel.Text = "Mã đơn: 123458";
            DetailAmountLabel.Text = "Số tiền: 70.000 VNĐ";
            DetailTimeLabel.Text = "Thời gian: 11:00";
            DetailStatusLabel.Text = "Trạng thái: Đã thanh toán";
            DetailItemsLabel.Text = "Sản phẩm: Cà phê sữa, Trà đào";
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

            FilterPopupOverlay.WidthRequest = scale * 500; // Chiều rộng linh hoạt
            FilterPopupOverlay.HeightRequest = scale * 600; // Chiều cao linh hoạt

            DetailPopupOverlay.WidthRequest = scale * 500;
            DetailPopupOverlay.HeightRequest = scale * 600;

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