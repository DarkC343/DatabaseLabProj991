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
    public partial class SearchResult : Form
    {
        private List<Student> _students;
        public SearchResult(List<Student> students)
        {
            _students = students;
            InitializeComponent();
        }

        private void SearchResult_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _students;
            dataGridView1.Columns["StudentId"].Visible = false;
        }
    }
}
