namespace MoMoSdk.Models;

public class Item
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string Currency { get; set; }
    public int Quantity { get; set; }
    public long Price { get; set; }
    public long TotalAmount { get; set; }
    public long PurchaseAmount { get; set; }
    public string? Manufacturer { get; set; }
    public string? Unit { get; set; }
    public long? TaxAmount { get; set; }
}
// {
//     "id": "204727",  
//     "name": "YOMOST Bac Ha&Viet Quat 170ml",  
//     "description": "YOMOST Sua Chua Uong Bac Ha&Viet Quat 170ml/1 Hop",
//     "category": "beverage",
//     "imageUrl":"https://momo.vn/uploads/product1.jpg",
//     "manufacturer":"Vinamilk",
//     "price": 11000,               
//     "quantity": 5,
//     "unit":"hộp",
//     "totalPrice": 55000,
//     "taxAmount":"200"
// }