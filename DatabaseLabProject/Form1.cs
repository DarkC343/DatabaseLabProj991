using DatabaseLabProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseLabProject
{
    public partial class Form1 : Form
    {
        private static MyDbContext db = new MyDbContext();
        public Form1()
        {
            InitializeComponent();
        }

        private void srchBtn_Click(object sender, EventArgs e)
        {
            //db.Students.AddRange(new Student { Name = "Ali", NattionalCode = "00123", Age = 22, AverageMark = 4, Rank = 12, IsIranian = true },
            //        new Student { Name = "Alex", NattionalCode = "00124", Age = 32, AverageMark = 25, Rank = 6, IsIranian = false },
            //        new Student { Name = "Hashem", NattionalCode = "00125", Age = 23, AverageMark = 17, Rank = 8, IsIranian = true },
            //        new Student { Name = "James", NattionalCode = "00126", Age = 21, AverageMark = 45, Rank = 1, IsIranian = false },
            //        new Student { Name = "Siamak", NattionalCode = "00127", Age = 20, AverageMark = 55, Rank = 2, IsIranian = true }
            //    );
            //db.SaveChanges();
            var allStudentList = db.Students.ToList();
            var filteredStudentList = allStudentList;
            if (nameCBox.Checked)
            {
                if (nameCoBox.Text == "Equals") filteredStudentList = allStudentList.Where(s => s.Name.ToLower() == nameTBox.Text.ToLower()).ToList();
                else if (nameCoBox.Text == "Contains") filteredStudentList = allStudentList.Where(s => s.Name.ToLower().Contains(nameTBox.Text.ToLower())).ToList();
            }
            if (nationalCodeCBox.Checked)
            {
                filteredStudentList = filteredStudentList.Where(s => s.NattionalCode == nationalCodeTBox.Text).ToList();
            }
            if (ageCBox.Checked)
            {
                if (ageCoBox.Text == "Equals") filteredStudentList = filteredStudentList.Where(s => s.Age == ageNBox.Value).ToList();
                else if (ageCoBox.Text == "<") filteredStudentList = filteredStudentList.Where(s => s.Age < ageNBox.Value).ToList();
                else if (ageCoBox.Text == "<=") filteredStudentList = filteredStudentList.Where(s => s.Age <= ageNBox.Value).ToList();
                else if (ageCoBox.Text == ">") filteredStudentList = filteredStudentList.Where(s => s.Age > ageNBox.Value).ToList();
                else if (ageCoBox.Text == ">=") filteredStudentList = filteredStudentList.Where(s => s.Age >= ageNBox.Value).ToList();
            }
            if (rankInGradeCBox.Checked)
            {
                if (rankInGradeCoBox.Text == "Equals") filteredStudentList = filteredStudentList.Where(s => s.Rank == rankInGrageNBox.Value).ToList();
                else if (rankInGradeCoBox.Text == "<") filteredStudentList = filteredStudentList.Where(s => s.Rank < rankInGrageNBox.Value).ToList();
                else if (rankInGradeCoBox.Text == "<=") filteredStudentList = filteredStudentList.Where(s => s.Rank <= rankInGrageNBox.Value).ToList();
                else if (rankInGradeCoBox.Text == ">") filteredStudentList = filteredStudentList.Where(s => s.Rank > rankInGrageNBox.Value).ToList();
                else if (rankInGradeCoBox.Text == ">=") filteredStudentList = filteredStudentList.Where(s => s.Rank >= rankInGrageNBox.Value).ToList();
            }
            if (averageMarkCBox.Checked)
            {
                if (averageMarkCoBox.Text == "Equals") filteredStudentList = filteredStudentList.Where(s => s.AverageMark == (float) averageMarkNBox.Value).ToList();
                else if (averageMarkCoBox.Text == "<") filteredStudentList = filteredStudentList.Where(s => s.AverageMark < (float)averageMarkNBox.Value).ToList();
                else if (averageMarkCoBox.Text == "<=") filteredStudentList = filteredStudentList.Where(s => s.AverageMark <= (float)averageMarkNBox.Value).ToList();
                else if (averageMarkCoBox.Text == ">") filteredStudentList = filteredStudentList.Where(s => s.AverageMark > (float)averageMarkNBox.Value).ToList();
                else if (averageMarkCoBox.Text == ">=") filteredStudentList = filteredStudentList.Where(s => s.AverageMark >= (float)averageMarkNBox.Value).ToList();
            }
            if (isIranianCBox.Checked)
            {
                if (isIranianCoBox.Text == "True") filteredStudentList = filteredStudentList.Where(s => s.IsIranian == true).ToList();
                else if (isIranianCoBox.Text == "False") filteredStudentList = filteredStudentList.Where(s => s.IsIranian == false).ToList();
            }

            //display
            SearchResult search = new SearchResult(filteredStudentList);
            search.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Student> allStudents = new List<Student>();
            foreach (var student in db.Students.ToList())
            {
                allStudents.Add(new Student { StudentId = student.StudentId, Name = student.Name, Age = student.Age, AverageMark = student.AverageMark, IsIranian = student.IsIranian, NattionalCode = student.NattionalCode, Rank = student.Rank });
            }
            dataGridView1.DataSource = allStudents;
            dataGridView1.Columns["StudentId"].Visible = false;
            dataGridView1.Columns["NattionalCode"].ReadOnly = true;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            var duplicateStudent = db.Students.Where(s => s.NattionalCode == reg_nationalCodeTBox.Text).FirstOrDefault();
            if(duplicateStudent == null)
            {
                var newStudent = new Student
                {
                    Name = reg_nameTBox.Text,
                    Age = (uint)reg_ageNBox.Value,
                    AverageMark = float.Parse(((float)reg_averageMarkNBox.Value).ToString("n2")),
                    NattionalCode = reg_nationalCodeTBox.Text,
                    Rank = (uint)reg_rankInGradeNBox.Value
                };
                if (reg_isIranianRBtn_yes.Checked) newStudent.IsIranian = true;
                else if (reg_isIranianRBtn_no.Checked) newStudent.IsIranian = false;
                try
                {
                    db.Students.Add(newStudent);
                    db.SaveChanges();
                    MessageBox.Show("Student added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("DB error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Duplicate user. Try with different national code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rfrshBtn_Click(object sender, EventArgs e)
        {
            List<Student> allStudents = new List<Student>();
            foreach(var student in db.Students.ToList())
            {
                allStudents.Add(new Student { StudentId = student.StudentId, Name = student.Name, Age = student.Age, AverageMark = student.AverageMark, IsIranian = student.IsIranian, NattionalCode = student.NattionalCode, Rank = student.Rank });
            }
            dataGridView1.DataSource = allStudents;
        }

        private void applyEditBtn_Click(object sender, EventArgs e)
        {
            var selectedStudent = dataGridView1.SelectedRows[0].DataBoundItem as Student;
            try
            {
                var foundStudent = db.Students.Where(s => s.NattionalCode == selectedStudent.NattionalCode).FirstOrDefault();
                if (selectedStudent.AverageMark < 0 || selectedStudent.AverageMark > 20)
                {
                    MessageBox.Show("Average mark out of range. Try something between 0 and 20.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    List<Student> allStudents = new List<Student>();
                    foreach (var student in db.Students.ToList())
                    {
                        allStudents.Add(new Student { StudentId = student.StudentId, Name = student.Name, Age = student.Age, AverageMark = student.AverageMark, IsIranian = student.IsIranian, NattionalCode = student.NattionalCode, Rank = student.Rank });
                    }
                    dataGridView1.DataSource = allStudents;
                }
                else
                {
                    foundStudent.Name = selectedStudent.Name;
                    foundStudent.NattionalCode = selectedStudent.NattionalCode;
                    foundStudent.IsIranian = selectedStudent.IsIranian;
                    foundStudent.Rank = selectedStudent.Rank;
                    foundStudent.Age = selectedStudent.Age;
                    foundStudent.AverageMark = float.Parse(((float)selectedStudent.AverageMark).ToString("n2"));
                    db.SaveChanges();
                    MessageBox.Show("Student edited successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("DB error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rmvBtn_Click(object sender, EventArgs e)
        {
            var selectedStudent = dataGridView1.SelectedRows[0].DataBoundItem as Student;
            try
            {
                var foundStudent = db.Students.Where(s => s.NattionalCode == selectedStudent.NattionalCode).FirstOrDefault();
                db.Students.Remove(foundStudent);
                db.SaveChanges();
                List<Student> allStudents = new List<Student>();
                foreach (var student in db.Students.ToList())
                {
                    allStudents.Add(new Student { StudentId = student.StudentId, Name = student.Name, Age = student.Age, AverageMark = student.AverageMark, IsIranian = student.IsIranian, NattionalCode = student.NattionalCode, Rank = student.Rank });
                }
                dataGridView1.DataSource = allStudents;
                MessageBox.Show("Student removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("DB error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
