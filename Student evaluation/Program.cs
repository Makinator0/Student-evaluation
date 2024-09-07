using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Student_evaluation
{
    internal static class Program
    {

        public static StudentsManager StudentsManager;
        [STAThread]
        static void Main()
        {
           
            StudentsManager = new StudentsManager();
            LoadDataFromFile();
            StartAap();
            
            
        }
        private static void StartAap()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AutoriseForm());
        }
        private static void LoadDataFromFile()
        {
            if (File.Exists("students.txt"))
            {
                string[] lines = File.ReadAllLines("students.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    string имя = parts[0];
                    string фамилия = parts[1];
                    string отчество = parts[2];
                    string группа = parts[3];

                    Student студент = new Student
                    {
                        Name = имя,
                        Surname = фамилия,
                        LastName = отчество,
                        Group = группа
                    };

                    StudentsManager.AddStudent(студент);
                }
            }
        }

        public static void SaveDataToFile()
        {
            List<string> lines = new List<string>();
            foreach (var студент in StudentsManager.Students)
            {
                string line = $"{студент.Name},{студент.Surname},{студент.LastName},{студент.Group}";
                lines.Add(line);
            }

            File.WriteAllLines("students.txt", lines);
        }


    }
}
