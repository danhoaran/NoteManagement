using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoteManagement.Core;
using NoteManagement.Core.Dtos;
using NoteManagement.Core.Interfaces;
using NotesManagemet.Web.Models;
using System.Reflection;

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
            if (ModelState.IsValid)
            {
                var dtoForCreation = new NoteForCreationDto();
                dtoForCreation.Body = model.Body;

                var selectedIds = new List<int>();

                foreach (var category in model.Categories)
                {
                    if (category.Selected)
                        selectedIds.Add(Convert.ToInt32(category.Value));
                }

                if (selectedIds.Count == 0)
                    return Json(-1);


                dtoForCreation.CategoryIds = selectedIds;
                var status = await _noteManagementService.AddNewNote(dtoForCreation);
                return Json(status.Success ? 1 : -2);
            } 
            else
            {
                return Json(-3);
            }

        }

        [HttpPost]
        public async Task<JsonResult> LoadAllNotes()
        {
            var notes = await _noteManagementService.GetAllNotes();
            return Json(notes);
        }

    }
}
