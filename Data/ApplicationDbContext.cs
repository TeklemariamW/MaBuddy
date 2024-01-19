using System;
using System.Collections.Generic;
using System.Text;
using mabuddy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace mabuddy.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Questions> Questions { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<HomeModel> Home { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Inbox> Inbox { get; set; }
        public DbSet<AddLesson> AddLesson { get; set; }
        public DbSet<Tips> Tips { get; set; }
    }
}