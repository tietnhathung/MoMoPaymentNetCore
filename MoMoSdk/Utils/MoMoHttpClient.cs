using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoMoSdk.Exceptions;

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
        try
        {
            var jsonString = JsonSerializer.Serialize(dataObject, _serializeOptions);
            _logger.LogInformation("MoMoHttpClient.Post requestUri:{RequestUri} paymentRequest:{JsonString}", requestUri,jsonString);
            var body = new StringContent(jsonString, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response  = await _httpClient.PostAsync(requestUri, body);
            var stringContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("MoMoHttpClient.Post requestUri:{RequestUri} PaymentResponse:{StringContent}", requestUri,stringContent);
            var content = JsonSerializer.Deserialize<T>(stringContent, _serializeOptions)!;
            return content;
        }
        catch (Exception e)
        {
            _logger.LogError("Http.Post ERROR {Message}",e.Message);
            throw;
        }
    }
}