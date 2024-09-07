using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Student_evaluation
{
    public partial class StudentListForm : Form
    {
        private const int ButtonWidth = 250;
        private const int ButtonHeight = 30;
        private const int ColumnSpacing = 20;
        private const int FooterButtonWidth = 120;
        private const int FooterButtonHeight = 40;
        private const int FooterMargin = 10;
        private const int ListBoxWidth = 250;
        private const int ListBoxHeight = 400;
        private const int HeaderHeight = 30;
        private const int ListBoxFontSize = 12;

        private Form _registerForm;
        private Panel _panel;

        public StudentListForm()
        {
            InitializeComponent();
            _registerForm = new RegisterForm();
            AddFooterButtons();
            LoadDataFromFileAndCreateListBoxes();
        }

        private void LoadDataFromFileAndCreateListBoxes()
        {
            // Clear existing controls on the panel
            if (_panel != null)
            {
                this.Controls.Remove(_panel);
                _panel.Dispose();
            }

            _panel = new Panel();
            _panel.AutoScroll = true;
            _panel.Size = new System.Drawing.Size(800, 500); // Adjusted size to fit footer buttons
            _panel.Location = new System.Drawing.Point(10, 50);
            this.Controls.Add(_panel);

            if (File.Exists("students.txt"))
            {
                string[] lines = File.ReadAllLines("students.txt");
                MessageBox.Show($"Found {lines.Length} lines in students.txt");

                var groupStudents = new Dictionary<string, List<Student>>();

                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 4)
                        {
                            var student = new Student
                            {
                                Name = parts[0],
                                Surname = parts[1],
                                LastName = parts[2],
                                Group = parts[3]
                            };

                            if (!groupStudents.ContainsKey(student.Group))
                            {
                                groupStudents[student.Group] = new List<Student>();
                            }
                            groupStudents[student.Group].Add(student);
                        }
                    }
                }

                int xPosition = 10;

                foreach (var group in groupStudents)
                {
                    // Create header label for each group
                    Label headerLabel = new Label();
                    headerLabel.Text = group.Key;
                    headerLabel.Size = new System.Drawing.Size(ListBoxWidth, HeaderHeight);
                    headerLabel.Location = new System.Drawing.Point(xPosition, 10);
                    _panel.Controls.Add(headerLabel);

                    // Create ListBox for each group
                    ListBox listBox = new ListBox();
                    listBox.Size = new System.Drawing.Size(ListBoxWidth, ListBoxHeight);
                    listBox.Location = new System.Drawing.Point(xPosition, 50);
                    listBox.Font = new System.Drawing.Font("Arial", ListBoxFontSize); // Set font size

                    foreach (var student in group.Value)
                    {
                        listBox.Items.Add($"{student.Surname} {student.Name} {student.LastName}");
                    }

                    listBox.SelectedIndexChanged += (sender, e) =>
                    {
                        if (listBox.SelectedIndex >= 0)
                        {
                            OpenStudentForm(group.Value[listBox.SelectedIndex]);
                        }
                    };

                    _panel.Controls.Add(listBox);
                    xPosition += ListBoxWidth + ColumnSpacing; // Adjust spacing between ListBoxes
                }

                _panel.PerformLayout();
            }
            else
            {
                MessageBox.Show("Файл students.txt не найден!");
            }
        }

        private void OpenStudentForm(Student student)
        {
            var semestrForm = new SemestrForm(student);
            semestrForm.Show();
        }

        private void AddFooterButtons()
        {
            // Adjusting positions to be lower and to the left
            int buttonSpacing = 10;

            Button button1 = new Button();
            button1.Text = "Register";
            button1.Size = new System.Drawing.Size(FooterButtonWidth, FooterButtonHeight);
            button1.Location = new System.Drawing.Point(buttonSpacing, this.ClientSize.Height - FooterButtonHeight - FooterMargin);
            button1.Click += (sender, e) => OpenRegisterForm();
            this.Controls.Add(button1);

            Button button2 = new Button();
            button2.Text = "Update";
            button2.Size = new System.Drawing.Size(FooterButtonWidth, FooterButtonHeight);
            button2.Location = new System.Drawing.Point(buttonSpacing + FooterButtonWidth + buttonSpacing, this.ClientSize.Height - FooterButtonHeight - FooterMargin);
            button2.Click += (sender, e) => UpdateStudents();
            this.Controls.Add(button2);
        }

        private void OpenRegisterForm()
        {
            
            var _registerForm = new RegisterForm();
            _registerForm.Show();
        }

        private void UpdateStudents()
        {
            // Add your update logic here
            MessageBox.Show("Students updated!");
            // Reload the data from the file and recreate the list boxes
            LoadDataFromFileAndCreateListBoxes();
        }
    }
}
