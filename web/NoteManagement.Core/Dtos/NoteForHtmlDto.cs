using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagement.Core.Dtos
{
    public class NoteForHtmlDto
    {
        public int NoteId { get; set; }
        public string BodyHtml { get; set; }
        public DateTime CreationDate { get; set; }
        public List<CategoryDto>? Categories { get; set; }
    }
}
