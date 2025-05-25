using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3MAUIApp.ViewModels.CashierViewModels
{
    public class StaffAndRevenue : INotifyPropertyChanged
    {
        private string _staffName = String.Empty;
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
        private decimal _revenue = 0;
        public decimal Revenue
        {
            get => _revenue;
            set
            {
                if (_revenue != value)
                {
                    _revenue = value;
                    OnPropertyChanged(nameof(Revenue));
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
