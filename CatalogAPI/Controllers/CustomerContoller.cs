using AutoMapper;
using CatalogAPI.DTOs;
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
        private readonly IMapper _mapper;

        public CustomerContollerUnitOfWorkStandard(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("customers")]
        public IActionResult Get()
        {
            var customers = _unitOfWork.CustomerRepository.GetAll();
            var customersMapper = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            return Ok(customersMapper);
        }

        [HttpGet("customer")]
        public IActionResult GetId(int id)
        {
            var customer = _unitOfWork.CustomerRepository.Get((c) => c.CustmoerId.Equals(id));
            if (customer is null)
                return NotFound();

            var customerMapper = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerMapper);
        }

        [HttpPost("customer")]
        public IActionResult Post([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO is null) return BadRequest();

            var customer = _mapper.Map<Customer>(customerDTO);

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
