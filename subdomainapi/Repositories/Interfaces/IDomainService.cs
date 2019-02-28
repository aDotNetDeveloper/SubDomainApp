using System.Collections.Generic;
using System.Threading.Tasks;

namespace SubDomain.Repositories
{
    public interface IDomainService
    {
        Task<IEnumerable<string>> GetThirdLevelDomains(string subDomain);
    }
}