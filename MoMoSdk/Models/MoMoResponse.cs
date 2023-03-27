using MoMoSdk.Enums;

namespace MoMoSdk.Models;

public abstract class MoMoResponse
{
    public string PartnerCode { get; set; }
    public string OrderId { get; set; }
    public string RequestId { get; set; }
    public string Message { get; set; }
    public MoMoResultCode ResultCode { get; set; }
    public long ResponseTime { get; set; }
}