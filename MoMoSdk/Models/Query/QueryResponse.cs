using System.Text.Json.Serialization;
using MoMoSdk.Enums;
using MoMoSdk.Utils;

namespace MoMoSdk.Models.Query;

public class QueryResponse:MoMoResponse
{
    public string ExtraData { get; set; }
    public long Amount { get; set; }
    public long TransId { get; set; }
    public string PayType { set; get; }
    public List<RefundTrans> RefundTrans { get; set; }
}