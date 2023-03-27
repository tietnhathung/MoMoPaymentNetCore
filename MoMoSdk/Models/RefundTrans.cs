using MoMoSdk.Enums;

namespace MoMoSdk.Models;

public class RefundTrans
{
    public string OrderId { set; get; }
    public long Amount { set; get; }
    public MoMoResultCode ResultCode { set; get; }
    public long TransId { set; get; }
    public long CreatedTime { set; get; }
}