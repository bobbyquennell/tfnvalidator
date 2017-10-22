using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tfnvalidator.Controllers
{
    [Produces("application/json")]
    [Route("api/validate")]
    public class validateController : Controller
    {
        // GET: api/validate
        [HttpGet]
        public string Get(string tfn)
        {
            var result = tfn.ToString();
            //ToDo: server sive validation

            return result;
        }

        // GET: api/validate/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/validate
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/validate/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
