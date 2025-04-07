using Dapr.Client;
using Dapr.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.Hosting
{
    public static class DaprExtensions
    {
        public static IHostApplicationBuilder ConfigureDapr(
            this IHostApplicationBuilder builder,
            string? configComponentName = null,
            IReadOnlyList<string>? configKeys = null,
            string? secretComponentName = null)
        {
            DaprClient client = new DaprClientBuilder().Build();
            builder.Services.TryAddSingleton(client);

            if (string.IsNullOrEmpty(configComponentName) == false
                && configKeys != null && configKeys.Count() > 0)
            {
                builder.Configuration.AddDaprConfigurationStore(
                    configComponentName, 
                    configKeys, 
                    client, 
                    TimeSpan.FromSeconds(20));
            }

            if (string.IsNullOrEmpty(secretComponentName) == false)
            {
                builder.Configuration.AddDaprSecretStore(
                    secretComponentName, 
                    client, 
                    TimeSpan.FromSeconds(10));
            }

            return builder;
        }
    }
}
