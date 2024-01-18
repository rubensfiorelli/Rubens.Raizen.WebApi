using Microsoft.AspNetCore.Mvc;
using Test.Rubens.Raizen.WebApi.External.Refit;
using Test.Rubens.Raizen.WebApi.External.Refit.Response;

namespace Test.Rubens.Raizen.WebApi.Controllers
{
    [Route("v1")]
    [ApiController]
    public class ZipCodeController : ControllerBase
    {
        private readonly IViaCep _viaCep;

        public ZipCodeController(IViaCep viaCep) => _viaCep = viaCep;
       
        [HttpGet("{zipCode}")]
        public async Task<ActionResult<ViaCepResponse>> GetZipCode(string zipCode)
        {
            var existing = await _viaCep.GetResponse(zipCode).ConfigureAwait(false);

            if(existing is null)
                return NotFound();

            return Ok(existing);
        }

    }
}
