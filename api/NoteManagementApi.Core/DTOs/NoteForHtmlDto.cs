using NoteManagementCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementCore.DTOs
{
    public class NoteForHtmlDto
    {
        public int NoteId { get; set; }
        public string BodyHtml { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<CategoryDto>? Categories { get; set; }
    }
}
