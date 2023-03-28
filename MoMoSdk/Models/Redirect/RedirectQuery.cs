using System.Text.Json.Serialization;
using MoMoSdk.Enums;

namespace MoMoSdk.Models.Redirect;

public class RedirectQuery
{
    public string PartnerCode { set; get; }
    public string OrderId { set; get; }
    public string RequestId { set; get; }
    public long Amount { set; get; }
    public string OrderInfo { set; get; }
    public MoMoOrderType OrderType { set; get; }
    public long TransId { set; get; }
    public MoMoResultCode ResultCode { set; get; }
    public string Message { set; get; }
    public string PayType { set; get; }
    public long ResponseTime { set; get; }
    public string ExtraData { set; get; }
    public string Signature { set; get; }
}

//partnerCode=MOMOLRJZ20181206&orderId=17e1530d-3387-4edb-95e7-3d7c36fd9b5b&requestId=394c6fbb-a28c-4e4a-b16a-69db8ce680ed&amount=235000
//&orderInfo=Mua+tivi&orderType=momo_wallet&transId=1679649685462&resultCode=1006&message=Giao+dịch+bị+từ+chối+bởi+người+dùng.
//&payType=&responseTime=1679649685558&extraData=&signature=7cbe81ab010074511fd07e7632a47a3ab983c9112343cdeed1f3d2eeec5749a7