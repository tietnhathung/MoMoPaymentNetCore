using System.Text.Json.Serialization;
using MoMoSdk.Enums;

namespace MoMoSdk.Models;

public abstract class MoMoRequest
{
    public string PartnerCode { get; set; }
    public string RequestId { get; set; }
    public string OrderId { get; set; }
 
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MoMoLang Lang { get; set; }
    public string Signature { get; set; }
}