using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels.ManagerViewModels
{
    public class ManagerViewModel
    {
        public AccountViewModel AccountVM { get; set; }
        public ProductViewModel OrderVM { get; set; }
        public ProductViewModel ProductVM { get; set; }
        public ProductViewModel PromotionVM { get; set; }
        public RevenueViewModel RevenueVM { get; set; }
        public ShiftViewModel ShiftVM { get; set; }
        public StaffViewModel StaffVM { get; set; }

        public ManagerViewModel ()
        {
            AccountVM = new AccountViewModel ();
            OrderVM = new ProductViewModel();
            ProductVM = new ProductViewModel();
            PromotionVM = new ProductViewModel();
            RevenueVM = new RevenueViewModel();
            ShiftVM = new ShiftViewModel();
            StaffVM = new StaffViewModel();
        }
    }
}