using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WSSale.Models;
using WSSale.Models.Response;
using WSSale.Models.Request;

namespace WSSale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCustomers()
        {
            Response mResponse = new Response();
            try
            {
                using (ActualSaleContext db = new ActualSaleContext())
                {
                    var lst = db.Customers.ToList();
                    mResponse.Success = 1;
                    mResponse.Data = lst;
                }
            }
            catch (Exception ex)
            {
                mResponse.Message = ex.Message;
            }
            return Ok(mResponse);
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerRequest oModelCustomer)
        {
            Response mResponse = new Response();
            try
            {
                using(ActualSaleContext db = new ActualSaleContext())
                {
                    Customer oCustomer = new Customer();
                    oCustomer.NameCustomer = oModelCustomer.NameCustomer;
                    db.Customers.Add(oCustomer);
                    db.SaveChanges();
                    mResponse.Success = 1;
                }
            }
            catch (Exception ex)
            {
                mResponse.Message = ex.Message;
            }
            return Ok(mResponse);
        }

        [HttpPut]
        public IActionResult UpDateCustomer(CustomerRequest mCustomer)
        {
            Response mResponse = new Response();
            try
            {
                using (ActualSaleContext db = new ActualSaleContext())
                {
                    Customer oCustomer = db.Customers.Find(mCustomer.IdCustomer);
                    oCustomer.NameCustomer = mCustomer.NameCustomer;
                    db.Entry(oCustomer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    mResponse.Success = 1;
                }
            }
            catch (Exception ex)
            {
                mResponse.Message = ex.Message;
            }
            return Ok(mResponse);
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteCustomer(int Id)
        {
            Response mResponse = new Response();

            try
            {
                using(ActualSaleContext db = new ActualSaleContext())
                {
                    Customer oCustomer = db.Customers.Find(Id);
                    db.Remove(oCustomer);
                    db.SaveChanges();
                    mResponse.Success = 1;
                }
            }
            catch (Exception ex)
            {
                mResponse.Message = ex.Message;
            }
            return Ok(mResponse);
        }
    }
}
