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

namespace PBL3MAUIApp.ViewModels;

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

    // LOC SAN PHAM THEO DANH MUC
    public async Task FilterCategory(string category)
    {
        List<Product> listProduct = await productService.GetProductsAsync();

        Products.Clear();
        foreach (var item in listProduct)
        {
            if (item.Category == category) Products.Add(item);
        }
        if (category == "All") await GetAllProduct();
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
    public async Task<bool> AddProduct(string name, string priceText, string cate, string description)
    {
        // KIEM TRA CAC GIA TRI DAU VAO
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(priceText) || string.IsNullOrWhiteSpace(cate))
        {
            await Shell.Current.DisplayAlert("Lỗi", "Hãy nhập đầy đủ thông tin cho sản phẩm.", "OK");
            return false; // Khong the them san pham neu cac truong bat buoc rong
        }
        if (!decimal.TryParse(priceText, out decimal price) || price < 0)
        {
            await Shell.Current.DisplayAlert("Lỗi", "Hãy nhập giá trị hợp lệ cho giá của sản phẩm.", "OK");
            return false; // Khong the them san pham neu gia khong hop le
        }
        // Tao san pham moi
        Product newProduct = new Product(name, price, cate, description);
        // Goi dich vu de them san pham
        bool result = await productService.AddProductAsync(newProduct);
        if (result)
        {
            await GetAllProduct();
        }
        else
        {
            await Shell.Current.DisplayAlert("Lỗi", "Không thể thêm sản phẩm, hãy thử lại.", "OK");
        }
        return result; // Tra ve ket qua them san pham
    }
    // CHINH SUA SAN PHAM
    public async Task<bool> UpdateProduct(int id, string name, string priceText, string cate, string description)
    {
        // KIEM TRA GIA TRI DAU VAO
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(priceText) || string.IsNullOrWhiteSpace(cate))
        {
            await Shell.Current.DisplayAlert("Lỗi", "Hãy nhập đầy đủ thông tin cho sản phẩm.", "OK");
            return false; // Khong the cap nhat san pham neu cac truong bat buoc rong
        }
        if (!decimal.TryParse(priceText, out decimal price) || price < 0)
        {
            await Shell.Current.DisplayAlert("Lỗi", "Hãy nhập giá trị hợp lệ cho giá của sản phẩm.", "OK");
            return false; // Khong the cap nhat san pham neu gia khong hop le
        }
        // Tao san pham moi voi id da cho
        Product updatedProduct = new Product(name, price, cate, description);
        // Goi dich vu de cap nhat san pham
        bool result = await productService.UpdateProductAsync(id, updatedProduct);
        if (result)
        {
            await GetAllProduct();
        }
        return true;
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
