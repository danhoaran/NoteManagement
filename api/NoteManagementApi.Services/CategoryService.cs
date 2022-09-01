using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NoteManagementCore.DTOs;
using NoteManagementCore.Models;
using NoteManagementCore.Services;
using NoteManagementInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementServices.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly NoteManagementContext _context;

        public CategoryService(IMapper mapper, NoteManagementContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task CreateCategoryAsync(CategoryForCreationDto categoryForCreationDto)
        {
            var entity = _mapper.Map<Category>(categoryForCreationDto);
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            return _mapper.Map<List<CategoryDto>>(await _context.Categories.ToListAsync());
        }
    }
}
