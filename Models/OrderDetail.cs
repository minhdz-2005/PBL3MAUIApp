
namespace PBL3MAUIApp.Models;
public class OrderDetail {
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public OrderDetail () {}
    public OrderDetail (int productId, int quantity, decimal totalPrice) {
        ProductId = productId;
        Quantity = quantity;
        TotalPrice = totalPrice;
    }
}