using PhotoSharing.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoSharing.Web.Controllers
{
    public class PhotoController : Controller
    {
        private PhotoSharingContext context = new PhotoSharingContext();
        // GET: Photo
        public ActionResult Index()
        {
            return View(context.Photos.ToList());
        }

        public ActionResult Display(int id)
        {
            var photo = context.Photos.FirstOrDefault(p => p.PhotoID == id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            else
            {

                return View(photo);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            var photo = new Photo();

            photo.CreatedDate = DateTime.Now.Date;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.CreatedDate = DateTime.Now.Date;

            if (ModelState.IsValid && image != null) {
                
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                    context.Photos.Add(photo);
                    return RedirectToAction("Index");

            }
            else
            {
                return View(photo);
            }
            
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var photo = context.Photos.FirstOrDefault(p => p.PhotoID == id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            else
            {

                return View(photo);
            }
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var photo = context.Photos.FirstOrDefault(p => p.PhotoID == id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Photos.Remove(photo);
                return RedirectToAction("Index");
            }
            
        }

        public FileContentResult GetImage(int id)
        {
            var photo = context.Photos.FirstOrDefault(p => p.PhotoID == id);
            if (photo == null)
            {
                return null;
            }
            else
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }
        }

        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
            var photos = context.Photos.ToList();
            if (number != 0) {
                photos = photos.Take(number).ToList();
            }
            return View(photos);

            

         

        }

    }
}