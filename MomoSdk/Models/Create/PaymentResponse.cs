using MomoSdk.Enums;

namespace MomoSdk.Models;

public class PaymentResponse
{
    public string PartnerCode { get; set; }
    public string OrderId { get; set; }
    public string RequestId { get; set; }
    public long Amount { get; set; }
    public long ResponseTime { get; set; }
    public string Message { get; set; }
    public MomoResultCode ResultCode { get; set; }
    public string PayUrl { get; set; }
    public string Deeplink { get; set; }
    public string QrCodeUrl { get; set; }
    public string Applink { get; set; }
    public string DeeplinkMiniApp { get; set; }
    public List<SubError> SubErrors { get; set; }
}

// success
// {
//     "partnerCode": "MOMOLRJZ20181206",
//     "orderId": "d60a2ffc-ba6e-4bdc-b5eb-1f897a765343",
//     "requestId": "b1992fd0-9559-45b8-b15f-2d679de082de",
//     "amount": 235000,
//     "responseTime": 1679644192569,
//     "message": "Thành công.",
//     "resultCode": 0,
//     "payUrl": "https://test-payment.momo.vn/v2/gateway/pay?t=TU9NT0xSSloyMDE4MTIwNnxkNjBhMmZmYy1iYTZlLTRiZGMtYjVlYi0xZjg5N2E3NjUzNDM",
//     "deeplink": "momo://app?action=payWithApp&isScanQR=false&serviceType=app&sid=TU9NT0xSSloyMDE4MTIwNnxkNjBhMmZmYy1iYTZlLTRiZGMtYjVlYi0xZjg5N2E3NjUzNDM&v=2.3",
//     "qrCodeUrl": "momo://app?action=payWithApp&isScanQR=true&serviceType=qr&sid=TU9NT0xSSloyMDE4MTIwNnxkNjBhMmZmYy1iYTZlLTRiZGMtYjVlYi0xZjg5N2E3NjUzNDM&v=2.3",
//     "applink": "https://page.dev.mservice.io/ONCYFTvtyBf",
//     "deeplinkMiniApp": "momo://app?action=payWithApp&isScanQR=false&serviceType=miniapp&sid=TU9NT0xSSloyMDE4MTIwNnxkNjBhMmZmYy1iYTZlLTRiZGMtYjVlYi0xZjg5N2E3NjUzNDM&v=2.3"
// }

//error
// {
//     "responseTime": 1679643167048,
//     "message": "Bad format request.",
//     "resultCode": 20,
//     "subErrors": [
//         {
//             "field": "orderInfo",
//             "message": "Invalid value. Field must not be blank"
//         },
//         {
//         "field": "ipnUrl",
//         "message": "Invalid value. Field must not be blank"
//         },
//         {
//             "field": "extraData",
//             "message": "Invalid value. Field must not be null"
//         },
//         {
//             "field": "redirectUrl",
//             "message": "Invalid value. Field must not be blank"
//         },
//         {
//             "field": "signature",
//             "message": "Invalid value. Field must not be blank"
//         },
//         {
//             "field": "partnerCode",
//             "message": "Invalid value. Field must not be blank"
//         },
//         {
//             "field": "requestId",
//             "message": "Invalid value. Field must not be blank"
//         },
//         {
//             "field": "requestType",
//             "message": "Invalid value. Field must not be null"
//         },
//         {
//             "field": "orderId",
//             "message": "Invalid value. Field must not be blank"
//         }
//     ]
// }