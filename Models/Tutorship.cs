using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorias.Models
{
    public class Tutorship
    {
        public string ID{ get; set; }
        public string StudentID { get; set; }
        public string TutorID { get; set; }
        public float? Score { get; set; }
        public Boolean Sent {get;set;} = false;
        public string? Description {get;set;} = "No se dieron comentarios";
    }
}