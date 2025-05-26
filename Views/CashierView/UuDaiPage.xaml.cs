using PBL3MAUIApp.ViewModels;
using PBL3MAUIApp.Models;
using System.Threading.Tasks;
namespace PBL3MAUIApp.Views.CashierView;

public partial class UuDaiPage : ContentPage
{
    public CashierViewModel? mainViewModel;
    public UuDaiPage()
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
    private bool isDangDienRaClicked = false;
    private bool isSapToiClicked = false;
    private bool isDaKetThucClicked = false;
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (mainViewModel != null) await mainViewModel.VoucherVM.GetAllVouchers();
        All.BackgroundColor = Color.FromArgb("#FFB800"); // mau click
        DangDienRa.BackgroundColor = Color.FromArgb("#C41E3A"); // mau mac dinh
        SapToi.BackgroundColor = Color.FromArgb("#7B6A54"); // mau mac dinh
        DaKetThuc.BackgroundColor = Color.FromArgb("#D4BFAA"); // mau mac dinh
    }

    // BAM VAO UU DAI
    private async void OnVoucherButtonClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedVoucher = button?.BindingContext as Voucher;

        if (selectedVoucher != null)
        {
            int id = selectedVoucher.Id;
            if(mainViewModel != null)
            {
                await mainViewModel.VoucherVM.GetVoucherById(id);
            }
        }
    }

    // VOUCHER FILTERING
    
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
    private async void OnDangDienRaClicked (object sender, EventArgs e)
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

}