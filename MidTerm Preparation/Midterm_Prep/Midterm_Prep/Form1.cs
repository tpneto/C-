using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Midterm_Prep
{
    public partial class Form1 : Form
    {
        private bool OKToChange = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void programsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource1.DataSource = Data.Programs.GetPrograms();
                bindingSource1.Sort = "ProgId";
                dataGridView1.DataSource = bindingSource1;
            }
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OKToChange)
            {
                dataGridView1.ReadOnly = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView1.AllowUserToDeleteRows = true;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                bindingSource2.DataSource = Data.Students.GetStudents();
                bindingSource2.Sort = "StudId";
                dataGridView1.DataSource = bindingSource2;

                dataGridView1.Columns["StudId"].HeaderText = "Student ID";
                dataGridView1.Columns["StudId"].DisplayIndex = 0;
                dataGridView1.Columns["Name"].DisplayIndex = 1;
                dataGridView1.Columns["YearEnrollment"].DisplayIndex = 2;
                dataGridView1.Columns["ProgId"].DisplayIndex = 3;
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            BusinessLayer.Programs.UpdatePrograms();
        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            if (BusinessLayer.Students.UpdateStudents() == -1) //Validation do press enter.
            {
                Validate();
            }
        }

        private void menuStrip1_Click(object sender, EventArgs e)
        {
            // =========================================================================
            // If the insertion / updated is ended by just changing to the other table 
            // (clicking on the menu strip) without clicking on datagrid, we need 
            // this code to ensure the database is updated. 
            // =========================================================================

            OKToChange = true;

            BindingSource temp = (BindingSource)dataGridView1.DataSource;

            //// It forces any current edition in DataGridView to be transmitted to the Datatable.
            //dataGridView1.DataSource = null;
            //// Ensure the same DataTable keeps showing, if the bindingSource is initialized.
            //dataGridView1.DataSource = temp;
            //
            // A better option for the two lines of code above is the following line.
            // It works even if there are DataErrors with e-Cancel = true (no automatic undoing).
            Validate();
            //

            if (temp == bindingSource1)
            {
                if (BusinessLayer.Programs.UpdatePrograms() == -1)
                {
                    OKToChange = false;
                }
            }
            else if(temp == bindingSource2)
            {
                if (BusinessLayer.Students.UpdateStudents() == -1)
                {
                    OKToChange = false;
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Impossible to insert / update / delete.");
            e.Cancel = false;// Ensure automatic undoing of the error (for instance: 
                             // when entering 'aaa' as a price). 
            OKToChange = false;
        }

        internal static void msgCommandInvalidYear()
        {
            MessageBox.Show("Command rejected! Invalid year.");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BindingSource temp = (BindingSource)dataGridView1.DataSource;

            if (temp == bindingSource1)
            {
                if (BusinessLayer.Programs.UpdatePrograms() == -1)
                {
                    OKToChange = false;
                }
            }
            else if (temp == bindingSource2)
            {
                if (BusinessLayer.Students.UpdateStudents() == -1)
                {
                    OKToChange = false;
                }
            }
            //MessageBox.Show("" + OKToChange);
            if (!OKToChange) { e.Cancel = true; OKToChange = true; }
        }
    }
}
