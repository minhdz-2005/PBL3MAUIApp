using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;


public partial class ShiftPage : ContentPage
{
    public ShiftPage()
    {
        InitializeComponent();
    }

    // Sự kiện khi chạm vào tiêu đề Ca 1
    private void ToggleShift1_Tapped(object sender, EventArgs e)
    {
        Shift1Details.IsVisible = !Shift1Details.IsVisible;
    }

    // Sự kiện khi chạm vào tiêu đề Ca 2
    private void ToggleShift2_Tapped(object sender, EventArgs e)
    {
        Shift2Details.IsVisible = !Shift2Details.IsVisible;
    }

    // Sự kiện khi chạm vào tiêu đề Ca 3
    private void ToggleShift3_Tapped(object sender, EventArgs e)
    {
        Shift3Details.IsVisible = !Shift3Details.IsVisible;
    }

    // Sự kiện khi nhấn nút "Thêm nhân viên" cho Ca 1
    private void AddEmployeeToShift1_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Nút Thêm nhân viên cho Ca 1 đã được nhấn!", "OK");
    }

    // Sự kiện khi nhấn nút "Thêm nhân viên" cho Ca 2
    private void AddEmployeeToShift2_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Nút Thêm nhân viên cho Ca 2 đã được nhấn!", "OK");
    }

    // Sự kiện khi nhấn nút "Thêm nhân viên" cho Ca 3
    private void AddEmployeeToShift3_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Nút Thêm nhân viên cho Ca 3 đã được nhấn!", "OK");
    }

    // Sự kiện khi nhấn nút "Xóa" nhân viên
    private void RemoveEmployee_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Nút Xóa nhân viên đã được nhấn!", "OK");
    }

    // Sự kiện khi chọn ngày
    private void SelectDate_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        // Reset màu sắc của tất cả nút ngày
        DateButton1.BackgroundColor = Color.FromArgb("#CCCCCC");
        DateButton2.BackgroundColor = Color.FromArgb("#CCCCCC");
        DateButton3.BackgroundColor = Color.FromArgb("#CCCCCC");
        DateButton4.BackgroundColor = Color.FromArgb("#CCCCCC");
        DateButton5.BackgroundColor = Color.FromArgb("#CCCCCC");
        DateButton6.BackgroundColor = Color.FromArgb("#CCCCCC");
        DateButton7.BackgroundColor = Color.FromArgb("#CCCCCC");
        DateButton8.BackgroundColor = Color.FromArgb("#CCCCCC");
        DateButton9.BackgroundColor = Color.FromArgb("#CCCCCC");
        DateButton10.BackgroundColor = Color.FromArgb("#CCCCCC");

        // Cập nhật màu nền cho nút ngày được chọn
        button.BackgroundColor = Color.FromArgb("#FF9999");

        // Cập nhật label ngày làm
        SelectedDateLabel.Text = $"Ngày làm: {button.Text}/2025";
    }

    // Sự kiện khi nhấn nút "Thêm ngày làm"
    private void AddDate_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Nút Thêm ngày làm đã được nhấn!", "OK");
    }
}