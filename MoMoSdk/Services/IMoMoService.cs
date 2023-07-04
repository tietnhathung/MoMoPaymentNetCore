using Microsoft.Extensions.Configuration;
using MoMoSdk.Enums;
using MoMoSdk.Exceptions;
using MoMoSdk.Models;
using MoMoSdk.Models.Confirm;
using MoMoSdk.Models.Create;
using MoMoSdk.Models.InstantPaymentNotification;
using MoMoSdk.Models.Query;
using MoMoSdk.Models.QueryRefund;
using MoMoSdk.Models.Redirect;
using MoMoSdk.Models.Refund;
using MoMoSdk.Utils;

namespace MoMoSdk.Services;

public interface IMoMoService
{
    Task<PaymentResponse> CreatePayment(PaymentRequest request);
    Task<PaymentResponse> CreatePayment(MoMoRequestType requestType,string orderId,long amount,string orderInfo);
    Task<PaymentResponse> CreatePayment(MoMoRequestType requestType,string orderId,long amount,string orderInfo,string extraData,List<Item> items,DeliveryInfo deliveryInfo,UserInfo userInfo);
    Task<PaymentResponse> CreatePayment(MoMoRequestType requestType,string orderId,long amount,string orderInfo,string? extraData,List<Item>? items,DeliveryInfo? deliveryInfo,UserInfo? userInfo,long? orderGroupId);
    Task<QueryResponse> QueryPayment(QueryRequest request);
    Task<QueryResponse> QueryPayment(string orderId);
    
    Task<ConfirmResponse> ConfirmPayment(ConfirmRequest request);
    Task<ConfirmResponse> ConfirmPayment(string orderId,ConfirmRequestType requestType,long amount,string? description);
    Task<RefundResponse> CreateRefund(RefundRequest request);
    Task<RefundResponse> CreateRefund(string orderId,long amount,long transId,string? description);
    
    Task<QueryRefundResponse> QueryRefund(QueryRefundRequest request);
    Task<QueryRefundResponse> QueryRefund(string orderId);
    bool CheckRedirectUrl(RedirectQuery query);
    bool CheckIPN(IPNModel query);
}

public class MoMoService : IMoMoService
{
    private readonly IConfigurationSection _configuration;
    private readonly IMoMoHttpClient _httpClient;

    private const string CreateUrl = "/v2/gateway/api/create";
    private const string QueryUrl = "/v2/gateway/api/query";
    private const string ConfirmUrl = "/v2/gateway/api/confirm";
    private const string RefundUrl = "/v2/gateway/api/refund";
    private const string QueryRefundUrl = "/v2/gateway/api/refund/query";

    public MoMoService(IConfiguration configuration, IMoMoHttpClient httpClient)
    {
        _httpClient = httpClient;
        _configuration = configuration.GetSection("MoMoPayment");
    }

    public async Task<PaymentResponse> CreatePayment(PaymentRequest request)
    {
        try
        {
            var paymentResponse = await _httpClient.Post<PaymentResponse>(CreateUrl,request);
            return paymentResponse;
        }
        catch (Exception e)
        {
            throw new MoMoException(e.Message,e);
        }
    }

    public Task<PaymentResponse> CreatePayment(MoMoRequestType requestType, string orderId, long amount, string orderInfo)
    {
        return CreatePayment(requestType, orderId, amount, orderInfo, null, null, null, null, null);
    }

    public Task<PaymentResponse> CreatePayment(MoMoRequestType requestType, string orderId, long amount, string orderInfo, string extraData, List<Item> items, DeliveryInfo deliveryInfo, UserInfo userInfo)
    {
        return CreatePayment(requestType, orderId, amount, orderInfo, extraData, items, deliveryInfo, userInfo, null);
    }

