using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoteManagement.Core;
using NoteManagement.Core.Interfaces;
using NotesManagemet.Web.Models;

namespace NotesManagemet.Web.Controllers
{
    public class NotesController : Controller
    {
        private readonly ICategoryRetrievalService _categoryRetrievalService;
        private readonly INoteManagementService _noteManagementService;

        public NotesController(ICategoryRetrievalService categoryRetrievalService, INoteManagementService noteManagementService)
        {
            _categoryRetrievalService = categoryRetrievalService;
            _noteManagementService = noteManagementService;
        }
        public async Task<IActionResult> Index()
        {
            var model = new CreateNoteViewModel();
            var categories = await _categoryRetrievalService.GetAllCategories();

            foreach(var cat in categories)
            {
                model.Categories.Add(new SelectListItem() { Text = cat.Name, Value = cat.CategoryId.ToString() });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewNote(CreateNoteViewModel model)
        {
            return Json(true);
        }


    }
}
