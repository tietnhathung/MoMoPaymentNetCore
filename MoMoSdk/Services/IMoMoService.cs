using Microsoft.Extensions.Configuration;
using MoMoSdk.Enums;
using MoMoSdk.Models;
using MoMoSdk.Models.InstantPaymentNotification;
using MoMoSdk.Models.Query;
using MoMoSdk.Models.Redirect;
using MoMoSdk.Utils;

namespace MoMoSdk.Services;

public interface IMoMoService
{
    Task<PaymentResponse> CreatePayment(MoMoRequestType requestType,long amount,string orderId,string orderInfo,long? orderGroupId,List<Item>? items,DeliveryInfo? deliveryInfo,UserInfo? userInfo);
    Task<QueryResponse> QueryPayment(string orderId);
    bool CheckRedirectUrl(RedirectQuery query);
    bool CheckIPN(IPNModel query);
}

public class MoMoService : IMoMoService
{
    private readonly IConfigurationSection _configuration;
    private readonly IMoMoHttpClient _httpClient;

    private const string CreateUrl = "/v2/gateway/api/create";
    private const string QueryUrl = "/v2/gateway/api/query";

    public MoMoService(IConfiguration configuration, IMoMoHttpClient httpClient)
    {
        _httpClient = httpClient;
        _configuration = configuration.GetSection("MoMoPayment");
    }

    private PaymentRequest CreatePaymentRequest(MoMoRequestType requestType,long amount,string orderId,string orderInfo,long? orderGroupId,List<Item>? items,DeliveryInfo? deliveryInfo,UserInfo? userInfo)
    {
        var partnerCode = _configuration["PartnerCode"] ?? "";
        var partnerName = _configuration["PartnerName"] ?? "";
        var storeId = _configuration["StoreId"] ?? "";
        var redirectUrl = _configuration["RedirectUrl"] ?? "";
        var ipnUrl = _configuration["IpnUrl"] ?? "";
        var secretKey = _configuration["SecretKey"] ?? "";
        var accessKey = _configuration["AccessKey"] ?? "";
        var requestGuiId = Guid.NewGuid();
        var request = new PaymentRequest
        {
            Amount = amount,
            ExtraData = "",
            IpnUrl = ipnUrl,
            OrderId = orderId,
            OrderInfo = orderInfo,
            PartnerCode = partnerCode,
            RedirectUrl = redirectUrl,
            RequestId = requestGuiId.ToString(),
            RequestType = requestType,
            PartnerName = partnerName,
            StoreId = storeId,
            OrderGroupId = orderGroupId,
            Items = items,
            Lang = MoMoLang.vi
        };
        var signatureString = $"accessKey={accessKey}&amount={request.Amount}&extraData={request.ExtraData}&ipnUrl={request.IpnUrl}&orderId={request.OrderId}&orderInfo={request.OrderInfo}&partnerCode={request.PartnerCode}&redirectUrl={request.RedirectUrl}&requestId={request.RequestId}&requestType={request.RequestType}";
        request.Signature = HmacHelper.Compute(secretKey,signatureString);
        return request;
    }

    public async Task<PaymentResponse> CreatePayment(MoMoRequestType requestType,long amount,string orderId,string orderInfo,long? orderGroupId,List<Item>? items,DeliveryInfo? deliveryInfo,UserInfo? userInfo)
    {
        var paymentRequest = CreatePaymentRequest(requestType,amount,  orderId,  orderInfo, orderGroupId, items, deliveryInfo, userInfo);
        var paymentResponse = await _httpClient.Post<PaymentResponse>(CreateUrl,paymentRequest);
        return paymentResponse;
    }

    public async Task<QueryResponse> QueryPayment(string orderId)
    {
        var requestGuiId = Guid.NewGuid();
        var secretKey = _configuration["SecretKey"] ?? "";
        var partnerCode = _configuration["PartnerCode"] ?? "";
        var accessKey = _configuration["AccessKey"] ?? "";
        var request = new QueryRequest
        {
            OrderId = orderId,
            PartnerCode = partnerCode,
            Lang = MoMoLang.vi,
            RequestId = requestGuiId.ToString()
        };
        var signatureString = $"accessKey={accessKey}&orderId={request.OrderId}&partnerCode={request.PartnerCode}&requestId={request.RequestId}";
        request.Signature = HmacHelper.Compute(secretKey,signatureString);
        var paymentResponse = await _httpClient.Post<QueryResponse>(QueryUrl,request);
        return paymentResponse;
    }

    public bool CheckRedirectUrl(RedirectQuery query)
    {
        var secretKey = _configuration["SecretKey"] ?? "";
        var accessKey = _configuration["AccessKey"] ?? "";
        
        var signatureString = $"accessKey={accessKey}&amount={query.Amount}&extraData={query.ExtraData}&message={query.Message}&orderId={query.OrderId}&orderInfo={query.OrderInfo}&orderType={query.OrderType}&partnerCode={query.PartnerCode}&payType={query.PayType}&requestId={query.RequestId}&responseTime={query.ResponseTime}&resultCode={((int)query.ResultCode)}&transId={query.TransId}";
        var signature = HmacHelper.Compute(secretKey,signatureString);

        return signature.Equals(query.Signature);
    }

    public bool CheckIPN(IPNModel model)
    {
        var secretKey = _configuration["SecretKey"] ?? "";
        var accessKey = _configuration["AccessKey"] ?? "";
        
        var signatureString = $"accessKey={accessKey}&amount={model.Amount}&extraData={model.ExtraData}&message={model.Message}&orderId={model.OrderId}&orderInfo={model.OrderInfo}&orderType={model.OrderType}&partnerCode={model.PartnerCode}&payType={model.PayType}&requestId={model.RequestId}&responseTime={model.ResponseTime}&resultCode={((int)model.ResultCode)}&transId={model.TransId}";
        var signature = HmacHelper.Compute(secretKey,signatureString);

        return signature.Equals(model.Signature);
    }
}