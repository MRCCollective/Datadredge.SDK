using Microsoft.Extensions.Options;

namespace Datadredge.SDK;

public static class ServerPageviewTrackingServiceCollectionExtensions
{
    public static IServiceCollection AddServerPageviewTracking(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ServerPageviewTrackingOptions>(configuration.GetSection(ServerPageviewTrackingOptions.SectionName));
        services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<ServerPageviewTrackingOptions>>().Value);
        services.AddSingleton<ServerPageviewTrackingQueue>();
        services.AddSingleton<IServerPageviewTrackingClient, ServerPageviewTrackingClient>();
        services.AddSingleton<IServerPageviewTrackingTransport, ServerPageviewTrackingTransport>();
        services.AddHostedService<ServerPageviewTrackingBackgroundService>();
        services.AddHttpClient(ServerPageviewTrackingTransport.HttpClientName, (serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<ServerPageviewTrackingOptions>>().Value;
            if (!string.IsNullOrWhiteSpace(options.DatadredgeBaseUrl))
            {
                client.BaseAddress = new Uri(options.DatadredgeBaseUrl, UriKind.Absolute);
            }
        });

        return services;
    }

    public static IApplicationBuilder UseServerPageviewTracking(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ServerPageviewTrackingMiddleware>();
    }
}
