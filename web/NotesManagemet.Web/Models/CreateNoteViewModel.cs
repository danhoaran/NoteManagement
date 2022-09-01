using Microsoft.AspNetCore.Mvc.Rendering;

namespace NotesManagemet.Web.Models
{
    public class CreateNoteViewModel
    {
        public CreateNoteViewModel()
        {
            Categories = new List<SelectListItem>();
        }
        public string Body { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
