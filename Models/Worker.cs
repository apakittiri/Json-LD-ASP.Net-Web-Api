using MXTires.Microdata;
using System.Collections.Generic;

namespace NetWebApi_Json_LD.Models
{
    public class WorkerModel : Person
    {
        public override string Type
        {
            get
            {
                return "Person";
            }
        }

        public string name { get; set; }

        public string number { get; set; }

        public Operation operation { get; set; }

        public List<Action> branches { get; set; }

    }

    public class Worker
    {
        public string name { get; set; }

        public string number { get; set; }

        public bool IsDispatchable { get; set; }

    }

    public class Operation : Action
    {
        private string _type;

        public Operation(string type)
        {
            _type = type;
        }

        public override string Type
        {
            get
            {
                return _type;
            }
        }
        public string method { get; set; }

    }

}