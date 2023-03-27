using System.Text.Json.Serialization;
using MomoSdk.Enums;

namespace MomoSdk.Models.Query;

public class QueryRequest
{
    public string PartnerCode { get; set; }
    public string RequestId { get; set; }
    public string OrderId { get; set; }
    public string Signature { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MomoLang Lang { get; set; }
}
// {
//     "partnerCode": "123456",
//     "requestId": "1527246504579",
//     "orderId": "1527246478428",
//     "signature": "13be80957a5ee32107198920fa26aa85a4ca238a29f46e292e8c33dd9186142a",
//     "lang": "en"
// }