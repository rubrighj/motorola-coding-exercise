using NASA_Client.Models;
using NASA_Client.Response;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace NASA_Client.Business
{
    public static class NasaOperations
    {
        public static ImageResponse DownloadRoverImages(string apiKey, string url, string endpoint,
            string photoPath, string earth_date, string camera, int page)
        {
            ImageResponse response = new ImageResponse();

            bool isDateValid = IsDateValid(earth_date);

            if (isDateValid)
            {
                string formattedEarthDate = FormatEarthDate(earth_date);

                List<RoverImage> roverImages = RetrieveImages(apiKey, url, endpoint, formattedEarthDate, camera, page);

                if (roverImages.Count > 0)
                {
                    var folderPath = string.Format(photoPath + "/{0}", formattedEarthDate);

                    var folderExists = FileOperations.FolderExists(folderPath);

                    if (!folderExists)
                    {
                        FileOperations.CreateFolder(folderPath);
                    }

                    int imageCount = FileOperations.GetFileCount(folderPath);

                    if (imageCount < roverImages.Count)
                    {
                        foreach (RoverImage roverImage in roverImages)
                        {
                            FileOperations.SaveImage(folderPath, roverImage.img_src);
                        }
                    }

                    ImageResponse getRoverImagesResponse = GetRoverImages(formattedEarthDate, photoPath);

                    if(getRoverImagesResponse.ResponseCode == 0)
                    {
                        response.LocalImages = getRoverImagesResponse.LocalImages;

                        response.ResponseCode = 0;
                        response.ResponseMessage = string.Format("{0} Images retrieved successfully", response.LocalImages.Count.ToString());
                    }
                    else
                    {
                        response.ResponseCode = -1;
                        response.ResponseMessage = getRoverImagesResponse.ResponseMessage;
                    }
                }
                else
                {
                    response.ResponseCode = -1;
                    response.ResponseMessage = "No images found for the supplied parameters";
                }
            }
            else
            {
                response.ResponseCode = -1;
                response.ResponseMessage = "Invalid Date Provided";
            }

            return response;
        }

        public static ImageResponse GetAllRoverImages(string photoPath)
        {
            ImageResponse response = new ImageResponse();

            var folderExists = FileOperations.FolderExists(photoPath);

            if (folderExists)
            {
               response.LocalImages = FileOperations.GetAllImages(photoPath);
            }
            else
            {
                response.ResponseCode = -1;
                response.ResponseMessage = "Folder Does Not Exist, Download Some Images First";
            }

            return response;
        }

        public static ImageResponse GetRoverImages(string earth_date, string photoPath)
        {
            ImageResponse response = new ImageResponse();

            var isDateValid = IsDateValid(earth_date);

            if(isDateValid)
            {
                var fullPath = string.Format("{0}/{1}", photoPath, earth_date);

                var folderExists = FileOperations.FolderExists(fullPath);

                if (folderExists)
                {
                    response.LocalImages = FileOperations.GetAllImages(fullPath);
                }
                else
                {
                    response.ResponseCode = -1;
                    response.ResponseMessage = "Folder Does Not Exist for the Date Provided";
                }
            }
            else
            {
                response.ResponseCode = -1;
                response.ResponseMessage = "Invalid Date Provided";
            }

            return response;
        }

        public static bool IsDateValid(string earth_date)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(earth_date))
            {
                DateTime outDate;

                isValid = DateTime.TryParse(earth_date, out outDate);
            }

            return isValid;
        }

        public static string FormatEarthDate(string earth_date)
        {
            DateTime outDateTime = DateTime.Parse(earth_date);

            string formattedDate = string.Format("{0}-{1}-{2}", outDateTime.Year.ToString("d4"),
                outDateTime.Month.ToString(), outDateTime.Day.ToString());

            return formattedDate;
        }

        public static List<RoverImage> RetrieveImages(string apiKey, string url, string endpoint,
            string earth_date, string camera, int page)
        {
            List<RoverImage> roverImages = new List<RoverImage>();

            string api = string.Format(endpoint, apiKey, earth_date, camera, page);

            var client = new RestClient(url);
            var request = new RestRequest(api, Method.GET);
            var response = client.Get(request);
            var responseObject = JObject.Parse(response.Content);

            JArray photosArray = JArray.Parse(responseObject.SelectToken("photos", false).ToString());

            foreach (JObject photo in photosArray)
            {
                roverImages.Add(photo.ToObject<RoverImage>());
            }

            return roverImages;
        }
    }
}
