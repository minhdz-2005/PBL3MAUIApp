using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.ViewModels.CashierViewModels
{
    public class OrderItemViewModel : INotifyPropertyChanged
    {
        public OrderDetail OrderDetail { get; set; }
        public Product Product { get; set; }
        public int Quantity
        {
            get => OrderDetail.Quantity;
            set
            {
                OrderDetail.Quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public decimal TotalPrice
        {
            get => OrderDetail.TotalPrice;
            set
            {
                OrderDetail.TotalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public OrderItemViewModel(OrderDetail o, Product p)
        {
            OrderDetail = o;
            Product = p;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
