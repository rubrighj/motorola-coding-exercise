using NASA_Client.Models;
using System.Collections.Generic;

namespace NASA_Client.Response
{
    public class ImageResponse : BaseResponse
    {
        public List<LocalImage> LocalImages { get; set; }

        public ImageResponse()
        {
            LocalImages = new List<LocalImage>();
        }
    }
}
