using DataAccess.Interfaces;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class KeyVault : IKeyVault
    {

        private KeyVaultClient keyVaultClient;
        private IConfiguration Config;
        private string KeyVaultUri;

        public KeyVault(IConfiguration _Config)
        {
            Config = _Config;
            Init();
        }

        public void Init()
        {
            var serviceTokenProvider = new AzureServiceTokenProvider();
            keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(serviceTokenProvider.KeyVaultTokenCallback));
            KeyVaultUri = Config["KeyVaultUri"];
        }

        public async Task<SecretBundle> GetSecretAsync(string secret)
        {
            try
            {
                //Building keyvault uri
                string uri = $"{KeyVaultUri}Secrets/{secret}";

                //Returning keyvault secret
                return await keyVaultClient.GetSecretAsync(uri);
            }
            catch (KeyVaultErrorException kex)
            {
                throw new Exception($"KeyVaultError: {kex.Message}");
            }
        }
    }
}
