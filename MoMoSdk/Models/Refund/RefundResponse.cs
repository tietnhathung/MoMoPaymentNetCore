using MoMoSdk.Enums;

namespace MoMoSdk.Models.Refund;

public class RefundResponse:MoMoResponse
{
    public long Amount { get; set; }
    public long TransId { get; set; }
    public List<TransGroupResponse> TransGroup{ set; get; }
}