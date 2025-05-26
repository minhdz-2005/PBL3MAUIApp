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

namespace PBL3MAUIApp.ViewModels
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Order> Orders { get; set; } = new();
        public ObservableCollection<OrderDetail> OrderDetails { get; set; } = new();

        private OrderService orderService = new OrderService();
        private OrderDetailService orderDetailService = new OrderDetailService();
        private ShiftStaffsService shiftStaffsService = new ShiftStaffsService();
        private StaffService staffService = new StaffService();
        private ShiftService shiftService = new ShiftService();

        // MANAGER
        private int _totalOrderDay;
        public int TotalOrderDay
        {
            get => _totalOrderDay;
            set
            {
                if (_totalOrderDay != value)
                {
                    _totalOrderDay = value;
                    OnPropertyChanged(nameof(TotalOrderDay));
                }
            }
        }
        private decimal _totalRevenueDay;
        public decimal TotalRevenueDay
        {
            get => _totalRevenueDay;
            set
            {
                if (_totalRevenueDay != value)
                {
                    _totalRevenueDay = value;
                    OnPropertyChanged(nameof(TotalRevenueDay));
                }
            }
        }
        private decimal _totalDiscountDay;
        public decimal TotalDiscountDay
        {
            get => _totalDiscountDay;
            set
            {
                if (_totalDiscountDay != value)
                {
                    _totalDiscountDay = value;
                    OnPropertyChanged(nameof(TotalDiscountDay));
                }
            }
        }
        private decimal _totalFinalDay;
        public decimal TotalFinalDay
        {
            get => _totalFinalDay;
            set
            {
                if (_totalFinalDay != value)
                {
                    _totalFinalDay = value;
                    OnPropertyChanged(nameof(TotalFinalDay));
                }
            }
        }


        public ObservableCollection<Staff?> StaffCa1 { get; set; } = new();
        public ObservableCollection<Staff?> StaffCa2 { get; set; } = new();
        public ObservableCollection<Staff?> StaffCa3 { get; set; } = new();

        public ObservableCollection<Order> OrderTam { get; set; } = new();



        private decimal _revenueShift1;
        public decimal RevenueShift1
        {
            get => _revenueShift1;
            set
            {
                if (_revenueShift1 != value)
                {
                    _revenueShift1 = value;
                    OnPropertyChanged(nameof(RevenueShift1));
                }
            }
        }
        private decimal _revenueShift2;
        public decimal RevenueShift2
        {
            get => _revenueShift2;
            set
            {
                if (_revenueShift2 != value)
                {
                    _revenueShift2 = value;
                    OnPropertyChanged(nameof(RevenueShift2));
                }
            }
        }
        private decimal _revenueShift3;
        public decimal RevenueShift3
        {
            get => _revenueShift3;
            set
            {
                if (_revenueShift3 != value)
                {
                    _revenueShift3 = value;
                    OnPropertyChanged(nameof(RevenueShift3));
                }
            }
        }


        private int _totalOrderShift1;
        public int TotalOrderShift1
        {
            get => _totalOrderShift1;
            set
            {
                if (_totalOrderShift1 != value)
                {
                    _totalOrderShift1 = value;
                    OnPropertyChanged(nameof(TotalOrderShift1));
                }
            }
        }
        private int _totalOrderShift2;
        public int TotalOrderShift2
        {
            get => _totalOrderShift2;
            set
            {
                if (_totalOrderShift2 != value)
                {
                    _totalOrderShift2 = value;
                    OnPropertyChanged(nameof(TotalOrderShift2));
                }
            }
        }
        private int _totalOrderShift3;
        public int TotalOrderShift3
        {
            get => _totalOrderShift3;
            set
            {
                if (_totalOrderShift3 != value)
                {
                    _totalOrderShift3 = value;
                    OnPropertyChanged(nameof(TotalOrderShift3));
                }
            }
        }


        private string _statusShift1 = string.Empty;
        public string StatusShift1
        {
            get => _statusShift1;
            set
            {
                if (_statusShift1 != value)
                {
                    _statusShift1 = value;
                    OnPropertyChanged(nameof(StatusShift1));
                }
            }
        }
        private string _statusShift2 = string.Empty;
        public string StatusShift2
        {
            get => _statusShift2;
            set
            {
                if (_statusShift2 != value)
                {
                    _statusShift2 = value;
                    OnPropertyChanged(nameof(StatusShift2));
                }
            }
        }
        private string _statusShift3 = string.Empty;
        public string StatusShift3
        {
            get => _statusShift3;
            set
            {
                if (_statusShift3 != value)
                {
                    _statusShift3 = value;
                    OnPropertyChanged(nameof(StatusShift3));
                }
            }
        }


        private decimal _totalRevenueInShift;
        public decimal TotalRevenueInShift
        {
            get => _totalRevenueInShift;
            set
            {
                if (_totalRevenueInShift != value)
                {
                    _totalRevenueInShift = value;
                    OnPropertyChanged(nameof(TotalRevenueInShift));
                }
            }
        }

        private DateTime selectedDay = DateTime.Now;
        private int selectedShift;


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


        // MANAGER

        // LOAD ORDER THEO NGAY
        public async Task LoadOrdersByDate(DateTime date)
        {
            // RESET CAC BIEN
            Orders.Clear();
            OrderTam.Clear();

            selectedDay = date;

            TotalOrderDay = 0;
            TotalRevenueDay = 0;
            TotalDiscountDay = 0;
            TotalFinalDay = 0;

            RevenueShift1 = 0;
            RevenueShift2 = 0;
            RevenueShift3 = 0;

            TotalOrderShift1 = 0;
            TotalOrderShift2 = 0;
            TotalOrderShift3 = 0;

            StatusShift1 = string.Empty;
            StatusShift2 = string.Empty;
            StatusShift3 = string.Empty;

            TotalRevenueInShift = 0;

            StaffCa1.Clear();
            StaffCa2.Clear();
            StaffCa3.Clear();

            // DANH SACH ORDER
            var orders = await orderService.GetOrdersByDateAsync(date);
            foreach (var order in orders)
            {
                Orders.Add(order);

                // Tính tổng doanh thu, giảm giá, và số lượng đơn hàng trong ngày
                TotalOrderDay++;
                TotalRevenueDay += order.Amount;
                TotalDiscountDay += order.DiscountValue;
                TotalFinalDay += order.FinalAmount;

                // Tính doanh thu theo ca
                if (order.TimeAndDate.Hour < 12)
                {
                    RevenueShift1 += order.FinalAmount;
                    TotalOrderShift1++;
                }
                else if (order.TimeAndDate.Hour >= 12 && order.TimeAndDate.Hour < 18)
                {
                    RevenueShift2 += order.FinalAmount;
                    TotalOrderShift2++;
                }
                else
                {
                    RevenueShift3 += order.FinalAmount;
                    TotalOrderShift3++;
                }
                
            }

            // Tính trạng thái ca
            if (DateTime.Now.Date > selectedDay.Date)
            {
                StatusShift1 = "Đã kết thúc";
                StatusShift2 = "Đã kết thúc";
                StatusShift3 = "Đã kết thúc";
            }
            if (DateTime.Now.Date < selectedDay.Date)
            {
                StatusShift1 = "Chưa bắt đầu";
                StatusShift2 = "Chưa bắt đầu";
                StatusShift3 = "Chưa bắt đầu";
            }

            if (DateTime.Now.Hour < 12 && DateTime.Now.Day == selectedDay.Day && DateTime.Now.Month == selectedDay.Month && DateTime.Now.Year == selectedDay.Year)
            {
                StatusShift1 = "Đang làm việc";
                StatusShift2 = "Chưa bắt đầu";
                StatusShift3 = "Chưa bắt đầu";
            }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18 && DateTime.Now.Day == selectedDay.Day && DateTime.Now.Month == selectedDay.Month && DateTime.Now.Year == selectedDay.Year)
            {
                StatusShift1 = "Đã kết thúc";
                StatusShift2 = "Đang làm việc";
                StatusShift3 = "Chưa bắt đầu";
            }
            else if (DateTime.Now.Hour >= 18 && DateTime.Now.Day == selectedDay.Day && DateTime.Now.Month == selectedDay.Month && DateTime.Now.Year == selectedDay.Year)
            {
                StatusShift1 = "Đã kết thúc";
                StatusShift2 = "Đã kết thúc";
                StatusShift3 = "Đang làm việc";
            }

            // DANH SACH NHAN VIEN
            int idCa1 = 0, idCa2 = 0, idCa3 = 0;
            var dayShifts = await shiftService.GetShiftsByDateAsync(date);
            foreach (var shift in dayShifts)
            {
                if (shift.EndTime.Hour < 12) idCa1 = shift.Id;
                if (shift.StartTime.Hour >= 12 && shift.EndTime.Hour < 18) idCa2 = shift.Id;
                if (shift.StartTime.Hour >= 18) idCa3 = shift.Id;
            }

            var ca1 = await shiftStaffsService.GetShiftStaffsByShiftIdAsync(idCa1);
            foreach (var ca in ca1)
            {
                StaffCa1.Add(await staffService.GetStaffByIdAsync(ca.StaffId));
            }
            var ca2 = await shiftStaffsService.GetShiftStaffsByShiftIdAsync(idCa2);
            foreach (var ca in ca2)
            {
                StaffCa2.Add(await staffService.GetStaffByIdAsync(ca.StaffId));
            }
            var ca3 = await shiftStaffsService.GetShiftStaffsByShiftIdAsync(idCa3);
            foreach (var ca in ca3)
            {
                StaffCa3.Add(await staffService.GetStaffByIdAsync(ca.StaffId));
            }

        }
        // LOAD ORDER THEO CA
        public void LoadOrderByShift(int shiftIndex)
        {
            selectedShift = shiftIndex;

            OrderTam.Clear();
            TotalRevenueInShift = 0;

            foreach (var oreder in Orders)
            {
                if (selectedShift == 1)
                {
                    if (oreder.TimeAndDate.Hour < 12)
                    {
                        OrderTam.Add(oreder);
                        TotalRevenueInShift += oreder.FinalAmount;
                    }
                }
                else if (selectedShift == 2)
                {
                    if (oreder.TimeAndDate.Hour >= 12 && oreder.TimeAndDate.Hour < 18)
                    {
                        OrderTam.Add(oreder);
                        TotalRevenueInShift += oreder.FinalAmount;
                    }
                }
                else if (selectedShift == 3)
                {
                    if (oreder.TimeAndDate.Hour >= 18)
                    {
                        OrderTam.Add(oreder);
                        TotalRevenueInShift += oreder.FinalAmount;
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
