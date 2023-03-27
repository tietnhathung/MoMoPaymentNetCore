namespace WebApp.Models;

public class RefundViewModel
{
    public string OrderId { get; set; }
    public long Amount { get; set; }
    public long TransId { get; set; }
    public string? Description { get; set; }
}