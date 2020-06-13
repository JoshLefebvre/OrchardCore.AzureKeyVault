using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace OrchardCore.AzureKeyVault
{
    /// <summary>
    /// A custom override of the DefaultKeyVaultManger class that retrieves secrets from azure keyvault and translates ---
    /// to the OC format using single underscore (illegal character in Azure KeyVault) and -- to : to define a section
    /// Examples:
    /// Key Vault Input: "OrchardCore--OrchardCore---Shells---Database--ConnectionString".
    /// Output: "OrchardCore:OrchardCore_Shells_Database:ConnectionString".
    /// See https://github.com/OrchardCMS/OrchardCore/issues/6359.
    /// </summary>
    public class CustomKeyVaultSecretManager : IKeyVaultSecretManager
    {
        public string GetKey(SecretBundle secret)
        {
            var key = secret.SecretIdentifier.Name.Replace("---", "_").Replace("--", ":");
            return key;        
        }

        public bool Load(SecretItem secret) => true;
    }
}
