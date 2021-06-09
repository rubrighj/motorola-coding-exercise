using Newtonsoft.Json.Linq;

namespace NASA_Client.Models
{
    public class Rover
    {
        public int id { get; set; }
        public string name { get; set; }
        public string landing_date { get; set; }
        public string launch_date { get; set; }
        public string status { get; set; }

        public Rover() { }

        public Rover(JToken jToken)
        {
            id = int.Parse(jToken["id"].ToString());
            name = jToken["name"].ToString();
            landing_date = jToken["landing_date"].ToString();
            launch_date = jToken["launch_date"].ToString();
            status = jToken["status"].ToString();
        }
    }
}
