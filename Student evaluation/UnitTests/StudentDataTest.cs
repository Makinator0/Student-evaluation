using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Student_evaluation
{
    [TestClass]
    public class StudentDataTest
    {
        private static Random random = new Random();
        
        private static string[] maleSurnames = {
            "Алексєєв", "Бондаренко", "Бочаров", 
            "Жерновський", "Запорожцев", "Костін", 
            "Куштим", "Макашов", "Півньов"
        };

        private static string[] femaleSurnames = {
            "Бєдарєва", "Гасюк", "Григоренко", 
            "Рабада"
        };

        private static string[] maleNames = {
            "Олександр", "Дмитро", "Віталій", "Максим", 
            "Вадим", "Владислав", "Олександр", "Павло", "Іван", "Євгеній"
        };

        private static string[] femaleNames = {
            "Галина", "Єлизавета"
        };
        private static string[] patronymicsMale = {
            "Олександрович", "Миколайович", "Валентинович",
            "Олександрович", "Сергійович", "Вікторович", 
            "Анатолійович", "Романович", "Олегович"
        };

        private static string[] patronymicsFemale = {
            "Анатоліївна", "Олександрівна"
        };

        private static string[] groups = { "КН-1022А", "КН-1022Б", "КН-1022В" };

        private static string[] subjects = { "Фізика", "Філософія", "Вища математика", "Укр мова" };

        [TestMethod]
        public void TestCreateRandomStudent()
        {
            
            string group = groups[random.Next(groups.Length)];

            bool isMale = random.Next(2) == 0; 
            string name = isMale ? maleNames[random.Next(maleNames.Length)] : femaleNames[random.Next(femaleNames.Length)];
            string patronymic = isMale ? patronymicsMale[random.Next(patronymicsMale.Length)] : patronymicsFemale[random.Next(patronymicsFemale.Length)];
            string surname = isMale ? maleSurnames[random.Next(maleSurnames.Length)] : femaleSurnames[random.Next(femaleSurnames.Length)];

            var student = new Student
            {
                Name = name,
                Surname = surname,
                LastName = patronymic,
                Group = group,
                Semesters = new List<Semester>
                {
                    new Semester
                    {
                        SemesterNumber = "1",
                        Grades = GenerateRandomGrades()
                    },
                    new Semester
                    {
                        SemesterNumber = "2",
                        Grades = GenerateRandomGrades()
                    }
                }
            };

            
            SaveData.AddStudent(student);

            
            var students = SaveData.LoadStudents();
            bool studentExists = students.Any(s => s.Name == student.Name && s.Surname == student.Surname &&
                                                   s.LastName == student.LastName && s.Group == student.Group);
    
            Assert.IsTrue(studentExists, "Student was not added to the list.");
        }

        private static Dictionary<string, SubjectGrades> GenerateRandomGrades()
        {
            var grades = new Dictionary<string, SubjectGrades>();
            foreach (var subject in subjects)
            {
                double averageScore = Math.Round(random.NextDouble() * 50 + 50, 2); 
                grades[subject] = new SubjectGrades
                {
                    FirstHW = GenerateRandomGradeWithLetter(),
                    FirstTest = GenerateRandomGradeWithLetter(),
                    SecondHW = GenerateRandomGradeWithLetter(),
                    AverageScore = averageScore,
                    LetterGrade = GetLetterGrade(averageScore)
                };
            }
            return grades;
        }

        private static string GenerateRandomGradeWithLetter()
        {
            int score = random.Next(50, 100); 
            return $"{score}{GetLetterGrade(score)}"; 
        }

        private static string GetLetterGrade(double score)
        {
            if (score >= 90) return "A";
            if (score >= 80) return "B";
            if (score >= 70) return "C";
            if (score >= 60) return "D";
            return "F";
        }
    }
}
