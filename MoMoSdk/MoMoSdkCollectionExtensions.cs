using Microsoft.Extensions.DependencyInjection;
using MoMoSdk.Services;
using MoMoSdk.Utils;

namespace MoMoSdk;

public static class MoMoSdkCollectionExtensions
{
    public static void AddMoMoSdk(this IServiceCollection services)
    {
        services.AddScoped<IMoMoHttpClient,MoMoHttpClient>();
        services.AddScoped<IMoMoService,MoMoService>();
    }
}