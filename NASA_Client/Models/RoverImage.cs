using Newtonsoft.Json.Linq;
using System;

namespace NASA_Client.Models
{
    public class RoverImage
    {
        public int id { get; set; }
        public string earth_date { get; set; }
        public string img_src { get; set; }
        public int sol { get; set; }

        public Camera camera { get; set; }
        public Rover rover { get; set; }

        public RoverImage() { }

        public RoverImage(JObject jObject)
        {
            id = int.Parse(jObject.GetValue("id").ToString());
            earth_date = jObject.GetValue("earth_date").ToString();
            img_src = jObject.GetValue("img_src").ToString();
            sol = int.Parse(jObject.GetValue("sol").ToString());

            camera = new Camera(jObject.GetValue("camera"));
            rover = new Rover(jObject.GetValue("rover"));
        }
    }
}
