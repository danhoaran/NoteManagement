using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementApi.Core.DTOs
{
    public class NoteForCreationDto
    {
        [Required]
        public string Body { get; set; }
        [Required]
        public List<int> CategoryIds { get; set; }
    }
}
