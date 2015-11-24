﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace SmartSpaceWeb.Models
{
    public class Sensor
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

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

        [JsonProperty(PropertyName = "contatore")]
        public string Contatore { get; set; }


    }

}