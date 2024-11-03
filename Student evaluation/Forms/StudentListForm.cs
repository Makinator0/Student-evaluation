using FormsManageraNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Student_evaluation
{
    public partial class StudentListForm : Form
    {
        private Form _registerForm;
        private ContextMenuStrip _contextMenu;

        public StudentListForm()
        {
            InitializeComponent();
            _registerForm = new RegisterForm();

            // Создаем контекстное меню
            _contextMenu = new ContextMenuStrip();
            var deleteMenuItem = new ToolStripMenuItem("Видалити", null, DeleteStudent);
            var editMenuItem = new ToolStripMenuItem("Редагувати", null, EditStudent);
            var semestrMenuItem = new ToolStripMenuItem("Звіт", null, OpenSemesterForm); 

            _contextMenu.Items.Add(deleteMenuItem);
            _contextMenu.Items.Add(editMenuItem);
            _contextMenu.Items.Add(semestrMenuItem); 

            // Включаем Drag-and-Drop для ListBox
            listBox1.AllowDrop = true;
            listBox2.AllowDrop = true;
            listBox3.AllowDrop = true;

            listBox1.MouseDown += listBox_MouseDown;
            listBox2.MouseDown += listBox_MouseDown;
            listBox3.MouseDown += listBox_MouseDown;

            listBox1.DragOver += listBox_DragOver;
            listBox2.DragOver += listBox_DragOver;
            listBox3.DragOver += listBox_DragOver;

            listBox1.DragDrop += listBox_DragDrop;
            listBox2.DragDrop += listBox_DragDrop;
            listBox3.DragDrop += listBox_DragDrop;


            LoadStudentsIntoListBoxes();
        }

        private void LoadStudentsIntoListBoxes()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            var students = SaveData.LoadStudents();

            // Сортируем студентов по фамилии, имени и отчеству с учётом кириллических символов
            var sortedStudents = students.OrderBy(s => s.Surname, StringComparer.CurrentCulture)
                                         .ThenBy(s => s.Name, StringComparer.CurrentCulture)
                                         .ThenBy(s => s.LastName, StringComparer.CurrentCulture)
                                         .ToList();

            foreach (var student in sortedStudents)
            {
                string displayName = $"{student.Surname} {student.Name} {student.LastName}";

                switch (student.Group)
                {
                    case "КН-1022А":
                        listBox1.Items.Add(displayName);
                        break;
                    case "КН-1022Б":
                        listBox2.Items.Add(displayName);
                        break;
                    case "КН-1022В":
                        listBox3.Items.Add(displayName);
                        break;
                }
            }
        }



        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListBox listBox = (ListBox)sender;
                int index = listBox.IndexFromPoint(e.Location);

                if (index != -1)
                {
                    listBox.SelectedIndex = index; // Выделяем элемент
                    _contextMenu.Show(listBox, e.Location); // Показываем контекстное меню
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                ListBox listBox = (ListBox)sender;
                if (listBox.SelectedItem != null)
                {
                    listBox.DoDragDrop(listBox.SelectedItem, DragDropEffects.Move);
                }
            }
        }

        private void listBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listBox_DragDrop(object sender, DragEventArgs e)
        {
            ListBox targetListBox = (ListBox)sender;
            string studentDisplayName = (string)e.Data.GetData(typeof(string));

            foreach (ListBox listBox in new ListBox[] { listBox1, listBox2, listBox3 })
            {
                if (listBox.Items.Contains(studentDisplayName))
                {
                    listBox.Items.Remove(studentDisplayName);
                    break;
                }
            }

            targetListBox.Items.Add(studentDisplayName);
            UpdateStudentGroup(studentDisplayName, targetListBox);
        }

        private void UpdateStudentGroup(string studentDisplayName, ListBox targetListBox)
        {
            string newGroup = targetListBox == listBox1 ? "КН-1022А" : targetListBox == listBox2 ? "КН-1022Б" : "КН-1022В";
            SaveData.UpdateStudentGroup(studentDisplayName, newGroup);
        }

        private void DeleteStudent(object sender, EventArgs e)
        {
            var selectedStudent = GetSelectedStudent();

            if (selectedStudent != null)
            {
                var students = SaveData.LoadStudents();
                var studentToRemove = students.FirstOrDefault(s => $"{s.Surname} {s.Name} {s.LastName}" == selectedStudent);

                if (studentToRemove != null)
                {
                    students.Remove(studentToRemove);
                    SaveData.SaveStudents(students);
                    LoadStudentsIntoListBoxes();
                }
            }
        }

        private void EditStudent(object sender, EventArgs e)
        {
            var selectedStudent = GetSelectedStudent(); 
            if (selectedStudent != null)
            {
                
                var studentData = selectedStudent.Split(' ');

                if (studentData.Length >= 3) 
                {
                    // Находим студента по ФИО
                    var originalDisplayName = $"{studentData[0]} {studentData[1]} {studentData[2]}";
                    var students = SaveData.LoadStudents();
                    var studentToEdit = students.FirstOrDefault(s => $"{s.Surname} {s.Name} {s.LastName}" == originalDisplayName);

                    if (studentToEdit != null)
                    {
                        // Передаем студента и информацию о том, что это режим редактирования
                        var registerForm = new RegisterForm(true, studentToEdit);
                        registerForm.ShowDialog(); // Открываем форму для редактирования
                        LoadStudentsIntoListBoxes();
                    }
                    else
                    {
                        MessageBox.Show("Студента не знайдено.");
                        LoadStudentsIntoListBoxes();
                    }
                }
                else
                {
                    MessageBox.Show("Неправильний формат даних студента.");
                }
            }
        }
        private void OpenSemesterForm(object sender, EventArgs e)
        {
            var selectedStudent = GetSelectedStudent();
            if (selectedStudent != null)
            {
                var studentData = selectedStudent.Split(' ');

                if (studentData.Length >= 3)
                {
                    // Находим студента по ФИО
                    var originalDisplayName = $"{studentData[0]} {studentData[1]} {studentData[2]}";
                    var students = SaveData.LoadStudents();
                    var studentToOpen = students.FirstOrDefault(s => $"{s.Surname} {s.Name} {s.LastName}" == originalDisplayName);

                    if (studentToOpen != null)
                    {
                        // Открываем форму SemestrForm с выбранным студентом
                        FormsManager.OpenForm(new SemestrForm(studentToOpen));
                    }
                    else
                    {
                        MessageBox.Show("Студента не знайдено.");
                    }
                }
                else
                {
                    MessageBox.Show("Неправильний формат даних студента.");
                }
            }
        }



        private string GetSelectedStudent()
        {
            if (listBox1.SelectedItem != null) return listBox1.SelectedItem.ToString();
            if (listBox2.SelectedItem != null) return listBox2.SelectedItem.ToString();
            if (listBox3.SelectedItem != null) return listBox3.SelectedItem.ToString();
            return null;
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            LoadStudentsIntoListBoxes();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.Show();

        }

        private void powerOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void авторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Створив Гасюк Максим");
        }

        private void StudentListForm_Load(object sender, EventArgs e)
        {
            LoadStudentsIntoListBoxes();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
            string surnameToSearch = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(surnameToSearch))
            {
                MessageBox.Show("Введіть прізвище для пошуку.");
                return;
            }

            ListBox targetListBox = null;
            foreach (ListBox listBox in new ListBox[] { listBox1, listBox2, listBox3 })
            {
                foreach (var item in listBox.Items)
                {
                    var studentDisplayName = item.ToString();
                    var studentData = studentDisplayName.Split(' ');
                    if (studentData.Length > 0 && studentData[0].Equals(surnameToSearch, StringComparison.OrdinalIgnoreCase))
                    {
                        targetListBox = listBox;
                        listBox.SelectedItem = item;
                        break;
                    }
                }

                if (targetListBox != null)
                    break;
            }

            if (targetListBox == null)
            {
                MessageBox.Show("Студента з таким прізвищем не знайдено.");
            }
          
        }
    }
}
