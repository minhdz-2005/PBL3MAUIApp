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
        public ObservableCollection<OrderDetail> OrderDetails { get; set; } = new();
        public OrderDetailService orderDetailService = new OrderDetailService();

        public async Task GetAllOrderDetail()
        {
            List<OrderDetail> listOrderDetail = await orderDetailService.GetOrderDetailsAsync();
            OrderDetails.Clear();
            foreach (var item in listOrderDetail)
                OrderDetails.Add(item);
        }

        public void ChooseProduct(Product p)
        {
            var existing = OrderDetails.FirstOrDefault(x => x.ProductId == p.Id);
            if (existing == null)
            {
                OrderDetails.Add(new OrderDetail(p.Id, 1, p.Price));
            }
                
        }
        
        public void IncreaseQuantity(OrderDetail orderDetail)
        {
            foreach (var item in OrderDetails)
            {
                if(item.ProductId == orderDetail.ProductId)
                {
                    item.Quantity += 1;
                    Debug.WriteLine($"quantity: {item.Quantity}");
                }
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
