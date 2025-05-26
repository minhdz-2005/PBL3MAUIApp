using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Services;
using PBL3MAUIApp.Models;

namespace PBL3MAUIApp.ViewModels
{
    public class RevenueViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Order> Orders = new();
        public ObservableCollection<StaffAndRevenue> StaffAndRevenues { get; set; } = new();
        public ObservableCollection<ProductAndRevenue> ProductAndRevenues { get; set; } = new();
        public ObservableCollection<ProductAndRevenue> ProductAndRevenuesByProduct { get; set; } = new();
        public ObservableCollection<CategoryAndRevenue> CategoryAndRevenues { get; set; } = new();
        public ObservableCollection<VoucherAndDiscount> VoucherAndDiscounts { get; set; } = new();

        // TONG DOANH THU
        private decimal _total;
        public decimal Total
        {
            get => _total;
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }
        }
        private DateTime _bestDayRevenue;
        public DateTime BestDayRevenue
        {
            get => _bestDayRevenue;
            set
            {
                if (_bestDayRevenue != value)
                {
                    _bestDayRevenue = value;
                    OnPropertyChanged(nameof(BestDayRevenue));
                }
            }
        }



        private decimal _totalRevenue;
        public decimal TotalRevenue
        {
            get => _totalRevenue;
            set
            {
                if (_totalRevenue != value)
                {
                    _totalRevenue = value;
                    OnPropertyChanged(nameof(TotalRevenue));
                }
            }
        }
        
        private int _totalOrders;
        public int TotalOrders
        {
            get => _totalOrders;
            set
            {
                if (_totalOrders != value)
                {
                    _totalOrders = value;
                    OnPropertyChanged(nameof(TotalOrders));
                }
            }
        }
        private int _totalDiscounts;
        public int TotalDiscounts
        {
            get => _totalDiscounts;
            set
            {
                if (_totalDiscounts != value)
                {
                    _totalDiscounts = value;
                    OnPropertyChanged(nameof(TotalDiscounts));
                }
            }
        }
        
        
        private decimal _totalRevenueShift_1;
        public decimal TotalRevenueShift_1
        {
            get => _totalRevenueShift_1;
            set
            {
                if (_totalRevenueShift_1 != value)
                {
                    _totalRevenueShift_1 = value;
                    OnPropertyChanged(nameof(TotalRevenueShift_1));
                }
            }
        }

        private decimal _totalRevenueShift_2;
        public decimal TotalRevenueShift_2
        {
            get => _totalRevenueShift_2;
            set
            {
                if (_totalRevenueShift_2 != value)
                {
                    _totalRevenueShift_2 = value;
                    OnPropertyChanged(nameof(TotalRevenueShift_2));
                }
            }
        }
        
        private decimal _totalRevenueShift_3;
        public decimal TotalRevenueShift_3
        {
            get => _totalRevenueShift_3;
            set
            {
                if (_totalRevenueShift_3 != value)
                {
                    _totalRevenueShift_3 = value;
                    OnPropertyChanged(nameof(TotalRevenueShift_3));
                }
            }
        }


        // SERVICE
        private OrderService orderService = new OrderService();
        private StaffService staffService = new StaffService();
        private ProductService productService = new ProductService();
        private OrderDetailService orderDetailService = new OrderDetailService();
        private VoucherService voucherService = new VoucherService();


        // TINH TONG DOANH THU
        public async Task CalcTotalAndBestDay()
        {
            Total = 0;
            BestDayRevenue = DateTime.Now;
            // NGAY VA DOANH THU TAM DE TINH NGAY CAO NHAT
            decimal maxRevenue = 0;
            decimal revenueTam = 0;
            DateTime bestDayTam = DateTime.MinValue;

            // TONG HOP TU DAONH SACH ORDER
            var OrderList = await orderService.GetOrdersAsync();
            if (OrderList != null)
            {
                foreach (var Order in OrderList)
                {
                    // TINH TONG DOANH THU
                    Total += Order.FinalAmount;
                    // TIM NGAY DOANH THU CAO NHAT
                    if (bestDayTam != Order.TimeAndDate.Date)
                    {
                        if (revenueTam > maxRevenue)
                        {
                            maxRevenue = revenueTam;
                            BestDayRevenue = bestDayTam;
                        }
                        bestDayTam = Order.TimeAndDate.Date;
                        revenueTam = Order.FinalAmount;
                        // NEU LA ORDER CUOI CUNG THIF KIEM TRA LUON
                        if (Order == OrderList.Last())
                        {
                            if (revenueTam > maxRevenue)
                            {
                                maxRevenue = revenueTam;
                                BestDayRevenue = bestDayTam;
                            }
                        }
                    }
                    else
                    {
                        revenueTam += Order.FinalAmount;
                        // NEU LA ORDER CUOI CUNG THIF KIEM TRA LUON
                        if (Order == OrderList.Last())
                        {
                            if (revenueTam > maxRevenue)
                            {
                                maxRevenue = revenueTam;
                                BestDayRevenue = bestDayTam;
                            }
                        }
                    }
                }
            }
        }
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
                    // LUU VAO ORDERS
                    Orders.Add(Order);
                    // TINH TONG DOANH THU
                    TotalRevenue += Order.FinalAmount;
                    // TINH TONG SO DON HANG
                    TotalOrders++;
                    // TINH TONG SO GIAO DICH DUOC GIAM GIA
                    if (Order.DiscountValue > 0)
                    {
                        TotalDiscounts++;
                    }
                    // TINH DOANH THU THEO CA
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
        // LOC THEO NGAY BAT DAU VA NGAY KET THUC
        public async Task ByDayFiltering (DateTime fromDate, DateTime toDate)
        {
            Debug.WriteLine($"Filtering from {fromDate} to {toDate}");
            // KIEM TRA NGAY BAT DAU VA NGAY KET THUC
            if (fromDate > toDate)
            {
                await Shell.Current.DisplayAlert("Error", "Ngày bắt đầu không thể lớn hơn ngày kết thúc.", "OK");
                return;
            }
            
            // XOA CAC DOANH THU DE LOAD LAI
            Orders.Clear();
            StaffAndRevenues.Clear();
            ProductAndRevenues.Clear();
            CategoryAndRevenues.Clear();
            ProductAndRevenuesByProduct.Clear();
            VoucherAndDiscounts.Clear();

            TotalRevenue = 0;
            TotalOrders = 0;
            TotalDiscounts = 0;

            TotalRevenueShift_1 = 0;
            TotalRevenueShift_2 = 0;
            TotalRevenueShift_3 = 0;
            // TONG HOP TU DANH SACH ORDER
            var OrderList = await orderService.GetOrdersAsync();
            if (OrderList != null)
            {
                foreach (var Order in OrderList)
                {
                    if (Order.TimeAndDate >= fromDate && Order.TimeAndDate <= toDate)
                    {
                        // LUU VAO ORDERS
                        Orders.Add(Order);
                        // TINH TONG DOANH THU
                        TotalRevenue += Order.FinalAmount;
                        // TINH TONG SO DOWN HANG
                        TotalOrders++;
                        // TINH TONG SO GIAO DICH DUOC GIAM GIA
                        if (Order.DiscountValue > 0)
                        {
                            TotalDiscounts++;
                        }
                        // TINH DOANH THU THEO CA
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
            //Debug.WriteLine($"Total Revenue: {TotalRevenue}, Total Orders: {TotalOrders}, Total Discounts: {TotalDiscounts}");
            if (Orders != null)
            {
                foreach (var order in Orders)
                {
                    // DANH SACH NHAN VIEN VA DOANH THU
                    var staff = await staffService.GetStaffByIdAsync(order.StaffId);
                    if (staff != null)
                    {
                        // KIEM TRA CO NHAN VIEN DA TON TAI CHUA
                        var existingStaffRevenue = StaffAndRevenues.FirstOrDefault(sr => sr.StaffName == staff.Name);
                        if (existingStaffRevenue != null)
                        {
                            // NANG CAP DOANH THU CUA NHAN VIEN
                            existingStaffRevenue.Revenue += order.FinalAmount;
                        }
                        else
                        {
                            // THEM NHAN VIEN MOI VAO DANH SACH
                            StaffAndRevenues.Add(new StaffAndRevenue
                            {
                                StaffName = staff.Name,
                                Revenue = order.FinalAmount
                            });
                        }
                    }
                    
                    // DANH SACH SAN PHAM VA DOANH THU
                    var listOrderDetails = await orderDetailService.GetOrderDetailsAsync();
                    if (listOrderDetails != null)
                    {
                        foreach (var orderDetail in listOrderDetails)
                        {
                            if (orderDetail.OrderId == order.Id)
                            {
                                var product = await productService.GetProductByIdAsync(orderDetail.ProductId);
                                if (product != null)
                                {
                                    // KIEM TRA CO SAN PHAM DA TON TAI CHUA
                                    var existingProductRevenue = ProductAndRevenues.FirstOrDefault(pr => pr.ProductName == product.Name);
                                    if (existingProductRevenue != null)
                                    {
                                        // NANG CAP DOANH THU CUA SAN PHAM
                                        existingProductRevenue.Revenue += orderDetail.TotalPrice;
                                        // NANG CAP QUANTITY
                                        existingProductRevenue.Quantity += orderDetail.Quantity;
                                    }
                                    else
                                    {
                                        // THEM SAN PHAM MOI VAO DANH SACH
                                        ProductAndRevenues.Add(new ProductAndRevenue
                                        {
                                            ProductName = product.Name,
                                            Quantity = orderDetail.Quantity,
                                            Revenue = orderDetail.TotalPrice
                                        });
                                        ProductAndRevenuesByProduct.Add(new ProductAndRevenue
                                        {
                                            ProductName = product.Name,
                                            Quantity = orderDetail.Quantity,
                                            Revenue = orderDetail.TotalPrice
                                        });
                                    }
                                }
                            }
                        }
                    }
                    
                    // DANH SACH LOAI SAN PHAM VA DOANH THU
                    if (listOrderDetails != null)
                    {
                        foreach (var orderDetail in listOrderDetails)
                        {
                            if (orderDetail.OrderId == order.Id)
                            {
                                var product = await productService.GetProductByIdAsync(orderDetail.ProductId);
                                if (product != null)
                                {
                                    // KIEM TRA CO LOAI SAN PHAM DA TON TAI CHUA
                                    var existingCategoryRevenue = CategoryAndRevenues.FirstOrDefault(cr => cr.Category == product.Category);
                                    if (existingCategoryRevenue != null)
                                    {
                                        // NANG CAP DOANH THU CUA LOAI SAN PHAM
                                        existingCategoryRevenue.Quantity += orderDetail.Quantity;
                                        existingCategoryRevenue.Revenue += orderDetail.TotalPrice;
                                    }
                                    else
                                    {
                                        // THEM LOAI SAN PHAM MOI VAO DANH SACH
                                        CategoryAndRevenues.Add(new CategoryAndRevenue
                                        {
                                            Category = product.Category,
                                            Quantity = orderDetail.Quantity,
                                            Revenue = orderDetail.TotalPrice
                                        });
                                    }
                                }
                            }

                        }
                    }

                    // DANH SACH VOUCHER VA GIAM GIA
                    var voucher = await voucherService.GetVoucherByIdAsync(order.VoucherId);
                    if (voucher != null)
                    {
                        // KIEM TRA CO VOUCHER DA TON TAI CHUA
                        var existingVoucherDiscount = VoucherAndDiscounts.FirstOrDefault(vd => vd.VoucherCode == voucher.Code);
                        if (existingVoucherDiscount != null)
                        {
                            // NANG CAP GIAM GIA CUA VOUCHER
                            existingVoucherDiscount.DiscountAmount += order.DiscountValue;
                            existingVoucherDiscount.ApplyTimes += 1; // TANG SO LAN SU DUNG VOUCHER
                        }
                        else
                        {
                            // THEM VOUCHER MOI VAO DANH SACH
                            VoucherAndDiscounts.Add(new VoucherAndDiscount
                            {
                                VoucherCode = voucher.Name,
                                ApplyTimes = 1, // SO LAN SU DUNG VOUCHER
                                DiscountAmount = order.DiscountValue
                            });
                        }
                    }


                }
                // SAP SEP DANH SACH NHAN VIEN VA DOANH THU THEO DOANH THU GIAM DAN
                // Sắp xếp theo Revenue giảm dần
                var sortedList = StaffAndRevenues
                    .OrderByDescending(sr => sr.Revenue)
                    .ToList();

                // Xóa danh sách cũ và thêm lại theo thứ tự mới
                StaffAndRevenues.Clear();
                foreach (var item in sortedList)
                {
                    StaffAndRevenues.Add(item);
                    // CHI LAY 5 NHAN VIEN CO DOANH THU CAO NHAT
                    if (StaffAndRevenues.Count == 5)
                    {
                        break;
                    }
                }
                // SAP SEP DANH SACH SAN PHAM VA DOANH THU THEO DOANH THU GIAM DAN
                // Sắp xếp theo Revenue giảm dần
                var sortedProductList = ProductAndRevenues
                    .OrderByDescending(pr => pr.Revenue)
                    .ToList();
                // Xóa danh sách cũ và thêm lại theo thứ tự mới
                ProductAndRevenues.Clear();
                foreach (var item in sortedProductList)
                {
                    ProductAndRevenues.Add(item);
                    // CHI LAY 5 SAN PHAM CO DOANH THU CAO NHAT
                    if (ProductAndRevenues.Count == 5)
                    {
                        break;
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
