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
    public class OrderViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Order> Orders { get; set; } = new();
        public ObservableCollection<OrderDetail> OrderDetails { get; set; } = new();

        private OrderService orderService = new OrderService();
        private OrderDetailService orderDetailService = new OrderDetailService();
        // GET ORDER
        public async Task GetAllOrders()
        {
            Orders.Clear();
            var orders = await orderService.GetOrdersAsync();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }
        // LOC DON HANG
        public async void OrderFiltering(int? id, DateTime start, DateTime end)
        {
            if (id == null) id = 0;
            // Debug.WriteLine($"ID: {id}");
            Orders.Clear();

            // LOC THEO ID
            if (id != 0)
            {
                var order = await orderService.GetOrderByIdAsync((int)id);
                if (order != null)
                {
                    Orders.Add(order);
                    return;
                }
            }

            // LOC THEO NGAY
            DateTime current = start.Date;
            DateTime endDate = end.Date;

            while (current <= endDate)
            {
                // Debug.WriteLine("Chay 1: " + current.ToShortDateString());

                var orders = await orderService.GetOrdersByDateAsync(current);

                if (orders != null)
                {
                    // Debug.WriteLine("Chay 2 - SL order: " + orders.Count);
                    foreach (var order in orders)
                    {
                        // Debug.WriteLine("Chay 3");
                        Orders.Add(order);
                    }
                }

                current = current.AddDays(1); // tăng từng ngày
            }

        }
        
        

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
