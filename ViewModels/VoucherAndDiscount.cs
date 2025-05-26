using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3MAUIApp.ViewModels
{
    public class VoucherAndDiscount : INotifyPropertyChanged
    {
        private string _voucherCode = string.Empty;
        public string VoucherCode
        {
            get => _voucherCode;
            set
            {
                if (_voucherCode != value)
                {
                    _voucherCode = value;
                    OnPropertyChanged(nameof(VoucherCode));
                }
            }
        }
        private int _applyTimes;
        public int ApplyTimes
        {
            get => _applyTimes;
            set
            {
                if (_applyTimes != value)
                {
                    _applyTimes = value;
                    OnPropertyChanged(nameof(ApplyTimes));
                }
            }
        }
        private decimal _discountAmount;
        public decimal DiscountAmount
        {
            get => _discountAmount;
            set
            {
                if (_discountAmount != value)
                {
                    _discountAmount = value;
                    OnPropertyChanged(nameof(DiscountAmount));
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
