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
using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels.CashierViewModels;

public class ProductViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Product> Products { get; set; } = new();
    public ObservableCollection<string> Categories { get; set; } = new();

    public ProductService productService = new ProductService();

    public async Task GetAllProduct()
    {
        List<Product> listProduct = await productService.GetProductsAsync();

        Products.Clear();
        Categories.Clear();
        foreach (var item in listProduct)
        {
            Products.Add(item);
            if (!Categories.Contains(item.Category))
            {
                Categories.Add(item.Category);
            }
        }
            
    }

    public async Task CoffeeCategory()
    {
        List<Product> listProduct = await productService.GetProductsAsync();

        Products.Clear();
        foreach (var item in listProduct)
        {
            if (item.Category == "Cà Phê") Products.Add(item);
        }
    }
    public async Task TeaCategory()
    {
        List<Product> listProduct = await productService.GetProductsAsync();

        Products.Clear();
        foreach (var item in listProduct)
        {
            if (item.Category == "Trà") Products.Add(item);
        }
    }
    public async Task CakeCategory()
    {
        List<Product> listProduct = await productService.GetProductsAsync();

        Products.Clear();
        foreach (var item in listProduct)
        {
            if (item.Category == "Bánh Ngọt") Products.Add(item);
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
}
