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
    public partial class SubjectsForm : Form
    {
        protected string _shortName;
        protected Student _student;
        private string _semesterNumber;
        public SubjectsForm(Student student, string semesterNumber)
        {
            InitializeComponent();
            _student = student;
            _semesterNumber = semesterNumber;
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
            FormsManager.OpenForm(new HighMathForm(_student, _semesterNumber, _shortName));
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
    }
}
