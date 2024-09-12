using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Student_evaluation.Test
{
    [TestClass]
    public class SsveDataTests
    {
        private const string TestFileName = "students.txt";

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
            string[] testData = {
                "John,Doe,Smith,GroupA",
                "Jane,Roe,Doe,GroupB"
            };
            File.WriteAllLines(TestFileName, testData);

            // Act
            SsveData.LoadDataFromFile();

            // Assert
            var students = SsveData.StudentsManager.Students;
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
            SsveData.StudentsManager.AddStudent(student);

            // Act
            SsveData.SaveDataToFile();
            string[] lines = File.ReadAllLines(TestFileName);

            // Assert
            Assert.AreEqual(1, lines.Length, "File content length does not match.");
            Assert.AreEqual("Alice,Johnson,Doe,GroupC", lines[0], "File content does not match.");
        }

        [TestMethod]
        public void SaveDataToFile_EmptyList_SavesEmptyFile()
        {
            // Act
            SsveData.SaveDataToFile();
            string[] lines = File.ReadAllLines(TestFileName);

            // Assert
            Assert.AreEqual(0, lines.Length, "Empty file content length does not match.");
        }
    }
}
