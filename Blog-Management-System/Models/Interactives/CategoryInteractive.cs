﻿using Blog_Management_System.Models.Tags;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blog_Management_System.Models.Interactives
{
    public class CategoryInteractive : ICategoryInteractive
    {
        private readonly BlogManagementSystemDbContext _blogManagementSystemDbContext;
        public List<Category> Categories { get; set; } = new();
        public CategoryInteractive(BlogManagementSystemDbContext blogManagementSystemDbContext)
        {
            _blogManagementSystemDbContext = blogManagementSystemDbContext;
            UpdateCategories();
        }
        public void UpdateCategories()
        {
            Categories = [.. _blogManagementSystemDbContext.Categories
                .Include(c => c.Forums)];
            //var a = string.Join("\n", Categories.Select(c =>
            //{
            //    if (c.Forums is not null)
            //        return string.Join(", ", c.Forums.Select(s => s.ForumId));
            //    return "";
            //}));
            //Console.WriteLine(a);
        }
    }
}
