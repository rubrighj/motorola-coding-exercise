using Newtonsoft.Json.Linq;

namespace NASA_Client.Models
{
    public class Camera
    {
        public string full_name { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int rover_id { get; set; }

        public Camera() { }

        public Camera(JToken jToken)
        {
            full_name = jToken["full_name"].ToString();
            id = int.Parse(jToken["id"].ToString());
            name = jToken["name"].ToString();
            rover_id = int.Parse(jToken["rover_id"].ToString());
        }
    }
}
