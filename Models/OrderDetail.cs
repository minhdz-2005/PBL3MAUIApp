
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
    private decimal totalPrice;
    public decimal TotalPrice 
    {
        get => totalPrice;
        set
        {
            if(totalPrice != value)
            {
                totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
    }
    private string? note;
    public string? Note
    {
        get => note;
        set
        {
            if (note != value)
            {
                note = value;
                OnPropertyChanged(nameof(Note));
            }
        }
    }

    public OrderDetail () {}
    public OrderDetail (int orderId ,int productId, int quantity, decimal totalPrice, string? note) {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        TotalPrice = totalPrice;
        Note = note;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}