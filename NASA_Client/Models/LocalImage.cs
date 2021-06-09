using System.Collections.Generic;

namespace NASA_Client.Models
{
    public class LocalImage
    {
        public string fileName { get; set; }
        public string folder { get; set; }
        public List<string> img_paths { get; set; }

        public LocalImage() { }
    }
}
