using System;
using System.Collections.Generic;

namespace BarneyGo.Models
{

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SyllabusId { get; set; }
    
        public virtual Syllabus Syllabus { get; set; }
    }
}
