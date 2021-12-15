using Tutorias.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Tutorias.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<Tutor> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if(context.Categories.Any() | context.Subjects.Any())
            {
                return;
            }

            if (!roleManager.RoleExistsAsync("Tutor").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Tutor";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Student").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Student";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            var categories = new Category[]
            {
                new Category {Name = "Ciencias Sociales"},
                new Category {Name = "Ciencias Naturales"},
                new Category {Name = "Ciencias Exactas"},
                new Category {Name = "Lenguaje"},
                new Category {Name = "Arte"},
                new Category {Name = "Otros"},
            };

            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            var subjects = new Subject[]
            {
                new Subject {Name = "Cálculo Diferencial", CategoryName = "Ciencias Exactas"},
                new Subject {Name = "Cálculo Integral", CategoryName = "Ciencias Exactas"},
                new Subject {Name = "Inglés", CategoryName = "Lenguaje"},
                new Subject {Name = "Francés", CategoryName = "Lenguaje"},
                new Subject {Name = "Historia", CategoryName = "Ciencias Sociales"},
                new Subject {Name = "Biología", CategoryName = "Ciencias Naturales"},
                new Subject {Name = "Química", CategoryName = "Ciencias Naturales"},
                new Subject {Name = "Matemáticas básicas", CategoryName = "Ciencias Exactas"},
                new Subject {Name = "Pintura", CategoryName = "Arte"},
            };

            foreach (Subject s in subjects)
            {
                context.Subjects.Add(s);
            }
            context.SaveChanges(); 
        }
    }
}