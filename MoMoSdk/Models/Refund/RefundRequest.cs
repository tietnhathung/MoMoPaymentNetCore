using System.Text.Json.Serialization;
using MoMoSdk.Enums;

namespace MoMoSdk.Models.Refund;

public class RefundRequest:MoMoRequest
{
    public long Amount { get; set; }
    public long TransId { get; set; }
    public string Description { get; set; }
    public List<TransGroup> TransGroup { get; set; }
}