using Neo4jClient.Cypher;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using NetWebApi_Json_LD.Models;
using System.Web.Http;
using System.Linq;

namespace NetWebApi_Json_LD.Controllers
{
    [RoutePrefix("api/branch")]
    public class BranchController : ApiController
    {
        [Route(""), HttpGet]
        public IHttpActionResult Get()
        {
            var query = WebApiConfig.GraphClient.Cypher
                .Match("(b:Branch)")
                .Return((b) => new
                {
                    branch = b.As<Branch>()
                });

            var data = query.Results.ToList();

            var branches = new List<string>();
            foreach (var item in data)
            {
                Branch node = new Branch { Id = @"api/branch/" + item.branch.number, Location = item.branch.Location };
                branches.Add(node.ToJson());
            }

            return Ok(new { branches });
        }

        [Route("{number}"), HttpGet]
        public IHttpActionResult Get(string number)
        {
            var query = WebApiConfig.GraphClient.Cypher
                .Match("(b:Branch)")
                .Where((Branch b) => b.number == number)
                .Return((b) => new
                {
                    branch = b.As<Branch>()
                });

            var data = query.Results.ToList();

            var branches = new List<string>();
            foreach (var item in data)
            {
                Branch node = new Branch { Id = @"api/branch/" + item.branch.number, Location = item.branch.Location};
                branches.Add(node.ToJson());
            }

            return Ok(new { branches });
        }
        
    }
}

