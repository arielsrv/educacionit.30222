using PhotoSharing.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoSharing.Test
{
    public class FakePhotoSharingContext : IPhotoSharingContext
    {
        private IDictionary<Type, object> db = new Dictionary<Type, object>();

        /// <summary>
        /// Gets or sets the photos.
        /// </summary>
        /// <value>
        /// The photos.
        /// </value>
        public IQueryable<Photo> Photos
        {
            get
            {
                Type key = typeof(List<Photo>);
                return ((List<Photo>)db[key]).AsQueryable();
            }
            set
            {
                Type key = typeof(List<Photo>); 

                if (db.ContainsKey(key))
                    db.Remove(key);

                db.Add(key, new List<Photo>(value));
            }
        }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public IQueryable<Comment> Comments
        {
            get
            {
                Type key = typeof(List<Comment>);
                return ((List<Comment>)db[key]).AsQueryable();
            }
            set
            {
                Type key = typeof(List<Comment>);

                if (db.ContainsKey(key))
                    db.Remove(key);

                db.Add(key, new List<Comment>(value));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [changes saved].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [changes saved]; otherwise, <c>false</c>.
        /// </value>
        public bool ChangesSaved { get; set; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            ChangesSaved = true;
            return 0;
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public T Add<T>(T entity) where T : class
        {
            Type type = typeof(List<T>);
            ((List<T>)db[type]).Add(entity);
            return entity;
        }

        /// <summary>
        /// Finds the photo by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Photo FindPhotoById(int id)
        {
            Photo item = (from photo in Photos
                          where photo.Id == id
                          select photo).First();

            return item;
        }

        /// <summary>
        /// Finds the comment by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Comment FindCommentById(int id)
        {
            Comment item = (from comment in this.Comments
                            where comment.Id == id
                            select comment).First();
            return item;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public T Delete<T>(T entity) where T : class
        {
            Type type = typeof(List<T>);
            ((List<T>)db[type]).Remove(entity);
            return entity;
        }

        /// <summary>
        /// Finds the photo by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public Photo FindPhotoByTitle(string title)
        {
            Photo item = (from p in this.Photos
                          where p.Title == title
                          select p).FirstOrDefault();

            return item;
        }
    }
}