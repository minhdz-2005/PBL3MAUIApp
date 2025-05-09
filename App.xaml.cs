namespace PBL3MAUIApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Shell.Current.GoToAsync("//LoginView", animate: false);
        }
    }
}
