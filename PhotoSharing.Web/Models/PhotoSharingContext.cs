using System.Data.Entity;
using System.Linq;

namespace PhotoSharing.Web.Models
{
    public class PhotoSharingContext : DbContext, IPhotoSharingContext
    {
        /// <summary>
        /// Gets or sets the photos.
        /// </summary>
        /// <value>
        /// The photos.
        /// </value>
        public DbSet<Photo> Photos { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public DbSet<Comment> Comments { get; set; }
        /// <summary>
        /// Gets the photos.
        /// </summary>
        /// <value>
        /// The photos.
        /// </value>
        IQueryable<Photo> IPhotoSharingContext.Photos
        {
            get { return Photos; }
        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        IQueryable<Comment> IPhotoSharingContext.Comments
        {
            get { return Comments; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoSharingContext"/> class.
        /// </summary>
        public PhotoSharingContext() : base("PhotoSharingDb")
        {
        }

        public T Add<T>(T entity) where T : class
        {
            return Set<T>().Add(entity);
        }

        public Photo FindPhotoById(int id)
        {
            return Set<Photo>().Find(id);
        }

        public Comment FindCommentById(int id)
        {
            return Set<Comment>().Find(id);
        }

        public T Delete<T>(T entity) where T : class
        {
            return Set<T>().Remove(entity);
        }

        public Photo FindPhotoByTitle(string title)
        {
            Photo photo = (from p in Set<Photo>()
                           where p.Title == title
                           select p).FirstOrDefault();
            return photo;
        }
    }
}