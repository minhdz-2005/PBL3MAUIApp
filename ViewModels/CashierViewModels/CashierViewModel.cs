using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3MAUIApp.ViewModels.CashierViewModels
{
    public class CashierViewModel
    {
        public ProductViewModel ProductVM { get; set; }
        public OrderDetailViewModel OrderDetailVM { get; set; }
        public OrderViewModel OrderVM { get; set; }
        public VoucherViewModel VoucherVM { get; set; }
        

        public CashierViewModel()
        {
            ProductVM = new ProductViewModel();
            OrderDetailVM = new OrderDetailViewModel();
            OrderVM = new OrderViewModel();
            VoucherVM = new VoucherViewModel();
        }
    }
}
