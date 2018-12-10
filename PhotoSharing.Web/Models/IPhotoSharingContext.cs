using System.Data.Entity;
using System.Linq;

namespace PhotoSharing.Web.Models
{
    public interface IPhotoSharingContext
    {
        IQueryable<Photo> Photos { get; }
        IQueryable<Comment> Comments { get; }
        int SaveChanges();
        T Add<T>(T entity) where T : class;
        Photo FindPhotoById(int id);
        Comment FindCommentById(int id);
        T Delete<T>(T entity) where T : class;
    }
}