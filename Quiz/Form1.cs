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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            panel2.Show();
            panel2.Width += 50;
            if (panel2.Width >=1149)
            {
                timer1.Stop();
                this.Hide();
            form2.Show();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
