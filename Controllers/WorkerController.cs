using Neo4jClient.Cypher;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using NetWebApi_Json_LD.Models;
using System.Web.Http;
using System.Linq;

namespace NetWebApi_Json_LD.Controllers
{
    [RoutePrefix("api/worker")]
    public class WorkerController : ApiController
    {
        // GET: api/Worker
        public IHttpActionResult Get()
        {
            var query = WebApiConfig.GraphClient.Cypher
                .Match("(w:Worker)-[:WORKS]->(b:Branch)")
                .Return((w, b) => new
                {
                    worker = w.As<Worker>(),
                    branch = Return.As<string>("collect(b.number)")
                });

            var data = query.Results.ToList();

            var workers = new List<string>();
            foreach (var item in data)
            {
                WorkerModel node = new WorkerModel { Id = @"api/worker/" + item.worker.number, name = item.worker.name, number = item.worker.number };

                if (item.worker.IsDispatchable)
                {
                    node.operation = new Operation("Dispatch");
                    node.operation.Id = @"api/worker/" + node.number + @"/dispatch";
                    node.operation.method = "Post";
                }
                if (!string.IsNullOrEmpty(item.branch))
                {
                    var branches = JsonConvert.DeserializeObject<JArray>(item.branch);
                    foreach (var b in branches)
                    {
                        if (null == node.branches) node.branches = new List<MXTires.Microdata.Action>();
                        node.branches.Add(new MXTires.Microdata.Action { Id = @"api/branch/" + b.Value<string>() });
                    }
                }
                workers.Add(node.ToJson());
            }

            return Ok(new { workers });
        }

        [Route("{number}"), HttpGet]
        public IHttpActionResult Get(string number)
        {
            var query = WebApiConfig.GraphClient.Cypher
                .Match("(w:Worker)-[:WORKS]->(b:Branch)")
                .Where((Worker w) => w.number == number)
                .Return((w, b) => new
                {
                    worker = w.As<Worker>(),
                    branch = Return.As<string>("collect(b.number)")
                });

            var data = query.Results.ToList();

            var workers = new List<string>();
            var rels = new List<object>();
            foreach (var item in data)
            {
                WorkerModel node = new WorkerModel { Id = @"api/worker/" + item.worker.number, name = item.worker.name, number = item.worker.number };

                if (item.worker.IsDispatchable)
                {
                    node.operation = new Operation("Dispatch");
                    node.operation.Id = @"api/worker/" + node.number + @"/dispatch";
                    node.operation.method = "Post";
                }
                if (!string.IsNullOrEmpty(item.branch))
                {
                    var branches = JsonConvert.DeserializeObject<JArray>(item.branch);
                    foreach (var b in branches)
                    {
                        if (null == node.branches) node.branches = new List<MXTires.Microdata.Action>();
                        node.branches.Add(new MXTires.Microdata.Action { Id = @"api/branch/" + b.Value<string>() });
                    }
                }
                workers.Add(node.ToJson());
            }

            return Ok(new { workers });
        }

        [Route(""), HttpGet]
        public IHttpActionResult Get([FromUri]string skill, [FromUri]string level)
        {
            var query = WebApiConfig.GraphClient.Cypher
                .Match("(s:Skills)<-[:" + level.ToUpper() + "]-(w:Worker)-[:WORKS]->(b:Branch)")
                .Where((Skills s) => s.name == skill)
                .Return((s,w, b) => new
                {
                    worker = w.As<Worker>(),
                    branch = Return.As<string>("collect(b.number)")
                });

            var data = query.Results.ToList();

            var workers = new List<string>();
            var rels = new List<object>();
            foreach (var item in data)
            {
                WorkerModel node = new WorkerModel { Id = @"api/worker/" + item.worker.number, name = item.worker.name, number = item.worker.number };

                if (item.worker.IsDispatchable)
                {
                    node.operation = new Operation("Dispatch");
                    node.operation.Id = @"api/worker/" + node.number + @"/dispatch";
                    node.operation.method = "Post";
                }
                if (!string.IsNullOrEmpty(item.branch))
                {
                    var branches = JsonConvert.DeserializeObject<JArray>(item.branch);
                    foreach (var b in branches)
                    {
                        if (null == node.branches) node.branches = new List<MXTires.Microdata.Action>();
                        node.branches.Add(new MXTires.Microdata.Action { Id = @"api/branch/" + b.Value<string>() });
                    }
                }
                workers.Add(node.ToJson());
            }

            return Ok(new { workers });
        }

        [Route("{number}/dispatch"), HttpGet]
        public IHttpActionResult Dispatch([FromBody]Worker worker)
        {
            throw new HttpResponseException(System.Net.HttpStatusCode.NotImplemented);
        }

        public class NodeResult
        {
            public string title { get; set; }
            public string label { get; set; }
        }

    }
}
