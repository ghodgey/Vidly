using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;


namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //GET /api/customers
        public IHttpActionResult GetCustomers()
        {
            var customerDtos = _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>); //mapping customer to customerDto 
            //this is done on the server - gets the customer, maps it to customerDto then returns it to the client
            return Ok(customerDtos);

        }

        //Get /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {       
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));//passing in customer to this method - mapping customer to customerDto

         }

        // POST /api/customers

        //IHttpActionResult returns a 201 response to the browser whiich is created successfully - similar to ActionResult in MVC
        //RESTFUL convention needs to be 201
        //Had CustomerDto first then changed to IHttpActionResult
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto) //when the clients sends in a customer it sends it as a dto
        {
            if(!ModelState.IsValid)
                return BadRequest(); //helper method from IHttpActionresult

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);//in create method we receive the Dto and now we want to map it
                                                                          //to the customer object - pass in the method the customerDto object

            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);//URI unified resource identifier = /api/customers/10
            //second argument is the actual object that was created
        }

        // PUT /api/Customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);//source object , target object - used two methods as we refer to _context. 
            //Previously we didnt use two objects as we stored it in a variable and returned it
            // by passing two parameters _context is aware of the changes made and is saved in the next line

            _context.SaveChanges();

            return Ok();

        }

        //Delete /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
