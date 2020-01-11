using System.Web.Mvc;
using System.Linq;
using System.Web;
using UnitedPigeonAirlines.EF.Repositories;
using UnitedPigeonAirlines.Data.Entities.PigeonAggregate;
using UnitedPigeonAirlines.WebUI.Models;

namespace UnitedPigeonAirlines.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        EFPigeonRepository repository;

        public AdminController(EFPigeonRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.GetAllPigeons());
        }
        public ViewResult Edit(int pigeonId)
        {



            Pigeon pigeon = repository.GetAllPigeons()
                .FirstOrDefault(g => g.PigeonId== pigeonId);
            PigeonViewModel pigeonDTO = new PigeonViewModel(pigeon);
            
            return View(pigeonDTO);
        }
        [HttpPost]
        public ActionResult Edit(PigeonViewModel pigeon, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    pigeon.ImageMimeType = image.ContentType;
                    pigeon.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(pigeon.ImageData, 0, image.ContentLength);
                }
                
                    repository.SavePigeon(pigeon.ToPigeon());
                
                TempData["message"] = string.Format("Changes in pigeon \"{0}\" info were saved", pigeon.PigeonName);
                return RedirectToAction("Index");
            }
            else
            {
                return View(pigeon);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new PigeonViewModel());
        }
        public ActionResult Delete(int pigeonId)
        {
            Pigeon deletedPigeon = repository.DeletePigeon(pigeonId);
            if (deletedPigeon != null)
            {
                TempData["message"] = string.Format("Pigeon \"{0}\" was deleted",
                    deletedPigeon.PigeonName);
            }
            return RedirectToAction("Index");
        }
    }
}