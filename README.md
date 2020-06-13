# OrchardCore.AzureKeyVault
A custom override of the DefaultKeyVaultManger class that retrieves secrets from Azure Key Vault and translates ---
to an underscore (_)  and -- to a colon (:). Both underscores and colons are illegal characters in Azure KeyVault.

Examples:
Key Vault Input: "OrchardCore--OrchardCore---Shells---Database--ConnectionString".
Output: "OrchardCore:OrchardCore_Shells_Database:ConnectionString".
See https://github.com/OrchardCMS/OrchardCore/issues/6359.


# Instructions:
**First**, install the OrchardCore.AzureKeyVault NuGet package into your app.
```
//TODO: This app is not yet publishd. Please clone this project into your repo
```

**Next**, ensure that you have the following environment variables are present:
```json
"AzureKeyVault":{
    "KeyVaultName": "YOUR_KEYVAULT_NAME",
    "ClientId": "YOUR_CLIENT_ID",
    "ClientSecret": "YOUR_CLIENT_SECRET"
}
```
You should **never check in your client secret into source control** as this defeats the purpose of using a Key Vault in the first place. Instead set your client secret as an environmnet variable on your machine, or create a seperate azurekeyvault.json file and add it to your gitignore.

**Finally**, add UseOrchardCoreAzureKeyVault() to the Generic Host in CreateHostBuilder() of your program.cs.
```csharp
using OrchardCore.AzureKeyVault;
public class Program
{
    public static Task Main(string[] args)
        => BuildHost(args).RunAsync();

    public static IHost BuildHost(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseOrchardCoreAzureKeyVault()
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .Build();
}
```
