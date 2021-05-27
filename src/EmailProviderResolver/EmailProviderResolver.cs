using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using DnsClient;

namespace EmailProvider.Library
{
    public static class EmailProviderResolver
    {
        public static readonly LookupClient _client;
        private static readonly Dictionary<string, EmailProviderType> _emailProviderTypes;
        public static IReadOnlyCollection<NameServer> x;

        static EmailProviderResolver()
        {
            _client = new LookupClient(new LookupClientOptions
            {
                MaximumCacheTimeout = new TimeSpan(1, 0, 0),
                MinimumCacheTimeout = new TimeSpan(0, 5, 0),
                ThrowDnsErrors = true,
            });

            _emailProviderTypes = new Dictionary<string, EmailProviderType>()
            {
                // possible gmail addresses https://support.google.com/mail/answer/10313?hl=en#zippy=%2Cgetting-messages-sent-to-an-googlemailcom-address
                { "gmail.com", EmailProviderType.Google },
                { "googlemail.com", EmailProviderType.Google },

                { "google.com", EmailProviderType.Google },

                { "microsoft.com", EmailProviderType.Microsoft },

                // default microsoft office 365 domains https://docs.microsoft.com/en-us/microsoft-365/admin/setup/domains-faq?view=o365-worldwide
                { "onmicrosoft.com", EmailProviderType.Microsoft },
                { "onmicrosoft.de", EmailProviderType.Microsoft },

                // outlook
                { "hotmail.com", EmailProviderType.Microsoft },
                { "outlook.com", EmailProviderType.Microsoft },
            };
        }

        /// <summary>
        /// Finds the email provider of the email address
        /// </summary>
        /// <returns>
        /// The email provider type enum.
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DnsResponseException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public static async Task<EmailProviderType> GetByEmailAsync(string email)
        {
            // validate the email and get the host the constructor
            // returns an exception if the email string is not in the correct format
            var host = new MailAddress(email).Host;

            if (_emailProviderTypes.ContainsKey(host))
            {
                return _emailProviderTypes[host];
            }

            return await GetByHostAsync(host);
        }

        private static async Task<EmailProviderType> GetByHostAsync(string host)
        {
            var dnsMxQueryResult = await _client.QueryAsync(host, QueryType.MX);

            // select mx record with the lowest preference to determine the email provider
            var dnsMxRecord = dnsMxQueryResult.AllRecords.MxRecords().OrderBy(x => x.Preference).First();

            var exchangeValue = dnsMxRecord.Exchange.Value;
            if (exchangeValue.EndsWith("google.com."))
            {
                return EmailProviderType.Google;
            }
            else if (exchangeValue.EndsWith("outlook.com."))
            {
                return EmailProviderType.Microsoft;
            }
            else if (exchangeValue.EndsWith("microsoft.com."))
            {
                return EmailProviderType.Microsoft;
            }

            return EmailProviderType.Other;
        }

    }
}
