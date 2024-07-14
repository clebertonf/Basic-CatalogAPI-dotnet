using CatalogAPI.Models;
using CatalogAPI.Repositories.Generic;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerContoller : ControllerBase
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerContoller(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("customers")]
        public IActionResult Get()
        {
            return Ok(_customerRepository.GetAll());
        }

        [HttpGet("customer")]
        public IActionResult GetId(int id)
        {
            var customer = _customerRepository.Get((c) => c.CustmoerId.Equals(id));
            if (customer is null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost("customer")]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer is null) return BadRequest();

            _customerRepository.Create(customer);

            return CreatedAtAction(nameof(GetId), new { id = customer.CustmoerId }, customer);
        }

        [HttpPut("customer")]
        public IActionResult Update([FromBody] Customer customer)
        {
            if(customer is null) return BadRequest();
         
            _customerRepository.Update(customer);

            return NoContent();
        }

        [HttpDelete("customer")]
        public IActionResult Delete(int id)
        {
            var customer = _customerRepository.Get((c) => c.CustmoerId.Equals(id));

            if (customer is null) return NotFound();

            _customerRepository.Delete(customer);

            return NoContent();
        }
    }
}
