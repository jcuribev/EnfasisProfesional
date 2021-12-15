using System;
using System.Collections.Generic;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Tutorias.Models
{
    public class TutorCategory
    {
        public string ID { get; set; }
        public string TutorID {get;set;}
        public string CategoryID{get;set;}
    }
}