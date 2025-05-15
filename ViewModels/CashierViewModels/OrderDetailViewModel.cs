using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using PBL3MAUIApp.Models;
using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels.CashierViewModels
{
    public class OrderDetailViewModel : INotifyPropertyChanged
    {
        // BIEN TAM THOI
        public ObservableCollection<OrderItemViewModel> OrderDetails { get; set; } = new();
        public ObservableCollection<Order> Orders { get; set; } = new();
        
        private Order order = new Order();

        static int countIdOrder = 0;

        private int _totalQuantity;
        public int TotalQuantity
        {
            get => _totalQuantity;
            set
            {
                if (_totalQuantity != value)
                {
                    _totalQuantity = value;
                    OnPropertyChanged(nameof(TotalQuantity));
                }
            }
        }

        private string _staffName = string.Empty;
        public string StaffName
        {
            get => _staffName;
            set
            {
                if (_staffName != value)
                {
                    _staffName = value;
                    OnPropertyChanged(nameof(StaffName));
                }
            }
        }
        // BIEN LUU TRU
        public ObservableCollection<Order> OrderQueue { get; set; } = new();
        public ObservableCollection<OrderItemViewModel> OrderDetailsQueue { get; set; } = new();

        // SERVICES
        private OrderService orderService = new OrderService();
        private OrderDetailService orderDetailService = new OrderDetailService();
        private ProductService productService = new ProductService();
        private StaffService staffService = new StaffService();

        private bool createOrder = false;


        // CHON SAN PHAM
        public void ChooseProduct(Product p)
        {
            // THEM ORDER VO LIST ORDER
            if(createOrder == false)
            {
                order.Id = ++countIdOrder;

                Orders.Add(order);
                createOrder = true;
            }

            var existing = OrderDetails.FirstOrDefault(x => x.Product.Id == p.Id);
            if (existing == null)
            {
                OrderDetails.Add(new OrderItemViewModel(new OrderDetail( countIdOrder, p.Id, 1, p.Price, null), p));

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
                    if(OrderDetails.Count == 0)
                    {
                        createOrder = false;
                        Orders.Clear();
                    }
                }
            }
        }
        // XOA HET QUANTITY
        public async void DeleteAllQuantity(OrderItemViewModel orderItem)
        {
            var item = OrderDetails.FirstOrDefault(x => x.OrderDetail.ProductId == orderItem.OrderDetail.ProductId);
            if (item != null)
            {
                while (item.Quantity > 0)
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
                        if (OrderDetails.Count == 0)
                        {
                            createOrder = false;
                            Orders.Clear();
                        }
                    }
                }
            }
        }

        // XAC NHAN ORDER
        public void ConfirmOrder()
        {
            // LUU VAO HANG CHO
            if (order.Amount > 0)
            {
                OrderQueue.Add(order);
            
                if (OrderDetails.Count > 0)
                {
                    foreach (var item in OrderDetails)
                    {
                        OrderDetailsQueue.Add(item);
                    }
                }
            }
            

            // XOA CAC BIEN TAM
            Orders.Clear();
            order = new Order();
            OrderDetails.Clear();
            createOrder = false;
        }
        // SHOW ORDER IN QUEUE
        public void ShowOrderQueue(int id)
        {
            // XOA BIEN TAM
            Orders.Clear();
            order = new Order();
            OrderDetails.Clear();
            createOrder = true;
            // LAY TU HANG CHO RA BIEN TAM
            foreach (var item in OrderQueue)
            {
                if(item.Id == id)
                {
                    order = item;
                    Orders.Add(item);
                }
            }
            foreach (var item in OrderDetailsQueue)
            {
                if(item.OrderDetail.OrderId == id)
                {
                    OrderDetails.Add(item);
                }
            }
        }
        // SAVE ORDER
        public void SaveOrderQueue(int id)
        {

            // XOA CAC BIEN TAM
            Orders.Clear();
            order = new Order();
            OrderDetails.Clear();
            createOrder = false;
        }

        // DELETE ORDER
        public void DeleteOrderQueue(int id)
        {
            // XOA BIEN TAM
            Orders.Clear();
            order = new Order();
            OrderDetails.Clear();
            createOrder = false;

            // XOA ORDER WUEUE
            foreach (var item in OrderQueue)
            {
                if (item.Id == id)
                {
                    OrderQueue.Remove(item);
                    break;
                }
            }
            // XOA ORDER DETAIL QUEUE
            foreach (var item in OrderDetailsQueue)
            {
                if (item.OrderDetail.OrderId == id)
                {
                    OrderDetailsQueue.Remove(item);
                    break;
                }
            }
        }

        // LUU NOTE
        public void SaveNote(OrderItemViewModel orderItem, string note)
        {
            var item = OrderDetails.FirstOrDefault(x => x.OrderDetail.ProductId == orderItem.OrderDetail.ProductId);
            if (item != null)
            {
                item.OrderDetail.Note = note;
            }
        }






        // Xem ORDER trang DS ORDER

        public async Task ViewOrder(int id)
        {
            // XOA BIEN TAM
            Orders.Clear();
            order = new Order();
            OrderDetails.Clear();
            createOrder = true;

            TotalQuantity = 0;

            // LOC ORDER RA BIEN TAM
            var existing = await orderService.GetOrderByIdAsync(id);
            if(existing != null)
            {
                order = existing;
                Orders.Add(order);
            }

            var ListOrderDetail = await orderDetailService.GetOrderDetailsAsync();
            foreach (var item in ListOrderDetail)
            {
                var p = await productService.GetProductByIdAsync(item.ProductId);
                if (p != null)
                {
                    OrderDetails.Add(new OrderItemViewModel(item, p));
                }
                TotalQuantity += item.Quantity;
            }
            var staff = await staffService.GetStaffByIdAsync(order.StaffId);
            if(staff != null)
            {
                Debug.WriteLine("hihi");
                StaffName = staff.Name;
            }

            Debug.WriteLine($"Total: {TotalQuantity}");
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
