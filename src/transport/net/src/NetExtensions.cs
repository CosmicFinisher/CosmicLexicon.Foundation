using System;
using System.Net;
using System.Threading.Tasks;

namespace CosmicLexicon.Foundation.Transport.Net
{
    public static class NetExtensions
    {
        /// <summary>
        /// Checks if there is an active internet connection by attempting to resolve a DNS address.
        /// </summary>
        /// <param name="host">The host name to resolve (e.g., "google.com").</param>
        /// <returns>True if the DNS resolution succeeds, indicating an internet connection; otherwise, false.</returns>
        public static async Task<bool> HasInternetConnectionAsync(string host = "google.com")
        {
            try
            {
                var hostEntry = await Dns.GetHostEntryAsync(host);
                return hostEntry.AddressList.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if there is an active internet connection by attempting to resolve a DNS address, using a default host.
        /// </summary>
        /// <returns>True if the DNS resolution succeeds, indicating an internet connection; otherwise, false.</returns>
        public static async Task<bool> HasInternetConnectionAsync()
        {
            return await HasInternetConnectionAsync("google.com");
        }
    }
}