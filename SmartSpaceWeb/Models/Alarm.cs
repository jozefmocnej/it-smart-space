using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace SmartSpaceWeb.Models
{
    public class Alarm
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "idDevice")]
        public string IdDevice { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "atLocation")]
        public string AtLocation { get; set; }

        [JsonProperty(PropertyName = "Place")]
        public string Place { get; set; }

        [JsonProperty(PropertyName = "Min")]
        public int Min { get; set; }

        [JsonProperty(PropertyName = "Max")]
        public int Max { get; set; }
    }
}