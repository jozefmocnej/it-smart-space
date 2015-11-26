using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartSpaceWeb.Models
{
    public class Sensor
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

        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "Counter")]
        public string Counter { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "Flag")]
        public int Flag { get; set; }
    }

}