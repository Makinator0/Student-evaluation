using System;
using System.Drawing;
using System.IO;
using System.Net.PeerToPeer;
using System.Text.Json;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Student_evaluation
{
    public partial class AutoriseForm : Form
    {
        private const string _allowedName = "student";
        private const string _allowedPassword = "123456";




        
        private Form _verifyForm;

        public AutoriseForm()
        {
            InitializeComponent();
           
            _verifyForm = new VerificationForm();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = loginField.Text;
            string password = passField.Text;


            if (username == _allowedName && password == _allowedPassword)
            {
                MessageBox.Show("Вхід виконано успішно!");
                _verifyForm.Show();
                this.Visible = false;  





            }
            else
            {
                MessageBox.Show("Неправильне ім'я користувача або пароль. Спробуйте знову.");
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        Point lastPoint;
        private string token1;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Black;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Red;
        }

        

        

        
    }
}