using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers.API
{
    /// <summary>
    /// Manage Type-DB Instance Databases
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        /// <summary>
        /// Get the list of all Instance Databases
        /// </summary>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Get a database
        /// </summary>
        [HttpGet("{name}", Name = "Get")]
        public string Get(string name)
        {
            return "value";
        }

        /// <summary>
        /// Create a new Database
        /// </summary>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Edit an existing database
        /// </summary>
        [HttpPut("{name}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Drop an Database
        /// </summary>
        [HttpDelete("{name}")]
        public void Delete(int id)
        {
        }
    }
}
