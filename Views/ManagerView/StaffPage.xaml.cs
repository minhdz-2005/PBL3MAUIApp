using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;
using PBL3MAUIApp.ViewModels;
using PBL3MAUIApp.Models;
using System.Threading.Tasks;
using System.Diagnostics;

public partial class StaffPage : ContentPage
{
    private double _lastScale = -1;

    public CashierViewModel? mainViewModel;
    public StaffPage()
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
        if(mainViewModel != null)
        {
            await mainViewModel.StaffVM.GetAllStaff();
        }
    }
    static int idStaff = 0;
    // Sự kiện khi người dùng chọn một vai trò (giữ lại để tương thích nếu cần sau này) x
    private async void OnRoleClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var role = button?.Text;
        Debug.WriteLine($"Selected role: {role}");

        if (mainViewModel != null && role != null)
        {
            if (role == "All")
            {
                await mainViewModel.StaffVM.GetAllStaff();
            }
            else
            {
                await mainViewModel.StaffVM.GetStaffByRole(role);
            }
        }
    }

    // Sự kiện khi nhấn nút "Xóa" trên mỗi hàng nhân viên
    private void RemoveEmployee_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        // Tìm Frame chứa hàng nhân viên
        var frame = button.Parent.Parent as Frame;
        if (frame != null)
        {
            StaffList.Children.Remove(frame);
            DisplayAlert("Success", "Staff deleted successfully!", "OK");
        }
    }


    // Sự kiện khi nhấn nút "Tìm" x
    private async void OnSearchClicked(object sender, EventArgs e)
    {
        string name = SearchEntry.Text;
        if (mainViewModel != null)
        {
            await mainViewModel.StaffVM.SearchStaff(name);
        }
    }


    // Sự kiện khi nhấn nút "Thêm" x
    private void OnAddStaffClicked(object sender, EventArgs e)
    {
        AddStaffPopup.IsVisible = true;

        UsernameEntry.IsEnabled = false;
        PasswordEntry.IsEnabled = false;

        // Thiết lập các trường trong popup thêm nhân viên
        StaffUsernameEntry.Text = string.Empty;
        StaffPasswordEntry.Text = string.Empty;
        StaffNameEntry.Text = string.Empty;
        StaffPhoneEntry.Text = string.Empty;
        StaffSalaryEntry.Text = string.Empty;
    }

    // Sự kiện khi nhấn nút "Lưu" trong popup x
    private async void OnSaveAddStaffClicked(object sender, EventArgs e)
    {
        string username = StaffUsernameEntry.Text;
        string password = StaffPasswordEntry.Text;
        string staffName = StaffNameEntry.Text;
        string staffPhone = StaffPhoneEntry.Text;
        string staffSalary = StaffSalaryEntry.Text;
        string staffRole = AddStaffRoleLabel.Text;

        if (staffRole != "Cashier")
        {
            Debug.WriteLine("Staff role is not Cashier, disabling username and password fields.");
            username = string.Empty; // Disable username for non-Cashier roles
            password = string.Empty; // Disable password for non-Cashier roles
        }

        if (mainViewModel != null)
        {
            bool added = await mainViewModel.StaffVM.AddStaff(username, password, staffName, staffPhone, staffRole, staffSalary);
            if (added)
            {
                AddStaffPopup.IsVisible = false;
            }
        }
    }

    // Sự kiện khi nhấn nút "Hủy" trong popup x
    private void OnCancelAddStaffClicked(object sender, EventArgs e)
    {
        AddStaffPopup.IsVisible = false;
        StaffUsernameEntry.Text = string.Empty;
        StaffPasswordEntry.Text = string.Empty;
        StaffNameEntry.Text = string.Empty;
        StaffPhoneEntry.Text = string.Empty;
        StaffSalaryEntry.Text = string.Empty;
        AddStaffRoleLabel.Text = "Role (Cashier/Barista/Waiter)";
    }

    // thay doi thong tin nhan vien x
    private void OnEditStaffClicked(object sender, EventArgs e)
    {
        EditStaffPopup.IsVisible = true;

        var button = sender as Button;
        var staff = button?.BindingContext as Staff;
        if (staff != null)
        {
            idStaff = staff.Id;
            EditStaffUsernameEntry.Text = staff.Username;
            if (staff.Role == "Cashier")
            {
                EditUsernameEntry.IsEnabled = true;
                EditPasswordEntry.IsEnabled = true;
            }
            EditStaffUsernameEntry.Text = staff.Username;
            EditStaffPasswordEntry.Text = string.Empty;

            EditStaffNameEntry.Text = staff.Name;
            EditStaffPhoneEntry.Text = staff.PhoneNumber;
            EditStaffSalaryEntry.Text = staff.Salary.ToString();
            EditStaffRoleLabel.Text = staff.Role;

        }
    }
    private async void OnSaveEditStaffClicked(object sender, EventArgs e)
    {
        string username = EditStaffUsernameEntry.Text;
        string password = EditStaffPasswordEntry.Text;
        string staffName = EditStaffNameEntry.Text;
        string staffPhone = EditStaffPhoneEntry.Text;
        string staffSalary = EditStaffSalaryEntry.Text;
        string staffRole = EditStaffRoleLabel.Text;

        if (mainViewModel != null)
        {
            bool edited = await mainViewModel.StaffVM.UpdateStaff(username, password, staffName, staffPhone, staffRole, staffSalary, idStaff);
            if (edited)
            {
                EditStaffPopup.IsVisible = false;
            }

        }
    }

    // Sự kiện khi nhấn nút "Hủy" trong popup chỉnh sửa nhân viên x
    private void OnCancelEditStaffClicked(object sender, EventArgs e)
    {
        EditStaffPopup.IsVisible = false;
    }
    
    // BAM VAO NUT XOA NHAN VIEN
    private async void OnDeleteStaffClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var staff = button?.BindingContext as Staff;
        if (staff != null)
        {
            if (mainViewModel != null)
            {
                await mainViewModel.StaffVM.DeleteStaff(staff.Id);
                await DisplayAlert("Thông báo", "Nhân viên đã được xóa!", "OK");
            }
        }
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
            // Resources["DynamicCornerRadius"] = new CornerRadius(cornerRadius);

            AddStaffPopupLayout.WidthRequest = scale * 500; // Chiều rộng linh hoạt
            AddStaffPopupLayout.HeightRequest = scale * 600; // Chiều cao linh hoạt

            EditStaffPopupLayout.WidthRequest = scale * 500;
            EditStaffPopupLayout.HeightRequest = scale * 600;

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
    
    private bool _isStaffGroupOptionsVisible = false;
    //Popup Nhóm nhân viên và các thao tác trong đó x
    private void OnStaffGroupLabelTapped(object sender, EventArgs e)
    {
        _isStaffGroupOptionsVisible = true;
        StaffGroupOptions.IsVisible = _isStaffGroupOptionsVisible;
    }

    private void OnCashierOptionSelected(object sender, EventArgs e)
    {
        AddStaffRoleLabel.Text = "Cashier";
        EditStaffRoleLabel.Text = "Cashier";

        UsernameEntry.IsEnabled = true;
        PasswordEntry.IsEnabled = true;

        EditUsernameEntry.IsEnabled = true;
        EditPasswordEntry.IsEnabled = true;

        _isStaffGroupOptionsVisible = false;
        StaffGroupOptions.IsVisible = _isStaffGroupOptionsVisible;
    }

    private void OnBaristaOptionSelected(object sender, EventArgs e)
    {
        AddStaffRoleLabel.Text = "Barista";
        EditStaffRoleLabel.Text = "Barista";

        UsernameEntry.IsEnabled = false;
        PasswordEntry.IsEnabled = false;

        EditUsernameEntry.IsEnabled = false;
        EditPasswordEntry.IsEnabled = false;


        _isStaffGroupOptionsVisible = false;
        StaffGroupOptions.IsVisible = _isStaffGroupOptionsVisible;
    }

    private void OnWaiterOptionSelected(object sender, EventArgs e)
    {
        AddStaffRoleLabel.Text = "Waiter";
        EditStaffRoleLabel.Text = "Waiter";

        UsernameEntry.IsEnabled = false;
        PasswordEntry.IsEnabled = false;

        EditUsernameEntry.IsEnabled = false;
        EditPasswordEntry.IsEnabled = false;


        _isStaffGroupOptionsVisible = false;
        StaffGroupOptions.IsVisible = _isStaffGroupOptionsVisible;
    }

}