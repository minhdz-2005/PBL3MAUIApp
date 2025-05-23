using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;
using PBL3MAUIApp.ViewModels.CashierViewModels;
using PBL3MAUIApp.Models;
using System.Threading.Tasks;
using System.Diagnostics;

public partial class StaffPage : ContentPage
{
    private Frame? _currentEditingFrame;
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
            if (role == "Tất cả")
            {
                await mainViewModel.StaffVM.GetAllStaff();
            }
            else
            {
                await mainViewModel.StaffVM.GetStaffByRole(role);
            }
        }
        //RoleAll.BackgroundColor = button.Text == "Tất cả" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
        //RoleCashier.BackgroundColor = button.Text == "Thu ngân" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
        //RoleBartender.BackgroundColor = button.Text == "Pha chế" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
        //RoleWaiter.BackgroundColor = button.Text == "Phục vụ" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
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
            DisplayAlert("Thông báo", "Nhân viên đã được xóa!", "OK");
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


        // await DisplayAlert("Thông báo", "Bạn đã nhấn nút Tìm!", "OK");
    }

    // Sự kiện khi nhấn nút "Xoá" (toàn bộ)
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Bạn đã nhấn nút Xoá toàn bộ!", "OK");
    }

    // Sự kiện khi nhấn nút "Thêm" x
    private void OnAddStaffClicked(object sender, EventArgs e)
    {
        AddStaffPopup.IsVisible = true;

        UsernameEntry.IsEnabled = false;
        PasswordEntry.IsEnabled = false;
    }

    // Sự kiện khi nhấn nút "Lưu" trong popup x
    private async void OnSaveAddStaffClicked(object sender, EventArgs e)
    {
        string username = StaffUsernameEntry.Text;
        string password = StaffPasswordEntry.Text;
        string staffName = StaffNameEntry.Text;
        string staffPhone = StaffPhoneEntry.Text;

        string staffRole = AddStaffRoleLabel.Text;

        decimal staffSalary;
        bool isSaValid = decimal.TryParse(StaffSalaryEntry.Text, out staffSalary);
        int phone;
        bool isPhoneValid = int.TryParse(StaffPhoneEntry.Text, out phone);

        if (isSaValid && isPhoneValid)
        {
            Debug.WriteLine($"{username}, {password}, {staffName}, {staffPhone}, {staffSalary}, {staffRole}");

            if (mainViewModel != null)
            {
                // await mainViewModel.StaffVM.AddStaff(new Staff(username, staffName, staffPhone, staffRole, staffSalary));
                //if (staffRole == "Thu ngân")
                //    await mainViewModel.AccountVM.AddAccount(username, password, staffRole);
            }
        }
        else
        {
            // Hiển thị lỗi hoặc xử lý khi nhập sai
            await DisplayAlert("Lỗi", "Vui lòng nhập giá hợp lệ.", "OK");
        }
        

        //if (!string.IsNullOrWhiteSpace(StaffNameEntry.Text) &&
        //    !string.IsNullOrWhiteSpace(StaffDOBEntry.Text) &&
        //    !string.IsNullOrWhiteSpace(StaffPhoneEntry.Text) &&
        //    !string.IsNullOrWhiteSpace(StaffAddressEntry.Text) &&
        //    !string.IsNullOrWhiteSpace(AddStaffRoleLabel.Text))
        //{
        //    var frame = new Frame
        //    {
        //        Margin = new Thickness(0, 0, 0, 5),
        //        Padding = 10,
        //        BackgroundColor = Color.FromArgb("#F8E0E0"),
        //        BorderColor = Colors.Transparent,
        //        CornerRadius = 5
        //    };

        //    var grid = new Grid
        //    {
        //        ColumnDefinitions = new ColumnDefinitionCollection
        //        {
        //            new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
        //            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
        //            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
        //            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
        //            new ColumnDefinition { Width = GridLength.Auto }
        //        },
        //        ColumnSpacing = 10
        //    };

        //    // Thêm Label cho Họ tên
        //    var nameLabel = new Label
        //    {
        //        Text = StaffNameEntry.Text,
        //        FontSize = 16,
        //        TextColor = Colors.Black,
        //        VerticalOptions = LayoutOptions.Center
        //    };
        //    grid.Children.Add(nameLabel);
        //    Grid.SetColumn(nameLabel, 0);

        //    // Thêm Label cho Ngày sinh
        //    var dobLabel = new Label
        //    {
        //        Text = StaffDOBEntry.Text,
        //        FontSize = 16,
        //        TextColor = Colors.Black,
        //        VerticalOptions = LayoutOptions.Center
        //    };
        //    grid.Children.Add(dobLabel);
        //    Grid.SetColumn(dobLabel, 1);

        //    // Thêm Label cho Số điện thoại
        //    var phoneLabel = new Label
        //    {
        //        Text = StaffPhoneEntry.Text,
        //        FontSize = 16,
        //        TextColor = Colors.Black,
        //        VerticalOptions = LayoutOptions.Center
        //    };
        //    grid.Children.Add(phoneLabel);
        //    Grid.SetColumn(phoneLabel, 2);

        //    // Thêm Label cho Vị trí
        //    var roleLabel = new Label
        //    {
        //        Text = AddStaffRoleLabel.Text,
        //        FontSize = 16,
        //        TextColor = Colors.Black,
        //        VerticalOptions = LayoutOptions.Center
        //    };
        //    grid.Children.Add(roleLabel);
        //    Grid.SetColumn(roleLabel, 3);

        //    // Thêm nút Xóa
        //    var deleteButton = new Button
        //    {
        //        Padding = 5,
        //        BackgroundColor = Colors.Red,
        //        CornerRadius = 5,
        //        FontSize = 14,
        //        Text = "Xóa",
        //        TextColor = Colors.White
        //    };
        //    deleteButton.Clicked += RemoveEmployee_Clicked;
        //    grid.Children.Add(deleteButton);
        //    Grid.SetColumn(deleteButton, 4);

        //    frame.Content = grid;
        //    StaffList.Children.Add(frame);

        //    AddStaffPopup.IsVisible = false;
        //    StaffNameEntry.Text = string.Empty;
        //    StaffDOBEntry.Text = string.Empty;
        //    StaffPhoneEntry.Text = string.Empty;
        //    StaffAddressEntry.Text = string.Empty;
        //    AddStaffRoleLabel.Text = string.Empty;
        //}
        //else
        //{
        //    DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin!", "OK");
        //}
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
        AddStaffRoleLabel.Text = "Vị trí(Thu ngân / Pha chế / Phục vụ)";
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
            if (staff.Role == "Thu ngân")
            {
                EditUsernameEntry.IsEnabled = true;
                EditPasswordEntry.IsEnabled = true;
            }

            EditStaffNameEntry.Text = staff.Name;
            EditStaffPhoneEntry.Text = staff.PhoneNumber;
            EditStaffSalaryEntry.Text = staff.Salary.ToString();
            EditStaffRoleLabel.Text = staff.Role;

        }
    }
    private async void OnSaveEditStaffClicked(object sender, EventArgs e)
    {
        string username = StaffUsernameEntry.Text;
        string password = StaffPasswordEntry.Text;
        string staffName = StaffNameEntry.Text;
        string staffPhone = StaffPhoneEntry.Text;

        string staffRole = AddStaffRoleLabel.Text;

        decimal staffSalary;
        bool isSaValid = decimal.TryParse(StaffSalaryEntry.Text, out staffSalary);
        int phone;
        bool isPhoneValid = int.TryParse(StaffPhoneEntry.Text, out phone);

        if (isSaValid && isPhoneValid)
        {
            Debug.WriteLine($"{username}, {password}, {staffName}, {staffPhone}, {staffSalary}, {staffRole}");

            if (mainViewModel != null)
            {
                // await mainViewModel.StaffVM.UpdateStaff(idStaff, new Staff(username, staffName, staffPhone, staffRole, staffSalary));
                //if (staffRole == "Thu ngân")
                //    await mainViewModel.AccountVM.UpdateAccount(username, password, staffRole);
            }
        }
        else
        {
            // Hiển thị lỗi hoặc xử lý khi nhập sai
            await DisplayAlert("Lỗi", "Vui lòng nhập giá hợp lệ.", "OK");
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
                // await mainViewModel.StaffVM.DeleteStaff(staff.Id);
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
            Resources["DynamicCornerRadius"] = new CornerRadius(cornerRadius);

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
        AddStaffRoleLabel.Text = "Thu ngân";
        EditStaffRoleLabel.Text = "Thu ngân";

        UsernameEntry.IsEnabled = true;
        PasswordEntry.IsEnabled = true;

        EditUsernameEntry.IsEnabled = true;
        EditPasswordEntry.IsEnabled = true;

        _isStaffGroupOptionsVisible = false;
        StaffGroupOptions.IsVisible = _isStaffGroupOptionsVisible;
    }

    private void OnBaristaOptionSelected(object sender, EventArgs e)
    {
        AddStaffRoleLabel.Text = "Pha chế";
        EditStaffRoleLabel.Text = "Pha chế";

        UsernameEntry.IsEnabled = false;
        PasswordEntry.IsEnabled = false;

        EditUsernameEntry.IsEnabled = false;
        EditPasswordEntry.IsEnabled = false;


        _isStaffGroupOptionsVisible = false;
        StaffGroupOptions.IsVisible = _isStaffGroupOptionsVisible;
    }

    private void OnWaiterOptionSelected(object sender, EventArgs e)
    {
        AddStaffRoleLabel.Text = "Phục vụ";
        EditStaffRoleLabel.Text = "Phục vụ";

        UsernameEntry.IsEnabled = false;
        PasswordEntry.IsEnabled = false;

        EditUsernameEntry.IsEnabled = false;
        EditPasswordEntry.IsEnabled = false;


        _isStaffGroupOptionsVisible = false;
        StaffGroupOptions.IsVisible = _isStaffGroupOptionsVisible;
    }

}