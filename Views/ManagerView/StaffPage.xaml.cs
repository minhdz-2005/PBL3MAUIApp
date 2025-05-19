using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;


public partial class StaffPage : ContentPage
{
    private Frame? _currentEditingFrame;
    private double _lastScale = -1;
    public StaffPage()
    {
        InitializeComponent();
    }

    // Sự kiện khi người dùng chọn một vai trò (giữ lại để tương thích nếu cần sau này)
    private void OnRoleClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null) return;

        RoleAll.BackgroundColor = button.Text == "Tất cả" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
        RoleCashier.BackgroundColor = button.Text == "Thu ngân" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
        RoleBartender.BackgroundColor = button.Text == "Pha chế" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
        RoleWaiter.BackgroundColor = button.Text == "Phục vụ" ? Color.FromArgb("#C6E2FF") : Color.FromArgb("#FFE4B5");
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


    // Sự kiện khi nhấn nút "Tìm"
    private void OnSearchClicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Bạn đã nhấn nút Tìm!", "OK");
    }

    // Sự kiện khi nhấn nút "Xoá" (toàn bộ)
    private void OnDeleteClicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Bạn đã nhấn nút Xoá toàn bộ!", "OK");
    }

    // Sự kiện khi nhấn nút "Thêm"
    private void OnAddStaffClicked(object sender, EventArgs e)
    {
        AddStaffPopup.IsVisible = true;
    }

    // Sự kiện khi nhấn nút "Lưu" trong popup
    private void OnSaveAddStaffClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(StaffNameEntry.Text) &&
            !string.IsNullOrWhiteSpace(StaffDOBEntry.Text) &&
            !string.IsNullOrWhiteSpace(StaffPhoneEntry.Text) &&
            !string.IsNullOrWhiteSpace(StaffAddressEntry.Text) &&
            !string.IsNullOrWhiteSpace(AddStaffRoleLabel.Text))
        {
            var frame = new Frame
            {
                Margin = new Thickness(0, 0, 0, 5),
                Padding = 10,
                BackgroundColor = Color.FromArgb("#F8E0E0"),
                BorderColor = Colors.Transparent,
                CornerRadius = 5
            };

            var grid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = GridLength.Auto }
                },
                ColumnSpacing = 10
            };

            // Thêm Label cho Họ tên
            var nameLabel = new Label
            {
                Text = StaffNameEntry.Text,
                FontSize = 16,
                TextColor = Colors.Black,
                VerticalOptions = LayoutOptions.Center
            };
            grid.Children.Add(nameLabel);
            Grid.SetColumn(nameLabel, 0);

            // Thêm Label cho Ngày sinh
            var dobLabel = new Label
            {
                Text = StaffDOBEntry.Text,
                FontSize = 16,
                TextColor = Colors.Black,
                VerticalOptions = LayoutOptions.Center
            };
            grid.Children.Add(dobLabel);
            Grid.SetColumn(dobLabel, 1);

            // Thêm Label cho Số điện thoại
            var phoneLabel = new Label
            {
                Text = StaffPhoneEntry.Text,
                FontSize = 16,
                TextColor = Colors.Black,
                VerticalOptions = LayoutOptions.Center
            };
            grid.Children.Add(phoneLabel);
            Grid.SetColumn(phoneLabel, 2);

            // Thêm Label cho Vị trí
            var roleLabel = new Label
            {
                Text = AddStaffRoleLabel.Text,
                FontSize = 16,
                TextColor = Colors.Black,
                VerticalOptions = LayoutOptions.Center
            };
            grid.Children.Add(roleLabel);
            Grid.SetColumn(roleLabel, 3);

            // Thêm nút Xóa
            var deleteButton = new Button
            {
                Padding = 5,
                BackgroundColor = Colors.Red,
                CornerRadius = 5,
                FontSize = 14,
                Text = "Xóa",
                TextColor = Colors.White
            };
            deleteButton.Clicked += RemoveEmployee_Clicked;
            grid.Children.Add(deleteButton);
            Grid.SetColumn(deleteButton, 4);

            frame.Content = grid;
            StaffList.Children.Add(frame);

            AddStaffPopup.IsVisible = false;
            StaffNameEntry.Text = string.Empty;
            StaffDOBEntry.Text = string.Empty;
            StaffPhoneEntry.Text = string.Empty;
            StaffAddressEntry.Text = string.Empty;
            AddStaffRoleLabel.Text = string.Empty;
        }
        else
        {
            DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin!", "OK");
        }
    }

    // Sự kiện khi nhấn nút "Hủy" trong popup
    private void OnCancelAddStaffClicked(object sender, EventArgs e)
    {
        AddStaffPopup.IsVisible = false;
        StaffNameEntry.Text = string.Empty;
        StaffDOBEntry.Text = string.Empty;
        StaffPhoneEntry.Text = string.Empty;
        StaffAddressEntry.Text = string.Empty;
        AddStaffRoleLabel.Text = string.Empty;
    }

    // thay doi thong tin nhan vien
    private void OnEditStaffClicked(object sender, EventArgs e)
    {
        // Vì danh sách là tĩnh, cần chọn nhân viên để chỉnh sửa
        // Ở đây tôi sẽ giả định chọn nhân viên đầu tiên để minh họa
        if (StaffList.Children.Count > 0)
        {
            // Tìm Frame đầu tiên chứa nhân viên
            foreach (var child in StaffList.Children)
            {
                if (child is Frame frame && frame.Content is Grid grid)
                {
                    _currentEditingFrame = frame;
                    var nameLabel = grid.Children[0] as Label;
                    var dobLabel = grid.Children[1] as Label;
                    var phoneLabel = grid.Children[2] as Label;
                    var roleLabel = grid.Children[3] as Label;

                    EditStaffNameEntry.Text = nameLabel?.Text;
                    EditStaffDOBEntry.Text = dobLabel?.Text;
                    EditStaffPhoneEntry.Text = phoneLabel?.Text;
                    EditStaffRoleLabel.Text = roleLabel?.Text;
                    EditStaffAddressEntry.Text = "Địa chỉ mặc định"; // Vì không có địa chỉ trong danh sách tĩnh

                    EditStaffPopup.IsVisible = true;
                    break;
                }
            }
        }
        else
        {
            DisplayAlert("Thông báo", "Không có nhân viên để chỉnh sửa!", "OK");
        }
    }
    private void OnSaveEditStaffClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(EditStaffNameEntry.Text) &&
            !string.IsNullOrWhiteSpace(EditStaffDOBEntry.Text) &&
            !string.IsNullOrWhiteSpace(EditStaffPhoneEntry.Text) &&
            !string.IsNullOrWhiteSpace(EditStaffAddressEntry.Text) &&
            !string.IsNullOrWhiteSpace(EditStaffRoleLabel.Text))
        {
            if (_currentEditingFrame != null && _currentEditingFrame.Content is Grid grid)
            {
                var nameLabel = grid.Children[0] as Label;
                var dobLabel = grid.Children[1] as Label;
                var phoneLabel = grid.Children[2] as Label;
                var roleLabel = grid.Children[3] as Label;

                if (nameLabel != null) nameLabel.Text = EditStaffNameEntry.Text;
                if (dobLabel != null) dobLabel.Text = EditStaffDOBEntry.Text;
                if (phoneLabel != null) phoneLabel.Text = EditStaffPhoneEntry.Text;
                if (roleLabel != null) roleLabel.Text = EditStaffRoleLabel.Text;

                EditStaffPopup.IsVisible = false;
                EditStaffNameEntry.Text = string.Empty;
                EditStaffDOBEntry.Text = string.Empty;
                EditStaffPhoneEntry.Text = string.Empty;
                EditStaffAddressEntry.Text = string.Empty;
                EditStaffRoleLabel.Text = string.Empty;
                _currentEditingFrame = null;
            }
        }
        else
        {
            DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin!", "OK");
        }
    }

    // Sự kiện khi nhấn nút "Hủy" trong popup chỉnh sửa nhân viên
    private void OnCancelEditStaffClicked(object sender, EventArgs e)
    {
        EditStaffPopup.IsVisible = false;
        EditStaffNameEntry.Text = string.Empty;
        EditStaffDOBEntry.Text = string.Empty;
        EditStaffPhoneEntry.Text = string.Empty;
        EditStaffAddressEntry.Text = string.Empty;
        EditStaffRoleLabel.Text = string.Empty;
        _currentEditingFrame = null;
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
    //Popup Nhóm nhân viên và các thao tác trong đó
    private void OnStaffGroupLabelTapped(object sender, EventArgs e)
    {
        _isStaffGroupOptionsVisible = true;
        StaffGroupOptions.IsVisible = _isStaffGroupOptionsVisible;
    }

    private void OnCashierOptionSelected(object sender, EventArgs e)
    {
        EditStaffRoleLabel.Text = "Thu ngân";
        _isStaffGroupOptionsVisible = false;
        StaffGroupOptions.IsVisible = _isStaffGroupOptionsVisible;
    }

    private void OnBaristaOptionSelected(object sender, EventArgs e)
    {
        EditStaffRoleLabel.Text = "Pha chế";
        _isStaffGroupOptionsVisible = false;
        StaffGroupOptions.IsVisible = _isStaffGroupOptionsVisible;
    }

    private void OnWaiterOptionSelected(object sender, EventArgs e)
    {
        EditStaffRoleLabel.Text = "Phục vụ";
        _isStaffGroupOptionsVisible = false;
        StaffGroupOptions.IsVisible = _isStaffGroupOptionsVisible;
    }
    private void OnCoffeeOptionClicked(object sender, EventArgs e) { }
    private void OnTeaOptionClicked(object sender, EventArgs e) { }
    private void OnPastryOptionClicked(object sender, EventArgs e) { }

}