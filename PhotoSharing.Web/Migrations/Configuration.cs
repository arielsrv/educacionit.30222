namespace PhotoSharing.Web.Migrations
{
    using PhotoSharing.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotoSharing.Web.Models.PhotoSharingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PhotoSharing.Web.Models.PhotoSharingContext";
        }

        protected override void Seed(PhotoSharing.Web.Models.PhotoSharingContext context)
        {
            base.Seed(context);

            //Create some photos
            List<Photo> photos = new List<Photo>
            {
                new Photo {
                    Title = "Me standing on top of a mountain",
                    Description = "I was very impressed with myself",
                    UserName = "Fred",
                    PhotoFile = GetFileBytes(@"\Images\flower.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today
                },
                new Photo {
                    Title = "My New Adventure Works Bike",
                    Description = "It's the bees knees!",
                    UserName = "Fred",
                    PhotoFile = GetFileBytes(@"\Images\orchard.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today
                },
                new Photo {
                    Title = "View from the start line",
                    Description = "I took this photo just before we started over my handle bars.",
                    UserName = "Sue",
                    PhotoFile = GetFileBytes(@"\Images\path.jpg"),
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
            string solutionFolder =  AppDomain.CurrentDomain.BaseDirectory + "..\\";
            FileStream fileOnDisk = new FileStream(string.Concat(solutionFolder, path), FileMode.Open);
            byte[] bytes;
            using (BinaryReader reader = new BinaryReader(fileOnDisk))
            {
                bytes = reader.ReadBytes((int)fileOnDisk.Length);
            }
            return bytes;
        }
    }
}
