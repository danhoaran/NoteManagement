using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementCore.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Note>? Notes { get; set; }
    }
}
