using NUnit.Framework;
using System.IO;

using NASA_Client.Models;
using NASA_Client.Business;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NASA_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDataConsistency()
        {
            Stream downloadStream = new FileStream(@"../../../Data/DownloadTest.json", FileMode.Open);
            StreamReader streamReader = new StreamReader(downloadStream);

            string testJSON = streamReader.ReadToEnd();
            var testJSONObject = JObject.Parse(testJSON);

            streamReader.Close();

            var client = new RestClient("https://api.nasa.gov/mars-photos");
            var request = new RestRequest("api/v1/rovers/curiosity/photos?earth_date=2015-6-3&camera=FHAZ&page=1&api_key=<DEMO_KEY>", Method.GET);
            var response = client.Get(request);
            var responseObject = JObject.Parse(response.Content);

            Assert.AreEqual(testJSONObject, responseObject);
        }

        [Test]
        public void TestProvidedValues()
        {
            Stream downloadStream = new FileStream(@"../../../Data/dates.txt", FileMode.Open);
            StreamReader streamReader = new StreamReader(downloadStream);

            List<bool> validationStates = new List<bool>();

            while(!streamReader.EndOfStream)
            {
                var earth_date = streamReader.ReadLine();

                if (NasaOperations.IsDateValid(earth_date))
                {
                    var formattedDate = NasaOperations.FormatEarthDate(earth_date);
                    var endpoint = string.Format("api/v1/rovers/curiosity/photos?earth_date={0}&api_key={1}",
                        formattedDate, "zkg7i3EpdaDZyYT4C19GpzFEQCeQ32FW91uWs68P");

                    List<RoverImage> roverImages = NasaOperations.RetrieveImages("<DEMO_KEY>", "https://api.nasa.gov/mars-photos",
                        endpoint, formattedDate, "all", 1);

                    if (roverImages.Count > 0)
                    {
                        var folderPath = string.Format("../../../photos" + "/{0}", formattedDate);

                        var folderExists = FileOperations.FolderExists(folderPath);

                        if (!folderExists)
                        {
                            FileOperations.CreateFolder(folderPath);
                        }

                        int imageCount = FileOperations.GetFileCount(folderPath);

                        if (imageCount < roverImages.Count)
                        {
                            Parallel.ForEach(roverImages, roverImage =>
                            {
                                FileOperations.SaveImage(folderPath, roverImage.img_src);
                            });
                            
                            validationStates.Add(true);
                        }
                        else
                        {
                            validationStates.Add(true);
                        }
                    }
                }
                else
                {
                    validationStates.Add(false);
                }
            }

            Assert.IsTrue(validationStates[0]);
            Assert.IsTrue(validationStates[1]);
            Assert.IsTrue(validationStates[2]);
            Assert.IsFalse(validationStates[3]);
        }

        [Test]
        public void TestDateValidation()
        {
            var isValid1 = NasaOperations.IsDateValid("02/27/17");
            var isValid2 = NasaOperations.IsDateValid("June 2, 2018");
            var isValid3 = NasaOperations.IsDateValid("Jul-13-2016");
            var isValid4 = NasaOperations.IsDateValid("April 31, 2018");

            Assert.IsTrue(isValid1);
            Assert.IsTrue(isValid2);
            Assert.IsTrue(isValid3);
            Assert.IsFalse(isValid4);
        }
    }
}