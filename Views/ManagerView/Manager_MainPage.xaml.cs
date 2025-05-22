namespace PBL3MAUIApp.Views.ManagerView;

public partial class Manager_MainPage : ContentPage
{
    private double _lastScale = -1;
    public Manager_MainPage()
    {
        try
        {
            InitializeComponent();

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
            System.Diagnostics.Debug.WriteLine("MainPage initialized successfully");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error initializing MainPage: {ex.Message}\nStackTrace: {ex.StackTrace}");
            throw;
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


