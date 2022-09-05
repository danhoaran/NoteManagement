using Markdig;
using Microsoft.AspNetCore.Mvc;
using NoteManagementApi.Core.DTOs;
using NoteManagementCore.Services;

namespace NoteManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategoriesAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryForCreationDto categoryForCreationDto)
        {
            await _categoryService.CreateCategoryAsync(categoryForCreationDto);
            return NoContent();
        }

    }
}
