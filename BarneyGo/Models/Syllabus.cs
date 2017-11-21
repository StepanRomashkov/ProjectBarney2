using System;
using System.Collections.Generic;

namespace BarneyGo.Models
{
    
    public class Syllabus
    {  
        public int Id { get; set; }
        public string Name { get; set; }
        public int AdminId { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual ICollection<Day> Day { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
