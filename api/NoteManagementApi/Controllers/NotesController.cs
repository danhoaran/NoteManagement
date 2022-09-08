using Markdig;
using Microsoft.AspNetCore.Mvc;
using NoteManagementApi.Core.DTOs;
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
        private readonly ILogger _logger;

        public NotesController(INoteService noteService, ILogger<NotesController> logger)
        {
            _noteService = noteService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<NoteForListingDto>>> GetAllNotes()
        {
            _logger.LogInformation("GetAllNotes accessed at: {time}", DateTimeOffset.UtcNow);
            return Ok(await _noteService.GetAllNotesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetNote(int id)
        {
            _logger.LogInformation("GetNote accessed at: {time}", DateTimeOffset.UtcNow);
            try
            {
                return Ok(await _noteService.GetNoteByIdAsync(id));
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("note id not found");
            }
        }

        [HttpGet("{id}/html")]
        public async Task<ActionResult<NoteForHtmlDto>> GetNoteWithHtml(int id)
        {
            _logger.LogInformation("GetNoteWithHtml accessed at: {time}", DateTimeOffset.UtcNow);
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
        public async Task<ActionResult<NoteCreationResponseDto>> Create(NoteForCreationDto noteForCreationDto)
        {
            _logger.LogInformation("Create note accessed at: {time}", DateTimeOffset.UtcNow);
            await _noteService.CreateNoteAsync(noteForCreationDto);
            return Ok(new NoteCreationResponseDto { Success = true });
        }

        [HttpPut]
        public async Task<ActionResult> Update(NoteForUpdateDto dto)
        {
            _logger.LogInformation("Update note api accessed at: {time}", DateTimeOffset.UtcNow);
            await _noteService.UpdateNoteAsync(dto);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Delete note api accessed at: {time}", DateTimeOffset.UtcNow);
            await _noteService.DeleteNoteAsync(id);
            return NoContent();
        }
    }
}
