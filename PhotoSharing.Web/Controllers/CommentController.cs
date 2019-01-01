using PhotoSharing.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace PhotoSharing.Web.Controllers
{
    public class CommentController : Controller
    {
        private IPhotoSharingContext context;

        //Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        public CommentController()
        {
            context = new PhotoSharingContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        /// <param name="Context">The context.</param>
        public CommentController(IPhotoSharingContext Context)
        {
            context = Context;
        }

        //
        // GET: A Partial View for displaying in the Photo Details view
        /// <summary>
        /// Commentses for photo.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <returns></returns>
        [ChildActionOnly] //This attribute means the action cannot be accessed from the browser's address bar
        public PartialViewResult _CommentsForPhoto(int photoId)
        {
            //The comments for a particular photo have been requested. Get those comments.
            var comments = from c in context.Comments
                           where c.Photo.Id == photoId
                           select c;
            //Save the PhotoID in the ViewBag because we'll need it in the view
            ViewBag.PhotoId = photoId;
            return PartialView(comments.ToList());
        }

        //
        //POST: This action creates the comment when the AJAX comment create tool is used
        /// <summary>
        /// Commentses for photo.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="photoId">The photo identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult _CommentsForPhoto(Comment comment, int photoId)
        {
            //Save the new comment
            context.Add<Comment>(comment);
            context.SaveChanges();

            //Get the updated list of comments
            var comments = from c in context.Comments
                           where c.Photo.Id == photoId
                           select c;
            //Save the PhotoID in the ViewBag because we'll need it in the view
            ViewBag.PhotoId = photoId;
            //Return the view with the new list of comments
            return PartialView("_CommentsForPhoto", comments.ToList());
        }

        //
        // GET: /Comment/_Create. A Partial View for displaying the create comment tool as a AJAX partial page update
        /// <summary>
        /// Creates the specified photo identifier.
        /// </summary>
        /// <param name="photoId">The photo identifier.</param>
        /// <returns></returns>
        [Authorize]
        public PartialViewResult _Create(int photoId)
        {
            //Create the new comment
            Comment newComment = new Comment();
            newComment.PhotoId = photoId;

            ViewBag.PhotoID = photoId;
            return PartialView("_CreateAComment");
        }

        //
        // GET: /Comment/Delete/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Comment comment = context.FindCommentById(id);
            ViewBag.PhotoID = comment.Photo.Id;
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        //
        // POST: /Comment/Delete/5
        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = context.FindCommentById(id);
            context.Delete<Comment>(comment);
            context.SaveChanges();
            return RedirectToAction("Display", "Photo", new { id = comment.PhotoId });
        }
    }
}