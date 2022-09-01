using Markdig;
using Microsoft.AspNetCore.Mvc;
using NoteManagementCore.DTOs;
using NoteManagementCore.Services;
using NoteManagementServices.Services;
using System.Linq.Expressions;

namespace NoteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteDto>>> GetAllNotes()
        {
            return Ok(await _noteService.GetAllNotesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetNote(int id)
        {
            return new NoteDto();
        }

        [HttpGet("{id}/html")]
        public async Task<ActionResult<NoteForHtmlDto>> GetNoteWithHtml(int id)
        {
            try
            {
                return Ok(await _noteService.GetNoteForHtmlAsync(id));
            } 
            catch (KeyNotFoundException)
            {
                return BadRequest("note id not found");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(NoteForCreationDto noteForCreationDto)
        {
            await _noteService.CreateNoteAsync(noteForCreationDto);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Update(NoteForUpdateDto dto)
        {
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return NoContent();
        }
    }
}
