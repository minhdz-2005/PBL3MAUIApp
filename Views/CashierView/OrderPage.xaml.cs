using System.Threading.Tasks;

using PBL3MAUIApp.Models;

using PBL3MAUIApp.ViewModels.CashierViewModels;

namespace PBL3MAUIApp.Views.CashierView;

public partial class OrderPage : ContentPage
{
    public CashierViewModel? mainViewModel;
    public OrderPage()
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
        if (mainViewModel != null) await mainViewModel.ProductVM.GetAllProduct();
    }
    // Lua chon DANH MUC
    private bool isCoffeeClick = false;
    private bool isTeaClick = false;
    private bool isCakeClick = false;
    // Nhan vao nut DANH MUC Ca Phe
    private async void OnCoffeeButtonClicked(object sender, EventArgs e)
    {
        if(mainViewModel != null)
        {
            if (isCoffeeClick == false)
            {
                CoffeeButton.BackgroundColor = Color.FromArgb("#FF9C9C");
                TeaButton.BackgroundColor = Colors.Transparent;
                CakeButton.BackgroundColor = Colors.Transparent;

                isCoffeeClick = true;
                isTeaClick = false;
                isCakeClick = false;
                await mainViewModel.ProductVM.CoffeeCategory();
            }
            else
            {
                CoffeeButton.BackgroundColor = Colors.Transparent;
                isCoffeeClick = false;
                await mainViewModel.ProductVM.GetAllProduct();
            }
        }
    }
    // Nhan vao nut DANH MUC Tra
    private async void OnTeaButtonClicked(object sender, EventArgs e)
    {
        if (mainViewModel != null)
        {
            if (isTeaClick == false)
            {
                TeaButton.BackgroundColor = Color.FromArgb("#FF9C9C");
                CoffeeButton.BackgroundColor = Colors.Transparent;
                CakeButton.BackgroundColor = Colors.Transparent;


                isTeaClick = true;
                isCoffeeClick = false;
                isCakeClick = false;
                await mainViewModel.ProductVM.TeaCategory();
            }
            else
            {
                TeaButton.BackgroundColor = Colors.Transparent;
                isTeaClick = false;
                await mainViewModel.ProductVM.GetAllProduct();
            }
        }
    }
    // Nhan vao nut DANH MUC Banh Ngot
    private async void OnCakeButtonClicked(object sender, EventArgs e)
    {
        if (mainViewModel != null)
        {
            if (isCakeClick == false)
            {
                CakeButton.BackgroundColor = Color.FromArgb("#FF9C9C");
                CoffeeButton.BackgroundColor = Colors.Transparent;
                TeaButton.BackgroundColor = Colors.Transparent;

                isCakeClick = true;
                isCoffeeClick = false;
                isTeaClick = false;
                await mainViewModel.ProductVM.CakeCategory();
            }
            else
            {
                CakeButton.BackgroundColor = Colors.Transparent;
                isCakeClick = false;
                await mainViewModel.ProductVM.GetAllProduct();
            }
        }
    }
    
    // Chon MON
    private void ChooseButtonClicked(object sender, EventArgs e)
    {
        if (mainViewModel != null)
        {
            var button = sender as Button;
            var product = button?.BindingContext as Product;

            if(product  != null)
            {
                mainViewModel.OrderDetailVM.ChooseProduct(product);
            }
        }
    }
    // Them QUANTITY
    private void AddQuantityClicked(object sender, EventArgs e)
    {
        if(mainViewModel != null)
        {
            var button = sender as Button;
            var orderItem = button?.BindingContext as OrderItemViewModel;

            if (orderItem != null)
            {
                mainViewModel.OrderDetailVM.IncreaseQuantity(orderItem);
            }
        }
    }
    // Giam QUANTITY
    private void DelQuantityClicked(object sender, EventArgs e)
    {
        if (mainViewModel != null)
        {
            var button = sender as Button;
            var orderItem = button?.BindingContext as OrderItemViewModel;

            if (orderItem != null)
            {
                mainViewModel.OrderDetailVM.DecreaseQuantity(orderItem);
            }
        }
    }
    // XAC NHAN DAT MON
    public void OnOrderButtonClicked(object sender, EventArgs e)
    {
        
        if (mainViewModel != null)
        {
            mainViewModel .OrderDetailVM.ConfirmOrder();
            
        }
        AddQueue.IsVisible = true;
        SaveQueue.IsVisible = false;
        var frame = new Frame
        {
            HeightRequest = 100,
            BackgroundColor = Colors.LightGray,
            CornerRadius = 10
        };
    }
    public void DeleteDetailClicked(object sender, EventArgs e) 
    {
        if (mainViewModel != null)
        {
            var button = sender as Button;
            var orderItem = button?.BindingContext as OrderItemViewModel;

            if (orderItem != null)
            {
                mainViewModel.OrderDetailVM.DeleteAllQuantity(orderItem);
            }
        }
    }

    //Nút tạo hoá đơn chờ
    public void OnAddQueueClicked(object sender, EventArgs e)
    {
        AddQueue.IsVisible = true;
        SaveQueue.IsVisible = false;
        var frame = new Frame
        {
            HeightRequest = 100,
            BackgroundColor = Colors.LightGray,
            CornerRadius = 10
        };
    }
    //Mở popup thêm ưu đãi
    public async void OnAddPromotionClicked(object sender, EventArgs e)
    {
        PopupPromotion.IsVisible = true;
        if (mainViewModel != null)
        {
            await mainViewModel.VoucherVM.GetAllVouchers();
        }
    }
    //Mở popup thêm mô tả và chi tiết sản phẩm
    public void OnDetailProductTapped(object sender, EventArgs e)
    {
        PopupOverlay.IsVisible = true;
    }
    //POPUP mở mô tả sản phẩm
    private void OnOverlayTapped(object sender, EventArgs e)
    {
        PopupOverlay.IsVisible = false;

    }

    //Thoát popup
    private void OnOutPromotionTapped(object sender, EventArgs e)
    {
        PopupPromotion.IsVisible = false;

    }

    //Thoát popup
    private void OnOpenDetailTapped(object sender, EventArgs e)
    {
        AddQueue.IsVisible = false;
        SaveQueue.IsVisible = true;

        if(mainViewModel != null)
        {
            var grid = sender as Grid;
            var Order = grid?.BindingContext as Order;
            

            if (Order != null)
            {
                int idOrder = Order.Id;
                mainViewModel.OrderDetailVM.ShowOrderQueue(idOrder);
            }
        }

    }


    // LUU GHI CHU
    private void OnSaveNoteClicked(object sender, EventArgs e)
    {
        if (mainViewModel != null)
        {
            var button = sender as Button;
            var orderItem = button?.BindingContext as OrderItemViewModel;
            
            if (orderItem != null)
            {
                //mainViewModel.OrderDetailVM.SaveNote(orderItem, note);
            }
        }
    }
}