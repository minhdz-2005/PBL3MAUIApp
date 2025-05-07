using PBL3MAUIApp.Views.CashierView;
using PBL3MAUIApp.Views.ManagerView;

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

            Routing.RegisterRoute(nameof(Manager_MainPage), typeof(Manager_MainPage));
            Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
            Routing.RegisterRoute(nameof(StaffPage), typeof(StaffPage));
            Routing.RegisterRoute(nameof(ShiftPage), typeof(ShiftPage));
            Routing.RegisterRoute(nameof(PromotionPage), typeof(PromotionPage));
            Routing.RegisterRoute(nameof(OrderPageManager), typeof(OrderPageManager));
            Routing.RegisterRoute(nameof(RevenuePage), typeof(RevenuePage));
        }
    }
}
