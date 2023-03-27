using MoMoSdk.Enums;
using MoMoSdk.Models;

namespace WebApp.Models;

public class CreateOrderViewModel
{
    public MomoRequestType RequestType { get; set; }
    public string OrderId { get; set; }
    public string OrderInfo { get; set; }
    public long Amount { get; set; }
    
    public DeliveryInfo DeliveryInfo { get; set; }
    
    public UserInfo UserInfo { get; set; }
}