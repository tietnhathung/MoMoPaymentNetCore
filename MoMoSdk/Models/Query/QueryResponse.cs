using System.Text.Json.Serialization;
using MoMoSdk.Enums;

namespace MoMoSdk.Models.Query;

public class QueryResponse:MoMoResponse
{
    public string ExtraData { get; set; }
    public long Amount { get; set; }
    public long TransId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MoMoPayType PayType { set; get; }
    public List<RefundTrans> RefundTrans { get; set; }
}