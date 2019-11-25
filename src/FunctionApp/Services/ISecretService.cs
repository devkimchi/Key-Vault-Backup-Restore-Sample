using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Azure.KeyVault.Models;

namespace DevKimchi.Sample.Functions.Services
{
    /// <summary>
    /// This provides interfaces to <see cref="SecretService"/>.
    /// </summary>
    public interface ISecretService
    {
        /// <summary>
        /// Gets the list of secrets.
        /// </summary>
        /// <param name="key">Key Vault instance identifier.</param>
        /// <returns>Returns the list of secrets from Key Vault.</returns>
        Task<List<string>> GetSecretsAsync(string key);

        /// <summary>
        /// Performs backup secrets.
        /// </summary>
        /// <param name="key">Key Vault instance identifier.</param>
        /// <param name="secrets">List of secret names.</param>
        /// <returns>Returns the list of <see cref="BackupSecretResult"/> instances.</returns>
        Task<List<BackupSecretResult>> BackupSecretsAsync(string key, List<string> secrets);
    }
}
