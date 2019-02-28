using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using SubDomain.Models;
using System.Net.Sockets;

namespace SubDomain.Repositories
{
    public class IPAddressFinder : IIPAddressFinder
    {
        private IDNSProvider _dnsProvider;

        public IPAddressFinder(IDNSProvider dnsProvider)
        {
            _dnsProvider = dnsProvider;
        }

        public async Task<IEnumerable<string>> LookupIPAddress(string hostName)
        {
            try
            {
                var addresses = await _dnsProvider?.GetHostAddressesAsync(hostName);
                return addresses?.Select(a => a.ToString());
            }
            catch( SocketException )
            {
                return null;
            }
        }

        public async Task<IEnumerable<HostAddressDetail>> LookupIPAddress(IEnumerable<string> hostNames)
        {
            var lookupTasks = hostNames.Select(async h => new HostAddressDetail { HostName = h, SubDomains = await LookupIPAddress(h) });

            var results = await Task.WhenAll(lookupTasks);

            return results.Select(t => t);
        }
    }
}