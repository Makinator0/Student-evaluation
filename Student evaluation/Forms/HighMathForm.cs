﻿using System;
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
        private Student _student;
        private string _semesterNumber;
        private string _shortName;
        private string _Subject;

        public HighMathForm(Student student, string semesterNumber, string shortName, string Subject)
        {
            InitializeComponent();
            _Subject = Subject;
            _student = student;
            _semesterNumber = semesterNumber;
            _shortName = shortName;
            label2.Text = _shortName;
            label4.Text = _student.Group;
            label5.Text = Subject;
        }
        

        private void HighMathForm_Load(object sender, EventArgs e)
        {
            // Загрузка данных при необходимости
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Получаем числовые оценки
            int firstHWScore = int.Parse(textBox1.Text);
            int firstTestScore = int.Parse(textBox6.Text);
            int secondHWScore = int.Parse(textBox14.Text);

            // Считаем среднее арифметическое
            double averageScore = (firstHWScore + firstTestScore + secondHWScore) / 3.0;

            // Определяем буквенную оценку
            string letterGrade;
            if (averageScore >= 90) letterGrade = "A";
            else if (averageScore >= 80) letterGrade = "B";
            else if (averageScore >= 70) letterGrade = "C";
            else if (averageScore >= 60) letterGrade = "D";
            else letterGrade = "F";

            // Выводим результаты
            label12.Text = averageScore.ToString("F2"); // Среднее арифметическое с двумя знаками после запятой
            label13.Text = letterGrade; // Буквенная оценка

            // Создаем объект SubjectGrades
            var grades = new SubjectGrades
            {
                FirstHW = firstHWScore + textBox4.Text, // Сочетание числа и буквенной оценки
                FirstTest = firstTestScore + textBox5.Text,
                SecondHW = secondHWScore + textBox13.Text,
                AverageScore = averageScore,
                LetterGrade = letterGrade
            };

            // Обновляем оценки студента
            SaveData.UpdateStudentGrades(_student.Name, _semesterNumber, _Subject, grades);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Здесь можно добавить код для рисования на панели, если требуется.
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

        private void closeButton1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void closeButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }

}
