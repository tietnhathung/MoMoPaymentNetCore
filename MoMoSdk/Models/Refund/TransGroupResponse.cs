using MoMoSdk.Enums;

namespace MoMoSdk.Models.Refund;

public class TransGroupResponse:TransGroup
{
    public MoMoResultCode ResultCode { get; set; }
    public string Message { get; set; }
}