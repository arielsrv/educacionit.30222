using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PhotoSharing.Web.Models;

namespace PhotoSharing.Test
{
    class FakePhotoSharingContext : IPhotoSharingContext
    {
        SetMap map = new SetMap();

        public IQueryable<Photo> Photos
        {
            get { return map.Get<Photo>().AsQueryable(); }
            set { map.Use<Photo>(value); }
        }

        public IQueryable<Comment> Comments
        {
            get { return map.Get<Comment>().AsQueryable(); }
            set { map.Use<Comment>(value); }
        }

        public bool ChangesSaved { get; set; }

        public int SaveChanges()
        {
            ChangesSaved = true;
            return 0;
        }

        public T Add<T>(T entity) where T : class
        {
            map.Get<T>().Add(entity);
            return entity;
        }

        public Photo FindPhotoById(int id)
        {
            Photo item = (from photo in Photos
                          where photo.Id == id
                          select photo).First();

            return item;
        }

        public Comment FindCommentById(int id)
        {
            Comment item = (from comment in this.Comments
                            where comment.Id == id
                            select comment).First();
            return item;
        }


        public T Delete<T>(T entity) where T : class
        {
            map.Get<T>().Remove(entity);
            return entity;
        }

        class SetMap : KeyedCollection<Type, object>
        {
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

            public HashSet<T> Get<T>()
            {
                return (HashSet<T>)this[typeof(T)];
            }

            protected override Type GetKeyForItem(object item)
            {
                return item.GetType().GetGenericArguments().Single();
            }
        }
    }
}
