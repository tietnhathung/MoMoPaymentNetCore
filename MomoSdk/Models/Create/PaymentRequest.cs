using System.Text.Json.Serialization;
using MomoSdk.Enums;

namespace MomoSdk.Models;

public class PaymentRequest
{
    public string PartnerCode { get; set; }
    public string? SubPartnerCode { get; set; }
    public string? PartnerName { get; set; }
    public string? StoreId { get; set; }
    public string RequestId { get; set; }
    public long Amount { get; set; }
    public string OrderId { get; set; }
    public string OrderInfo { get; set; }
    public long? OrderGroupId { get; set; }
    public string RedirectUrl { get; set; }
    public string IpnUrl { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MomoRequestType RequestType { get; set; }
    public string ExtraData { get; set; }
    public List<Item>? Items { get; set; }
    public DeliveryInfo? DeliveryInfo { get; set; }
    public UserInfo? UserInfo { get; set; }
    public bool? AutoCapture { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MomoLang Lang { get; set; }
    public string Signature { get; set; }
}
// {
//     "partnerCode": "MOMO",
//     "partnerName" : "Test",
//     "storeId" : "MoMoTestStore",
//     "requestType": "captureWallet",
//     "ipnUrl": "https://momo.vn",
//     "redirectUrl": "https://momo.vn",
//     "orderId": "MM1540456472575",
//     "amount": 150000,
//     "lang":  "vi",
//     "orderInfo": "SDK team.",
//     "requestId": "MM1540456472575",
//     "extraData": "eyJ1c2VybmFtZSI6ICJtb21vIn0=",
//     "signature": "fd37abbee777e13eaa0d0690d184e4d7e2fb43977281ab0e20701721f07a0e07"
// }