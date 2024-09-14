using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_evaluation
{
    public class Semester
    {
        public string SemesterNumber { get; set; }
        public Dictionary<string, SubjectGrades> Grades { get; set; } = new Dictionary<string, SubjectGrades>();
    }

}
