using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SubDomain.Repositories
{
    public class DNSProvider : IDNSProvider
    {
        public async Task<IPAddress[]> GetHostAddressesAsync(string hostNameOrAddress)
        {
            try
            {
                return await Dns.GetHostAddressesAsync(hostNameOrAddress);
            }
            catch (SocketException)
            {
                return null;
            }
        }
    }
}