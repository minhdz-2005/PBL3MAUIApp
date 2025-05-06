
namespace PBL3MAUIApp.Models;
public class Product {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Product () {}
    public Product(string name, decimal price, string category, string description)
    {
        Name = name;
        Price = price;
        Category = category;
        Description = description;
    }
}