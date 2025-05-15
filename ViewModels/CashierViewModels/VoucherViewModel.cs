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
        public ObservableCollection<Voucher> VoucherList { get; set; } = new();
        private VoucherService voucherService = new VoucherService();

        // GET VOUCHER
        public async Task GetAllVouchers()
        {
            Vouchers.Clear();
            VoucherList.Clear();
            var vouchers = await voucherService.GetVouchersAsync();
            foreach (var voucher in vouchers)
            {
                Vouchers.Add(voucher);
                VoucherList.Add(voucher);
            }
        }

        // GET VOUCHER BY ID
        public async Task GetVoucherById(int id)
        {
            Vouchers.Clear();
            var voucher = await voucherService.GetVoucherByIdAsync(id);
            if (voucher != null)
            {
                Vouchers.Add(voucher);
            }
        }

        // FILTER VOUCHER
        public async void FilterVouchers(string filter)
        {
            var listVoucher = await voucherService.GetVouchersAsync(); // DANH SACH VOUCHER LUU TRU TAM THOI
            Vouchers.Clear(); // XOA DANH SACH VOUCHER HIEN TAI TRONG UI
            VoucherList.Clear(); // XOA DANH SACH VOUCHER HIEN TAI TRONG UI

            if (filter == "DangDienRa")
            {
                foreach (var voucher in listVoucher)
                {
                    if (voucher.StartDate <= DateTime.Now && voucher.EndDate >= DateTime.Now)
                    {
                        Vouchers.Add(voucher);
                        VoucherList.Add(voucher);
                    }
                }
            }
            if(filter == "SapToi")
            {
                foreach (var voucher in listVoucher)
                {
                    if (voucher.StartDate > DateTime.Now)
                    {
                        Vouchers.Add(voucher);
                        VoucherList.Add(voucher);
                    }
                }
            }
            if (filter == "DaKetThuc")
            {
                foreach (var voucher in listVoucher)
                {
                    if (voucher.EndDate < DateTime.Now)
                    {
                        Vouchers.Add(voucher);
                        VoucherList.Add(voucher);
                    }
                }
            }
        }
        
        // APPLY VOUCHER ORDERPAGE
        public async Task ApplyVoucher(int id)
        {
            var voucher = await voucherService.GetVoucherByIdAsync(id);
            if (voucher != null)
            {
                Vouchers.Clear();
                Vouchers.Add(voucher);
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
