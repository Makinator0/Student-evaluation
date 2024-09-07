using System.Collections.Generic;
using System.IO;

namespace Student_evaluation
{
    public static class SsveData
    {
        public static StudentsManager StudentsManager { get; private set; }

        static SsveData()
        {
            StudentsManager = new StudentsManager();
            LoadDataFromFile();
        }

        private static void LoadDataFromFile()
        {
            if (File.Exists("students.txt"))
            {
                string[] lines = File.ReadAllLines("students.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 4)
                    {
                        Student student = new Student
                        {
                            Name = parts[0],
                            Surname = parts[1],
                            LastName = parts[2],
                            Group = parts[3]
                        };

                        StudentsManager.AddStudent(student);
                    }
                }
            }
        }

        public static void SaveDataToFile()
        {
            List<string> lines = new List<string>();
            foreach (var student in StudentsManager.Students)
            {
                string line = $"{student.Name},{student.Surname},{student.LastName},{student.Group}";
                lines.Add(line);
            }

            File.WriteAllLines("students.txt", lines);
        }
    }
}
