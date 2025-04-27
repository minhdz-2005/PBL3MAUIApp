
namespace PBL3MAUIApp.Models;
public class Order {
    public int Id { get; set; }
    public int StaffId { get; set; }
    public int CustomerId { get; set; }
    public int ShiftId { get; set; }
    public int VoucherId { get; set; }
    public DateTime TimeAndDate = DateTime.Now;
    public decimal Amount { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal FinalAmount  {get; set; }
    public bool Status { get; set; }

    public Order () {}
    public Order (int staffId, int customerId, int shiftId, int voucherId, DateTime timeAndDate, decimal amount, decimal discountValue, bool status) {
        StaffId = staffId;
        CustomerId = customerId;
        ShiftId = shiftId;
        VoucherId = voucherId;
        TimeAndDate = timeAndDate;
        Amount = amount;
        DiscountValue = discountValue;
        FinalAmount = Amount - DiscountValue;
        Status = status;
    }
}