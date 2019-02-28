using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubDomain.Models;
using SubDomain.Repositories;

namespace SubDomain.Controllers
{
    [Produces("application/json")]
    public class SubDomainController : ControllerBase
    {
        private IDomainService _domainService;
        private IIPAddressFinder _ipAddressFinder;

        public SubDomainController(IDomainService domainService, IIPAddressFinder ipAddressFinder)
        {
            _domainService = domainService;
            _ipAddressFinder = ipAddressFinder;
        }

        [HttpGet("[controller]/enumerate/{domain}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<string>>> EnumerateAsync(string domain)
        {
            var domains = await _domainService.GetThirdLevelDomains(domain);
            return Ok(domains);
        }

        [HttpPost("[controller]/findipaddresses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<HostAddressDetail>>> FindIPAddressesAsync([FromBody]string[] addresses)
        {
            if (addresses?.Any() != true)
                return BadRequest(new ArgumentNullException(nameof(addresses)));

            var ipAddresses = await _ipAddressFinder.LookupIPAddress(addresses);

            var result = ipAddresses.ToList();
            
            return Ok(result);
        }
    }
}
