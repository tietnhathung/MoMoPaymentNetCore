using MoMoSdk.Enums;

namespace MoMoSdk.Models.Query;

public class QueryResponse
{
    public string PartnerCode { get; set; }
    public string RequestId { get; set; }
    public string OrderId { get; set; }
    public string ExtraData { get; set; }
    public long Amount { get; set; }
    public long TransId { get; set; }
    public string PayType { get; set; }
    public MomoResultCode ResultCode { get; set; }
    public List<RefundTrans> RefundTrans { get; set; }
    public string Message { get; set; }
    public long ResponseTime { get; set; }
}