using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NASA_Client.Business;
using NASA_Client.Request;
using NASA_Client.Response;

namespace NASA_Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public ImageController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Produces("application/json")]
        [Route("DownloadRoverImages"), HttpPost]
        public JsonResult DownloadRoverImages([FromBody] ImageRequest request)
        {
            ImageResponse response = NasaOperations.DownloadRoverImages(Configuration["ApiKey"].ToString(),
                Configuration["URL"].ToString(), request.camera.Equals("all") ? Configuration["EndPointAll"].ToString() : Configuration["EndPoint"].ToString(),
                Configuration["PhotoPath"].ToString(),
                request.earth_date, request.camera, request.page);

            return new JsonResult(response);
        }

        [Produces("application/json")]
        [Route("GetAllRoverImages"), HttpGet]
        public JsonResult GetAllRoverImages()
        {
            ImageResponse response = NasaOperations.GetAllRoverImages(Configuration["PhotoPath"].ToString());

            return new JsonResult(response);
        }

        [Produces("application/json")]
        [Route("GetRoverImages"), HttpPost]
        public JsonResult GetRoverImages([FromBody] ImageRequest request)
        {
            ImageResponse response = NasaOperations.GetRoverImages(request.earth_date, Configuration["PhotoPath"].ToString());

            return new JsonResult(response);
        }
    }
}
