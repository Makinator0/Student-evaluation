using FormsManageraNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using iText.IO.Font;
using iText.Kernel.Font;

namespace Student_evaluation
{
    public partial class SubjectsForm : Form
    {
        protected string _shortName;
        protected Student _student;
        private string _semesterNumber;
        private string _Subject;
        private Form _semestrForm; 
        public SubjectsForm(Student student, string semesterNumber, Form semestrForm)
        {
            InitializeComponent();
            _student = student;
            _semesterNumber = semesterNumber;
            _semestrForm = semestrForm;
            _shortName = GenerateShortName(_student.Surname, _student.Name, _student.LastName);
            label2.Text = _shortName;
            label4.Text = _student.Group;
        }
        protected string GenerateShortName(string lastName, string firstName, string middleName)
        {
            string firstLetterOfFirstName = !string.IsNullOrEmpty(firstName) ? firstName.Substring(0, 1) + "." : string.Empty;
            string firstLetterOfMiddleName = !string.IsNullOrEmpty(middleName) ? middleName.Substring(0, 1) + "." : string.Empty;

            return $"{lastName} {firstLetterOfFirstName}{firstLetterOfMiddleName}";
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
           
            _Subject = "Вища математика";
            FormsManager.OpenForm(new HighMathForm(_student, _semesterNumber, _shortName, _Subject));
        }
        Point lastPoint3;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint3.X;
                this.Top += e.Y - lastPoint3.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint3 = new Point(e.X, e.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {      

            _Subject = "Прогамування";
            FormsManager.OpenForm(new HighMathForm(_student, _semesterNumber, _shortName, _Subject));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            _Subject = "Історія Україна";
            FormsManager.OpenForm(new HighMathForm(_student, _semesterNumber, _shortName, _Subject));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _Subject = "Філософія";
            FormsManager.OpenForm(new HighMathForm(_student, _semesterNumber, _shortName, _Subject));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _Subject = "Укр мова";
            FormsManager.OpenForm(new HighMathForm(_student, _semesterNumber, _shortName, _Subject));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _Subject = "Фізика";
            FormsManager.OpenForm(new HighMathForm(_student, _semesterNumber, _shortName, _Subject));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _Subject = "ОКМ";
            FormsManager.OpenForm(new HighMathForm(_student, _semesterNumber, _shortName, _Subject));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _Subject = "Алгебра програмування";
            FormsManager.OpenForm(new HighMathForm(_student, _semesterNumber, _shortName, _Subject));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            _semestrForm.Visible = true;
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string selectedSemester = _semesterNumber;
            GeneratePdfReport(selectedSemester);
        }
        private void GeneratePdfReport(string selectedSemester)
{
    
    string currentDate = DateTime.Now.ToString("yyyy\\MM\\dd_HH.mm.ss");
    string filePath = $"StudentReport_{_student.Surname}_{_student.Name}_{currentDate}.pdf";
    
    if (string.IsNullOrEmpty(filePath) || Path.GetInvalidPathChars().Any(filePath.Contains))
    {
        MessageBox.Show("Неправильний шлях до файлу");
        return;
    }

    using (PdfWriter writer = new PdfWriter(filePath))
    {
        using (PdfDocument pdf = new PdfDocument(writer))
        {
            Document document = new Document(pdf);

            
            PdfFont font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "Identity-H");

            
            document.Add(new Paragraph($"Студент: {_student.Surname} {_student.Name} {_student.LastName}").SetFont(font));
            document.Add(new Paragraph($"Група: {_student.Group}").SetFont(font));
            document.Add(new Paragraph($"Семестри:").SetFont(font));

            
            var semester = _student.Semesters.FirstOrDefault(s => s.SemesterNumber == selectedSemester);
            if (semester != null)
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
                    document.Add(new Paragraph($"Немає оцінок за цей семестр").SetFont(font));
                }
            }

            document.Close();
        }
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






    }
}


