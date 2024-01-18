using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Rubens.Raizen.WebApi.Contracts;
using Test.Rubens.Raizen.WebApi.Entities;

namespace Test.Rubens.Raizen.WebApi.Controllers
{
    [Route("v1")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        public CustomerController(ICustomerRepository repository) => _repository = repository;

        [HttpGet("customers")]
        public async Task<IActionResult> GetAll()
        {
            var existingList = await _repository.GetCustomers();
            return Ok(existingList);
        }

        [HttpGet("customers/{customerId:guid}")]
        public async Task<IActionResult> GetId(Guid customerId)
        {
            var existing = await _repository.GetCustomerId(customerId);

            if (existing is null)
                return NotFound();

            return Ok(existing);
        }

        [HttpPost("customers")]
        public async Task<IActionResult> Post(Customer model)
        {
            await _repository.AddAsync(model);

            return Created($"customers/{model.Id}", model);
        }

        [HttpPut("customers/{customerId:guid}")]
        public async Task<IActionResult> Update(Guid customerId, Customer model)
        {
            var existing = await _repository.GetCustomerId(customerId);

            if (existing is null)
                return NotFound();


            await _repository.Update(customerId, model);

            return NoContent();

        }

        [HttpDelete("customers/{customerId}")]
        public async Task<IActionResult> Delete(Guid customerId)
        {
            try
            {
                var existing = await _repository.Delete(customerId);

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, ("Unable to remove client"));
            }

        }

    }
}
