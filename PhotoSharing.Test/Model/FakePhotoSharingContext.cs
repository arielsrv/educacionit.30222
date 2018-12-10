using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PhotoSharing.Web.Models;

namespace PhotoSharing.Test
{
    class FakePhotoSharingContext : IPhotoSharingContext
    {
        InternalDatabase db = new InternalDatabase();

        /// <summary>
        /// Gets or sets the photos.
        /// </summary>
        /// <value>
        /// The photos.
        /// </value>
        public IQueryable<Photo> Photos
        {
            get { return db.Get<Photo>().AsQueryable(); }
            set { db.Use<Photo>(value); }
        }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public IQueryable<Comment> Comments
        {
            get { return db.Get<Comment>().AsQueryable(); }
            set { db.Use<Comment>(value); }
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
            db.Get<T>().Add(entity);
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
            db.Get<T>().Remove(entity);
            return entity;
        }

        class InternalDatabase : KeyedCollection<Type, object>
        {
            /// <summary>
            /// Uses the specified source data.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="sourceData">The source data.</param>
            /// <returns></returns>
            public HashSet<T> Use<T>(IEnumerable<T> sourceData)
            {
                var set = new HashSet<T>(sourceData);
                if (Contains(typeof(T)))
                {
                    Remove(typeof(T));
                }
                Add(set);
                return set;
            }

            /// <summary>
            /// Gets this instance.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public HashSet<T> Get<T>()
            {
                return (HashSet<T>)this[typeof(T)];
            }

            /// <summary>
            /// When implemented in a derived class, extracts the key from the specified element.
            /// </summary>
            /// <param name="item">The element from which to extract the key.</param>
            /// <returns>
            /// The key for the specified element.
            /// </returns>
            protected override Type GetKeyForItem(object item)
            {
                return item.GetType().GetGenericArguments().Single();
            }
        }
    }
}
