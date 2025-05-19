using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;


public partial class ProductPage : ContentPage
{
    private double _lastScale = -1;
    private string _selectedProductName = string.Empty;



    public ProductPage()
    {
        InitializeComponent();

    }

    /////////////
    private void OnCategoryTapped(object sender, EventArgs e)
    {
        if (sender is not Label label) return;

        CategoryCoffee.BackgroundColor = label.Text == "☕ CÀ PHÊ" ? Colors.White : Colors.Transparent;
        CategoryMilkTea.BackgroundColor = label.Text == "🍵 TRÀ" ? Colors.White : Colors.Transparent;

        DisplayAlert("Thông báo", $"Bạn đã chọn danh mục: {label.Text}", "OK");
    }

    private void OnSearchClicked(object sender, EventArgs e)
    {
        DisplayAlert("Thông báo", "Bạn đã nhấn nút Tìm!", "OK");
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

            // AddProductPopup.WidthRequest = scale * 500; // Chiều rộng linh hoạt
            // AddProductPopup.HeightRequest = scale * 600; // Chiều cao linh hoạt

            // EditProductPopupFrame.WidthRequest = scale * 500;
            // EditProductPopupFrame.HeightRequest = scale * 600;

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

    private bool _isProductGroupOptionsVisible = false; // Biến trạng thái cho tùy chọn nhóm sản phẩm trong popup
    //Popup them san pham
    private void OnAddProductClicked(object sender, EventArgs e)
    {
        PopupOverlay.IsVisible = true;
    }
    private void OnSaveProductClicked(object sender, EventArgs e)
    {

        PopupOverlay.IsVisible = false;

    }

    private void OnCancelProductClicked(object sender, EventArgs e)
    {
        PopupOverlay.IsVisible = false;

    }



    //Popup edit san pharm
    private void OnEditProductClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            _selectedProductName = button.BindingContext as string ?? string.Empty;
            EditProductNameEntry.Text = _selectedProductName;
            EditProductDescriptionEntry.Text = "Thông tin mô tả mẫu";
            EditProductPriceEntry.Text = "100000";

            EditProductPopup.IsVisible = true;
        }
    }
    private void OnSaveEditProductClicked(object sender, EventArgs e)
    {
        string name = EditProductNameEntry.Text;
        string description = EditProductDescriptionEntry.Text;
        string priceText = EditProductPriceEntry.Text;

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(priceText))
        {
            DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ tên và giá sản phẩm.", "OK");
            return;
        }

        if (!decimal.TryParse(priceText, out decimal price))
        {
            DisplayAlert("Lỗi", "Giá sản phẩm không hợp lệ.", "OK");
            return;
        }

        SaveEditedProduct(name, description, price);

        EditProductPopup.IsVisible = false;
    }

    private void OnCancelEditProductClicked(object sender, EventArgs e)
    {
        EditProductPopup.IsVisible = false;
    }

    private void SaveEditedProduct(string name, string description, decimal price)
    {
        Console.WriteLine($"Đã lưu sản phẩm: {name}, {description}, Giá: {price}");
    }

    public void ShowEditProductPopup(string name, string description, decimal price, string group)
    {
        EditProductNameEntry.Text = name;
        EditProductDescriptionEntry.Text = description;
        EditProductPriceEntry.Text = price.ToString();
        EditProductGroupLabel.Text = group;

        EditProductPopup.IsVisible = true;
    }
    //Popup nhom san pham va cac thao tac trong do
    private void OnProductGroupLabelTapped(object sender, EventArgs e)
    {
        _isProductGroupOptionsVisible = true;
        ProductGroupOptions.IsVisible = _isProductGroupOptionsVisible;
    }

    private void OnCoffeeOptionSelected(object sender, EventArgs e)
    {
        ProductGroupLabel.Text = "Cà phê";
        _isProductGroupOptionsVisible = false;
        ProductGroupOptions.IsVisible = _isProductGroupOptionsVisible;
    }

    private void OnTeaOptionSelected(object sender, EventArgs e)
    {
        ProductGroupLabel.Text = "Trà";
        _isProductGroupOptionsVisible = false;
        ProductGroupOptions.IsVisible = _isProductGroupOptionsVisible;
    }

    private void OnPastryOptionSelected(object sender, EventArgs e)
    {
        ProductGroupLabel.Text = "Bánh ngọt";
        _isProductGroupOptionsVisible = false;
        ProductGroupOptions.IsVisible = _isProductGroupOptionsVisible;
    }
    private void OnCoffeeOptionClicked(object sender, EventArgs e) { }
    private void OnTeaOptionClicked(object sender, EventArgs e) { }
    private void OnPastryOptionClicked(object sender, EventArgs e) { }

}