using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;


public partial class ShiftPage : ContentPage
{
    private double _lastScale = -1;
    public ShiftPage()
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
        // DisplayAlert("Thông báo", "Nút Thêm nhân viên cho Ca 1 đã được nhấn!", "OK");
        AddStaffIntoShiftPopup.IsVisible = true;
    }

    // Sự kiện khi nhấn nút "Thêm nhân viên" cho Ca 2
    private void AddEmployeeToShift2_Clicked(object sender, EventArgs e)
    {
        // DisplayAlert("Thông báo", "Nút Thêm nhân viên cho Ca 2 đã được nhấn!", "OK");
        AddStaffIntoShiftPopup.IsVisible = true;

    }

    // Sự kiện khi nhấn nút "Thêm nhân viên" cho Ca 3
    private void AddEmployeeToShift3_Clicked(object sender, EventArgs e)
    {
        // DisplayAlert("Thông báo", "Nút Thêm nhân viên cho Ca 3 đã được nhấn!", "OK");
        AddStaffIntoShiftPopup.IsVisible = true;
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
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        double baseWidth = 400;
        double baseHeight = 800;

        // Tính scale mà không dùng Math.Min hoặc Math.Max
        double widthScale = width / baseWidth;
        double heightScale = height / baseHeight;
        double minScale = widthScale < heightScale ? widthScale : heightScale;
        double scale = minScale > 0.5 ? minScale : 0.5;

        // Kiểm tra sự thay đổi scale mà không dùng Math.Abs
        double scaleDiff = scale - _lastScale;
        double absScaleDiff = scaleDiff < 0 ? -scaleDiff : scaleDiff;
        double horizontalScale = (width / baseWidth) > 0.5 ? (width / baseWidth) : 0.5;

        if (absScaleDiff > 0.01)
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

            // EditShiftPopupOverlay.WidthRequest = scale * 500; // Chiều rộng linh hoạt
            // EditShiftPopupOverlay.HeightRequest = scale * 600; // Chiều cao linh hoạt



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
    // Popup thêm nhân viên vào ca làm và các chức năng có trong đó
    private void OnRoleClicked(object sender, EventArgs e)
    {
        var label = sender as Label;
        if (label == null) return;

        RoleCashier.BackgroundColor = label.Text == "Thu ngân" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
        RoleBarista.BackgroundColor = label.Text == "Pha chế" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
        RoleWaiter.BackgroundColor = label.Text == "Phục vụ" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
    }
    private string _selectedShifttName = string.Empty;

    private void OnCancelEditShiftClicked(object sender, EventArgs e)
    {
        AddStaffIntoShiftPopup.IsVisible = false;
    }

}