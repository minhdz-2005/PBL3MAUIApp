
namespace PBL3MAUIApp.Models;
public class Voucher {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Status { get; set; }
    public decimal DiscountValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Voucher () {}
    public Voucher (string name, string description, string code, bool status, decimal discountValue, DateTime startDate, DateTime endDate) {
        Name = name;
        Status = status;
        DiscountValue = discountValue;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
        Code = code;
    }
}