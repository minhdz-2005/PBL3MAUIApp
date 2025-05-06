using PBL3MAUIApp.Views.CashierView;

namespace PBL3MAUIApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPageCashier), typeof(MainPageCashier));
            Routing.RegisterRoute(nameof(OrderPage), typeof(OrderPage));
            Routing.RegisterRoute(nameof(DSOrderPage), typeof(DSOrderPage));
            Routing.RegisterRoute(nameof(UuDaiPage), typeof(UuDaiPage));
            Routing.RegisterRoute(nameof(DoanhThuPage), typeof(DoanhThuPage));
        }
    }
}
