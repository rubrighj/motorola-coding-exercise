using NASA_Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace NASA_Client.Business
{
    public static class FileOperations
    {
        public static bool FolderExists(string path)
        {
            bool exists = Directory.Exists(path);

            return exists;
        }

        public static bool CreateFolder(string path)
        {
            bool created;

            try
            {
                Directory.CreateDirectory(path);

                created = FolderExists(path);
            }
            catch
            {
                created = false;
            }

            return created;
        }

        public static void SaveImage(string path, string imagePath)
        {
            using (WebClient client = new WebClient())
            {
                var fileName = Path.GetFileName(imagePath);

                client.DownloadFile(new Uri(imagePath), string.Format(path + @"\{0}", fileName));
            }
        }

        public static int GetFileCount(string path)
        {
            return Directory.GetFiles(path).Length;
        }

        public static List<LocalImage> GetAllImages(string path)
        {
            List<LocalImage> localImages = new List<LocalImage>();

            List<string> directories = Directory.GetDirectories(path).ToList();

            if(directories.Count > 0)
            {
                foreach (string dir in directories)
                {
                    localImages.Add(new LocalImage
                    {
                        folder = Path.GetFileName(dir),
                        img_paths = Directory.GetFiles(dir).ToList()
                    });
                }
            }
            else
            {
                localImages.Add(new LocalImage
                {
                    folder = Path.GetFileName(path),
                    img_paths = Directory.GetFiles(path).ToList()
                });
            }
            

            return localImages;
        }
    }
}
