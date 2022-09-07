using NoteManagementCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementApi.Core.DTOs
{
    public class NoteForHtmlDto
    {
        public int NoteId { get; set; }
        public string BodyHtml { get; set; }
        public string CreationDate { get; set; }
        public List<CategoryDto>? Categories { get; set; }
    }
}
