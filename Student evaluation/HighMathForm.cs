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
    public partial class HighMathForm : Form
    {
        public HighMathForm(Student student, string ShortName)
        {
            InitializeComponent();
            label2.Text = ShortName;
            label4.Text = student.Group;
        }

        private void HighMathForm_Load(object sender, EventArgs e)
        {

        }
    }
}
