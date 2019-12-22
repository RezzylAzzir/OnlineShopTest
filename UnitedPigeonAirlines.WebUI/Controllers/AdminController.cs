using System.Web.Mvc;
using UnitedPigeonAirlines.Domain.Abstract;
using UnitedPigeonAirlines.Domain.Entities;
using System.Linq;
using System.Web;
using UnitedPigeonAirlines.Data.Repositories;
using UnitedPigeonAirlines.Domain.Concrete;
using UnitedPigeonAirlines.Data.Entities;
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
                if (pigeon.PigeonId != 0)
                {
                    repository.SavePigeon(repository.GetPigeon(pigeon.PigeonId));
                }
                else
                {
                    repository.SavePigeon(pigeon.ToPigeon());
                }
                TempData["message"] = string.Format("Changes in pigeon \"{0}\" info were saved", pigeon.PigeonName);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
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