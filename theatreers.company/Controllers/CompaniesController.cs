using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using theatreers.shared.Models;
using theatreers.shared.Interfaces;

namespace theatreers.company.Controllers
{
    //[Authorize]
    [Route("companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {

        private readonly ICompanyService _service;
    
        public CompaniesController(ICompanyService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Company>> Get()
        {               
            var items = _service.GetAll();
            return Ok(items);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Company> Get(int id)
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
        public ActionResult Post([FromBody] Company body)
        {      
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
    
            Company item = _service.Create(body);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Company> Put([FromBody] Company body)
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
