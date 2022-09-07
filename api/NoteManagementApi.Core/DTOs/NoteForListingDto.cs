using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementApi.Core.DTOs
{
    public class NoteForListingDto
    {
        public int NoteId { get; set; }
        public string Body { get; set; }
        public string CreationDate { get; set; }
        public List<string>? Categories { get; set; }
    }
}
