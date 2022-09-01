using NoteManagement.Core.Dtos;

namespace NoteManagement.Core.Interfaces
{
    public interface INoteApiService
    {
        Task<List<NoteForListingDto>> GetAllNotes();
        Task<NoteCreationResponseDto> CreateNote(NoteForCreationDto dto);
        Task<NoteForHtmlDto> GetNoteForWebsite(int noteId);
    }
}
