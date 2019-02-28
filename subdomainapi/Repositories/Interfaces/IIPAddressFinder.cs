using SubDomain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubDomain.Repositories
{
    public interface IIPAddressFinder
    {
        Task<IEnumerable<string>> LookupIPAddress(string hostName);
        Task<IEnumerable<HostAddressDetail>> LookupIPAddress(IEnumerable<string> hostNames);
    }
}