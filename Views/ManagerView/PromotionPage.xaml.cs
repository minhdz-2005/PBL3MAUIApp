using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;

public partial class PromotionPage : ContentPage
{
    public PromotionPage()
    {
        InitializeComponent();

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
}
