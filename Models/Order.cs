
using System.ComponentModel;

namespace PBL3MAUIApp.Models;
public class Order : INotifyPropertyChanged
{
    public int Id { get; set; }
    public int StaffId { get; set; }
    public int CustomerId { get; set; }
    public int ShiftId { get; set; }
    public int VoucherId { get; set; }
    public DateTime TimeAndDate { get; set; } = DateTime.Now;
    private decimal _amount;
    public decimal Amount
    {
        get => _amount;
        set
        {
            if (_amount != value)
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
    }
    private decimal _discount;
    public decimal DiscountValue
    {
        get => _discount;
        set
        {
            if (_discount != value)
            {
                _discount = value;
                OnPropertyChanged(nameof(DiscountValue));
            }
        }
    }
    private decimal _final;
    public decimal FinalAmount
    {
        get => _final;
        set
        {
            if (_final != value)
            {
                _final = value;
                OnPropertyChanged(nameof(FinalAmount));
            }
        }
    }
    public bool Status { get; set; }

    public Order () {}
    public Order (int staffId, int customerId, int shiftId, int voucherId, decimal amount, decimal discountValue, bool status) {
        StaffId = staffId;
        CustomerId = customerId;
        ShiftId = shiftId;
        VoucherId = voucherId;
        Amount = amount;
        DiscountValue = discountValue;
        FinalAmount = Amount - DiscountValue;
        Status = status;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}