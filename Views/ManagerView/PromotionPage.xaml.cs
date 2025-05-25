using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace PBL3MAUIApp.Views.ManagerView;
using PBL3MAUIApp.ViewModels.CashierViewModels;
using PBL3MAUIApp.Models;
using System.Threading.Tasks;
using System.Diagnostics;

public partial class PromotionPage : ContentPage
{
    private double _lastScale = -1;
    /// ///////////////
    public CashierViewModel? mainViewModel;
    public PromotionPage()
    {
        InitializeComponent();
        mainViewModel = BindingContext as CashierViewModel;
        this.SizeChanged += (s, e) =>
        {
            double width = this.Width;

            double baseWidth = 1440; // chi?u r?ng chu?n thi?t k?
            double scale = this.Width / baseWidth;
            mainViewModel = BindingContext as CashierViewModel;

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
        if (mainViewModel != null) await mainViewModel.VoucherVM.GetAllVouchers();
        All.BackgroundColor = Color.FromArgb("#FFB800"); // mau click
        DangDienRa.BackgroundColor = Color.FromArgb("#C41E3A"); // mau mac dinh
        SapToi.BackgroundColor = Color.FromArgb("#7B6A54"); // mau mac dinh
        DaKetThuc.BackgroundColor = Color.FromArgb("#D4BFAA"); // mau mac dinh
    }

    private static int idVoucher = 0;

    // Sự kiện khi nhấn nút "Thêm" (chỉ hiển thị thông báo, không lưu dữ liệu)
    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        // HIEN THI THONG TIN TRUOC KHI THEM
        //Debug.WriteLine($"{AddName.Text}, {AddCode.Text}, {AddDescription.Text}, {AddDiscount.Text}, {AddStart.Date.ToShortDateString()}, {AddEnd.Date.ToShortDateString()}");
        if (mainViewModel != null)
        {
            bool isAdded = await mainViewModel.VoucherVM.AddVoucher(AddName.Text, AddCode.Text, AddDescription.Text, AddDiscount.Text, AddStart.Date, AddEnd.Date);

            if (isAdded)
            {
                AddName.Text = string.Empty;
                AddCode.Text = string.Empty;
                AddDescription.Text = string.Empty;
                AddDiscount.Text = string.Empty;
                AddStart.Date = DateTime.Now;
                AddEnd.Date = DateTime.Now;
            }
        }
    }

