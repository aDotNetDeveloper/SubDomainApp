using System.Collections.Generic;

namespace SubDomain.Models
{
    public class HostAddressDetail
    {
        public string HostName { get; set; }
        public IEnumerable<string> SubDomains { get; set; }
    }
}