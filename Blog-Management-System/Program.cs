using Blog_Management_System.Models;
using Blog_Management_System.Models.Interactives;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IForumInteractive, ForumInteractive>();
builder.Services.AddScoped<IUserInteractive, UserInteractive>();
builder.Services.AddScoped<ICategoryInteractive, CategoryInteractive>();
builder.Services.AddScoped<IStatusInteractive, StatusInteractive>();
builder.Services.AddScoped<ICommentInteractive, CommentInteractive>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();
    //.AddJsonOptions(options =>
    // {
    //     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    // });

builder.Services.AddDbContext<BlogManagementSystemDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("BlogManagementSystemDbContextConnection"));
        options.EnableSensitiveDataLogging();
    }
);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
