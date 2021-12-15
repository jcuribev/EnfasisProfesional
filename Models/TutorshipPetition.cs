using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tutorias.Models
{
    public class TutorshipPetition
    {
        public string ID { get; set; }
        public string StudentID { get; set; }
        public string TutorID { get; set; }
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public string State { get; set; } = "Pendiente";
        public string? Message {get;set;} = "Sin mensaje.";
    }
}