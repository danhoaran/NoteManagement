using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NoteManagementCore.Services;
using NoteManagementInfrastructure;
using NoteManagementServices.Services;
using Serilog;
using static System.Net.Mime.MediaTypeNames;



var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("logs/log.txt")
    .CreateLogger();

builder.Logging.AddSerilog();

//ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
//{
//builder.AddConsole();
//builder.AddDebug();
//builder.AddFile($"\\Logs\\myapp.txt");
//});

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<NoteManagementContext>(opt =>
    opt.UseInMemoryDatabase("NoteManagement"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new() { Title = "TodoApi", Version = "v1" });
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<INoteService, NoteService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NoteManagementContext>();
    dbContext.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
