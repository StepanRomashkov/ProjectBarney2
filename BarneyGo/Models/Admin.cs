using System;
using System.Collections.Generic;

namespace BarneyGo.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Syllabus> Syllabus { get; set; }
    }
}