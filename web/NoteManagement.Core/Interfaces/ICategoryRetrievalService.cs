using NoteManagement.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagement.Core
{
    public interface ICategoryRetrievalService
    {
        Task<List<CategoryDto>> GetAllCategories();
    }
}
