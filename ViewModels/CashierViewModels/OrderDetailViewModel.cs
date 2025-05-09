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

        public ObservableCollection<Order> Orders { get; set; } = new();
        public Order order = new Order();

        public OrderDetailService orderDetailService = new OrderDetailService();
        public ProductService productService = new ProductService();

        private bool createOrder = false;
        // CHON SAN PHAM
        public void ChooseProduct(Product p)
        {
            // THEM ORDER VO LIST ORDER
            if(createOrder == false)
            {
                order.Amount = 0;
                order.DiscountValue = 0;
                order.FinalAmount = 0;

                Orders.Add(order);
                createOrder = true;
            }

            var existing = OrderDetails.FirstOrDefault(x => x.Product.Id == p.Id);
            if (existing == null)
            {
                OrderDetails.Add(new OrderItemViewModel(new OrderDetail( 1, p.Id, 1, p.Price), p));

                // Gia Tien ORDER
                order.Amount += p.Price;
                order.FinalAmount += p.Price;
            }
                
        }
        
        // THEM QUANTITY
        public async void IncreaseQuantity(OrderItemViewModel orderItem)
        {
            var item = OrderDetails.FirstOrDefault(x => x.OrderDetail.ProductId == orderItem.OrderDetail.ProductId);
            if (item != null)
            {
                item.Quantity += 1;
                //Debug.WriteLine($"Quantity: {item.Quantity}");
                var product = await productService.GetProductByIdAsync(item.OrderDetail.ProductId);
                if (product != null)
                {
                    item.OrderDetail.TotalPrice = product.Price * item.OrderDetail.Quantity;

                    order.Amount += product.Price;
                    order.FinalAmount += product.Price;
                }
            }
        }
        // GIAM QUANTITY
        public async void DecreaseQuantity(OrderItemViewModel orderItem)
        {
            var item = OrderDetails.FirstOrDefault(x => x.OrderDetail.ProductId == orderItem.OrderDetail.ProductId);
            if (item != null)
            {
                item.Quantity -= 1;
                var product = await productService.GetProductByIdAsync(item.OrderDetail.ProductId);
                if (product != null)
                {
                    item.OrderDetail.TotalPrice = product.Price * item.OrderDetail.Quantity;

                    order.Amount -= product.Price;
                    order.FinalAmount -= product.Price;
                }
                if (item.Quantity < 1)
                {
                    OrderDetails.Remove(item);
                }
            }
        }
        // CAP NHAT GIA CUOI CUNG CHO OrderDetail
        public void UpdateTotalPrice()
        {
            foreach (var item in OrderDetails)
            {
                // Debug.WriteLine($"TotalPrice: {item.OrderDetail.TotalPrice}");

                // Them ORDER va ORDER DETAIL VAO DATABASE
                //

                // XOA OrderDetails va Orders
                
            }
            Orders.Clear();
            OrderDetails.Clear();
            createOrder = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
