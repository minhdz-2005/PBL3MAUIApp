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
                        await Shell.Current.DisplayAlert("Không thành công", "Mã ưu đãi đã tồn tại !", "OK");
                        return false;
                    }
                    if (start <  DateTime.Now)
                    {
                        await Shell.Current.DisplayAlert("Không thành công", "Ngày bắt đầu không được nhỏ hơn ngày hiện tại !", "OK");
                        return false;
                    }
                    if (end < DateTime.Now)
                    {
                        await Shell.Current.DisplayAlert("Không thành công", "Ngày kết thúc không được nhỏ hơn ngày hiện tại !", "OK");
                        return false;
                    }
                    if (start > end)
                    {
                        await Shell.Current.DisplayAlert("Không thành công", "Ngày bắt đầu không được lớn hơn ngày kết thúc !", "OK");
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
                await Shell.Current.DisplayAlert("Thông báo", "Giá trị giảm giá không hợp lệ", "OK");
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
            await Shell.Current.DisplayAlert("Thành công", "Thêm ưu đãi thành công !", "OK");

            return true;
        }
        // SUA UU DAI
        public async Task UpdateVoucher(int id, string name, string code, string description, string discountValueText, DateTime start, DateTime end)
        {
            // KIEM TRA UU DAI DA QUA HAY CHUA
            var v = await voucherService.GetVoucherByIdAsync(id);
            if (v!= null && v.EndDate < DateTime.Now)
            {
                await Shell.Current.DisplayAlert("Không thành công", "Không thể sửa ưu đãi đã kết thúc !", "OK");
                return;
            }
            // KIEM TRA GIA TRI DAU VAO
            foreach (var voucher in Vouchers)
            {
                if (voucher != null)
                {
                    if (voucher.Code == code && voucher.Id != id)
                    {
                        await Shell.Current.DisplayAlert("Không thành công", "Mã ưu đãi đã tồn tại !", "OK");
                        return;
                    }
                    if (start < DateTime.Now)
                    {
                        await Shell.Current.DisplayAlert("Không thành công", "Ngày bắt đầu không được nhỏ hơn ngày hiện tại !", "OK");
                        return;
                    }
                    if (end < DateTime.Now)
                    {
                        await Shell.Current.DisplayAlert("Không thành công", "Ngày kết thúc không được nhỏ hơn ngày hiện tại !", "OK");
                        return;
                    }
                    if (start > end)
                    {
                        await Shell.Current.DisplayAlert("Không thành công", "Ngày bắt đầu không được lớn hơn ngày kết thúc !", "OK");
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
                await Shell.Current.DisplayAlert("Thông báo", "Giá trị giảm giá không hợp lệ", "OK");
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
            await Shell.Current.DisplayAlert("Thành công", "Sửa ưu đãi thành công !", "OK");
        }
        // XOA UU DAI
        public async Task<bool> DeleteVoucher(int id)
        {
            var voucher = await voucherService.GetVoucherByIdAsync(id);

            if (voucher != null)
            {
                if (voucher.EndDate < DateTime.Now)
                {
                    await Shell.Current.DisplayAlert("Không thành công", "Không thể xóa ưu đãi đã kết thúc !", "OK");
                    return false;
                }

                bool confirm = await Shell.Current.DisplayAlert("Xác nhận", "Bạn có chắc chắn muốn xóa ưu đãi này?", "Có", "Không");
                if (confirm)
                {
                    await voucherService.DeleteVoucherAsync(id);
                    await GetAllVouchers(); // CẬP NHẬT DANH SÁCH VOUCHER TRÊN GIAO DIỆN
                    await Shell.Current.DisplayAlert("Thành công", "Xóa ưu đãi thành công !", "OK");

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
