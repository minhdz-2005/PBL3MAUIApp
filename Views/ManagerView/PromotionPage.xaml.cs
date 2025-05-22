using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;

public partial class PromotionPage : ContentPage
{
    private double _lastScale = -1;
    /// ///////////////

    public PromotionPage()
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


    // Sự kiện khi nhấn nút "Thêm" (chỉ hiển thị thông báo, không lưu dữ liệu)
    private void AddButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Nút Thêm đã được nhấn (không lưu dữ liệu)!", "OK");
    }

    // Sự kiện khi nhấn các nút bộ lọc (chỉ thay đổi màu sắc)
    private void FilterButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        // Reset màu sắc
        CompletedFilterButton.BackgroundColor = Colors.LightGray;
        OngoingFilterButton.BackgroundColor = Colors.LightGray;
        UpcomingFilterButton.BackgroundColor = Colors.LightGray;

        // Đặt màu cho nút được chọn
        button.BackgroundColor = Color.FromArgb("#DDA0DD");
    }

    // Sự kiện khi tap vào một Frame ưu đãi


    // Sự kiện khi nhấn nút "Lưu" (chỉ hiển thị thông báo)
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Nút Lưu đã được nhấn (không cập nhật dữ liệu)!", "OK");
    }

    // Sự kiện khi nhấn nút "Xóa" (chỉ hiển thị thông báo và reset)
    private void DeleteButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Nút Xóa đã được nhấn (không xóa dữ liệu)!", "OK");
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
