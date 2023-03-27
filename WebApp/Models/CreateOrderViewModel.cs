using MoMoSdk.Enums;
using MoMoSdk.Models;
using MoMoSdk.Models.Create;

namespace WebApp.Models;

public class CreateOrderViewModel
{
    public MoMoRequestType RequestType { get; set; }
    public string OrderId { get; set; }
    public string OrderInfo { get; set; }
    public long Amount { get; set; }
    
    public DeliveryInfo DeliveryInfo { get; set; }
    
    public UserInfo UserInfo { get; set; }
}