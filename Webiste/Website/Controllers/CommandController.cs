using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Website.Controllers
{
    public class CommandController : ApiController
    {
        // GET: api/Command
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Command/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Command
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Command/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Command/5
        public void Delete(int id)
        {
        }
    }
}
