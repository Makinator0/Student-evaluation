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
        private Form _autoriseForm;

        public StudentInfoForm(Form autoriseForm)
        {
            InitializeComponent();
            _autoriseForm = autoriseForm;
            LoadStudents();
            comboBoxStudents.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxStudents.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        


        private void LoadStudents()
        {
            _students = SaveData.LoadStudents(); 
            UpdateComboBoxItems(string.Empty); 
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
                MessageBox.Show("Студента не знайдено. Будь ласка, виберіть коректного студента.");
            }
        }
        private void GeneratePdfReport()
{
    // Форматируем текущую дату и время
    string currentDate = DateTime.Now.ToString("yyyy\\MM\\dd_HH.mm.ss");
    string filePath = $"StudentReport_{_student.Surname}_{_student.Name}_{currentDate}.pdf";

    // Проверка на корректность пути
    if (string.IsNullOrEmpty(filePath) || Path.GetInvalidPathChars().Any(filePath.Contains))
    {
        MessageBox.Show("Неправильний шлях до файлу.");
        return;
    }

    using (PdfWriter writer = new PdfWriter(filePath))
    using (PdfDocument pdf = new PdfDocument(writer))
    {
        Document document = new Document(pdf);
        PdfFont font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "Identity-H");

        document.Add(new Paragraph($"Студент: {_student.Surname} {_student.Name} {_student.LastName}").SetFont(font));
        document.Add(new Paragraph($"Група: {_student.Group}").SetFont(font));
        document.Add(new Paragraph("Семестри:").SetFont(font));

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
                    document.Add(new Paragraph($"    Перше ІДЗ: {details.FirstHW}").SetFont(font));
                    document.Add(new Paragraph($"    Перший тест: {details.FirstTest}").SetFont(font));
                    document.Add(new Paragraph($"    Друге ІДЗ: {details.SecondHW}").SetFont(font));
                    document.Add(new Paragraph($"    Средній бал: {details.AverageScore:F2}").SetFont(font));
                    document.Add(new Paragraph($"    Оцінка: {details.LetterGrade}").SetFont(font));
                }
            }
            else
            {
                document.Add(new Paragraph("Немає оцінок за ці семестр").SetFont(font));
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
        MessageBox.Show($"Не вдалося відкрити PDF документ: {ex.Message}");
    }

    MessageBox.Show("PDF-звіт успішно згенеровано за шляхом: " + Path.GetFullPath(filePath));
}


        private void closeButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _autoriseForm.Visible = true;
            this.Close();
        }
    }
    
}
