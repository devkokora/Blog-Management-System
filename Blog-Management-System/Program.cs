using Blog_Management_System.Models;
using Blog_Management_System.Models.Interactives;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IForumInteractive, ForumInteractive>();
builder.Services.AddScoped<IUserInteractive, UserInteractive>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogManagementSystemDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("BlogManagementSystemDbContextConnection"));
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
