using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

using PBL3MAUIApp.ViewModels.CashierViewModels;
using PBL3MAUIApp.Models;
namespace PBL3MAUIApp.Views.ManagerView;


public partial class ProductPage : ContentPage
{
    private double _lastScale = -1;
    private string _selectedProductName = string.Empty;

    public CashierViewModel? mainViewModel;

    public ProductPage()
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
    // LOAD DANH SACH SAN PHAM KHI VAO TRANG
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (mainViewModel != null)
        {
            await mainViewModel.ProductVM.GetAllProduct();
        }
    }

    // BAM VAO CHON DANH MUC
    private async void OnCategoryTapped(object sender, EventArgs e)
    {
        if (sender is not Label label) return;

        string cate = label.Text;

        if (mainViewModel != null)
        {
            if (cate == "Tất cả") 
                await mainViewModel.ProductVM.GetAllProduct();
            else
                await mainViewModel.ProductVM.FilterCategory(cate);
        }

        //CategoryCoffee.BackgroundColor = label.Text == "☕ CÀ PHÊ" ? Colors.White : Colors.Transparent;
        //CategoryMilkTea.BackgroundColor = label.Text == "🍵 TRÀ" ? Colors.White : Colors.Transparent;

        // await DisplayAlert("Thông báo", $"Bạn đã chọn danh mục: {label.Text}", "OK");
    }

    // BAM VAO NUT TIM SAN PHAM
    private async void OnSearchClicked(object sender, EventArgs e)
    {
        string searchText = SearchEntry.Text;
        Debug.WriteLine($"Tìm kiếm sản phẩm với từ khóa: {searchText}");

        if (mainViewModel != null)
        {
            if (searchText == null)
                await mainViewModel.ProductVM.GetAllProduct();
            else
                await mainViewModel.ProductVM.SearchProduct(searchText);
        }


        // DisplayAlert("Thông báo", "Bạn đã nhấn nút Tìm!", "OK");
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
    // BAM VAO NUT THEM SAN PHAM
    private void OnAddProductClicked(object sender, EventArgs e)
    {
        PopupOverlay.IsVisible = true;
    }
    // BAM VAO NUT THEM
    private async void OnSaveProductClicked(object sender, EventArgs e)
    {
        string name = AddProductNameEntry.Text;
        string description = AddProductDescriptionEntry.Text;
        string cate = ProductGroupLabel.Text;

        decimal price;
        bool isValid = decimal.TryParse(AddProductPriceEntry.Text, out price);

        if (isValid && cate != "Nhóm sản phẩm" && cate != null)
        {
            Debug.WriteLine($"{name}, {description}, {price}, {cate}");

            if(mainViewModel != null)
            {
                await mainViewModel.ProductVM.AddProduct(new Product(name, price, cate, description));
            }
            PopupOverlay.IsVisible = false;
        }
        else
        {
            // Hiển thị lỗi hoặc xử lý khi nhập sai
            await DisplayAlert("Lỗi", "Vui lòng nhập giá hợp lệ.", "OK");
        }
    }
    // BAM VAO NUT HUY
    private void OnCancelProductClicked(object sender, EventArgs e)
    {
        PopupOverlay.IsVisible = false;

    }

    //Popup edit san pharm
    static int idProduct = 0;
    // BAM VAO NUT CHINH SUA
    private void OnEditProductClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            var product = button.BindingContext as Product;

            idProduct = 0;
            if (product != null)
            {
                idProduct = product.Id;
            }

            Debug.WriteLine($"Sản phẩm được chọn: {product?.Name}, {product?.Price}");

            EditProductGroupLabel.Text = product?.Category;
            EditProductNameEntry.Text = product?.Name;
            EditProductDescriptionEntry.Text = product?.Description;
            EditProductPriceEntry.Text = product?.Price.ToString();

            EditProductPopup.IsVisible = true;
        }
    }
    // BAM VAO NUT LUU
    private async void OnSaveEditProductClicked(object sender, EventArgs e)
    {
        string name = EditProductNameEntry.Text;
        string description = EditProductDescriptionEntry.Text;
        string cate = EditProductGroupLabel.Text;

        decimal price;
        bool isValid = decimal.TryParse(EditProductPriceEntry.Text, out price);

        if (isValid)
        {
            Debug.WriteLine($"{idProduct}, {name}, {description}, {price}, {cate}");

            if (mainViewModel != null)
            {
                await mainViewModel.ProductVM.UpdateProduct(idProduct, new Product(name, price, cate, description));
            }
            PopupOverlay.IsVisible = false;
        }
        else
        {
            // Hiển thị lỗi hoặc xử lý khi nhập sai
            await DisplayAlert("Lỗi", "Vui lòng nhập giá hợp lệ.", "OK");
        }

        EditProductPopup.IsVisible = false;
    }

    private void OnCancelEditProductClicked(object sender, EventArgs e)
    {
        EditProductPopup.IsVisible = false;
    }

    // XOA SAN PHAM
    private async void OnDeleteProductClicked(object sender, EventArgs e)
    {
        if (sender is Button button)
        {
            var product = button.BindingContext as Product;
            if (product != null && mainViewModel != null)
            {
                Debug.WriteLine($"Sản phẩm được chọn để xóa: {product.Name}, {product.Price}");
                // Xóa sản phẩm
                await mainViewModel.ProductVM.DeleteProduct(product.Id);
            }
        }
    }

    //Popup nhom san pham va cac thao tac trong do
    private void OnProductGroupLabelTapped(object sender, EventArgs e)
    {
        _isProductGroupOptionsVisible = true;
        ProductGroupOptions.IsVisible = _isProductGroupOptionsVisible;
    }

    private void OnGroupOptionSelected(object sender, EventArgs e)
    {
        if (sender is not Label label) return;

        ProductGroupLabel.Text = label.Text;
        EditProductGroupLabel.Text = label.Text;
        _isProductGroupOptionsVisible = false;
        ProductGroupOptions.IsVisible = _isProductGroupOptionsVisible;
    }
    // THEM DANH MUC MOi
    private void OnAddGroupButtonClicked (object sender, EventArgs e)
    {
        ProductGroupLabel.Text = AddGroupProductEntry.Text;
        ProductGroupOptions.IsVisible = false;
    }
}