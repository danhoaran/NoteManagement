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

        public Task DeleteNoteAsync(int id)
        {
            throw new NotImplementedException();
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
            var note = await _context.Notes.Include(x => x.Categories).FirstOrDefaultAsync();

            if (note == null)
                throw new KeyNotFoundException("No note found with specified id");

            note.Body = Markdown.ToHtml(note.Body);

            return _mapper.Map<NoteForHtmlDto>(note);
        }

        public Task<NoteDto> GetNoteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateNoteAsync(NoteForUpdateDto noteForUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
