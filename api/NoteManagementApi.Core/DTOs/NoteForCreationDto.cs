﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementApi.Core.DTOs
{
    public class NoteForCreationDto
    {
        public string Body { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
