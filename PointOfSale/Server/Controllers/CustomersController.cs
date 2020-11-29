using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using PointOfSale.Services.ISevices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PointOfSale.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : _BaseController<Customer, CustomerDTO>
    {
        private readonly IWebHostEnvironment env;
        private ICustomerService _customerService;
        public CustomersController(ICustomerService businessService, IWebHostEnvironment env) : base(businessService, env)
        {
            _customerService = businessService;
            this.env = env;
        }
        [HttpGet("GetDropDownListAll")]
        public IActionResult GetDropDownListAll()
        {
            return Ok(this._BusinessService.GetAll<Customer>().Select(x => new DropDownList { Id = x.Id, Name = x.Name }).ToList());
        }
        [HttpGet("GetChartGroubByDate")]
        public IActionResult GetChartGroubByDate(DateTime? StartDate, DateTime? EndDate)
        {
            return Ok(this._BusinessService.GetAll<Customer>().Where(c => (c.CreationDate >= StartDate || StartDate == null) && (c.CreationDate <= EndDate || EndDate == null)).GroupBy(u => new { year = u.CreationDate.Year, month = u.CreationDate.Month })
                .Select(x => new PiChartsDTO { Text = x.Key.year + "-" + x.Key.month, Value = x.Count() }).ToList());
        }
        [HttpPost("Upload")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}