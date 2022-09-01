using Microsoft.AspNetCore.Mvc;
using NotesManagemet.Web.Models;

namespace NotesManagemet.Web.Controllers
{
    public class NotesController : Controller
    {
        public IActionResult Index()
        {
            var model = new CreateNoteViewModel();
            return View(model);
        }
    }
}
