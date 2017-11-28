using System;
using System.Collections.Generic;

namespace BarneyGo.Models
{
    
    public class Day
    {
        public int Id { get; set; }
        public int SyllabusId { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Wifi { get; set; }
        public string Topics { get; set; }
        public string Labs { get; set; }
        public string NextClass { get; set; }
    
        public virtual Syllabus Syllabus { get; set; }
    }
}
