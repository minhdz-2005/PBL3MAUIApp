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


        public CashierViewModel()
        {
            ProductVM = new ProductViewModel();
        }
    }
}
