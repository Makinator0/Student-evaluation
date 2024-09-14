﻿using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Student_evaluation;
using System.Linq;

public static class SaveData
{
    private const string FilePath = "DataProvider.json";

    public static List<Student> LoadStudents()
    {
        if (!File.Exists(FilePath))
        {
            System.Diagnostics.Debug.WriteLine("File does not exist, creating a new list.");
            return new List<Student>();
        }

        var json = File.ReadAllText(FilePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            System.Diagnostics.Debug.WriteLine("File is empty, creating a new list.");
            return new List<Student>();
        }

        System.Diagnostics.Debug.WriteLine($"Loaded data: {json}");
        return JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
    }

    public static void SaveStudents(List<Student> students)
    {
        System.Diagnostics.Debug.WriteLine($"Saving students: {JsonConvert.SerializeObject(students, Formatting.Indented)}");
        var json = JsonConvert.SerializeObject(students, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }

    public static void AddStudent(Student student)
    {
        var students = LoadStudents();
        students.Add(student);
        System.Diagnostics.Debug.WriteLine($"Adding student: {JsonConvert.SerializeObject(student, Formatting.Indented)}");
        SaveStudents(students);
    }

    public static void UpdateStudentGrades(string studentName, string semesterNumber, string subjectName, SubjectGrades grades)
    {
        var students = LoadStudents();
        var student = students.FirstOrDefault(s => s.Name == studentName);

        if (student != null)
        {
            var semester = student.Semesters.FirstOrDefault(s => s.SemesterNumber == semesterNumber);
            if (semester == null)
            {
                semester = new Semester { SemesterNumber = semesterNumber };
                student.Semesters.Add(semester);
            }

            semester.Grades[subjectName] = grades;
            SaveStudents(students);
        }
    }
}
