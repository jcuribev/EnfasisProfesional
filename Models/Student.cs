using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Tutorias.Models
{
    public class Student : ApplicationUser
    {
        public ICollection<Tutorship> Tutorships { get; set; }
    }
}