using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;


        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {

            //            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //            return View(customers);
            return View();
        }


        [Route("customers/details/{customerId}")]
        public ActionResult Details(int? customerId)
        {

            var customerName = _context.Customers.Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == customerId);


            if (customerName != null)
                return View(customerName);

            //return Content("The ID is " + customerName.Name);

            return HttpNotFound();
            //
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(), //initializes customer to its default values - passes an ID for the validation of the ID field - fixed the validation of Modelstate when it complains about having an ID of null
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }

        //submits data to be processed to a specified resource.
        //modify state on the server -Httppost - updating information on the server
        //Http get is used for read-only operations - parameters displayed in the URL
        [HttpPost]
        [ValidateAntiForgeryToken] // In the CustomerFormView - saves an encryption in user cookie to validate from attacks
        public ActionResult Save(Customer customer)
        {
            //Checks the dataannotations on your properties eg. [Required] in the model. This then checks that those annotations are met when
            //the user fills in the form
            //In the view we added ValidationFor for each property that has validation
            //To check why modelstate is false
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            //ModelState.Remove("customer.Id");

            if (!ModelState.IsValid)                        
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }
            //checking of the customer exists - if does not exist add the new customer else update
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
        [Route("customers/edit/{customerId}")]
        public ActionResult Edit(int customerId)
        {
            
            var customer = _context.Customers.SingleOrDefault(c => c.Id == customerId);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()

            };
            return View("CustomerForm", viewModel);
        }
    }
}