    public async Task<PaymentResponse> CreatePayment(MoMoRequestType requestType,string orderId,long amount,string orderInfo,string? extraData,List<Item>? items,DeliveryInfo? deliveryInfo,UserInfo? userInfo,long? orderGroupId)
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
            ExtraData = extraData ?? "",
            IpnUrl = ipnUrl,
            OrderId = orderId,
            OrderInfo = orderInfo,
            PartnerCode = partnerCode,
            RedirectUrl = redirectUrl,
            RequestId = requestGuiId.ToString(),
            RequestType = requestType,
            PartnerName = partnerName,
            StoreId = storeId,
            DeliveryInfo = deliveryInfo,
            UserInfo = userInfo,
            OrderGroupId = orderGroupId,
            Items = items,
            Lang = MoMoLang.vi
        };
        var signatureString = $"accessKey={accessKey}&amount={request.Amount}&extraData={request.ExtraData}&ipnUrl={request.IpnUrl}&orderId={request.OrderId}&orderInfo={request.OrderInfo}&partnerCode={request.PartnerCode}&redirectUrl={request.RedirectUrl}&requestId={request.RequestId}&requestType={request.RequestType}";
        request.Signature = MoMoCodeHelper.Hmac(secretKey,signatureString);
        return await CreatePayment(request);
    }

    public async Task<QueryResponse> QueryPayment(QueryRequest request)
    {
        try
        {
            var paymentResponse = await _httpClient.Post<QueryResponse>(QueryUrl,request);
            return paymentResponse;
        }
        catch (Exception e)
        {
            throw new MoMoException(e.Message,e);
        }
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
        request.Signature = MoMoCodeHelper.Hmac(secretKey,signatureString);
        return await QueryPayment(request);
    }

    public async Task<ConfirmResponse> ConfirmPayment(ConfirmRequest request)
    {
        try
        {
            var confirmResponse = await _httpClient.Post<ConfirmResponse>(ConfirmUrl,request);
            return confirmResponse;
        }
        catch (Exception e)
        {
            throw new MoMoException(e.Message,e);
        }
    }

    public async Task<ConfirmResponse> ConfirmPayment(string orderId, ConfirmRequestType requestType, long amount,string? description)
    {
        var requestGuiId = Guid.NewGuid();
        var secretKey = _configuration["SecretKey"] ?? "";
        var partnerCode = _configuration["PartnerCode"] ?? "";
        var accessKey = _configuration["AccessKey"] ?? "";
        ConfirmRequest request = new ConfirmRequest()
        {
            PartnerCode = partnerCode,
            OrderId = orderId,
            RequestType = requestType,
            RequestId =  requestGuiId.ToString(),
            Amount = amount,
            Lang = MoMoLang.vi,
            Description = description ?? ""
        };
        var signatureString = $"accessKey={accessKey}&amount={request.Amount}&description={request.Description}&orderId={request.OrderId}&partnerCode={request.PartnerCode}&requestId={request.RequestId}&requestType={request.RequestType}";
        request.Signature = MoMoCodeHelper.Hmac(secretKey,signatureString);
        return await ConfirmPayment(request);
    }

    public async Task<RefundResponse> CreateRefund(RefundRequest request)
    {
        try
        {
            var paymentResponse = await _httpClient.Post<RefundResponse>(RefundUrl,request);
            return paymentResponse;
        }
        catch (Exception e)
        {
            throw new MoMoException(e.Message,e);
        }
        
    }

    public async Task<RefundResponse> CreateRefund(string orderId, long amount, long transId, string? description)
    {
        var requestGuiId = Guid.NewGuid();
        var secretKey = _configuration["SecretKey"] ?? "";
        var partnerCode = _configuration["PartnerCode"] ?? "";
        var accessKey = _configuration["AccessKey"] ?? "";
        RefundRequest request = new RefundRequest();
        request.PartnerCode = partnerCode;
        request.OrderId = orderId;
        request.RequestId = requestGuiId.ToString();
        request.Amount = amount;
        request.TransId = transId;
        request.Lang = MoMoLang.vi;
        request.Description = description ?? "";
        var signatureString = $"accessKey={accessKey}&amount={request.Amount}&description={request.Description}&orderId={request.OrderId}&partnerCode={request.PartnerCode}&requestId={request.RequestId}&transId={request.TransId}";
        request.Signature = MoMoCodeHelper.Hmac(secretKey,signatureString);
        return await CreateRefund(request);
    }

    public async Task<QueryRefundResponse> QueryRefund(QueryRefundRequest request)
    {
        try
        {
            var paymentResponse = await _httpClient.Post<QueryRefundResponse>(QueryRefundUrl,request);
            return paymentResponse;
        }
        catch (Exception e)
        {
            throw new MoMoException(e.Message,e);
        }
    }

    public async Task<QueryRefundResponse> QueryRefund(string orderId)
    {
        var requestGuiId = Guid.NewGuid();
        var secretKey = _configuration["SecretKey"] ?? "";
        var partnerCode = _configuration["PartnerCode"] ?? "";
        var accessKey = _configuration["AccessKey"] ?? "";
        var request = new QueryRefundRequest()
        {
            OrderId = orderId,
            PartnerCode = partnerCode,
            Lang = MoMoLang.vi,
            RequestId = requestGuiId.ToString()
        };
        var signatureString = $"accessKey={accessKey}&orderId={request.OrderId}&partnerCode={request.PartnerCode}&requestId={request.RequestId}";
        request.Signature = MoMoCodeHelper.Hmac(secretKey,signatureString);
        return await QueryRefund(request);
    }

    public bool CheckRedirectUrl(RedirectQuery query)
    {
        var secretKey = _configuration["SecretKey"] ?? "";
        var accessKey = _configuration["AccessKey"] ?? "";
        
        var signatureString = $"accessKey={accessKey}&amount={query.Amount}&extraData={query.ExtraData}&message={query.Message}&orderId={query.OrderId}&orderInfo={query.OrderInfo}&orderType={query.OrderType}&partnerCode={query.PartnerCode}&payType={query.PayType}&requestId={query.RequestId}&responseTime={query.ResponseTime}&resultCode={((int)query.ResultCode)}&transId={query.TransId}";
        var signature = MoMoCodeHelper.Hmac(secretKey,signatureString);

        return signature.Equals(query.Signature);
    }

    public bool CheckIPN(IPNModel model)
    {
        var secretKey = _configuration["SecretKey"] ?? "";
        var accessKey = _configuration["AccessKey"] ?? "";
        
        var signatureString = $"accessKey={accessKey}&amount={model.Amount}&extraData={model.ExtraData}&message={model.Message}&orderId={model.OrderId}&orderInfo={model.OrderInfo}&orderType={model.OrderType}&partnerCode={model.PartnerCode}&payType={model.PayType}&requestId={model.RequestId}&responseTime={model.ResponseTime}&resultCode={((int)model.ResultCode)}&transId={model.TransId}";
        var signature = MoMoCodeHelper.Hmac(secretKey,signatureString);

        return signature.Equals(model.Signature);
    }
}