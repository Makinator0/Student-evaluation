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
    public partial class VerificationForm : Form
    {
        public static bool Verification = false;
        public string Token1 { get; set; }
        private Form _registerForm;
        private Form _studentListForm;
        public VerificationForm()
        {
            InitializeComponent();
            //Random random = new Random();
            //Token1 = random.Next(1000, 10000).ToString();
            //var twilioConfigString = File.ReadAllText("twilioConfig.json");
            //var twilioConfig = JsonSerializer.Deserialize<TwilioConfig>(twilioConfigString);
            //TwilioClient.Init(twilioConfig.AccountSID, twilioConfig.AuthToken);
            //MessageResource.Create(
            //                   body: "Your token is " + Token1 + "",
              //                                from: new Twilio.Types.PhoneNumber("+14158559808"),
                //                                             to: new Twilio.Types.PhoneNumber("+380986294196"));
            //Console.WriteLine("Sms was sent");
            _registerForm = new RegisterForm();
            _studentListForm = new StudentListForm();


        }

        private void loginField_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string token = SmsField.Text;
            Token1 = "8739";
            if (token == Token1)
            {

                MessageBox.Show("Вхід виконано успішно!");
                this.Close();
                _studentListForm.Show();
                
                



            }
            else
            {
                MessageBox.Show("Ви не пройшли верифікацію");
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Point lastPoint;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
