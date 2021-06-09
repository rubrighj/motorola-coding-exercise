namespace NASA_Client.Request
{
    public class ImageRequest
    {
        public string earth_date { get; set; }
        public string camera { get; set; }
        public int page { get; set; }

        public ImageRequest() { }
    }
}
