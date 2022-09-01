using NoteManagement.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagement.Core.Interfaces
{
    public interface ICategoryApiService
    {
        Task<List<CategoryDto>> GetAllCategories();
    }
}
