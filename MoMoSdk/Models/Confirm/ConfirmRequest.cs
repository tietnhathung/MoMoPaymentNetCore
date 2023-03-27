using System.Text.Json.Serialization;

namespace MoMoSdk.Models.Confirm;

public class ConfirmRequest:MoMoRequest
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ConfirmRequestType RequestType { set; get; }
    public long Amount { set; get; }
    public string Description { set; get; }
}