using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;


public partial class ProductPage : ContentPage
{
    public ProductPage()
    {
        InitializeComponent();
        // Hiển thị sản phẩm mặc định khi trang được tải (Cà phê)
        // ShowProducts("Cà phê");
    }

    // Sự kiện khi người dùng chọn một danh mục
    private void OnCategoryTapped(object sender, EventArgs e)
    {
        var label = sender as Label;
        if (label == null) return;

        CategoryCoffee.BackgroundColor = label.Text == "Cà phê" ? Colors.White : Colors.Transparent;
        CategoryMilkTea.BackgroundColor = label.Text == "Trà sữa" ? Colors.White : Colors.Transparent;


    }


    // Sự kiện khi nhấn nút "Tìm"
    private void OnSearchClicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Bạn đã nhấn nút Tìm!", "OK");
    }

    // Sự kiện khi nhấn nút "Thêm sản phẩm"
    private void OnAddProductClicked(object sender, EventArgs e)
    {
        // Hiển thị popup cùng với lớp nền mờ
        PopupOverlay.IsVisible = true;
    }

    // Sự kiện khi nhấn nút "Lưu" trong popup
    private void OnSaveProductClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ProductNameEntry.Text) && !string.IsNullOrWhiteSpace(ProductPriceEntry.Text))
        {
            var newProduct = new Label
            {
                Text = $"{ProductNameEntry.Text} - {ProductPriceEntry.Text} VNĐ",
                FontSize = 16,
                TextColor = Colors.Black
            };
            // ProductList.Children.Add(newProduct);
            PopupOverlay.IsVisible = false;
            ProductNameEntry.Text = string.Empty;
            ProductPriceEntry.Text = string.Empty;
        }
        else
        {
            DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin!", "OK");
        }
    }

    // Sự kiện khi nhấn nút "Hủy" trong popup
    private void OnCancelProductClicked(object sender, EventArgs e)
    {
        // Ẩn popup và lớp nền mờ
        PopupOverlay.IsVisible = false;
        ProductNameEntry.Text = string.Empty;
        ProductPriceEntry.Text = string.Empty;
    }

    private void OnEditProductClicked(object sender, EventArgs e)
    {

    }
}