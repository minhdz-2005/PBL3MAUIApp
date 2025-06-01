using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PBL3MAUIApp.Models;
using PBL3MAUIApp.Services;

namespace PBL3MAUIApp.ViewModels
{
    public class VoucherViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Voucher> Vouchers { get; set; } = new();
        public ObservableCollection<Voucher> VoucherList { get; set; } = new();
        private VoucherService voucherService = new VoucherService();
        private OrderService orderService = new OrderService();

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
            foreach (var voucher in Vouchers)
            {
                if (voucher.StartDate <= DateTime.Now && voucher.EndDate >= DateTime.Now)
                {
                    voucher.Status = true; // Dang dien ra
                }
                else if (voucher.StartDate > DateTime.Now)
                {
                    voucher.Status = false; // Sap toi
                }
                else if (voucher.EndDate < DateTime.Now)
                {
                    voucher.Status = false; // Da ket thuc
                }
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
        // GET VOUCHER BY NAME
        public async Task GetVoucherByName(string name)
        {
            Vouchers.Clear();
            var vouchers = await voucherService.GetVouchersAsync();
            foreach (var voucher in vouchers)
            {
                if (voucher.Name.ToLower().Contains(name.ToLower()))
                {
                    Vouchers.Add(voucher);
                }
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
        
        
        // MANAGER
        
        // THEM UU DAI
        public async Task<bool> AddVoucher(string name, string code, string description, string discountValueText, DateTime start, DateTime end)
        {
            // KIREM TRA GIA TRI DAU VAO
            foreach(var voucher in Vouchers)
            {
                if (voucher != null)
                {
                    if (voucher.Code == code)
                    {
                        await Shell.Current.DisplayAlert("Error", "Voucher code already exists !", "OK");
                        return false;
                    }
                    if (start <  DateTime.Now)
                    {
                        await Shell.Current.DisplayAlert("Error", "Start date cannot be less than current date !", "OK");
                        return false;
                    }
                    if (end < DateTime.Now)
                    {
                        await Shell.Current.DisplayAlert("Error", "End date cannot be less than current date !", "OK");
                        return false;
                    }
                    if (start > end)
                    {
                        await Shell.Current.DisplayAlert("Error", "Start date cannot be greater than end date !", "OK");
                        return false;
                    }
                }
            }

            // CHUYEN DANG STRING SANG DECIMAL
            decimal discountValue;
            bool isValid = decimal.TryParse(
                discountValueText,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out discountValue
            );
            if (!isValid || discountValue < 0)
            {
                await Shell.Current.DisplayAlert("Error", "Invalid Discount value", "OK");
                return false;
            }

            // TAO UU DAI
            Voucher newVoucher = new Voucher
            {
                Name = name,
                Code = code,
                Description = description,
                Status = false,
                DiscountValue = discountValue,
                StartDate = start,
                EndDate = end
            };

            // THEM UU DAI VAO DANH SACH
            await voucherService.AddVoucherAsync(newVoucher);
            await GetAllVouchers(); // CẬP NHẬT DANH SÁCH VOUCHER TRÊN GIAO DIỆN
            await Shell.Current.DisplayAlert("Success", "Add voucher successfully !", "OK");

            return true;
        }
        // SUA UU DAI
        public async Task UpdateVoucher(int id, string name, string code, string description, string discountValueText, DateTime start, DateTime end)
        {
            // KIEM TRA UU DAI DA QUA HAY CHUA
            var v = await voucherService.GetVoucherByIdAsync(id);
            if (v!= null && v.EndDate < DateTime.Now)
            {
                await Shell.Current.DisplayAlert("Error", "Cannot edit expired voucher !", "OK");
                return;
            }
            // KIEM TRA GIA TRI DAU VAO
            foreach (var voucher in Vouchers)
            {
                if (voucher != null)
                {
                    if (voucher.Code == code && voucher.Id != id)
                    {
                        await Shell.Current.DisplayAlert("Error", "Voucher code already exists !", "OK");
                        return;
                    }
                    if (start < DateTime.Now)
                    {
                        await Shell.Current.DisplayAlert("Error", "Start date cannot be less than current date !", "OK");
                        return;
                    }
                    if (end < DateTime.Now)
                    {
                        await Shell.Current.DisplayAlert("Error", "End date cannot be less than current date !", "OK");
                        return;
                    }
                    if (start > end)
                    {
                        await Shell.Current.DisplayAlert("Error", "Start date cannot be greater than end date !", "OK");
                        return;
                    }
                }
            }
            // CHUYEN DANG STRING SANG DECIMAL
            decimal discountValue;
            bool isValid = decimal.TryParse(
                discountValueText,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out discountValue
            );
            if (!isValid || discountValue < 0)
            {
                await Shell.Current.DisplayAlert("Error", "Invalid Discount value", "OK");
                return;
            }
            // TAO UU DAI
            Voucher updatedVoucher = new Voucher
            {
                Id = id,
                Name = name,
                Code = code,
                Description = description,
                Status = false,
                DiscountValue = discountValue,
                StartDate = start,
                EndDate = end
            };
            // SUA UU DAI VAO DANH SACH
            await voucherService.UpdateVoucherAsync(id, updatedVoucher);
            await GetAllVouchers(); // CẬP NHẬT DANH SÁCH VOUCHER TRÊN GIAO DIỆN
            await Shell.Current.DisplayAlert("Success", "Edit voucher successful !", "OK");
        }
        // XOA UU DAI
        public async Task<bool> DeleteVoucher(int id)
        {
            var voucher = await voucherService.GetVoucherByIdAsync(id);

            if (voucher != null)
            {
                if (voucher.EndDate < DateTime.Now)
                {
                    await Shell.Current.DisplayAlert("Error", "Cannot delete expired voucher !", "OK");
                    return false;
                }

                bool confirm = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to delete this voucher?", "Yes", "No");
                if (confirm)
                {
                    var listOrder = await orderService.GetOrdersAsync();
                    // KIEM TRA UU DAI DA DUOC SU DUNG TRONG DON HANG CHUA
                    foreach (var order in listOrder)
                    {
                        if (order.VoucherId == id)
                        {
                            await Shell.Current.DisplayAlert("Error", "Cannot delete voucher that has been used in an order !", "OK");
                            return false;
                        }
                    }
                    // XOA UU DAI
                    await voucherService.DeleteVoucherAsync(id);
                    await GetAllVouchers(); // CẬP NHẬT DANH SÁCH VOUCHER TRÊN GIAO DIỆN
                    await Shell.Current.DisplayAlert("Success", "Voucher deleted successfully !", "OK");

                    return true;
                }
                return false;
            }
            else return false;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
