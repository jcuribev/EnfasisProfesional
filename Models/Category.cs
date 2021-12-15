using System;
using System.Collections.Generic;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Tutorias.Models
{
    public class Category
    {
        public string ID { get; set; }
        public string Name{get;set;}
        public ICollection<TutorCategory>? TutorCategories {get;set;}
        public ICollection<Subject> Subjects {get;set;}   
    }
}