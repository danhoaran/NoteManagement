using NoteManagement.Core.Dtos;
using NoteManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagement.Services
{
    public class NoteManagementService : INoteManagementService
    {
        private readonly INoteApiService _noteApiService;

        public NoteManagementService(INoteApiService noteApiService)
        {
            _noteApiService = noteApiService;
        }
        public async Task<NoteCreationResponseDto> AddNewNote(NoteForCreationDto dto)
        {
            return await _noteApiService.CreateNote(dto);
        }

        public async Task<List<NoteForListingDto>> GetAllNotes()
        {
            return await _noteApiService.GetAllNotes();
        }

        public async Task<NoteForHtmlDto> GetNoteForHtml(int id)
        {
            return await _noteApiService.GetNoteForWebsite(id);
        }
    }
}
