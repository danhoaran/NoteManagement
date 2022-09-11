using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NoteManagementApi.Core.DTOs;
using NoteManagementCore.Mappings;
using NoteManagementCore.Models;
using NoteManagementInfrastructure;
using NoteManagementServices.Services;
using System.Security.Cryptography.X509Certificates;

namespace NoteManagementApi.Services.UnitTests
{
    [TestClass]
    public class NoteServiceUnitTests
    {
        private NoteManagementContext context;
        private MapperConfiguration mappingConfig;
        private IMapper mapper;
        private NoteService NoteSvc;
        private CategoryService categorySvc;

        private void SetupDependencies()
        {
            context = new NoteManagementContext(TestDbOptions.GetUniqueInMemoryDbOptions());
            mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AppMapping());
            });
            mapper = mappingConfig.CreateMapper();
            categorySvc = new CategoryService(mapper, context);
            NoteSvc = new NoteService(mapper, context, categorySvc);
        }

        [TestMethod]
        public async Task Can_Create_Note()
        {
            //Arrange
            SetupDependencies();

            var noteForCreation = new NoteForCreationDto()
            {
                Body = "I just love **bold text**.",
                CategoryIds = new List<int> { 1, 2 }
            };

            await context.Categories.AddRangeAsync(new List<Category> { new Category() { Name = "Work" }, new Category() { Name = "Home" } });
            await context.SaveChangesAsync();

            //Act
            await NoteSvc.CreateNoteAsync(noteForCreation);
            //Assert
            var notes = context.Notes.ToList();
            Assert.IsTrue(notes.Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task Test_Null_Exception_Note_Missing_Categories()
        {
            //Arrange
            SetupDependencies();

            var noteForCreation = new NoteForCreationDto()
            {
                Body = "test content",
                CategoryIds = new List<int> { 1, 2 }
            };

            //Act
            await NoteSvc.CreateNoteAsync(noteForCreation);

        }

        [TestMethod]
        public async Task Can_Get_All_Notes()
        {
            //Arrange
            SetupDependencies();

            await context.Categories.AddRangeAsync(new List<Category> { new Category() { Name = "Work" }, 
                new Category() { Name = "Home" }, 
                new Category() { Name = "Holiday" } });
            await context.SaveChangesAsync();

            var categoriesToAdd = await context.Categories.ToListAsync();

            var notesToAdd = new List<Note>()
            {
                new Note()
                {
                    Body = "test content",
                    Categories = categoriesToAdd.Where(x => x.CategoryId == 1 && x.CategoryId == 2).ToList()
                },
                new Note()
                {
                    Body = "test content",
                    Categories = categoriesToAdd.Where(x => x.CategoryId == 1 && x.CategoryId == 3).ToList()
                },
                new Note()
                {
                    Body = "test content",
                    Categories = categoriesToAdd.Where(x => x.CategoryId == 1).ToList()
                }
            };

            await context.AddRangeAsync(notesToAdd);
            await context.SaveChangesAsync();

            //Act
            var notes = await NoteSvc.GetAllNotesAsync();
            //Assert
            Assert.IsTrue(notes.Count == 3);
        }

        [TestMethod]
        public async Task Get_All_Notes_Empty_Set_Returned()
        {
            //Arrange
            SetupDependencies();
            //Act
            var notes = await NoteSvc.GetAllNotesAsync();
            //Assert
            Assert.IsTrue(notes.Count == 0);
        }

        [TestMethod]
        public async Task Can_Get_Note_With_Body_Converted_To_Html()
        {
            //Arrange
            SetupDependencies();

            await context.Categories.AddRangeAsync(new List<Category> { new Category() { Name = "Work" },
                new Category() { Name = "Home" },
                new Category() { Name = "Holiday" } });
            await context.SaveChangesAsync();

            var categoriesToAdd = await context.Categories.ToListAsync();

            var expectedHtml = "<p>This is a text with some <em>emphasis</em></p>";

            var notesToAdd = new List<Note>()
            {
                new Note()
                {
                    Body = "This is a text with some *emphasis*",
                    Categories = categoriesToAdd.Where(x => x.CategoryId == 1 && x.CategoryId == 2).ToList()
                }
            };

            await context.AddRangeAsync(notesToAdd);
            await context.SaveChangesAsync();

            //Act
            var note = await NoteSvc.GetNoteForHtmlAsync(1);
            //Assert
            Assert.IsTrue(note != null && note.NoteId == 1 && note.BodyHtml == expectedHtml);
        }

        [TestMethod]
        public async Task Can_Update_Note()
        {
            //Arrange
            SetupDependencies();

            await context.Categories.AddRangeAsync(new List<Category> { new Category() { Name = "Work" },
                new Category() { Name = "Home" },
                new Category() { Name = "Holiday" } });
            await context.SaveChangesAsync();

            var categoriesToAdd = await context.Categories.ToListAsync();

            var notesToAdd = new List<Note>()
            {
                new Note()
                {
                    Body = "This is a text with some *emphasis*",
                    Categories = categoriesToAdd.Where(x => x.CategoryId == 1 && x.CategoryId == 2).ToList()
                }
            };

            await context.AddRangeAsync(notesToAdd);
            await context.SaveChangesAsync();

            var updatedBody = "Love**is**bold";
    
            var noteForUpdateDto = new NoteForUpdateDto()
            {
                Body = "Love**is**bold",
                CategoryIds = new List<int>() { 2, 3 },
                NoteId = 1
            };

            //Act
            await NoteSvc.UpdateNoteAsync(noteForUpdateDto);
            //Assert
            var updatedNote = await context.Notes.Where(x => x.NoteId == 1).FirstOrDefaultAsync();

            Assert.IsTrue(updatedNote != null && updatedNote.Body == updatedBody 
                && (updatedNote.Categories.Any(x => x.CategoryId == 2) && updatedNote.Categories.Any(x => x.CategoryId == 3)));
        }

        [TestMethod]
        public async Task Can_Delete_Note()
        {
            //Arrange
            SetupDependencies();

            await context.Categories.AddRangeAsync(new List<Category> { new Category() { Name = "Work" },
                new Category() { Name = "Home" },
                new Category() { Name = "Holiday" } });
            await context.SaveChangesAsync();

            var categoriesToAdd = await context.Categories.ToListAsync();
            var notesToAdd = new List<Note>()
            {
                new Note()
                {
                    Body = "test content",
                    Categories = categoriesToAdd.Where(x => x.CategoryId == 1 && x.CategoryId == 2).ToList()
                },
                new Note()
                {
                    Body = "test content",
                    Categories = categoriesToAdd.Where(x => x.CategoryId == 1 && x.CategoryId == 3).ToList()
                },
                new Note()
                {
                    Body = "test content",
                    Categories = categoriesToAdd.Where(x => x.CategoryId == 1).ToList()
                }
            };

            await context.AddRangeAsync(notesToAdd);
            await context.SaveChangesAsync();

            //Act
            await NoteSvc.DeleteNoteAsync(1);

            //Assert
            Assert.IsTrue(context.Notes.ToList().Count == 2);
        }

        [TestMethod]
        public async Task Can_Get_Note_By_Id()
        {
            //Arrange
            SetupDependencies();

            await context.Categories.AddRangeAsync(new List<Category> { new Category() { Name = "Work" },
                new Category() { Name = "Home" },
                new Category() { Name = "Holiday" } });
            await context.SaveChangesAsync();

            var categoriesToAdd = await context.Categories.ToListAsync();

            var notesToAdd = new List<Note>()
            {
                new Note()
                {
                    Body = "This is a text with some *emphasis*",
                    Categories = categoriesToAdd.Where(x => x.CategoryId == 1 && x.CategoryId == 2).ToList()
                }
            };

            await context.AddRangeAsync(notesToAdd);
            await context.SaveChangesAsync();

            //Act
            var note = await NoteSvc.GetNoteByIdAsync(1);
            //Assert
            Assert.IsTrue(note != null && note.NoteId == 1);
        }


    }
}