using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Azure.KeyVault.Models;

namespace DevKimchi.Sample.Functions.Services
{
    /// <summary>
    /// This provides interfaces to <see cref="BlobService"/>.
    /// </summary>
    public interface IBlobService
    {
        /// <summary>
        /// Uploads backup.
        /// </summary>
        /// <returns>Returns <c>True</c>, if upload successful; otherwise returns <c>False</c>.</returns>
        Task<bool> UploadAsync(List<BackupSecretResult> results);
    }
}
