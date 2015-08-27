using MXTires.Microdata;
using Newtonsoft.Json;

namespace NetWebApi_Json_LD.Models
{
    public class Branch : Place
    {
        public override string Type
        {
            get
            {
                return "Place";
            }
        }

        [JsonProperty("number")]
        public string number { get; set; }

        public string Location { get; set; }

    }
}