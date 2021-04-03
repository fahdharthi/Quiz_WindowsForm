using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Quiz
{
    public partial class CreateQusetion_UserControl1 : UserControl
    {
        public CreateQusetion_UserControl1()
        {
            InitializeComponent();
        }

        List<QuestionBlock> questionBlocks = new List<QuestionBlock>(); 
   
        public string ID
        {
            get { return IDtextBox1.Text; }
            set { IDtextBox1.Text = value; }
        }

        public string answerID_0 ="";
        public string answerID_1 = "";
        public string answerID_2 = "";
        public string answerID_3 = "";

        public string TrueFalse_VariantA_choose
        {
            get { return TrueFalseguna2ComboBox1.Text; }
            set { TrueFalseguna2ComboBox1.Text = value; }
        }

        public string TrueFalse_VariantB_choose
        {
            get { return TrueFalseguna2ComboBox2.Text; }
            set { TrueFalseguna2ComboBox2.Text = value; }
        }

        public string TrueFalse_VariantC_choose
        {
            get { return TrueFalseguna2ComboBox3.Text; }
            set { TrueFalseguna2ComboBox3.Text = value; }
        }

        public string TrueFalse_VariantD_choose
        {
            get { return TrueFalseguna2ComboBox4.Text; }
            set { TrueFalseguna2ComboBox4.Text = value; }
        }

        public int trueanswer=0;
       public int falseanswer = 0;


        public string VariantA_choose
        {
            get { return Variant_ArichTextBox.Text; }
            set { Variant_ArichTextBox.Text = value; }
        }

        public string VariantB_choose
        {
            get { return Variant_BrichTextBox.Text; }
            set { Variant_BrichTextBox.Text = value; }
        }

        public string VariantC_choose
        {
            get { return Variant_CrichTextBox.Text; }
            set { Variant_CrichTextBox.Text = value; }
        }

        public string VariantD_choose
        {
            get { return Variant_CrichTextBox.Text; }
            set { Variant_ArichTextBox.Text = value; }
        }

        public string Question
        {
            get { return QuestionrichTextBox1.Text; }
            set { QuestionrichTextBox1.Text = value; }
        }

        private void IDtextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;

            }
        }

        private void TrueFalseguna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrueFalseguna2ComboBox4.Text == "Yes")
            {
                TrueFalseguna2ComboBox1.Text = "No";
                TrueFalseguna2ComboBox2.Text = "No";
                TrueFalseguna2ComboBox3.Text = "No";

            }
        }

        private void TrueFalseguna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrueFalseguna2ComboBox2.Text == "Yes")
            {
                TrueFalseguna2ComboBox1.Text = "No";
                TrueFalseguna2ComboBox3.Text = "No";
                TrueFalseguna2ComboBox4.Text = "No";

            }
        }

        private void TrueFalseguna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (TrueFalseguna2ComboBox3.Text == "Yes")
            {
                TrueFalseguna2ComboBox1.Text = "No";
                TrueFalseguna2ComboBox2.Text = "No";
                TrueFalseguna2ComboBox4.Text = "No";

            }

    
        }

        private void TrueFalseguna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TrueFalseguna2ComboBox1.Text == "Yes")
            {
                TrueFalseguna2ComboBox2.Text = "No";
                TrueFalseguna2ComboBox3.Text = "No";
                TrueFalseguna2ComboBox4.Text = "No";

            }

 
        }

        private void OK1pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
