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
        //This method puts sample data into the database
        protected override void Seed(PhotoSharingContext context)
        {
            base.Seed(context);

            //Create some photos
            List<Photo> photos = new List<Photo>
            {
                new Photo {
                    Title = "Me standing on top of a mountain",
                    Description = "I was very impressed with myself",
                    UserName = "Fred",
                    PhotoFile = GetFileBytes("\\Images\\flower.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today
                },
                new Photo {
                    Title = "My New Adventure Works Bike",
                    Description = "It's the bees knees!",
                    UserName = "Fred",
                    PhotoFile = GetFileBytes("\\Images\\orchard.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today
                },
                new Photo {
                    Title = "View from the start line",
                    Description = "I took this photo just before we started over my handle bars.",
                    UserName = "Sue",
                    PhotoFile = GetFileBytes("\\Images\\path.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today
                }
            };
            photos.ForEach(photo => context.Photos.Add(photo));
            context.SaveChanges();

            //Create some comments
            var comments = new List<Comment>
            {
                new Comment {                    
                    UserName = "Bert",
                    Subject = "A Big Mountain",
                    Body = "That looks like a very high mountain you have climbed",
                    Photo = photos[0]
                },
                new Comment {                    
                    UserName = "Sue",
                    Subject = "So?",
                    Body = "I climbed a mountain that high before breakfast everyday",
                    Photo = photos[0]
                },
                new Comment {                    
                    UserName = "Fred",
                    Subject = "Jealous",
                    Body = "Wow, that new bike looks great!",
                    Photo = photos[1]
                }
            };
            comments.ForEach(comment => context.Comments.Add(comment));
            context.SaveChanges();
        }

        /// <summary>
        /// Gets the file bytes.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private byte[] GetFileBytes(string path)
        {
            FileStream fileStream = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] bytes;
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                bytes = reader.ReadBytes((int)fileStream.Length);
            }
            return bytes;
        }

    }
}