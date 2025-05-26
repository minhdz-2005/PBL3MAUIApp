using System.Threading.Tasks;

using PBL3MAUIApp.ViewModels;

namespace PBL3MAUIApp.Views.CashierView;
public partial class DoanhThuPage : ContentPage
{
    public CashierViewModel? mainViewModel;
    public DoanhThuPage()
	{
		InitializeComponent();

        ShiftPicker.SelectedIndex = 0;

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
        if (mainViewModel != null) await mainViewModel.RevenueVM.CalcRevenue();
    }
    // LAY GIA TRI PICKER KHI BAM NUT XEM THONG KE
    private async void OnViewStatisticsClicked(object sender, EventArgs e)
    {
        // Lấy ngày từ DatePicker
        DateTime selectedDate = WorkDatePicker.Date;

        // Lấy ca làm việc từ Picker
        string selectedShift = ShiftPicker.SelectedItem?.ToString() ?? "Tất cả";

        // LOC THEO CA
        if (selectedShift == "Tất cả")
        {
            CaSang.IsVisible = true;
            CaChieu.IsVisible = true;
            CaToi.IsVisible = true;
        }
        if (selectedShift == "Ca sáng (6h00 - 12h00)")
        {
            CaSang.IsVisible = true;
            CaChieu.IsVisible = false;
            CaToi.IsVisible = false;
        }
        if (selectedShift == "Ca chiều (12h00 - 18h00)")
        {
            CaSang.IsVisible = false;
            CaChieu.IsVisible = true;
            CaToi.IsVisible = false;
        }
        if (selectedShift == "Ca tối (18h00 - 24h00)")
        {
            CaSang.IsVisible = false;
            CaChieu.IsVisible = false;
            CaToi.IsVisible = true;
        }

        // TONG HOP DOANH THU THEO NGAY VA CA
        if (mainViewModel != null)
        {
            // Gọi phương thức lọc doanh thu theo ngày và ca làm việc
            await mainViewModel.RevenueVM.RevenueFiltering(selectedDate, selectedShift);
        }
        // Ví dụ: hiển thị trong alert
        // await DisplayAlert("Thông tin", $"Ngày: {selectedDate:dd/MM/yyyy}\nCa: {selectedShift}", "OK");
    }

}