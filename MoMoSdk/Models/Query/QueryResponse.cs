using MoMoSdk.Enums;

namespace MoMoSdk.Models.Query;

public class QueryResponse:MoMoResponse
{
    public string ExtraData { get; set; }
    public long Amount { get; set; }
    public long TransId { get; set; }
    public string PayType { get; set; }
    public List<RefundTrans> RefundTrans { get; set; }
}