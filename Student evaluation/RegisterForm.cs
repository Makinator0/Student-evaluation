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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Student_evaluation
{
    public partial class RegisterForm : Form
    {
        
        public RegisterForm()
        {
            InitializeComponent();
            userNameField.Text = "Введіть ім`я";
            userSurnameField.Text = "Введіть прізвище";
            userLastnameField.Text = "Введіть ім`я по-батькові";
            userGroupField.Text = "Введіть групу";
           

        }

        

       


       

       

        private void closeButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void closeButton1_MouseEnter(object sender, EventArgs e)
        {
            closeButton1.ForeColor = Color.Black;
        }

        private void closeButton1_MouseLeave(object sender, EventArgs e)
        {
            closeButton1.ForeColor = Color.Red;
        }

        private void userNameField_Enter(object sender, EventArgs e)
        {
            if (userNameField.Text == "Введіть ім`я")
            {
                userNameField.Text = "";
                userNameField.ForeColor = Color.Black;
            }
        }

        private void userNameField_Leave(object sender, EventArgs e)
        {
            if (userNameField.Text == "")
            {
               userNameField.Text = "Введіть ім`я";
               userNameField.ForeColor = Color.Gray;
            }
        }

        private void userSurnameField_Enter(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "Введіть прізвище")
            {
                userSurnameField.Text = "";
                userSurnameField.ForeColor = Color.Black;
            }
        }

        private void userSurnameField_Leave(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "")
            {
                userSurnameField.Text = "Введіть прізвище";
                userSurnameField.ForeColor = Color.Gray;
            }
        }

        
    

        Point lastPoint1;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint1.X;
                this.Top += e.Y - lastPoint1.Y;
            }

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint1 = new Point(e.X, e.Y);
        }

        private void userLastnameField_Enter(object sender, EventArgs e)
        {
            if (userLastnameField.Text == "Введіть ім`я по-батькові")
            {
                userLastnameField.Text = "";
                userLastnameField.ForeColor = Color.Black;
            }
        }

        private void userLastnameField_Leave(object sender, EventArgs e)
        {
            if (userLastnameField.Text == "")
            {
                userLastnameField.Text = "Введіть ім`я по-батькові";
                userLastnameField.ForeColor = Color.Gray;
            }
        }

        private void userGroupField_Enter(object sender, EventArgs e)
        {
            if (userGroupField.Text == "Введіть групу")
            {
                userGroupField.Text = "";
                userGroupField.ForeColor = Color.Black;
            }
        }

        private void userGroupField_Leave(object sender, EventArgs e)
        {
            if (userGroupField.Text == "")
            {
                userGroupField.Text = "Введіть групу";
                userGroupField.ForeColor = Color.Gray;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Student student = new Student
            {
                Name = userNameField.Text,
                Surname = userSurnameField.Text,
                LastName = userLastnameField.Text,
                Group = userGroupField.Text
            };

            SaveData.AddStudent(student);
            MessageBox.Show("Data saved.");

            FormsManager.OpenForm(new SubjectsForm(student));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
