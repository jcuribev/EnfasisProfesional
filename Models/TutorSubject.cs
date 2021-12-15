using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorias.Models
{
    public class TutorSubject
    {
        public string ID { get; set; }
        public string TutorID { get; set; }
        public string SubjectID {get;set;}
    }
}