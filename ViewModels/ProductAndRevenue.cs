using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3MAUIApp.ViewModels
{
    public class ProductAndRevenue : INotifyPropertyChanged
    {
        private string _productName = String.Empty;
        public string ProductName
        {
            get => _productName;
            set
            {
                if (_productName != value)
                {
                    _productName = value;
                    OnPropertyChanged(nameof(ProductName));
                }
            }
        }
        public int _quantity = 0;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
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
