using System;
using Newtonsoft.Json;

namespace DockpadAPI.Models
{
    public class Mood
    {
        [JsonProperty(PropertyName = "mood")]
        public int DayMood { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

    }
}
