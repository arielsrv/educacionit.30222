using System.Data.Entity;

namespace PhotoSharing.Web.Models
{
    public class PhotoSharingContext : DbContext
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
        /// Initializes a new instance of the <see cref="PhotoSharingContext"/> class.
        /// </summary>
        public PhotoSharingContext() : base("PhotoSharingDb")
        {
        }
    }
}