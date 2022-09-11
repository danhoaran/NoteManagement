using NoteManagement.Core;
using NoteManagement.Core.Interfaces;
using NoteManagement.Infrastructure.Configuration;
using NoteManagement.Infrastructure.Services;
using NoteManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("Api"));
builder.Services.AddScoped<INoteApiService, NoteApiService>();
builder.Services.AddScoped<ICategoryApiService, CategoryApiService>();
builder.Services.AddScoped<INoteManagementService, NoteManagementService>();
builder.Services.AddScoped<ICategoryRetrievalService, CategoryRetrievalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
