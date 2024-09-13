using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Student_evaluation.Test
{
    [TestClass]
    public class SaveDataTests
    {
        private const string TestFileName = "DataProvider.json";

        [TestInitialize]
        public void Setup()
        {
            if (File.Exists(TestFileName))
            {
                File.Delete(TestFileName);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(TestFileName))
            {
                File.Delete(TestFileName);
            }
        }

        [TestMethod]
        public void LoadDataFromFile_FileExists_LoadsStudentsCorrectly()
        {
            // Arrange
            var testData = new List<Student>
            {
                new Student { Name = "John", Surname = "Doe", LastName = "Smith", Group = "GroupA" },
                new Student { Name = "Jane", Surname = "Roe", LastName = "Doe", Group = "GroupB" }
            };
            File.WriteAllText(TestFileName, JsonConvert.SerializeObject(testData, Newtonsoft.Json.Formatting.Indented));

            // Act
            var students = SaveData.LoadStudents();

            // Assert
            Assert.AreEqual(2, students.Count, "Student count does not match.");

            // Additional assertions to check individual student properties
            Assert.AreEqual("John", students[0].Name);
            Assert.AreEqual("Doe", students[0].Surname);
            Assert.AreEqual("Smith", students[0].LastName);
            Assert.AreEqual("GroupA", students[0].Group);

            Assert.AreEqual("Jane", students[1].Name);
            Assert.AreEqual("Roe", students[1].Surname);
            Assert.AreEqual("Doe", students[1].LastName);
            Assert.AreEqual("GroupB", students[1].Group);
        }

        [TestMethod]
        public void SaveDataToFile_SavesCorrectly()
        {
            // Arrange
            var student = new Student
            {
                Name = "Alice",
                Surname = "Johnson",
                LastName = "Doe",
                Group = "GroupC"
            };
            SaveData.AddStudent(student);

            // Act
            var students = SaveData.LoadStudents();
            string jsonContent = File.ReadAllText(TestFileName);

            // Assert
            Assert.AreEqual(1, students.Count, "Student count does not match after saving.");
            Assert.IsTrue(jsonContent.Contains("Alice"), "Saved JSON content does not contain the correct student data.");
        }

        [TestMethod]
        public void SaveDataToFile_EmptyList_SavesEmptyFile()
        {
            // Arrange
            var students = new List<Student>();

            // Act
            SaveData.SaveStudents(students);
            string jsonContent = File.ReadAllText(TestFileName);

            // Assert
            Assert.AreEqual("[]", jsonContent.Trim(), "File content for empty list does not match.");
        }
    }
}
