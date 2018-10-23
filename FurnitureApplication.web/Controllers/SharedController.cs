using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureApplication.web.Controllers
{
    public class SharedController : Controller
    {
        
        public JsonResult UploadImages()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                var file = Request.Files[0];

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var path = Path.Combine(Server.MapPath("/images/shared/"), fileName);

                file.SaveAs(path);

                result.Data = new { Success = true, ImageUrl = string.Format("/images/shared/{0}", fileName) };

                //var newImage = new Image() { NameValueCollectionExtensions = fileName };
                //if (ImagesService.Instance.SaveNewImage(newImage))
                //{
                //    result.Data = new { Success = true, Image = fileName, File = newImage.ID, ImageUrl = string.Format("{0}{1}", Variables.ImageFolderPath, fileName) };
                //}
                //else
                //{
                //    result.Data = new { success = false, message = new HttpStatusCodeResult(500) };
                //}
            }
            catch(Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result;
        }
    }
}