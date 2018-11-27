using PhotoSharing.Web.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoSharing.Web.Controllers
{
    [LogActionFilter]
    public class PhotoController : Controller
    {
        private PhotoSharingContext context = new PhotoSharingContext();

        //
        // GET: /Photo/

        public ActionResult Index()
        {
            return View("Index", context.Photos.ToList());
        }

        public ActionResult Display(int id)
        {
            Photo photo = context.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }

        public ActionResult Create()
        {
            Photo photo = new Photo
            {
                CreatedDate = DateTime.Today
            };

            return View("Create", photo);
        }

        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.CreatedDate = DateTime.Today;
            if (!ModelState.IsValid)
            {
                return View("Create", photo);
            }
            else
            {
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                }
                context.Photos.Add(photo);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            Photo photo = context.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Delete", photo);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = context.Photos.Find(id);
            context.Photos.Remove(photo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int id)
        {
            Photo photo = context.Photos.Find(id);
            if (photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}