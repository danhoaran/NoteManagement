using NoteManagement.Core;
using NoteManagement.Core.Dtos;
using NoteManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagement.Services
{
    public class CategoryRetrievalService : ICategoryRetrievalService
    {
        private readonly ICategoryApiService _categoryApiService;

        public CategoryRetrievalService(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            return await _categoryApiService.GetAllCategories();
        }
    }
}
