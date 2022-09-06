using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace NotesManagemet.Web.Models
{
    public class CreateNoteViewModel
    {
        public CreateNoteViewModel()
        {
            Categories = new List<SelectListItem>();
        }
        [MaxLength(70)]
        public string Body { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
