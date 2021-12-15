using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Tutorias.Models
{
    public class ApplicationUser : IdentityUser
    {
        public override string UserName { get; set;}
        [MaxLength(10)] public override string? PhoneNumber { get; set; }
    }
}