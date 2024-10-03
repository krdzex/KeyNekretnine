using FluentValidation;
using Google.Analytics.Data.V1Beta;
using KeyNekretnine.Application.Abstraction.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace KeyNekretnine.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddSingleton<BetaAnalyticsDataClient>(sp =>
        {
            var aa = $@"{{
                      'type': '{Environment.GetEnvironmentVariable("GA4_GOOGLE_TYPE")}',
                      'project_id': '{Environment.GetEnvironmentVariable("GA4_GOOGLE_PROJECT_ID")}',
                      'private_key_id': '{Environment.GetEnvironmentVariable("GA4_GOOGLE_PRIVATE_KEY_ID")}',
                      'private_key': '{Environment.GetEnvironmentVariable("GA4_GOOGLE_PRIVATE_KEY")}',
                      'client_email': '{Environment.GetEnvironmentVariable("GA4_GOOGLE_CLIENT_EMAIL")}',
                      'client_id': '{Environment.GetEnvironmentVariable("GA4_GOOGLE_CLIENT_ID")}',
                      'auth_uri': '{Environment.GetEnvironmentVariable("GA4_GOOGLE_AUTH_URI")}',
                      'token_uri': '{Environment.GetEnvironmentVariable("GA4_GOOGLE_TOKEN_URI")}',
                      'auth_provider_x509_cert_url': '{Environment.GetEnvironmentVariable("GA4_GOOGLE_AUTH_PROVIDER_CERT_URL")}',
                      'client_x509_cert_url': '{Environment.GetEnvironmentVariable("GA4_GOOGLE_CLIENT_CERT_URL")}',
                      'universe_domain': '{Environment.GetEnvironmentVariable("GA4_GOOGLE_UNIVERSE_DOMAIN")}'
                }}";

            return new BetaAnalyticsDataClientBuilder
            {
                JsonCredentials = aa
            }.Build();
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        return services;
    }
}