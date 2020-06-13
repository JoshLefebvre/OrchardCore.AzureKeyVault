using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace OrchardCore.AzureKeyVault
{
    public static class AzureKeyVaultWebHostBuilderExtension
    {
        /// <summary>
        /// Adds Azure Key Vault Configuration.
        /// </summary>
        /// <param name="builder">The web host builder to configure.</param>
        /// <returns>The web host builder.</returns>
        public static IHostBuilder UseOrchardCoreAzureKeyVault(this IHostBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.ConfigureAppConfiguration((context, configuration) =>
            {
                var builtConfig = configuration.Build();
                var keyVaultName = builtConfig["AzureKeyVault:KeyVaultName"];
                var clientId = builtConfig["AzureKeyVault:ClientId"];
                var clientSecret = builtConfig["AzureKeyVault:ClientSecret"];

                var keyVaultEndpoint = "https://" + keyVaultName + ".vault.azure.net";
                configuration.AddAzureKeyVault(
                    keyVaultEndpoint, 
                    clientId, 
                    clientSecret,  
                    new CustomKeyVaultSecretManager()
                );
            });


            return builder;
        }


    }
}
