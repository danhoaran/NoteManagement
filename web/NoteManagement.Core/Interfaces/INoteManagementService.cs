using NoteManagement.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagement.Core.Interfaces
{
    public interface INoteManagementService
    {
        public Task<NoteCreationResponseDto> AddNewNote(NoteForCreationDto DTO);
        public Task<List<NoteForListingDto>> GetAllNotes();
        public Task<NoteForHtmlDto> GetNoteForHtml(int id);
    }
}
