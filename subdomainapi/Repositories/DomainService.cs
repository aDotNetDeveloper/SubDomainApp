using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SubDomain.Repositories
{
    public class DomainService : IDomainService
    {
        internal IEnumerable<string> GetPostFix(string prefix = null)
        {
            for (var i = 0; i < 26; i++)
            {
                yield return $"{prefix}{(char)((int)('a') + i)}";
            }
        }

        public async Task<IEnumerable<string>> GetThirdLevelDomains(string domain)
        {
            var list = new List<string>();
            var prefixList = GetPostFix();

            foreach (var prefix in prefixList)
            {
                list.Add(prefix);
                list.AddRange(GetPostFix(prefix));
            }
            return await Task.FromResult(list.Select(l => $"{l}.{domain}"));
        }
    }
}