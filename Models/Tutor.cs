using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Tutorias.Models
{
    public class Tutor : ApplicationUser
    {
        [MaxLength(700)] public string Description { get; set; }
        [MaxLength(100)] public string? TwitterLink { get; set; }
        [MaxLength(100)] public string? FacebookLink { get; set; }
        [MaxLength(100)] public string? InstagramLink { get; set; }
        public float? AverageScore { get; set; }
        public ICollection<Tutorship>? Tutorships { get; set; }
        public ICollection<TutorCategory>? TutorCategories { get; set; }
        public ICollection<TutorSubject>? TutorSubjects { get; set; }
        public ICollection<TutorshipPetition>? TutorshipPetitions { get; set; }
        public string? ImagePath { get; set; }
        public string? ImageName { get; set; }
    }
}