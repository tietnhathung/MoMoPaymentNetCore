using System.Text.Json.Serialization;

namespace MoMoSdk.Models.Confirm;

public class ConfirmResponse:MoMoResponse
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ConfirmRequestType RequestType { set; get; }
    public long Amount { set; get; }
    public long TransId { set; get; }
}