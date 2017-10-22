using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using validator.Domain.Feature;
using Microsoft.Extensions.Caching.Memory;
using validator.Domain.Model;

namespace tfnvalidator.Controllers
{
    [Produces("application/json")]
    [Route("api/validate")]
    public class validateController : Controller
    {
        /*private IMemoryCache cache;
        public validateController (IMemoryCache cache)
        {
            this.cache = cache;
        }*/
        // GET: api/validate
        [HttpGet]
        public IActionResult Get(string tfn)
        {
            //ToDo: server sive validation

            var alg = new WeightedAlgorithm();
            var validator = new TfnValidator(alg);
            //var protector = new BruteTryProtector();
            try
            {
                bool isAttack = BruteTryProtector.IfBruteAttack(Convert.ToInt32(tfn.Replace(" ", String.Empty)));
                if (isAttack)
                {
                    return BadRequest("linked");
                }
                var result = validator.Validate(Convert.ToInt32(tfn.Replace(" ", String.Empty)));
                if (result == 0)
                {
                    return Ok("Valid TFN");
                }
                else
                {
                    return Ok("Invalid TFN");
                }

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
           

            //return result==0 ? "Valid TFN" : "InValid TFN";
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
