﻿
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class OnlineCoursesContext : IdentityDbContext<Users>
    {
        public OnlineCoursesContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<Courses> Courses { get; set; }
        public DbSet<Prices> Prices { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Instructors> Instructors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Courses>()
                .HasOne(p => p.Prices)
                .WithOne(c => c.Courses)
                .HasForeignKey<Prices>(c => c.CourseId);
        }

    }
}
