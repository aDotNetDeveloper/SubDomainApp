using System.Net;
using System.Threading.Tasks;

namespace SubDomain.Repositories
{
    public interface IDNSProvider
    {
        Task<IPAddress[]> GetHostAddressesAsync(string hostNameOrAddress);
    }
}