using Microsoft.EntityFrameworkCore;
using NoteManagementCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementInfrastructure
{
    public class NoteManagementContext : DbContext
    {

        public NoteManagementContext(DbContextOptions<NoteManagementContext> options)
    : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasMany(x => x.Categories);

            modelBuilder.Entity<Category>().HasData(
                new Category() { CategoryId = 1, Name = "Work" },
                new Category() { CategoryId = 2, Name = "Home" },
                new Category() { CategoryId = 3, Name = "Holiday" }
            );
        }

    }
}
