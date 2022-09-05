using NoteManagementApi.Core.DTOs;
using NoteManagementCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementCore.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task CreateCategoryAsync(CategoryForCreationDto categoryForCreationDto);
    }
}
