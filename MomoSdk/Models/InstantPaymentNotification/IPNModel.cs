using MomoSdk.Enums;

namespace MomoSdk.Models.InstantPaymentNotification;

public class IPNModel
{
    public string PartnerCode { set; get; }
    public string OrderId { set; get; }
    public string RequestId { set; get; }
    public long Amount { set; get; }
    public string OrderInfo { set; get; }
    public string OrderType { set; get; }
    public long TransId { set; get; }
    public MomoResultCode ResultCode { set; get; }
    public string Message { set; get; }
    public string PayType { set; get; }
    public long ResponseTime { set; get; }
    public string ExtraData { set; get; }
    public string Signature { set; get; }
}

// {"partnerCode":"MOMOIOLD20190129","orderId":"01234567890123451633504872421","requestId":"01234567890123451633504872421","amount":1000,
// "orderInfo":"Test Thue 1234556","orderType":"momo_wallet","transId":2588659987,"resultCode":0,"message":"Giao dịch thành công.",
// "payType":"qr","responseTime":1633504902954,"extraData":"eyJyZXN1bHRfbmFtZXNwYWNlIjoidW1hcmtldCIsImVycm9yIjoiIiwic3RhdGUiOjZ9",
// "signature":"90482b3881bdf863d5f61ace078921bbc6dbb58b2fded35261c71c9af3b1ce4f"}'