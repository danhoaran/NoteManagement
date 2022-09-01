using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementCore.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
    }
}
