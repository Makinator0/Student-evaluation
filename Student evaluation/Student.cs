using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_evaluation
{
    public class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public string Group { get; set; }
        public List<Semester> Semesters { get; set; } = new List<Semester>();
        
    }
}
