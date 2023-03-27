using Microsoft.Extensions.DependencyInjection;
using MomoSdk.Services;
using MomoSdk.Utils;

namespace MomoSdk;

public static class MomoSdkCollectionExtensions
{
    public static void AddMomoSdk(this IServiceCollection services)
    {
        services.AddScoped<IMomoHttpClient,MomoHttpClient>();
        services.AddScoped<IMomoService,MomoService>();
    }
}