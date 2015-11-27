using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace SmartSpaceWeb.Models
{
    public class SensorSearch
    {
        //tepelny, svetelny...
        [DisplayName("Type")]
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        public List<SelectListItem> Types { get; set; }

        //ktora miestnost
        [DisplayName("Location")]
        [JsonProperty(PropertyName = "atLocation")]
        public string AtLocation { get; set; }

        public List<SelectListItem> Locations { get; set; }

        ////kde v miestnosti
        //[JsonProperty(PropertyName = "Place")]
        //public string Place { get; set; }

        //[JsonProperty(PropertyName = "timestamp")]
        //public string Timestamp { get; set; }

        //[JsonProperty(PropertyName = "Flag")]
        //public int Flag { get; set; }

        public SensorSearch()
        {
            Locations = new List<SelectListItem>();
            Types = new List<SelectListItem>();
        }
    }

}