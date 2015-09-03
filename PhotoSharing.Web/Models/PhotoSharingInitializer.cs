using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotoSharing.Web.Models
{
    public class PhotoSharingInitializer : DropCreateDatabaseAlways<PhotoSharingContext>
    {
        protected override void Seed(PhotoSharingContext context)
        {
            base.Seed(context);

            var photos = new List<Photo>
            {
                new Photo {
                    Title = "Test Photo",
                    Description = "Photo test",
                    UserName = "NaokiSato",
                    PhotoFile = getFileBytes("\\Images\\flower.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Now.Date
                },
            };

            photos.ForEach(s => context.Photos.Add(s));
            context.SaveChanges();

            var comments = new List<Comment>
            {
                new Comment {
                    PhotoID = 1,
                    UserName = "NaokiSato",
                    Subject = "Test Comment",
                    Body = "This comment should appear in photo 1"
                }
            };
            comments.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();
        }

        private byte[] getFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }
            return fileBytes;
        }
    }
}