namespace CDNApplication.Data.Services
{
    using System.Collections.Generic;
    using Microsoft.Azure.KeyVault.Models;

    /// <summary>
    /// Defines a key vault service
    /// </summary>
    public interface IKeyVaultService
    {
        /// <summary>
        /// Gets the secret by name from the key vault.
        /// </summary>
        /// <param name="secretName">The secret's name.</param>
        /// <returns>The secret's value</returns>
        string GetSecretByName(string secretName);

        // TODO: Wrap within our own object.
        /// <summary>
        /// Gets the list of secrets from the key vault
        /// </summary>
        /// <returns>The secret item.</returns>
        IEnumerable<SecretItem> GetListOfSecrets();
    }
}
