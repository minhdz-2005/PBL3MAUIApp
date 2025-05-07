using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Models;
using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels.CashierViewModels
{
    public class OrderDetailViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<OrderItemViewModel> OrderDetails { get; set; } = new();

        public OrderDetailService orderDetailService = new OrderDetailService();
        public ProductService productService = new ProductService();

        // CHON SAN PHAM
        public void ChooseProduct(Product p)
        {
            var existing = OrderDetails.FirstOrDefault(x => x.Product.Id == p.Id);
            if (existing == null)
            {
                OrderDetails.Add(new OrderItemViewModel(new OrderDetail(p.Id, 1, p.Price), p));
            }
                
        }
        
        // THEM QUANTITY
        public void IncreaseQuantity(OrderItemViewModel orderItem)
        {
            var item = OrderDetails.FirstOrDefault(x => x.OrderDetail.ProductId == orderItem.OrderDetail.ProductId);
            if (item != null)
            {
                item.Quantity += 1;
                //Debug.WriteLine($"Quantity: {item.Quantity}");
            }
        }
        // GIAM QUANTITY
        public void DecreaseQuantity(OrderItemViewModel orderItem)
        {
            var item = OrderDetails.FirstOrDefault(x => x.OrderDetail.ProductId == orderItem.OrderDetail.ProductId);
            if (item != null)
            {
                item.Quantity -= 1;
                if (item.Quantity < 1)
                {
                    OrderDetails.Remove(item);
                }
            }
        }
        // CAP NHAT GIA CUOI CUNG CHO OrderDetail
        public async Task UpdateTotalPrice()
        {
            foreach (var item in OrderDetails)
            {
                var product = await productService.GetProductByIdAsync(item.OrderDetail.ProductId);
                if (product != null)
                {
                    item.OrderDetail.TotalPrice = product.Price * item.OrderDetail.Quantity;
                    Debug.WriteLine($"TotalPrice: {item.OrderDetail.TotalPrice}");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
