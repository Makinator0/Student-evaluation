using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Student_evaluation;

public static class StudentManager

{
    private const string FilePath = "DataProvider.json";

    public static List<Student> LoadStudents()
    {
        if (!File.Exists(FilePath))
        {

            // Отладочная информация
            System.Diagnostics.Debug.WriteLine("File does not exist, creating a new list.");
            return new List<Student>();
        }

        var json = File.ReadAllText(FilePath);
        if (string.IsNullOrWhiteSpace(json))
        {
            // Отладочная информация
            System.Diagnostics.Debug.WriteLine("File is empty, creating a new list.");
            return new List<Student>();
        }

        // Отладочная информация
        System.Diagnostics.Debug.WriteLine($"Loaded data: {json}");

        return JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
    }

    public static void SaveStudents(List<Student> students)
    {
        // Отладочная информация
        System.Diagnostics.Debug.WriteLine($"Saving students: {JsonConvert.SerializeObject(students, Formatting.Indented)}");

        var json = JsonConvert.SerializeObject(students, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }

    public static void AddStudent(Student student)
    {
        var students = LoadStudents();
        students.Add(student);

        // Отладочная информация
        System.Diagnostics.Debug.WriteLine($"Adding student: {JsonConvert.SerializeObject(student, Formatting.Indented)}");

        SaveStudents(students);
    }
}
