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

    // LOC SAN PHAM THEO DANH MUC
    public async Task FilterCategory(string category)
    {
        List<Product> listProduct = await productService.GetProductsAsync();

        Products.Clear();
        foreach (var item in listProduct)
        {
            if (item.Category == category) Products.Add(item);
        }
    }

    // TIM KIEM SAN PHAM THEO TEN
    public async Task SearchProduct(string name)
    {
        List<Product> listProduct = await productService.GetProductsAsync();
        Products.Clear();
        foreach (var item in listProduct)
        {
            if (item.Name.ToLower().Contains(name.ToLower())) Products.Add(item);
        }
    }

    // THEM SAN PHAM
    public async Task AddProduct(Product a)
    {
        await productService.AddProductAsync(a);
        Products.Add(a);
    }
    // CHINH SUA SAN PHAM
    public async Task UpdateProduct(int id, Product a)
    {
        await productService.UpdateProductAsync(id, a);
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            product.Name = a.Name;
            product.Price = a.Price;
            product.Category = a.Category;
            product.Description = a.Description;
        }
    }
    // XOA SAN PHAM
    public async Task DeleteProduct(int id)
    {
        await productService.DeleteProductAsync(id);
        var product = Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            Products.Remove(product);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
