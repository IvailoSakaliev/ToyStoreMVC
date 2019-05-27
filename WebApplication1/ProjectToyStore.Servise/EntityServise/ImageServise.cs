using ProjectToyStore.Data.Models;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace ProjectToyStore.Servise.EntityServise
{
    public class ImageServise
        :GenericServise<Images>
    {
        
        public ImageServise()
            :base()
        {
        }

       

        public string UploadImages(IFormFile[] photo, int productID)
        {
            if (photo == null || photo.Length == 0)
            {
                return "File not selected";
            }
            else
            {
                int i = 1;
                foreach (IFormFile item in photo)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Galery/", item.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    item.CopyToAsync(stream);
                    if (i != 1)
                    {
                        Images img = new Images();
                        img.Path = "../images/Galery/" + item.FileName;
                        img.Subject_id = productID;
                        Save(img);
                    }
                    i++;
                    
                }
            }
            return "Uploaded";
        }
        public string UploadImagesForUser(IFormFile[] photo)
        {
            if (photo == null || photo.Length == 0)
            {
                return "File not selected";
            }
            else
            {
                foreach (IFormFile item in photo)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Galery/", item.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    item.CopyToAsync(stream);
                }
            }
            return "Uploaded";
        }
    }
}
