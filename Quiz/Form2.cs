using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Exitguna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }




        public Form activeform = null;
        void openChildForm(Form childForm)
        {
            if (activeform != null)
            {
                activeform.Close();
            }
            activeform = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Top;
                this.panel2.Controls.Add(childForm);
                this.panel2.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
            
        }

        private void Studentguna2Button2_Click(object sender, EventArgs e)
        {
            openChildForm(new Form4());
        }

        private void Teacherguna2Button1_Click(object sender, EventArgs e)
        {
            openChildForm(new Form3());
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void SaveButton2_Click(object sender, EventArgs e)
        {
          
        }
    }
}
