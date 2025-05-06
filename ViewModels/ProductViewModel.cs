using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.ViewModels
{
    class ProductViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; } = new();

        public async Task LoadProductsAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7221/api/Product");

            if (response != null)
            {
                Debug.WriteLine("LoadProductsAsync: " + response.Count);
                Products.Clear();
                foreach (var item in response)
                    Products.Add(item);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
