using NoteManagementApi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementCore.Services
{
    public interface INoteService
    {
        Task<IList<NoteForListingDto>> GetAllNotesAsync();
        Task<NoteForHtmlDto> GetNoteForHtmlAsync(int id);
        Task<NoteDto> GetNoteByIdAsync(int id);
        Task CreateNoteAsync(NoteForCreationDto noteFotCreationDto);
        Task UpdateNoteAsync(NoteForUpdateDto noteForUpdateDto);
        Task DeleteNoteAsync(int id);

    }
}
