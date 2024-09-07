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
        private Student _student;
        public SemestrForm(Student student)
        {
            InitializeComponent();
            _student = student;

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

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormsManager.OpenForm(new SubjectsForm(_student));
        }
    }
}
