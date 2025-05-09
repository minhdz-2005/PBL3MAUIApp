using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Models;
using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels.CashierViewModels
{
    public class VoucherViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Voucher> Vouchers { get; set; } = new();
        public VoucherService voucherService = new VoucherService();

        // GET VOUCHER
        public async Task GetAllVouchers()
        {
            Vouchers.Clear();
            var vouchers = await voucherService.GetVouchersAsync();
            foreach (var voucher in vouchers)
            {
                Vouchers.Add(voucher);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
