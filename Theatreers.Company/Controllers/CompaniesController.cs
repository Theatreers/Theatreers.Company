using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Theatreers.Shared.Models;
using Theatreers.Shared.Interfaces;
using Theatreers.Shared.Services;

namespace Theatreers.Company.Controllers
{
    //[Authorize]
    [Route("companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {

        private readonly ICompanyService _service;
    
        public CompaniesController(ICompanyService service)
        {
            if !(service){
                _service = new CompanyService();
            } else {
            _service = service;}
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<CompanyModel>> Get()
        {               
            var items = _service.GetAll();
            return Ok(items);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<CompanyModel> Get(int id)
        {   
            var item = _service.GetById(id);
    
            if (item == null)
            {
                return NotFound();
            }
    
            return Ok(item);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] CompanyModel body)
        {      
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
    
            CompanyModel item = _service.Create(body);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<CompanyModel> Put([FromBody] CompanyModel body)
        {                
            var existingItem = _service.GetById(body.Id);
 
            if (existingItem == null)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
    
            _service.Update(body);
            return Ok(body);

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingItem = _service.GetById(id);
 
            if (existingItem == null)
            {
                return NotFound();
            }
    
            _service.Delete(id);
            return Ok();
        }
    }
}
