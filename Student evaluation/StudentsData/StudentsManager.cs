using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_evaluation
{
    public class StudentsManager
    {
        public List<Student> Students { get; private set; } = new List<Student>();
        public void AddStudent(Student student) 
        { 
            Students.Add(student);
        }

    }
}
