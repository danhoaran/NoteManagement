using Microsoft.EntityFrameworkCore;
using NoteManagementInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteManagementApi.Services.UnitTests
{
    public class TestDbOptions
    {
        public static DbContextOptions<NoteManagementContext> GetUniqueInMemoryDbOptions()
        {
            var options = new DbContextOptionsBuilder<NoteManagementContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            return options;
        }
    }
}
