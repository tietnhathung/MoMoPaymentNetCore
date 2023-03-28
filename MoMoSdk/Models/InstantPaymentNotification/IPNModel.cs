using System.Text.Json.Serialization;
using MoMoSdk.Enums;
using MoMoSdk.Utils;

namespace MoMoSdk.Models.InstantPaymentNotification;

public class IPNModel:MoMoResponse
{
    public long Amount { set; get; }
    public string OrderInfo { set; get; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MoMoOrderType OrderType { set; get; }
    public long TransId { set; get; }
    public string PayType { set; get; }
    public string ExtraData { set; get; }
    
    public List<IPNItem> Items {set; get;}
    public string Signature { set; get; }
}

// {"partnerCode":"MOMOIOLD20190129","orderId":"01234567890123451633504872421","requestId":"01234567890123451633504872421","amount":1000,
// "orderInfo":"Test Thue 1234556","orderType":"momo_wallet","transId":2588659987,"resultCode":0,"message":"Giao dịch thành công.",
// "payType":"qr","responseTime":1633504902954,"extraData":"eyJyZXN1bHRfbmFtZXNwYWNlIjoidW1hcmtldCIsImVycm9yIjoiIiwic3RhdGUiOjZ9",
// "signature":"90482b3881bdf863d5f61ace078921bbc6dbb58b2fded35261c71c9af3b1ce4f"}'