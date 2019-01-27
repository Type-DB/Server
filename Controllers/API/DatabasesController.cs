using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TypeDB;
using TypeDB.Exceptions;

namespace Server.Controllers.API
{
    /// <summary>
    /// Manage Type-DB Instance Databases
    /// </summary>
    //[Route("api/[controller]")]
    [Route("api/databases")]
    [ApiController]
    public class DatabasesController : ControllerBase
    {
        public Core Core { get; }
        public Instance Instance { get; }

        public DatabasesController(Core core, Instance instance)
        {
            this.Core = core;
            this.Instance = instance;
        }

        /// <summary>
        /// Get the list of all Databases
        /// </summary>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return this.Instance.GetDatabases().Select(x => x.Name);
        }

        /// <summary>
        /// Get informations of a Database
        /// </summary>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{database}", Name = "Get")]
        public ActionResult<Database> Get(string database)
        {
            try
            {
                return this.Instance.OpenDatabase(database);
            }
            catch(TypeDBNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create a new Database
        /// </summary>
        [HttpPost]
        public void Post(string database)
        {
            try
            {
                this.Instance.CreateDatabase(database);
            }
            catch(TypeDBGeneralException)
            {
                Response.ContentType = "application/json";
                Response.StatusCode = 400;
                Response.WriteAsync(JsonConvert.SerializeObject(new { error = $"The database '{database}' already exists." }));
            }
        }

        /// <summary>
        /// Edit an existing database
        /// </summary>
        [HttpPut("{database}")]
        public void Put(string database, string newdatabase)    //, [FromBody] string value)
        {
            this.Instance.RenameDatabase(database, newdatabase);
        }

        /// <summary>
        /// Drop an Database
        /// </summary>
        [HttpDelete("{database}")]
        public ActionResult Delete(string database)
        {
            try
            {
                this.Instance.DropDatabase(database);
                return Ok();
            }
            catch(TypeDBNotFoundException)
            {
                return NotFound($"The database '{database}' does not exists.");
            }
        }
    }
}
