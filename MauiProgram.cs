using Microsoft.Extensions.Logging;

using PBL3MAUIApp.Services;

namespace PBL3MAUIApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //builder.Services.AddSingleton<AccountService>();
            //builder.Services.AddSingleton<CustomerService>();
            //builder.Services.AddSingleton<OrderService>();
            //builder.Services.AddSingleton<OrderDetailService>();
            //builder.Services.AddSingleton<ProductService>();
            //builder.Services.AddSingleton<ShiftService>();
            //builder.Services.AddSingleton<ShiftStaffsService>();
            //builder.Services.AddSingleton<StaffService>();
            //builder.Services.AddSingleton<VoucherService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            
            return builder.Build();
        }
    }
}
