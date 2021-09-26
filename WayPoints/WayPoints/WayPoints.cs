using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WayPoints
{
    

    public class WayPoints
    {
        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("speed_limit")]
        public double SpeedLimit { get; set; }
    }

    public class Position
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        public override string ToString()
        {
            string LatLongValue = Latitude + ", " + Longitude;
            return LatLongValue;
        }
    }
    
}
