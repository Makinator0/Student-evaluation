using FormsManageraNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_evaluation
{
    public partial class SemestrForm : Form
    {
        private Form _subjectsForm;
        protected string _shortName;
        private Student _student;
        private bool _isFirstSemestr = false;
        public SemestrForm(Student student)
        {
            InitializeComponent();
            _student = student;
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
        Point lastPoint2;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint2.X;
                this.Top += e.Y - lastPoint2.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint2 = new Point(e.X, e.Y);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var newSemester = new Semester
            {
                SemesterNumber = "2",
                Grades = new Dictionary<string, SubjectGrades>() // Инициализация словаря
            };

            _student.Semesters.Add(newSemester);

            // Передаем номер семестра в форму
            FormsManager.OpenForm(new SubjectsForm(_student, "2"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var newSemester = new Semester
            {
                SemesterNumber = "1",
                Grades = new Dictionary<string, SubjectGrades>() // Инициализация словаря
            };

            _student.Semesters.Add(newSemester);

            // Передаем номер семестра в форму
            FormsManager.OpenForm(new SubjectsForm(_student, "1"));
        }
    }
}
