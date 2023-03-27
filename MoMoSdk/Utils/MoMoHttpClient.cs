using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MoMoSdk.Utils;

public interface IMoMoHttpClient
{
    Task<T> Post<T>(string requestUri,object dataObject);
}
public class MoMoHttpClient:IMoMoHttpClient
{
    private readonly ILogger<MoMoHttpClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializeOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    public MoMoHttpClient(IConfiguration configuration, ILogger<MoMoHttpClient> logger)
    {
        _logger = logger;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=UTF-8");
        _httpClient.BaseAddress = new Uri(configuration["MoMoPayment:Endpoint"] ?? "");
    }

    public async Task<T> Post<T>(string requestUri,object dataObject)
    {
        var jsonString = JsonSerializer.Serialize(dataObject, _serializeOptions);
        _logger.LogInformation("MoMoHttpClient.Post requestUri:{} paymentRequest:{}",requestUri,jsonString);
        var body = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response  = await _httpClient.PostAsync(requestUri, body);
        var stringContent = await response.Content.ReadAsStringAsync();
        _logger.LogInformation("MoMoHttpClient.Post requestUri:{} PaymentResponse:{}",requestUri,stringContent);
        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<T>(stringContent,_serializeOptions) ?? throw new Exception("Không thể convert body");
        }
        throw new Exception("Có lỗi trong quá trình xử lý");
    }
}