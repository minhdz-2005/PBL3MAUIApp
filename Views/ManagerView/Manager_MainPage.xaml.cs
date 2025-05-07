namespace PBL3MAUIApp.Views.ManagerView;

public partial class Manager_MainPage : ContentPage
{
	public Manager_MainPage()
	{
		try
		{
			InitializeComponent();
			System.Diagnostics.Debug.WriteLine("MainPage initialized successfully");
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.WriteLine($"Error initializing MainPage: {ex.Message}\nStackTrace: {ex.StackTrace}");
			throw;
		}
	}
}


