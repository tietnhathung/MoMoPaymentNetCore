using MoMoSdk.Enums;

namespace MoMoSdk.Models.QueryRefund;

public class QueryRefundItem
{
    public string Id { set; get; }
    public MoMoResultCode ResultCode { set; get; }
    public string ResultDescription { set; get; }
    public long RefundCount { set; get; }
    public long RefundAmount { set; get; }
}

// {
//     "id": "1671179937SKU_2",
//     "resultCode": 0,
//     "resultDescription": "Successful.",
//     "refundCount": 0,
//     "refundAmount": 0
// }