    // Sự kiện khi nhấn các nút bộ lọc (chỉ thay đổi màu sắc)
    // VOUCHER FILTERING
    private bool isDangDienRaClicked = false;
    private bool isSapToiClicked = false;
    private bool isDaKetThucClicked = false;
    private async void OnAllClicked(object sender, EventArgs e)
    {
        All.BackgroundColor = Color.FromArgb("#FFB800"); // mau click

        if (mainViewModel != null)
        {
            await mainViewModel.VoucherVM.GetAllVouchers();
        }

        isDangDienRaClicked = false;
        isSapToiClicked = false;
        isDaKetThucClicked = false;

        DangDienRa.BackgroundColor = Color.FromArgb("#C41E3A"); // mau mac dinh
        SapToi.BackgroundColor = Color.FromArgb("#7B6A54"); // mau mac dinh
        DaKetThuc.BackgroundColor = Color.FromArgb("#D4BFAA"); // mau mac dinh
    }
    private async void OnDangDienRaClicked(object sender, EventArgs e)
    {
        if (!isDangDienRaClicked)
        {
            DangDienRa.BackgroundColor = Color.FromArgb("#FFB800");
            All.BackgroundColor = Color.FromArgb("#C41EAC"); // mau mac dinh

            if (mainViewModel != null)
            {
                mainViewModel.VoucherVM.FilterVouchers("DangDienRa");
            }

            isDangDienRaClicked = true;
            isSapToiClicked = false;
            isDaKetThucClicked = false;
        }
        else
        {
            if (mainViewModel != null) await mainViewModel.VoucherVM.GetAllVouchers();

            All.BackgroundColor = Color.FromArgb("#FFB800"); // mau click
            DangDienRa.BackgroundColor = Color.FromArgb("#C41E3A"); // mau mac dinh

            isDangDienRaClicked = false;
            isSapToiClicked = false;
            isDaKetThucClicked = false;
        }
        SapToi.BackgroundColor = Color.FromArgb("#7B6A54"); // mau mac dinh
        DaKetThuc.BackgroundColor = Color.FromArgb("#D4BFAA"); // mau mac dinh
    }
    private async void OnSapToiClicked(object sender, EventArgs e)
    {
        if (!isSapToiClicked)
        {
            SapToi.BackgroundColor = Color.FromArgb("#FFB800");
            All.BackgroundColor = Color.FromArgb("#C41EAC"); // mau mac dinh

            if (mainViewModel != null)
            {
                mainViewModel.VoucherVM.FilterVouchers("SapToi");
            }

            isDangDienRaClicked = false;
            isSapToiClicked = true;
            isDaKetThucClicked = false;
        }
        else
        {
            if (mainViewModel != null) await mainViewModel.VoucherVM.GetAllVouchers();

            All.BackgroundColor = Color.FromArgb("#FFB800"); // mau click
            SapToi.BackgroundColor = Color.FromArgb("#7B6A54"); // mau mac dinh

            isDangDienRaClicked = false;
            isSapToiClicked = false;
            isDaKetThucClicked = false;

        }
        DangDienRa.BackgroundColor = Color.FromArgb("#C41E3A"); // mau mac dinh
        DaKetThuc.BackgroundColor = Color.FromArgb("#D4BFAA"); // mau mac dinh
    }
    private async void OnDaKetThucClicked(object sender, EventArgs e)
    {
        if (!isDaKetThucClicked)
        {
            DaKetThuc.BackgroundColor = Color.FromArgb("#FFB800");
            All.BackgroundColor = Color.FromArgb("#C41EAC"); // mau mac dinh

            if (mainViewModel != null)
            {
                mainViewModel.VoucherVM.FilterVouchers("DaKetThuc");
            }

            isDangDienRaClicked = false;
            isSapToiClicked = false;
            isDaKetThucClicked = true;
        }
        else
        {
            if (mainViewModel != null) await mainViewModel.VoucherVM.GetAllVouchers();

            All.BackgroundColor = Color.FromArgb("#FFB800"); // mau click
            DaKetThuc.BackgroundColor = Color.FromArgb("#D4BFAA"); // mau mac dinh

            isDangDienRaClicked = false;
            isSapToiClicked = false;
            isDaKetThucClicked = false;

        }
        DangDienRa.BackgroundColor = Color.FromArgb("#C41E3A"); // mau mac dinh
        SapToi.BackgroundColor = Color.FromArgb("#7B6A54"); // mau mac dinh
    }

    // Sự kiện khi tap vào một Frame ưu đãi
    private void OnPromotionTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Voucher voucher)
        {
            // Hiển thị thông tin chi tiết của Voucher
            // DisplayAlert("Thông tin ưu đãi", $"Mã ưu đãi: {voucher.Code}\nGiá trị: {voucher.DiscountValue}\nNgày bắt đầu: {voucher.StartDate.ToShortDateString()}\nNgày kết thúc: {voucher.EndDate.ToShortDateString()}", "OK");
            EditName.IsEnabled = true;
            EditCode.IsEnabled = true;
            EditDescription.IsEnabled = true;
            EditDiscount.IsEnabled = true;
            EditStart.IsEnabled = true;
            EditEnd.IsEnabled = true;


            idVoucher = voucher.Id; // Lưu ID của Voucher để sử dụng trong các thao tác khác

            EditName.Text = voucher.Name;
            EditCode.Text = voucher.Code;
            EditDescription.Text = voucher.Description;
            EditDiscount.Text = voucher.DiscountValue.ToString();
            EditStart.Date = voucher.StartDate;
            EditEnd.Date = voucher.EndDate;
        }
    }

    // Sự kiện khi nhấn nút "Lưu"
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (mainViewModel != null)
        {
            await mainViewModel.VoucherVM.UpdateVoucher(idVoucher, EditName.Text, EditCode.Text, EditDescription.Text, EditDiscount.Text, EditStart.Date, EditEnd.Date);
        }
    }

    // Sự kiện khi nhấn nút "Xóa"
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (mainViewModel != null)
        {
            bool isDel = await mainViewModel.VoucherVM.DeleteVoucher(idVoucher);
            if (isDel)
            {
                EditName.Text = string.Empty;
                EditCode.Text = string.Empty;
                EditDescription.Text = string.Empty;
                EditDiscount.Text = string.Empty;
                EditStart.Date = DateTime.Now;
                EditEnd.Date = DateTime.Now.AddDays(7); // Giả sử ngày kết thúc là 7 ngày sau

                EditName.IsEnabled = false;
                EditCode.IsEnabled = false;
                EditDescription.IsEnabled = false;
                EditDiscount.IsEnabled = false;
                EditStart.IsEnabled = false;
                EditEnd.IsEnabled = false;
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


}
