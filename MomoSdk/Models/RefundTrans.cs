namespace MomoSdk.Models;

public class RefundTrans
{
    public string OrderId { set; get; }
    public string Amount { set; get; }
    public string ResultCode { set; get; }
    public string TransId { set; get; }
    public string CreatedTime { set; get; }
}