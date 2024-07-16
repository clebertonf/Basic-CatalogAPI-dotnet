using CatalogAPI.Models;
using CatalogAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerContollerUnitOfWorkStandard : ControllerBase
    {
        // private readonly IRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerContollerUnitOfWorkStandard(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("customers")]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.CustomerRepository.GetAll());
        }

        [HttpGet("customer")]
        public IActionResult GetId(int id)
        {
            var customer = _unitOfWork.CustomerRepository.Get((c) => c.CustmoerId.Equals(id));
            if (customer is null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost("customer")]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer is null) return BadRequest();

            _unitOfWork.CustomerRepository.Create(customer);
            _unitOfWork.Commit();

            return CreatedAtAction(nameof(GetId), new { id = customer.CustmoerId }, customer);
        }

        [HttpPut("customer")]
        public IActionResult Update([FromBody] Customer customer)
        {
            if(customer is null) return BadRequest();
         
            _unitOfWork.CustomerRepository.Update(customer);
            _unitOfWork.Commit();

            return NoContent();
        }

        [HttpDelete("customer")]
        public IActionResult Delete(int id)
        {
            var customer = _unitOfWork.CustomerRepository.Get((c) => c.CustmoerId.Equals(id));

            if (customer is null) return NotFound();

            _unitOfWork.CustomerRepository.Delete(customer);
            _unitOfWork.Commit();

            return NoContent();
        }
    }
}
