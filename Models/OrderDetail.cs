
using System.ComponentModel;

namespace PBL3MAUIApp.Models;
public class OrderDetail : INotifyPropertyChanged
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    private int _quantity;
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
    public decimal TotalPrice { get; set; }

    public OrderDetail () {}
    public OrderDetail (int productId, int quantity, decimal totalPrice) {
        ProductId = productId;
        Quantity = quantity;
        TotalPrice = totalPrice;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}