using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DevKimchi.Sample.Functions.Configs;

using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DevKimchi.Sample.Functions.Services
{
    /// <summary>
    /// This represents the service entity to handle secrets from Key Vault.
    /// </summary>
    public class SecretService : ISecretService
    {
        private readonly static Dictionary<string, string> collection =
            new Dictionary<string, string>() { { "backup", "Backup" }, { "restore", "Restore" } };

        private readonly AppSettings _settings;
        private readonly IKeyVaultClient _kv;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretService"/> class.
        /// </summary>
        /// <param name="settings"><see cref="AppSettings"/> instance.</param>
        /// <param name="kv"><see cref="IKeyVaultClient"/> instance.</param>
        public SecretService(AppSettings settings, IKeyVaultClient kv)
        {
            this._settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this._kv = kv ?? throw new ArgumentNullException(nameof(kv));
        }

        /// <inheritdoc />
        public async Task<List<string>> GetSecretsAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var baseUri = this.GetKeyVaultInstance(key).BaseUri;

            var secrets = await this._kv
                                    .GetSecretsAsync(baseUri)
                                    .ConfigureAwait(false);

            return secrets.Select(p => p.Identifier.Name).ToList();
        }

        /// <inheritdoc />
        public async Task<List<BackupSecretResult>> BackupSecretsAsync(string key, List<string> secrets)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (secrets == null)
            {
                throw new ArgumentNullException(nameof(secrets));
            }

            var baseUri = this.GetKeyVaultInstance(key).BaseUri;

            var results = new List<BackupSecretResult>();
            foreach (var name in secrets)
            {
                var result = await this._kv
                                       .BackupSecretAsync(baseUri, name)
                                       .ConfigureAwait(false);

                results.Add(result);
            }

            return results;
        }

        private KeyVaultInstanceSettings GetKeyVaultInstance(string key)
        {
            var name = collection[key];
            var instance = this._settings.KeyVault.Instances[name];

            return instance;
        }
    }
}
