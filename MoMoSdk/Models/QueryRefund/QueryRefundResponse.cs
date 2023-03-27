using MoMoSdk.Enums;

namespace MoMoSdk.Models.QueryRefund;

public class QueryRefundResponse:MoMoResponse
{
    public List<RefundTrans> RefundTrans { get; set; }
    public List<QueryRefundItem> Items { get; set; }
}