using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels
{
    public class CashierViewModel
    {
        public ProductViewModel ProductVM { get; set; }
        public OrderDetailViewModel OrderDetailVM { get; set; }
        public OrderViewModel OrderVM { get; set; }
        public VoucherViewModel VoucherVM { get; set; }
        public RevenueViewModel RevenueVM { get; set; }
        public AccountViewModel AccountVM { get; set; }
        public StaffViewModel StaffVM { get; set; }
        public ShiftViewModel ShiftVM { get; set; }

        public CashierViewModel()
        {
            ProductVM = new ProductViewModel();
            OrderDetailVM = new OrderDetailViewModel();
            OrderVM = new OrderViewModel();
            VoucherVM = new VoucherViewModel();
            RevenueVM = new RevenueViewModel();
            AccountVM = new AccountViewModel();
            StaffVM = new StaffViewModel();
            ShiftVM = new ShiftViewModel();
        }
    }
}
