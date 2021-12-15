using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tutorias.Models;
using Microsoft.AspNetCore.Identity;

namespace Tutorias.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Tutorship> Tutorships { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TutorCategory> TutorCategories { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TutorSubject> TutorSubjects { get; set; }
        public DbSet<TutorshipPetition> TutorshipPetitions {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Tutor>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Student>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>()
            .Property(e => e.ID)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Subject>()
            .Property(e => e.ID)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Tutor>().ToTable("Tutor");
            modelBuilder.Entity<Tutorship>().ToTable("Tutorship");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<TutorCategory>().ToTable("TutorCategory");
            modelBuilder.Entity<Subject>().ToTable("Subject");
            modelBuilder.Entity<TutorSubject>().ToTable("TutorSubject");
            modelBuilder.Entity<TutorshipPetition>().ToTable("TutorshipPetition");
        }
    }
}
