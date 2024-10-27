using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;

namespace Student_evaluation
{
    public partial class StudentInfoForm : Form
    {
        private List<Student> _students;
        private Student _student;

        public StudentInfoForm()
        {
            InitializeComponent();
            LoadStudents();
            comboBoxStudents.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxStudents.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void LoadStudents()
        {
            _students = SaveData.LoadStudents(); // Загружаем студентов из JSON файла
            UpdateComboBoxItems(string.Empty); // Инициализируем элементы ComboBox
        }

        private void comboBoxStudents_TextChanged(object sender, EventArgs e)
        {
            string input = comboBoxStudents.Text;
            UpdateComboBoxItems(input);
        }

        private void UpdateComboBoxItems(string input)
        {
            var filteredStudents = _students
                .Where(s => s.Surname.StartsWith(input, StringComparison.OrdinalIgnoreCase))
                .Select(s => $"{s.Surname} {s.Name} {s.LastName}")
                .ToList();

            comboBoxStudents.Items.Clear();
            comboBoxStudents.Items.AddRange(filteredStudents.ToArray());
        }

        private void comboBoxStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedText = comboBoxStudents.SelectedItem?.ToString();
            _student = _students.FirstOrDefault(s =>
                $"{s.Surname} {s.Name} {s.LastName}" == selectedText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            var selectedText = comboBoxStudents.Text;

            
            _student = _students.FirstOrDefault(s =>
                $"{s.Surname} {s.Name} {s.LastName}" == selectedText);

            if (_student != null)
            {
                GeneratePdfReport();
            }
            else
            {
                MessageBox.Show("Студент не найден. Пожалуйста, выберите корректного студента.");
            }
        }


        private void GeneratePdfReport()
        {
            
            string currentDate = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string filePath = $"StudentReport_{_student.Surname}_{_student.Name}_{currentDate}.pdf";

           
            if (string.IsNullOrEmpty(filePath) || Path.GetInvalidPathChars().Any(filePath.Contains))
            {
                MessageBox.Show("Неверный путь к файлу.");
                return;
            }

            using (PdfWriter writer = new PdfWriter(filePath))
            using (PdfDocument pdf = new PdfDocument(writer))
            {
                Document document = new Document(pdf);
                PdfFont font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "Identity-H");

                document.Add(new Paragraph($"Студент: {_student.Surname} {_student.Name} {_student.LastName}").SetFont(font));
                document.Add(new Paragraph($"Группа: {_student.Group}").SetFont(font));
                document.Add(new Paragraph("Семестры:").SetFont(font));

                foreach (var semester in _student.Semesters)
                {
                    document.Add(new Paragraph($"Семестр {semester.SemesterNumber}:").SetFont(font));

                    if (semester.Grades.Count > 0)
                    {
                        foreach (var grade in semester.Grades)
                        {
                            var subject = grade.Key;
                            var details = grade.Value;

                            document.Add(new Paragraph($"  Предмет: {subject}").SetFont(font));
                            document.Add(new Paragraph($"    Первая домашка: {details.FirstHW}").SetFont(font));
                            document.Add(new Paragraph($"    Первый тест: {details.FirstTest}").SetFont(font));
                            document.Add(new Paragraph($"    Вторая домашка: {details.SecondHW}").SetFont(font));
                            document.Add(new Paragraph($"    Средний балл: {details.AverageScore:F2}").SetFont(font));
                            document.Add(new Paragraph($"    Оценка: {details.LetterGrade}").SetFont(font));
                        }
                    }
                    else
                    {
                        document.Add(new Paragraph("Нет оценок за этот семестр").SetFont(font));
                    }
                }

                document.Close();
            }

            
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть PDF документ: {ex.Message}");
            }

            MessageBox.Show("PDF отчет успешно сгенерирован по пути: " + Path.GetFullPath(filePath));
        }

        private void closeButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
