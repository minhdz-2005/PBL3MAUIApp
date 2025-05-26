namespace PBL3MAUIApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

             // Shell.Current.GoToAsync("//Manager_MainPage", animate: false);
            // Shell.Current.GoToAsync("//MainPageCashier", animate: false);
            Shell.Current.GoToAsync("//LoginView", animate: false);
        }
    }
}
