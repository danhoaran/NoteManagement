using AutoMapper;
using Markdig;
using Microsoft.EntityFrameworkCore;
using NoteManagementApi.Core.DTOs;
using NoteManagementCore.Models;
using NoteManagementCore.Services;
using NoteManagementInfrastructure;
using System.Linq.Expressions;

namespace NoteManagementServices.Services
{
    public class NoteService : INoteService
    {
        private readonly IMapper _mapper;
        private readonly NoteManagementContext _context;
        private readonly ICategoryService _categoryService;

        public NoteService(IMapper mapper, NoteManagementContext context, ICategoryService categoryService)
        {
            _mapper = mapper;
            _context = context;
            _categoryService = categoryService;
        }

        public async Task CreateNoteAsync(NoteForCreationDto noteFotCreationDto)
        {
            var allCategories = _context.Categories.ToList();
            var categoriesToAdd = new List<Category>();

            foreach (var categoryId in noteFotCreationDto.CategoryIds)
            {
                var category = allCategories.FirstOrDefault(x => x.CategoryId == categoryId);
                if (category != null)
                {
                    categoriesToAdd.Add(category);
                }
            }

            if (categoriesToAdd == null || categoriesToAdd.Count == 0)
            {
                throw new ArgumentNullException("Problem creating note - please make sure at least one category is added");
            }

            var entity = _mapper.Map<Note>(noteFotCreationDto);

            entity.Categories = categoriesToAdd;

            await _context.Notes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await _context.Notes.Include(x => x.Categories).Where(x => x.NoteId == id).FirstOrDefaultAsync();

            if (note == null)
                throw new KeyNotFoundException("No note found with specified id");

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

        }

        public async Task<IList<NoteForListingDto>> GetAllNotesAsync()
        {
            var notes = await _context.Notes.Include(x => x.Categories).ToListAsync();

            if (notes == null)
                return new List<NoteForListingDto>();

            return _mapper.Map<List<NoteForListingDto>>(notes);
        }

        public async Task<NoteForHtmlDto> GetNoteForHtmlAsync(int id)
        {
            var note = await _context.Notes.Include(x => x.Categories).Where(x => x.NoteId == id).FirstOrDefaultAsync();

            if (note == null)
                throw new KeyNotFoundException("No note found with specified id");

            var converted = Markdown.ToHtml(note.Body);
            note.Body = converted.Trim();

            return _mapper.Map<NoteForHtmlDto>(note);
        }

        public async Task<NoteDto> GetNoteByIdAsync(int id)
        {
            var note = await _context.Notes.Include(x => x.Categories).Where(x => x.NoteId == id).FirstOrDefaultAsync();

            if (note == null)
                throw new KeyNotFoundException("No note found with specified id");

            return _mapper.Map<NoteDto>(note);
        }

        public async Task UpdateNoteAsync(NoteForUpdateDto noteForUpdateDto)
        {
            var existing = await _context.Notes.Include(x => x.Categories).Where(x => x.NoteId == noteForUpdateDto.NoteId).FirstOrDefaultAsync();

            if (existing == null)
                throw new KeyNotFoundException("No note found with specified id");

            var allCategories = _context.Categories.ToList();
            var categoriesToAdd = new List<Category>();

            foreach (var categoryId in noteForUpdateDto.CategoryIds)
            {
                var category = allCategories.FirstOrDefault(x => x.CategoryId == categoryId);
                if (category != null)
                {
                    categoriesToAdd.Add(category);
                }
            }

            if (categoriesToAdd == null || categoriesToAdd.Count == 0)
            {
                throw new ArgumentNullException("Problem updating note - please make sure at least one category is added");
            }

            existing.Categories = categoriesToAdd;
            existing.Body = noteForUpdateDto.Body;

            await _context.SaveChangesAsync();
        }
    }
}
