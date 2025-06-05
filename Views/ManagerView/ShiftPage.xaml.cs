using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;
using PBL3MAUIApp.ViewModels;
using PBL3MAUIApp.Models;
using System.Threading.Tasks;
using System.Diagnostics;

public partial class ShiftPage : ContentPage
{
    private double _lastScale = -1;
    public CashierViewModel? mainViewModel;
    public ShiftPage()
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
            await mainViewModel.ShiftVM.GetAllDay();
        }
    }
    //
    static DateTime selectedDate = new DateTime();
    static int selectedShiftIndex = 0;


    // Sự kiện khi chạm vào tiêu đề Ca 1
    private void ToggleShift1_Tapped(object sender, EventArgs e)
    {
        Shift1Details.IsVisible = !Shift1Details.IsVisible;
        selectedShiftIndex = 1;
    }

    // Sự kiện khi chạm vào tiêu đề Ca 2
    private void ToggleShift2_Tapped(object sender, EventArgs e)
    {
        Shift2Details.IsVisible = !Shift2Details.IsVisible;
        selectedShiftIndex = 2;
    }

    // Sự kiện khi chạm vào tiêu đề Ca 3
    private void ToggleShift3_Tapped(object sender, EventArgs e)
    {
        Shift3Details.IsVisible = !Shift3Details.IsVisible;
        selectedShiftIndex = 3;
    }

    // Sự kiện khi nhấn nút "Thêm nhân viên" cho Ca 1
    private async void AddEmployeeToShift1_Clicked(object sender, EventArgs e)
    {
        selectedDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 12, 0, 0);

        if (selectedDate < DateTime.Now)
        {
            await DisplayAlert("Lỗi", "Không thể thêm nhân viên vào ngày làm đã kết thúc.", "OK");
            return;
        }

        AddStaffIntoShiftPopup.IsVisible = true;

        if(mainViewModel != null)
        {
            selectedShiftIndex = 1;
            await mainViewModel.ShiftVM.ShowStaffShift(selectedDate, selectedShiftIndex);
        }
    }

    // Sự kiện khi nhấn nút "Thêm nhân viên" cho Ca 2
    private async void AddEmployeeToShift2_Clicked(object sender, EventArgs e)
    {
        selectedDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 18, 0, 0);

        if (selectedDate < DateTime.Now)
        {
            await DisplayAlert("Lỗi", "Không thể thêm nhân viên vào ngày làm đã kết thúc.", "OK");
            return;
        }

        AddStaffIntoShiftPopup.IsVisible = true;
        if (mainViewModel != null)
        {
            selectedShiftIndex = 2;
            await mainViewModel.ShiftVM.ShowStaffShift(selectedDate, selectedShiftIndex);
        }
    }

    // Sự kiện khi nhấn nút "Thêm nhân viên" cho Ca 3
    private async void AddEmployeeToShift3_Clicked(object sender, EventArgs e)
    {
        selectedDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 23, 59, 59);

        if (selectedDate < DateTime.Now)
        {
            await DisplayAlert("Lỗi", "Không thể thêm nhân viên vào ngày làm đã kết thúc.", "OK");
            return;
        }

        AddStaffIntoShiftPopup.IsVisible = true;
        if (mainViewModel != null)
        {
            selectedShiftIndex = 3;
            await mainViewModel.ShiftVM.ShowStaffShift(selectedDate, selectedShiftIndex);
        }
    }

    // Sự kiện khi nhấn nút "Xóa" nhân viên
    private async void RemoveEmployee_Clicked(object sender, EventArgs e)
    {
        if (selectedShiftIndex == 1) selectedDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 12, 0, 0);
        else if (selectedShiftIndex == 2) selectedDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 18, 0, 0);
        else if (selectedShiftIndex == 3) selectedDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, 23, 59, 59);
        
        if (selectedDate < DateTime.Now)
        {
            await DisplayAlert("Lỗi", "Không thể xóa nhân viên khỏi ca làm đã kết thúc.", "OK");
            return;
        }

        if (mainViewModel != null)
        {
            var button = sender as Button;
            if (button?.BindingContext is Staff staff)
            {
                Debug.WriteLine($"Removing staff: {staff.Name} from shift {selectedShiftIndex} on {selectedDate}");
                await mainViewModel.ShiftVM.RemoveStaffFromShift(staff);
            }
        }
    }
    // Sự kiện khi nhấn nút "Them" nhân viên
    private async void AddEmployee_Clicked(object sender, EventArgs e)
    {
        if (mainViewModel != null)
        {
            var button = sender as Button;
            if (button?.BindingContext is Staff staff)
            {
                await mainViewModel.ShiftVM.AddStaffToShift(staff);
            }
        }
    }

    // Sự kiện khi chọn ngày
    private async void SelectDate_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (mainViewModel != null && button?.BindingContext is DateTime date)
        {
            selectedDate = date;

            await mainViewModel.ShiftVM.StaffInShift(date);

            // Cập nhật label ngày làm
            SelectedDateLabel.Text = $"Ngày làm: {date:dd/MM/yyyy}";
        }
    }

    // Sự kiện khi nhấn nút "Thêm ngày làm"
    private async void AddDate_Clicked(object sender, EventArgs e)
    {
        if (mainViewModel != null)
        {
            await mainViewModel.ShiftVM.AddDayShift();
        }
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
            //Resources["DynamicCornerRadius"] = new CornerRadius(cornerRadius);

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
    private async void OnRoleClicked(object sender, EventArgs e)
    {
        var label = sender as Label;
        if (label == null) return;

        RoleAll.BackgroundColor = label.Text == "Tất cả" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
        RoleCashier.BackgroundColor = label.Text == "Thu ngân" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
        RoleBarista.BackgroundColor = label.Text == "Pha chế" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
        RoleWaiter.BackgroundColor = label.Text == "Phục vụ" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");

        if (mainViewModel != null)
        {
            await mainViewModel.ShiftVM.FilterStaffByRole(label.Text);
        }
    }

    private void OnCancelEditShiftClicked(object sender, EventArgs e)
    {
        AddStaffIntoShiftPopup.IsVisible = false;
    }

}