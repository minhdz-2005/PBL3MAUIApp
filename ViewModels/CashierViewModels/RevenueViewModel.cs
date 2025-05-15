using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels.CashierViewModels
{
    public class RevenueViewModel : INotifyPropertyChanged
    {
        // TONG DOANH THU
        private decimal _totalRevenue;
        public decimal TotalRevenue
        {
            get => _totalRevenue;
            set
            {
                _totalRevenue = value;
                OnPropertyChanged(nameof(TotalRevenue));
            }
        }
        
        private decimal _totalRevenueShift_1;
        public decimal TotalRevenueShift_1
        {
            get => _totalRevenueShift_1;
            set
            {
                _totalRevenueShift_1 = value;
                OnPropertyChanged(nameof(TotalRevenueShift_1));
            }
        }

        private decimal _totalRevenueShift_2;
        public decimal TotalRevenueShift_2
        {
            get => _totalRevenueShift_2;
            set
            {
                _totalRevenueShift_2 = value;
                OnPropertyChanged(nameof(TotalRevenueShift_2));
            }
        }
        
        private decimal _totalRevenueShift_3;
        public decimal TotalRevenueShift_3
        {
            get => _totalRevenueShift_3;
            set
            {
                _totalRevenueShift_3 = value;
                OnPropertyChanged(nameof(TotalRevenueShift_3));
            }
        }


        // SERVICE
        private OrderService orderService = new OrderService();


        public async Task CalcRevenue()
        {
            // XOA CAC DOANH THU DE LOAD LAIJ
            TotalRevenue = 0;
            TotalRevenueShift_1 = 0;
            TotalRevenueShift_2 = 0;
            TotalRevenueShift_3 = 0;
            // TONG HOP TU DAONH SACH ORDER
            var OrderList = await orderService.GetOrdersAsync();
            if (OrderList !=  null)
            {
                foreach (var Order in OrderList)
                {
                    // TINH TONG DOANH THU
                    TotalRevenue += Order.FinalAmount;
                    int h = Order.TimeAndDate.Hour;
                    if (h >= 6 && h < 12)
                    {
                        TotalRevenueShift_1 += Order.FinalAmount;
                    }
                    else if (h >= 12 && h < 18)
                    {
                        TotalRevenueShift_2 += Order.FinalAmount;
                    }
                    else if (h >= 18 && h < 24)
                    {
                        TotalRevenueShift_3 += Order.FinalAmount;
                    }
                }
            }
        }
        // LOC THEO NGAY VA CA LAM
        public async Task RevenueFiltering(DateTime date, string shift)
        {
            // XOA CAC DOANH THU DE LOAD LAI
            TotalRevenue = 0;
            TotalRevenueShift_1 = 0;
            TotalRevenueShift_2 = 0;
            TotalRevenueShift_3 = 0;
            // TONG HOP TU DANH SACH ORDER
            var OrderList = await orderService.GetOrdersByDateAsync(date);
            if (OrderList != null)
            {
                foreach (var Order in OrderList)
                {
                    if (Order.TimeAndDate.Day == date.Day && Order.TimeAndDate.Month == date.Month && Order.TimeAndDate.Year == date.Year)
                    {
                        // TINH TONG DOANH THU
                        TotalRevenue += Order.FinalAmount;
                        int h = Order.TimeAndDate.Hour;
                        if (h >= 6 && h < 12)
                        {
                            TotalRevenueShift_1 += Order.FinalAmount;
                        }
                        else if (h >= 12 && h < 18)
                        {
                            TotalRevenueShift_2 += Order.FinalAmount;
                        }
                        else if (h >= 18 && h < 24)
                        {
                            TotalRevenueShift_3 += Order.FinalAmount;
                        }
                    }
                    
                }
            }
        }




        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
