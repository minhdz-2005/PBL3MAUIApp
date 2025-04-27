using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

using PBL3MAUIApp.Models;
using PBL3MAUIApp.Services;

namespace PBL3MAUIApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        private readonly CustomerService _accountService = new CustomerService();
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var accs = await _accountService.GetCustomersAsync();

            foreach (var a in accs)
            {
                Debug.WriteLine($"Cus: {a.Id}, {a.Name}, {a.Username}, {a.PhoneNumber}");
            }
        }
    }

}
