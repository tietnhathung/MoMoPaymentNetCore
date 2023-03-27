using MoMoSdk.Enums;

namespace MoMoSdk.Models.InstantPaymentNotification;

public class IPNItem
{
    public string OrderId { set; get; }
    public string ItemId { set; get; }
    public long TransId { set; get; }
    public long RefundTid { set; get; }
    public MoMoResultCode ResultCode { get; set; }
    public long Amount { get; set; }
}