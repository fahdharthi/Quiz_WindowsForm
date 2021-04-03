
using iTextSharp.text;
using iTextSharp.text.pdf;
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
using System.Xml;
using System.Xml.Serialization;

namespace Quiz
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        string filename = "";

        List<StudentAccount> studentAccounts = new List<StudentAccount>();
        StringBuilder stringBuilder = new StringBuilder();
        DateTime d = DateTime.Now;

        private void SingUpStudent_Click(object sender, EventArgs e)
        {
            
            try
            {
                studentAccounts.Add(new StudentAccount(SingUpNameStudent.Text, SingUpSurnameStudent.Text, SingUpEmailStudent.Text, SingUpPasswordStudent.Text, SingUpConfirmPasswordStudent.Text, SingUpUsernameStudent.Text)
                {

                    _StudentName = SingUpNameStudent.Text,
                    StudentSurname = SingUpSurnameStudent.Text,
                    StudentEmail = SingUpEmailStudent.Text,
                    StudentPassword = SingUpPasswordStudent.Text,
                    StudentConfirmPassword = SingUpConfirmPasswordStudent.Text,
                    StudentUsername = SingUpUsernameStudent.Text,
                });


                if (SingUpPasswordStudent.Text == SingUpConfirmPasswordStudent.Text)
                {
                    SaveStudentAccont();

                }

                else
                {
                    MessageBox.Show("Error");
                }

            }
            catch (Exception)
            {


            }

            SingUpStudentguna2Button2.Enabled = false;
        }


    

        public void SaveStudentAccont()
        {

            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }
            if (!Directory.Exists("TotalData"))
            {
                Directory.CreateDirectory("TotalData");
            }


            if (File.Exists($@"Data\RegisterStudent {d.ToString("MM dd yyyy")}.xml"))
            {
                File.Delete($@"Data\RegisterStudent {d.ToString("MM dd yyyy")}.xml");

            }

            string a = "";

            filename = SingUpUsernameStudent.Text;
            var xml = new XmlSerializer(typeof(List<StudentAccount>));
            using (var fs = new FileStream($@"Data\RegisterStudent {d.ToString("MM dd yyyy")}.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, studentAccounts);
            }

            XmlReaderSettings settings = new XmlReaderSettings();
         
            settings.CheckCharacters = false;

            StudentAccount studentAccount = null;

            var xml2 = new XmlSerializer(typeof(List<StudentAccount>));

            using (var fs2 = new FileStream($@"Data\RegisterStudent {d.ToString("MM dd yyyy")}.xml", FileMode.OpenOrCreate))
            {
                studentAccount = xml2.Deserialize(fs2) as StudentAccount;

                foreach (var item in studentAccounts)
                {
                    a = $"{item._StudentName} \n {item.StudentSurname} \n {item.StudentPassword} \n {item.StudentEmail} \n {item.StudentUsername} \n {item.StudentConfirmPassword}";

                    stringBuilder.Append(a);

                    a = stringBuilder.ToString();
                }


            }




            copyto();



        }

        void copyto()
        {
            XmlDocument document = new XmlDocument();

            document.Load($@"Data\RegisterStudent {d.ToString("MM dd yyyy")}.xml");
            if (!File.Exists($@"TotalData\Student.xml"))
            {
                File.Create($@"TotalData\Student.xml").Close();

            }




            document.Save($@"TotalData\Student.xml");



        }

        string str = "";
        private void SingInStudent_Click(object sender, EventArgs e)
        {
    

            DirectoryInfo di = new DirectoryInfo($@"Data");


   
            
            /*
            
            FileInfo[] f = di.GetFiles("*.xml", SearchOption.AllDirectories);

            foreach (FileInfo file in f)
            {
                fileresult = fileresult + " " + file.Name;


            }

            */

            string path = $@"Data\{filename} {d.ToString("MM dd yyyy")}.xml";







            string sname = SingInUserNameStudent.Text;

            string spasw = SingInPasswordStudent.Text;

            try
            {

                XmlDocument doc = new XmlDocument();
                doc.Load($@"Data\RegisterStudent {d.ToString("MM dd yyyy")}.xml");
                XmlNodeList xmlNodeList = doc.GetElementsByTagName("StudentUsername");
                XmlNodeList xmlNodeList2 = doc.GetElementsByTagName("StudentPassword");
                for (int i = 0; i < xmlNodeList.Count; i++)
                {


                    for (int j = 0; j < xmlNodeList2.Count; j++)
                    {

                        if (sname == xmlNodeList[i].InnerText.ToString() && spasw == xmlNodeList2[j].InnerText.ToString())
                        {
                            MessageBox.Show($"{xmlNodeList[i].InnerText.ToString()} {xmlNodeList2[j].InnerText.ToString()}");


                            panel1.Show();
                            SingUpStudentguna2Button2.Hide();


                            DirectoryInfo d = new DirectoryInfo($@"Question");
                          
                            FileInfo[] files = d.GetFiles("*.xml");
             
                     
                  
                            foreach (FileInfo file in files)
                            {
                                string r= file.Name.Replace(".xml", string.Empty);
                                str = r;


                                listBox1.Items.Add(str);

                            }

                     
                        }



                        if (sname != xmlNodeList[i].InnerText.ToString() || spasw != xmlNodeList2[j].InnerText.ToString())
                        {
                            MessageBox.Show($"Error");
                        }

                    }


                }
            }
            catch (Exception)
            {


            }


        }

        private void Form4_Load(object sender, EventArgs e)
        {
            panel1.Hide();

            Selectguna2Button1.Enabled = false;

            flowLayoutPanel1.Hide();

            Resultguna2Button1.Hide();

            Truelabel11.Hide();
            Falselabel14.Hide();
            TPlabel12.Hide();
            FPlabel13.Hide();

        

        }

     
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

               


            if (listBox1.SelectedIndex == -1)
            {
                label9.Text = label9.Text;

            }
            if (listBox1.SelectedIndex != -1)
            {
                label9.Text = listBox1.SelectedItem.ToString();
                Selectguna2Button1.Enabled = true;

            }
            
        }

        CreateQusetion_UserControl1[] createQusetion_UserControl1s = new CreateQusetion_UserControl1[20];

        XmlDocument doc = new XmlDocument(); 
        XmlDocument doc2 = new XmlDocument();
        private void Selectguna2Button1_Click(object sender, EventArgs e)
        {
            Resultguna2Button1.Show();
          
            flowLayoutPanel1.Show();
            label9.Hide();
            Selectguna2Button1.Hide();
            listBox1.Hide();

            Truelabel11.Show();
            Falselabel14.Show();
            TPlabel12.Show();
            FPlabel13.Show();




            if (label9.Text != "" && !String.IsNullOrEmpty(label9.Text))
            {

                label9.Text = listBox1.SelectedItem.ToString();

        
                doc.Load($@"Question\{label9.Text}.xml");

                if (!Directory.Exists($@"Answer"))
                {
                    Directory.CreateDirectory($@"Answer");

                }








                if (!File.Exists($@"Answer\SelectQuestion.xml"))
                {
                    File.Create($@"Answer\SelectQuestion.xml").Close();

                }

                doc.Save($@"Answer\SelectQuestion.xml");


        
                doc2.Load($@"Answer\SelectQuestion.xml");
                XmlNodeList xmlNodeList = doc2.GetElementsByTagName("Text");
                XmlNodeList xmlNodeList2 = doc2.GetElementsByTagName("Answer");



                string id0 = "0";
                string id1 = "1";
                string id2 = "2";
                string id3 = "3";
                string id4 = "4";
                string id5 = "5";
                string id6 = "6";
                string id7 = "7";
                string id8 = "8";
                string id9 = "9";
                string id10 = "10";
                string id11 = "11";
                string id12 = "12";
                string id13 = "13";
                string id14 = "14";
                string id15 = "15";
                string id16 = "16";
                string id17 = "17";
                string id18 = "18";
                string id19 = "19";







                createQusetion_UserControl1s[0] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[0].ID = id0;

                createQusetion_UserControl1s[0].answerID_0 = "0";
                createQusetion_UserControl1s[0].answerID_1 = "1";
                createQusetion_UserControl1s[0].answerID_2 = "2";
                createQusetion_UserControl1s[0].answerID_3 = "3";

                createQusetion_UserControl1s[1] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[1].ID = id1;

                createQusetion_UserControl1s[1].answerID_0 = "0";
                createQusetion_UserControl1s[1].answerID_1 = "1";
                createQusetion_UserControl1s[1].answerID_2 = "2";
                createQusetion_UserControl1s[1].answerID_3 = "3";

                createQusetion_UserControl1s[2] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[2].ID = id2;

                createQusetion_UserControl1s[2].answerID_0 = "0";
                createQusetion_UserControl1s[2].answerID_1 = "1";
                createQusetion_UserControl1s[2].answerID_2 = "2";
                createQusetion_UserControl1s[2].answerID_3 = "3";


                createQusetion_UserControl1s[3] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[3].ID = id3;

                createQusetion_UserControl1s[3].answerID_0 = "0";
                createQusetion_UserControl1s[3].answerID_1 = "1";
                createQusetion_UserControl1s[3].answerID_2 = "2";
                createQusetion_UserControl1s[3].answerID_3 = "3";

                createQusetion_UserControl1s[4] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[4].ID = id4;

                createQusetion_UserControl1s[4].answerID_0 = "0";
                createQusetion_UserControl1s[4].answerID_1 = "1";
                createQusetion_UserControl1s[4].answerID_2 = "2";
                createQusetion_UserControl1s[4].answerID_3 = "3";

                createQusetion_UserControl1s[5] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[5].ID = id5;

                createQusetion_UserControl1s[5].answerID_0 = "0";
                createQusetion_UserControl1s[5].answerID_1 = "1";
                createQusetion_UserControl1s[5].answerID_2 = "2";
                createQusetion_UserControl1s[5].answerID_3 = "3";

                createQusetion_UserControl1s[6] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[6].ID = id6;

                createQusetion_UserControl1s[6].answerID_0 = "0";
                createQusetion_UserControl1s[6].answerID_1 = "1";
                createQusetion_UserControl1s[6].answerID_2 = "2";
                createQusetion_UserControl1s[6].answerID_3 = "3";

                createQusetion_UserControl1s[7] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[7].ID = id7;

                createQusetion_UserControl1s[7].answerID_0 = "0";
                createQusetion_UserControl1s[7].answerID_1 = "1";
                createQusetion_UserControl1s[7].answerID_2 = "2";
                createQusetion_UserControl1s[7].answerID_3 = "3";

                createQusetion_UserControl1s[8] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[8].ID = id8;

                createQusetion_UserControl1s[8].answerID_0 = "0";
                createQusetion_UserControl1s[8].answerID_1 = "1";
                createQusetion_UserControl1s[8].answerID_2 = "2";
                createQusetion_UserControl1s[8].answerID_3 = "3";

                createQusetion_UserControl1s[9] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[9].ID = id9;

                createQusetion_UserControl1s[9].answerID_0 = "0";
                createQusetion_UserControl1s[9].answerID_1 = "1";
                createQusetion_UserControl1s[9].answerID_2 = "2";
                createQusetion_UserControl1s[9].answerID_3 = "3";

                createQusetion_UserControl1s[10] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[10].ID = id10;

                createQusetion_UserControl1s[10].answerID_0 = "0";
                createQusetion_UserControl1s[10].answerID_1 = "1";
                createQusetion_UserControl1s[10].answerID_2 = "2";
                createQusetion_UserControl1s[10].answerID_3 = "3";

                createQusetion_UserControl1s[11] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[11].ID = id11;

                createQusetion_UserControl1s[11].answerID_0 = "0";
                createQusetion_UserControl1s[11].answerID_1 = "1";
                createQusetion_UserControl1s[11].answerID_2 = "2";
                createQusetion_UserControl1s[11].answerID_3 = "3";

                createQusetion_UserControl1s[12] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[12].ID = id12;

                createQusetion_UserControl1s[12].answerID_0 = "0";
                createQusetion_UserControl1s[12].answerID_1 = "1";
                createQusetion_UserControl1s[12].answerID_2 = "2";
                createQusetion_UserControl1s[12].answerID_3 = "3";

                createQusetion_UserControl1s[13] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[13].ID = id13;

                createQusetion_UserControl1s[13].answerID_0 = "0";
                createQusetion_UserControl1s[13].answerID_1 = "1";
                createQusetion_UserControl1s[13].answerID_2 = "2";
                createQusetion_UserControl1s[13].answerID_3 = "3";

                createQusetion_UserControl1s[14] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[14].ID = id14;

                createQusetion_UserControl1s[14].answerID_0 = "0";
                createQusetion_UserControl1s[14].answerID_1 = "1";
                createQusetion_UserControl1s[14].answerID_2 = "2";
                createQusetion_UserControl1s[14].answerID_3 = "3";

                createQusetion_UserControl1s[15] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[15].ID = id15;

                createQusetion_UserControl1s[15].answerID_0 = "0";
                createQusetion_UserControl1s[15].answerID_1 = "1";
                createQusetion_UserControl1s[15].answerID_2 = "2";
                createQusetion_UserControl1s[15].answerID_3 = "3";

                createQusetion_UserControl1s[16] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[16].ID = id16;

                createQusetion_UserControl1s[16].answerID_0 = "0";
                createQusetion_UserControl1s[16].answerID_1 = "1";
                createQusetion_UserControl1s[16].answerID_2 = "2";
                createQusetion_UserControl1s[16].answerID_3 = "3";

                createQusetion_UserControl1s[17] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[17].ID = id17;

                createQusetion_UserControl1s[17].answerID_0 = "0";
                createQusetion_UserControl1s[17].answerID_1 = "1";
                createQusetion_UserControl1s[17].answerID_2 = "2";
                createQusetion_UserControl1s[17].answerID_3 = "3";

                createQusetion_UserControl1s[18] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[18].ID = id18;

                createQusetion_UserControl1s[18].answerID_0 = "0";
                createQusetion_UserControl1s[18].answerID_1 = "1";
                createQusetion_UserControl1s[18].answerID_2 = "2";
                createQusetion_UserControl1s[18].answerID_3 = "3";

                createQusetion_UserControl1s[19] = new CreateQusetion_UserControl1();
                createQusetion_UserControl1s[19].ID = id19;


                createQusetion_UserControl1s[19].answerID_0 = "0";
                createQusetion_UserControl1s[19].answerID_1 = "1";
                createQusetion_UserControl1s[19].answerID_2 = "2";
                createQusetion_UserControl1s[19].answerID_3 = "3";



                if (panel1.Controls.Count < 0)
                {
                    panel1.Controls.Clear();
                }

                else



                    createQusetion_UserControl1s[0].Question = createQusetion_UserControl1s[0].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[0].VariantA_choose = createQusetion_UserControl1s[0].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[0].VariantB_choose = createQusetion_UserControl1s[0].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[0].VariantC_choose = createQusetion_UserControl1s[0].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[0].VariantD_choose = createQusetion_UserControl1s[0].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[0].TrueFalse_VariantA_choose = createQusetion_UserControl1s[0].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[0].TrueFalse_VariantB_choose = createQusetion_UserControl1s[0].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[0].TrueFalse_VariantC_choose = createQusetion_UserControl1s[0].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[0].TrueFalse_VariantD_choose = createQusetion_UserControl1s[0].TrueFalseguna2ComboBox4.Text;


                createQusetion_UserControl1s[1].Question = createQusetion_UserControl1s[1].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[1].VariantA_choose = createQusetion_UserControl1s[1].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[1].VariantB_choose = createQusetion_UserControl1s[1].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[1].VariantC_choose = createQusetion_UserControl1s[1].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[1].VariantD_choose = createQusetion_UserControl1s[1].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[1].TrueFalse_VariantA_choose = createQusetion_UserControl1s[1].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[1].TrueFalse_VariantB_choose = createQusetion_UserControl1s[1].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[1].TrueFalse_VariantC_choose = createQusetion_UserControl1s[1].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[1].TrueFalse_VariantD_choose = createQusetion_UserControl1s[1].TrueFalseguna2ComboBox4.Text;





                createQusetion_UserControl1s[2].Question = createQusetion_UserControl1s[2].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[2].VariantA_choose = createQusetion_UserControl1s[2].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[2].VariantB_choose = createQusetion_UserControl1s[2].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[2].VariantC_choose = createQusetion_UserControl1s[2].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[2].VariantD_choose = createQusetion_UserControl1s[2].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[2].TrueFalse_VariantA_choose = createQusetion_UserControl1s[2].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[2].TrueFalse_VariantB_choose = createQusetion_UserControl1s[2].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[2].TrueFalse_VariantC_choose = createQusetion_UserControl1s[2].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[2].TrueFalse_VariantD_choose = createQusetion_UserControl1s[2].TrueFalseguna2ComboBox4.Text;





                createQusetion_UserControl1s[3].Question = createQusetion_UserControl1s[3].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[3].VariantA_choose = createQusetion_UserControl1s[3].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[3].VariantB_choose = createQusetion_UserControl1s[3].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[3].VariantC_choose = createQusetion_UserControl1s[3].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[3].VariantD_choose = createQusetion_UserControl1s[3].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[3].TrueFalse_VariantA_choose = createQusetion_UserControl1s[3].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[3].TrueFalse_VariantB_choose = createQusetion_UserControl1s[3].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[3].TrueFalse_VariantC_choose = createQusetion_UserControl1s[3].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[3].TrueFalse_VariantD_choose = createQusetion_UserControl1s[3].TrueFalseguna2ComboBox4.Text;










                createQusetion_UserControl1s[4].Question = createQusetion_UserControl1s[4].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[4].VariantA_choose = createQusetion_UserControl1s[4].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[4].VariantB_choose = createQusetion_UserControl1s[4].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[4].VariantC_choose = createQusetion_UserControl1s[4].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[4].VariantD_choose = createQusetion_UserControl1s[4].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[4].TrueFalse_VariantA_choose = createQusetion_UserControl1s[4].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[4].TrueFalse_VariantB_choose = createQusetion_UserControl1s[4].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[4].TrueFalse_VariantC_choose = createQusetion_UserControl1s[4].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[4].TrueFalse_VariantD_choose = createQusetion_UserControl1s[4].TrueFalseguna2ComboBox4.Text;








                createQusetion_UserControl1s[5].Question = createQusetion_UserControl1s[5].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[5].VariantA_choose = createQusetion_UserControl1s[5].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[5].VariantB_choose = createQusetion_UserControl1s[5].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[5].VariantC_choose = createQusetion_UserControl1s[5].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[5].VariantD_choose = createQusetion_UserControl1s[5].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[5].TrueFalse_VariantA_choose = createQusetion_UserControl1s[5].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[5].TrueFalse_VariantB_choose = createQusetion_UserControl1s[5].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[5].TrueFalse_VariantC_choose = createQusetion_UserControl1s[5].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[5].TrueFalse_VariantD_choose = createQusetion_UserControl1s[5].TrueFalseguna2ComboBox4.Text;








                createQusetion_UserControl1s[6].Question = createQusetion_UserControl1s[6].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[6].VariantA_choose = createQusetion_UserControl1s[6].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[6].VariantB_choose = createQusetion_UserControl1s[6].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[6].VariantC_choose = createQusetion_UserControl1s[6].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[6].VariantD_choose = createQusetion_UserControl1s[6].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[6].TrueFalse_VariantA_choose = createQusetion_UserControl1s[6].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[6].TrueFalse_VariantB_choose = createQusetion_UserControl1s[6].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[6].TrueFalse_VariantC_choose = createQusetion_UserControl1s[6].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[6].TrueFalse_VariantD_choose = createQusetion_UserControl1s[6].TrueFalseguna2ComboBox4.Text;









                createQusetion_UserControl1s[7].Question = createQusetion_UserControl1s[7].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[7].VariantA_choose = createQusetion_UserControl1s[7].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[7].VariantB_choose = createQusetion_UserControl1s[7].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[7].VariantC_choose = createQusetion_UserControl1s[7].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[7].VariantD_choose = createQusetion_UserControl1s[7].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[7].TrueFalse_VariantA_choose = createQusetion_UserControl1s[7].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[7].TrueFalse_VariantB_choose = createQusetion_UserControl1s[7].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[7].TrueFalse_VariantC_choose = createQusetion_UserControl1s[7].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[7].TrueFalse_VariantD_choose = createQusetion_UserControl1s[7].TrueFalseguna2ComboBox4.Text;

                


                createQusetion_UserControl1s[8].Question = createQusetion_UserControl1s[8].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[8].VariantA_choose = createQusetion_UserControl1s[8].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[8].VariantB_choose = createQusetion_UserControl1s[8].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[8].VariantC_choose = createQusetion_UserControl1s[8].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[8].VariantD_choose = createQusetion_UserControl1s[8].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[8].TrueFalse_VariantA_choose = createQusetion_UserControl1s[8].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[8].TrueFalse_VariantB_choose = createQusetion_UserControl1s[8].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[8].TrueFalse_VariantC_choose = createQusetion_UserControl1s[8].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[8].TrueFalse_VariantD_choose = createQusetion_UserControl1s[8].TrueFalseguna2ComboBox4.Text;


                

                createQusetion_UserControl1s[9].Question = createQusetion_UserControl1s[9].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[9].VariantA_choose = createQusetion_UserControl1s[9].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[9].VariantB_choose = createQusetion_UserControl1s[9].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[9].VariantC_choose = createQusetion_UserControl1s[9].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[9].VariantD_choose = createQusetion_UserControl1s[9].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[9].TrueFalse_VariantA_choose = createQusetion_UserControl1s[9].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[9].TrueFalse_VariantB_choose = createQusetion_UserControl1s[9].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[9].TrueFalse_VariantC_choose = createQusetion_UserControl1s[9].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[9].TrueFalse_VariantD_choose = createQusetion_UserControl1s[9].TrueFalseguna2ComboBox4.Text;






                createQusetion_UserControl1s[10].Question = createQusetion_UserControl1s[10].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[10].VariantA_choose = createQusetion_UserControl1s[10].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[10].VariantB_choose = createQusetion_UserControl1s[10].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[10].VariantC_choose = createQusetion_UserControl1s[10].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[10].VariantD_choose = createQusetion_UserControl1s[10].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[10].TrueFalse_VariantA_choose = createQusetion_UserControl1s[10].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[10].TrueFalse_VariantB_choose = createQusetion_UserControl1s[10].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[10].TrueFalse_VariantC_choose = createQusetion_UserControl1s[10].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[10].TrueFalse_VariantD_choose = createQusetion_UserControl1s[10].TrueFalseguna2ComboBox4.Text;






                createQusetion_UserControl1s[11].Question = createQusetion_UserControl1s[11].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[11].VariantA_choose = createQusetion_UserControl1s[11].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[11].VariantB_choose = createQusetion_UserControl1s[11].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[11].VariantC_choose = createQusetion_UserControl1s[11].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[11].VariantD_choose = createQusetion_UserControl1s[11].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[11].TrueFalse_VariantA_choose = createQusetion_UserControl1s[11].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[11].TrueFalse_VariantB_choose = createQusetion_UserControl1s[11].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[11].TrueFalse_VariantC_choose = createQusetion_UserControl1s[11].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[11].TrueFalse_VariantD_choose = createQusetion_UserControl1s[11].TrueFalseguna2ComboBox4.Text;




                createQusetion_UserControl1s[12].Question = createQusetion_UserControl1s[12].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[12].VariantA_choose = createQusetion_UserControl1s[12].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[12].VariantB_choose = createQusetion_UserControl1s[12].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[12].VariantC_choose = createQusetion_UserControl1s[12].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[12].VariantD_choose = createQusetion_UserControl1s[12].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[12].TrueFalse_VariantA_choose = createQusetion_UserControl1s[12].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[12].TrueFalse_VariantB_choose = createQusetion_UserControl1s[12].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[12].TrueFalse_VariantC_choose = createQusetion_UserControl1s[12].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[12].TrueFalse_VariantD_choose = createQusetion_UserControl1s[12].TrueFalseguna2ComboBox4.Text;
                





                createQusetion_UserControl1s[13].Question = createQusetion_UserControl1s[13].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[13].VariantA_choose = createQusetion_UserControl1s[13].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[13].VariantB_choose = createQusetion_UserControl1s[13].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[13].VariantC_choose = createQusetion_UserControl1s[13].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[13].VariantD_choose = createQusetion_UserControl1s[13].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[13].TrueFalse_VariantA_choose = createQusetion_UserControl1s[13].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[13].TrueFalse_VariantB_choose = createQusetion_UserControl1s[13].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[13].TrueFalse_VariantC_choose = createQusetion_UserControl1s[13].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[13].TrueFalse_VariantD_choose = createQusetion_UserControl1s[13].TrueFalseguna2ComboBox4.Text;







                createQusetion_UserControl1s[14].Question = createQusetion_UserControl1s[14].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[14].VariantA_choose = createQusetion_UserControl1s[14].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[14].VariantB_choose = createQusetion_UserControl1s[14].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[14].VariantC_choose = createQusetion_UserControl1s[14].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[14].VariantD_choose = createQusetion_UserControl1s[14].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[14].TrueFalse_VariantA_choose = createQusetion_UserControl1s[14].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[14].TrueFalse_VariantB_choose = createQusetion_UserControl1s[14].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[14].TrueFalse_VariantC_choose = createQusetion_UserControl1s[14].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[14].TrueFalse_VariantD_choose = createQusetion_UserControl1s[14].TrueFalseguna2ComboBox4.Text;







                createQusetion_UserControl1s[15].Question = createQusetion_UserControl1s[6].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[15].VariantA_choose = createQusetion_UserControl1s[6].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[15].VariantB_choose = createQusetion_UserControl1s[6].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[15].VariantC_choose = createQusetion_UserControl1s[6].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[15].VariantD_choose = createQusetion_UserControl1s[6].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[15].TrueFalse_VariantA_choose = createQusetion_UserControl1s[15].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[15].TrueFalse_VariantB_choose = createQusetion_UserControl1s[15].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[15].TrueFalse_VariantC_choose = createQusetion_UserControl1s[15].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[15].TrueFalse_VariantD_choose = createQusetion_UserControl1s[15].TrueFalseguna2ComboBox4.Text;







                createQusetion_UserControl1s[16].Question = createQusetion_UserControl1s[16].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[16].VariantA_choose = createQusetion_UserControl1s[16].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[16].VariantB_choose = createQusetion_UserControl1s[16].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[16].VariantC_choose = createQusetion_UserControl1s[16].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[16].VariantD_choose = createQusetion_UserControl1s[16].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[16].TrueFalse_VariantA_choose = createQusetion_UserControl1s[16].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[16].TrueFalse_VariantB_choose = createQusetion_UserControl1s[16].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[16].TrueFalse_VariantC_choose = createQusetion_UserControl1s[16].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[16].TrueFalse_VariantD_choose = createQusetion_UserControl1s[16].TrueFalseguna2ComboBox4.Text;

    





                createQusetion_UserControl1s[17].Question = createQusetion_UserControl1s[17].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[17].VariantA_choose = createQusetion_UserControl1s[17].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[17].VariantB_choose = createQusetion_UserControl1s[17].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[17].VariantC_choose = createQusetion_UserControl1s[17].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[17].VariantD_choose = createQusetion_UserControl1s[17].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[17].TrueFalse_VariantA_choose = createQusetion_UserControl1s[17].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[17].TrueFalse_VariantB_choose = createQusetion_UserControl1s[17].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[17].TrueFalse_VariantC_choose = createQusetion_UserControl1s[17].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[17].TrueFalse_VariantD_choose = createQusetion_UserControl1s[17].TrueFalseguna2ComboBox4.Text;







                createQusetion_UserControl1s[18].Question = createQusetion_UserControl1s[18].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[18].VariantA_choose = createQusetion_UserControl1s[18].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[18].VariantB_choose = createQusetion_UserControl1s[18].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[18].VariantC_choose = createQusetion_UserControl1s[18].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[18].VariantD_choose = createQusetion_UserControl1s[18].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[18].TrueFalse_VariantA_choose = createQusetion_UserControl1s[18].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[18].TrueFalse_VariantB_choose = createQusetion_UserControl1s[18].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[18].TrueFalse_VariantC_choose = createQusetion_UserControl1s[18].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[18].TrueFalse_VariantD_choose = createQusetion_UserControl1s[18].TrueFalseguna2ComboBox4.Text;







                createQusetion_UserControl1s[19].Question = createQusetion_UserControl1s[19].QuestionrichTextBox1.Text;

                createQusetion_UserControl1s[19].VariantA_choose = createQusetion_UserControl1s[19].Variant_ArichTextBox.Text;

                createQusetion_UserControl1s[19].VariantB_choose = createQusetion_UserControl1s[19].Variant_BrichTextBox.Text;

                createQusetion_UserControl1s[19].VariantC_choose = createQusetion_UserControl1s[19].Variant_CrichTextBox.Text;

                createQusetion_UserControl1s[19].VariantD_choose = createQusetion_UserControl1s[19].Variant_DrichTextBox.Text;

                createQusetion_UserControl1s[19].TrueFalse_VariantA_choose = createQusetion_UserControl1s[19].TrueFalseguna2ComboBox1.Text;

                createQusetion_UserControl1s[19].TrueFalse_VariantB_choose = createQusetion_UserControl1s[19].TrueFalseguna2ComboBox2.Text;

                createQusetion_UserControl1s[19].TrueFalse_VariantC_choose = createQusetion_UserControl1s[19].TrueFalseguna2ComboBox3.Text;

                createQusetion_UserControl1s[19].TrueFalse_VariantD_choose = createQusetion_UserControl1s[19].TrueFalseguna2ComboBox4.Text;








                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[0]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[1]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[2]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[3]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[4]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[5]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[6]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[7]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[8]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[9]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[10]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[11]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[12]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[13]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[14]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[15]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[16]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[17]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[18]);
                flowLayoutPanel1.Controls.Add(createQusetion_UserControl1s[19]);










                createQusetion_UserControl1s[0].OK1pictureBox1.Hide();
              

                createQusetion_UserControl1s[0].X1pictureBox1.Hide();

                createQusetion_UserControl1s[0].pictureBox1.Hide();
                createQusetion_UserControl1s[0].pictureBox2.Hide();
                createQusetion_UserControl1s[0].pictureBox3.Hide();
                createQusetion_UserControl1s[0].pictureBox4.Hide();



                createQusetion_UserControl1s[1].OK1pictureBox1.Hide();
               

                createQusetion_UserControl1s[1].X1pictureBox1.Hide();

                createQusetion_UserControl1s[1].pictureBox1.Hide();
                createQusetion_UserControl1s[1].pictureBox2.Hide();
                createQusetion_UserControl1s[1].pictureBox3.Hide();
                createQusetion_UserControl1s[1].pictureBox4.Hide();



                createQusetion_UserControl1s[2].OK1pictureBox1.Hide();
              

                createQusetion_UserControl1s[2].X1pictureBox1.Hide();

                createQusetion_UserControl1s[2].pictureBox1.Hide();
                createQusetion_UserControl1s[2].pictureBox2.Hide();
                createQusetion_UserControl1s[2].pictureBox3.Hide();
                createQusetion_UserControl1s[2].pictureBox4.Hide();



                createQusetion_UserControl1s[3].OK1pictureBox1.Hide();
              

                createQusetion_UserControl1s[3].X1pictureBox1.Hide();


                createQusetion_UserControl1s[3].pictureBox1.Hide();
                createQusetion_UserControl1s[3].pictureBox2.Hide();
                createQusetion_UserControl1s[3].pictureBox3.Hide();
                createQusetion_UserControl1s[3].pictureBox4.Hide();





                createQusetion_UserControl1s[4].OK1pictureBox1.Hide();
              

                createQusetion_UserControl1s[4].X1pictureBox1.Hide();

                createQusetion_UserControl1s[4].pictureBox1.Hide();
                createQusetion_UserControl1s[4].pictureBox2.Hide();
                createQusetion_UserControl1s[4].pictureBox3.Hide();
                createQusetion_UserControl1s[4].pictureBox4.Hide();



                createQusetion_UserControl1s[5].OK1pictureBox1.Hide();
              

                createQusetion_UserControl1s[5].X1pictureBox1.Hide();


                createQusetion_UserControl1s[5].pictureBox1.Hide();
                createQusetion_UserControl1s[5].pictureBox2.Hide();
                createQusetion_UserControl1s[5].pictureBox3.Hide();
                createQusetion_UserControl1s[5].pictureBox4.Hide();



                createQusetion_UserControl1s[6].OK1pictureBox1.Hide();
                

                createQusetion_UserControl1s[6].X1pictureBox1.Hide();


                createQusetion_UserControl1s[6].pictureBox1.Hide();
                createQusetion_UserControl1s[6].pictureBox2.Hide();
                createQusetion_UserControl1s[6].pictureBox3.Hide();
                createQusetion_UserControl1s[6].pictureBox4.Hide();



                createQusetion_UserControl1s[7].OK1pictureBox1.Hide();
              
               

                createQusetion_UserControl1s[7].X1pictureBox1.Hide();


                createQusetion_UserControl1s[7].pictureBox1.Hide();
                createQusetion_UserControl1s[7].pictureBox2.Hide();
                createQusetion_UserControl1s[7].pictureBox3.Hide();
                createQusetion_UserControl1s[7].pictureBox4.Hide();



                createQusetion_UserControl1s[8].OK1pictureBox1.Hide();
                

                createQusetion_UserControl1s[8].X1pictureBox1.Hide();



                createQusetion_UserControl1s[8].pictureBox1.Hide();
                createQusetion_UserControl1s[8].pictureBox2.Hide();
                createQusetion_UserControl1s[8].pictureBox3.Hide();
                createQusetion_UserControl1s[8].pictureBox4.Hide();


                createQusetion_UserControl1s[9].OK1pictureBox1.Hide();
              

                createQusetion_UserControl1s[9].X1pictureBox1.Hide();

                createQusetion_UserControl1s[9].pictureBox1.Hide();
                createQusetion_UserControl1s[9].pictureBox2.Hide();
                createQusetion_UserControl1s[9].pictureBox3.Hide();
                createQusetion_UserControl1s[9].pictureBox4.Hide();


                createQusetion_UserControl1s[10].OK1pictureBox1.Hide();
               

                createQusetion_UserControl1s[10].X1pictureBox1.Hide();

                createQusetion_UserControl1s[10].pictureBox1.Hide();
                createQusetion_UserControl1s[10].pictureBox2.Hide();
                createQusetion_UserControl1s[10].pictureBox3.Hide();
                createQusetion_UserControl1s[10].pictureBox4.Hide();




                createQusetion_UserControl1s[11].OK1pictureBox1.Hide();
                

                createQusetion_UserControl1s[11].X1pictureBox1.Hide();


                createQusetion_UserControl1s[11].pictureBox1.Hide();
                createQusetion_UserControl1s[11].pictureBox2.Hide();
                createQusetion_UserControl1s[11].pictureBox3.Hide();
                createQusetion_UserControl1s[11].pictureBox4.Hide();



                createQusetion_UserControl1s[12].OK1pictureBox1.Hide();
                

                createQusetion_UserControl1s[12].X1pictureBox1.Hide();


                createQusetion_UserControl1s[12].pictureBox1.Hide();
                createQusetion_UserControl1s[12].pictureBox2.Hide();
                createQusetion_UserControl1s[12].pictureBox3.Hide();
                createQusetion_UserControl1s[12].pictureBox4.Hide();



                createQusetion_UserControl1s[13].OK1pictureBox1.Hide();
              

                createQusetion_UserControl1s[13].X1pictureBox1.Hide();

                createQusetion_UserControl1s[13].pictureBox1.Hide();
                createQusetion_UserControl1s[13].pictureBox2.Hide();
                createQusetion_UserControl1s[13].pictureBox3.Hide();
                createQusetion_UserControl1s[13].pictureBox4.Hide();



                createQusetion_UserControl1s[14].OK1pictureBox1.Hide();
                

                createQusetion_UserControl1s[14].X1pictureBox1.Hide();


                createQusetion_UserControl1s[14].pictureBox1.Hide();
                createQusetion_UserControl1s[14].pictureBox2.Hide();
                createQusetion_UserControl1s[14].pictureBox3.Hide();
                createQusetion_UserControl1s[14].pictureBox4.Hide();



                createQusetion_UserControl1s[15].OK1pictureBox1.Hide();
               
                createQusetion_UserControl1s[15].X1pictureBox1.Hide();

                createQusetion_UserControl1s[15].pictureBox1.Hide();
                createQusetion_UserControl1s[15].pictureBox2.Hide();
                createQusetion_UserControl1s[15].pictureBox3.Hide();
                createQusetion_UserControl1s[15].pictureBox4.Hide();




                createQusetion_UserControl1s[16].OK1pictureBox1.Hide();
              

                createQusetion_UserControl1s[16].X1pictureBox1.Hide();


                createQusetion_UserControl1s[16].pictureBox1.Hide();
                createQusetion_UserControl1s[16].pictureBox2.Hide();
                createQusetion_UserControl1s[16].pictureBox3.Hide();
                createQusetion_UserControl1s[16].pictureBox4.Hide();




                createQusetion_UserControl1s[17].OK1pictureBox1.Hide();
           

                createQusetion_UserControl1s[17].X1pictureBox1.Hide();


                createQusetion_UserControl1s[17].pictureBox1.Hide();
                createQusetion_UserControl1s[17].pictureBox2.Hide();
                createQusetion_UserControl1s[17].pictureBox3.Hide();
                createQusetion_UserControl1s[17].pictureBox4.Hide();




                createQusetion_UserControl1s[18].OK1pictureBox1.Hide();
                

                createQusetion_UserControl1s[18].X1pictureBox1.Hide();


                createQusetion_UserControl1s[18].pictureBox1.Hide();
                createQusetion_UserControl1s[18].pictureBox2.Hide();
                createQusetion_UserControl1s[18].pictureBox3.Hide();
                createQusetion_UserControl1s[18].pictureBox4.Hide();






                createQusetion_UserControl1s[19].OK1pictureBox1.Hide();
              

                createQusetion_UserControl1s[19].X1pictureBox1.Hide();



                createQusetion_UserControl1s[19].pictureBox1.Hide();
                createQusetion_UserControl1s[19].pictureBox2.Hide();
                createQusetion_UserControl1s[19].pictureBox3.Hide();
                createQusetion_UserControl1s[19].pictureBox4.Hide();






                createQusetion_UserControl1s[0].QuestionrichTextBox1.Text = xmlNodeList[0].InnerText.ToString();

                createQusetion_UserControl1s[0].Variant_ArichTextBox.Text = xmlNodeList[1].InnerText.ToString();
                createQusetion_UserControl1s[0].Variant_BrichTextBox.Text = xmlNodeList[2].InnerText.ToString();
                createQusetion_UserControl1s[0].Variant_CrichTextBox.Text = xmlNodeList[3].InnerText.ToString();
                createQusetion_UserControl1s[0].Variant_DrichTextBox.Text = xmlNodeList[4].InnerText.ToString();

                createQusetion_UserControl1s[0].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[0].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[0].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[0].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[0].Variant_DrichTextBox.ReadOnly = true;







                createQusetion_UserControl1s[1].QuestionrichTextBox1.Text = xmlNodeList[5].InnerText.ToString();

                createQusetion_UserControl1s[1].Variant_ArichTextBox.Text = xmlNodeList[6].InnerText.ToString();
                createQusetion_UserControl1s[1].Variant_BrichTextBox.Text = xmlNodeList[7].InnerText.ToString();
                createQusetion_UserControl1s[1].Variant_CrichTextBox.Text = xmlNodeList[8].InnerText.ToString();
                createQusetion_UserControl1s[1].Variant_DrichTextBox.Text = xmlNodeList[9].InnerText.ToString();


                createQusetion_UserControl1s[1].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[1].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[1].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[1].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[1].Variant_DrichTextBox.ReadOnly = true;



                createQusetion_UserControl1s[2].QuestionrichTextBox1.Text = xmlNodeList[10].InnerText.ToString();

                createQusetion_UserControl1s[2].Variant_ArichTextBox.Text = xmlNodeList[11].InnerText.ToString();
                createQusetion_UserControl1s[2].Variant_BrichTextBox.Text = xmlNodeList[12].InnerText.ToString();
                createQusetion_UserControl1s[2].Variant_CrichTextBox.Text = xmlNodeList[13].InnerText.ToString();
                createQusetion_UserControl1s[2].Variant_DrichTextBox.Text = xmlNodeList[14].InnerText.ToString();

               
                createQusetion_UserControl1s[2].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[2].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[2].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[2].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[2].Variant_DrichTextBox.ReadOnly = true;




                createQusetion_UserControl1s[3].QuestionrichTextBox1.Text = xmlNodeList[15].InnerText.ToString();

                createQusetion_UserControl1s[3].Variant_ArichTextBox.Text = xmlNodeList[16].InnerText.ToString();
                createQusetion_UserControl1s[3].Variant_BrichTextBox.Text = xmlNodeList[17].InnerText.ToString();
                createQusetion_UserControl1s[3].Variant_CrichTextBox.Text = xmlNodeList[18].InnerText.ToString();
                createQusetion_UserControl1s[3].Variant_DrichTextBox.Text = xmlNodeList[19].InnerText.ToString();

              
                createQusetion_UserControl1s[3].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[3].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[3].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[3].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[3].Variant_DrichTextBox.ReadOnly = true;




                createQusetion_UserControl1s[4].QuestionrichTextBox1.Text = xmlNodeList[20].InnerText.ToString();

                createQusetion_UserControl1s[4].Variant_ArichTextBox.Text = xmlNodeList[21].InnerText.ToString();
                createQusetion_UserControl1s[4].Variant_BrichTextBox.Text = xmlNodeList[22].InnerText.ToString();
                createQusetion_UserControl1s[4].Variant_CrichTextBox.Text = xmlNodeList[23].InnerText.ToString();
                createQusetion_UserControl1s[4].Variant_DrichTextBox.Text = xmlNodeList[24].InnerText.ToString();


                createQusetion_UserControl1s[4].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[4].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[4].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[4].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[4].Variant_DrichTextBox.ReadOnly = true;




                createQusetion_UserControl1s[5].QuestionrichTextBox1.Text = xmlNodeList[25].InnerText.ToString();

                createQusetion_UserControl1s[5].Variant_ArichTextBox.Text = xmlNodeList[26].InnerText.ToString();
                createQusetion_UserControl1s[5].Variant_BrichTextBox.Text = xmlNodeList[27].InnerText.ToString();
                createQusetion_UserControl1s[5].Variant_CrichTextBox.Text = xmlNodeList[28].InnerText.ToString();
                createQusetion_UserControl1s[5].Variant_DrichTextBox.Text = xmlNodeList[29].InnerText.ToString();


                createQusetion_UserControl1s[5].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[5].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[5].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[5].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[5].Variant_DrichTextBox.ReadOnly = true;




                createQusetion_UserControl1s[6].QuestionrichTextBox1.Text = xmlNodeList[30].InnerText.ToString();

                createQusetion_UserControl1s[6].Variant_ArichTextBox.Text = xmlNodeList[31].InnerText.ToString();
                createQusetion_UserControl1s[6].Variant_BrichTextBox.Text = xmlNodeList[32].InnerText.ToString();
                createQusetion_UserControl1s[6].Variant_CrichTextBox.Text = xmlNodeList[33].InnerText.ToString();
                createQusetion_UserControl1s[6].Variant_DrichTextBox.Text = xmlNodeList[34].InnerText.ToString();


                createQusetion_UserControl1s[6].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[6].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[6].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[6].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[6].Variant_DrichTextBox.ReadOnly = true;




                createQusetion_UserControl1s[7].QuestionrichTextBox1.Text = xmlNodeList[35].InnerText.ToString();

                createQusetion_UserControl1s[7].Variant_ArichTextBox.Text = xmlNodeList[36].InnerText.ToString();
                createQusetion_UserControl1s[7].Variant_BrichTextBox.Text = xmlNodeList[37].InnerText.ToString();
                createQusetion_UserControl1s[7].Variant_CrichTextBox.Text = xmlNodeList[38].InnerText.ToString();
                createQusetion_UserControl1s[7].Variant_DrichTextBox.Text = xmlNodeList[39].InnerText.ToString();


                createQusetion_UserControl1s[7].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[7].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[7].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[7].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[7].Variant_DrichTextBox.ReadOnly = true;




                createQusetion_UserControl1s[8].QuestionrichTextBox1.Text = xmlNodeList[40].InnerText.ToString();

                createQusetion_UserControl1s[8].Variant_ArichTextBox.Text = xmlNodeList[41].InnerText.ToString();
                createQusetion_UserControl1s[8].Variant_BrichTextBox.Text = xmlNodeList[42].InnerText.ToString();
                createQusetion_UserControl1s[8].Variant_CrichTextBox.Text = xmlNodeList[43].InnerText.ToString();
                createQusetion_UserControl1s[8].Variant_DrichTextBox.Text = xmlNodeList[44].InnerText.ToString();


                createQusetion_UserControl1s[8].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[8].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[8].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[8].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[8].Variant_DrichTextBox.ReadOnly = true;




                createQusetion_UserControl1s[9].QuestionrichTextBox1.Text = xmlNodeList[45].InnerText.ToString();

                createQusetion_UserControl1s[9].Variant_ArichTextBox.Text = xmlNodeList[46].InnerText.ToString();
                createQusetion_UserControl1s[9].Variant_BrichTextBox.Text = xmlNodeList[47].InnerText.ToString();
                createQusetion_UserControl1s[9].Variant_CrichTextBox.Text = xmlNodeList[48].InnerText.ToString();
                createQusetion_UserControl1s[9].Variant_DrichTextBox.Text = xmlNodeList[49].InnerText.ToString();


                createQusetion_UserControl1s[9].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[9].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[9].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[9].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[9].Variant_DrichTextBox.ReadOnly = true;




                createQusetion_UserControl1s[10].QuestionrichTextBox1.Text = xmlNodeList[50].InnerText.ToString();

                createQusetion_UserControl1s[10].Variant_ArichTextBox.Text = xmlNodeList[51].InnerText.ToString();
                createQusetion_UserControl1s[10].Variant_BrichTextBox.Text = xmlNodeList[52].InnerText.ToString();
                createQusetion_UserControl1s[10].Variant_CrichTextBox.Text = xmlNodeList[53].InnerText.ToString();
                createQusetion_UserControl1s[10].Variant_DrichTextBox.Text = xmlNodeList[54].InnerText.ToString();


                createQusetion_UserControl1s[10].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[10].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[10].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[10].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[10].Variant_DrichTextBox.ReadOnly = true;





                createQusetion_UserControl1s[11].QuestionrichTextBox1.Text = xmlNodeList[55].InnerText.ToString();

                createQusetion_UserControl1s[11].Variant_ArichTextBox.Text = xmlNodeList[56].InnerText.ToString();
                createQusetion_UserControl1s[11].Variant_BrichTextBox.Text = xmlNodeList[57].InnerText.ToString();
                createQusetion_UserControl1s[11].Variant_CrichTextBox.Text = xmlNodeList[58].InnerText.ToString();
                createQusetion_UserControl1s[11].Variant_DrichTextBox.Text = xmlNodeList[59].InnerText.ToString();

                createQusetion_UserControl1s[11].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[11].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[11].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[11].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[11].Variant_DrichTextBox.ReadOnly = true;





                createQusetion_UserControl1s[12].QuestionrichTextBox1.Text = xmlNodeList[60].InnerText.ToString();

                createQusetion_UserControl1s[12].Variant_ArichTextBox.Text = xmlNodeList[61].InnerText.ToString();
                createQusetion_UserControl1s[12].Variant_BrichTextBox.Text = xmlNodeList[62].InnerText.ToString();
                createQusetion_UserControl1s[12].Variant_CrichTextBox.Text = xmlNodeList[63].InnerText.ToString();
                createQusetion_UserControl1s[12].Variant_DrichTextBox.Text = xmlNodeList[64].InnerText.ToString();

               
                createQusetion_UserControl1s[12].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[12].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[12].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[12].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[12].Variant_DrichTextBox.ReadOnly = true;










                createQusetion_UserControl1s[13].QuestionrichTextBox1.Text = xmlNodeList[65].InnerText.ToString();

                createQusetion_UserControl1s[13].Variant_ArichTextBox.Text = xmlNodeList[66].InnerText.ToString();
                createQusetion_UserControl1s[13].Variant_BrichTextBox.Text = xmlNodeList[67].InnerText.ToString();
                createQusetion_UserControl1s[13].Variant_CrichTextBox.Text = xmlNodeList[68].InnerText.ToString();
                createQusetion_UserControl1s[13].Variant_DrichTextBox.Text = xmlNodeList[69].InnerText.ToString();


                createQusetion_UserControl1s[13].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[13].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[13].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[13].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[13].Variant_DrichTextBox.ReadOnly = true;










                createQusetion_UserControl1s[14].QuestionrichTextBox1.Text = xmlNodeList[70].InnerText.ToString();

                createQusetion_UserControl1s[14].Variant_ArichTextBox.Text = xmlNodeList[71].InnerText.ToString();
                createQusetion_UserControl1s[14].Variant_BrichTextBox.Text = xmlNodeList[72].InnerText.ToString();
                createQusetion_UserControl1s[14].Variant_CrichTextBox.Text = xmlNodeList[73].InnerText.ToString();
                createQusetion_UserControl1s[14].Variant_DrichTextBox.Text = xmlNodeList[74].InnerText.ToString();

               
                createQusetion_UserControl1s[14].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[14].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[14].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[14].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[14].Variant_DrichTextBox.ReadOnly = true;










                createQusetion_UserControl1s[15].QuestionrichTextBox1.Text = xmlNodeList[75].InnerText.ToString();

                createQusetion_UserControl1s[15].Variant_ArichTextBox.Text = xmlNodeList[76].InnerText.ToString();
                createQusetion_UserControl1s[15].Variant_BrichTextBox.Text = xmlNodeList[77].InnerText.ToString();
                createQusetion_UserControl1s[15].Variant_CrichTextBox.Text = xmlNodeList[78].InnerText.ToString();
                createQusetion_UserControl1s[15].Variant_DrichTextBox.Text = xmlNodeList[79].InnerText.ToString();


                createQusetion_UserControl1s[15].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[15].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[15].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[15].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[15].Variant_DrichTextBox.ReadOnly = true;










                createQusetion_UserControl1s[16].QuestionrichTextBox1.Text = xmlNodeList[80].InnerText.ToString();

                createQusetion_UserControl1s[16].Variant_ArichTextBox.Text = xmlNodeList[81].InnerText.ToString();
                createQusetion_UserControl1s[16].Variant_BrichTextBox.Text = xmlNodeList[82].InnerText.ToString();
                createQusetion_UserControl1s[16].Variant_CrichTextBox.Text = xmlNodeList[83].InnerText.ToString();
                createQusetion_UserControl1s[16].Variant_DrichTextBox.Text = xmlNodeList[84].InnerText.ToString();


                createQusetion_UserControl1s[16].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[16].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[16].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[16].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[16].Variant_DrichTextBox.ReadOnly = true;










                createQusetion_UserControl1s[17].QuestionrichTextBox1.Text = xmlNodeList[85].InnerText.ToString();

                createQusetion_UserControl1s[17].Variant_ArichTextBox.Text = xmlNodeList[86].InnerText.ToString();
                createQusetion_UserControl1s[17].Variant_BrichTextBox.Text = xmlNodeList[87].InnerText.ToString();
                createQusetion_UserControl1s[17].Variant_CrichTextBox.Text = xmlNodeList[88].InnerText.ToString();
                createQusetion_UserControl1s[17].Variant_DrichTextBox.Text = xmlNodeList[89].InnerText.ToString();


                createQusetion_UserControl1s[17].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[17].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[17].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[17].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[17].Variant_DrichTextBox.ReadOnly = true;










                createQusetion_UserControl1s[18].QuestionrichTextBox1.Text = xmlNodeList[90].InnerText.ToString();

                createQusetion_UserControl1s[18].Variant_ArichTextBox.Text = xmlNodeList[91].InnerText.ToString();
                createQusetion_UserControl1s[18].Variant_BrichTextBox.Text = xmlNodeList[92].InnerText.ToString();
                createQusetion_UserControl1s[18].Variant_CrichTextBox.Text = xmlNodeList[93].InnerText.ToString();
                createQusetion_UserControl1s[18].Variant_DrichTextBox.Text = xmlNodeList[94].InnerText.ToString();


                createQusetion_UserControl1s[18].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[18].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[18].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[18].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[18].Variant_DrichTextBox.ReadOnly = true;










                createQusetion_UserControl1s[19].QuestionrichTextBox1.Text = xmlNodeList[95].InnerText.ToString();

                createQusetion_UserControl1s[19].Variant_ArichTextBox.Text = xmlNodeList[96].InnerText.ToString();
                createQusetion_UserControl1s[19].Variant_BrichTextBox.Text = xmlNodeList[97].InnerText.ToString();
                createQusetion_UserControl1s[19].Variant_CrichTextBox.Text = xmlNodeList[98].InnerText.ToString();
                createQusetion_UserControl1s[19].Variant_DrichTextBox.Text = xmlNodeList[99].InnerText.ToString();


                createQusetion_UserControl1s[19].QuestionrichTextBox1.ReadOnly = true;

                createQusetion_UserControl1s[19].Variant_ArichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[19].Variant_BrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[19].Variant_CrichTextBox.ReadOnly = true;
                createQusetion_UserControl1s[19].Variant_DrichTextBox.ReadOnly = true;




                if (createQusetion_UserControl1s[0].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[0].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[0].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[0].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[0].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[0].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[0].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[0].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[0].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[0].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[0].TrueFalseguna2ComboBox4.Enabled = false;
                }


                if (createQusetion_UserControl1s[1].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[1].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[1].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[1].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[1].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[1].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[1].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[1].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[1].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[1].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[1].TrueFalseguna2ComboBox4.Enabled = false;
                }


                if (createQusetion_UserControl1s[2].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[2].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[2].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[2].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[2].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[2].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[2].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[2].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[2].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[2].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[2].TrueFalseguna2ComboBox4.Enabled = false;
                }


                if (createQusetion_UserControl1s[3].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[3].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[3].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[3].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[3].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[3].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[3].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[3].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[3].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[3].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[3].TrueFalseguna2ComboBox4.Enabled = false;
                }



                if (createQusetion_UserControl1s[4].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[4].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[4].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[4].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[4].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[4].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[4].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[4].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[4].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[4].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[4].TrueFalseguna2ComboBox4.Enabled = false;
                }



                if (createQusetion_UserControl1s[5].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[5].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[5].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[5].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[5].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[5].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[5].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[5].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[5].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[5].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[5].TrueFalseguna2ComboBox4.Enabled = false;
                }



                if (createQusetion_UserControl1s[6].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[6].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[6].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[6].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[6].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[6].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[6].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[6].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[6].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[6].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[6].TrueFalseguna2ComboBox4.Enabled = false;
                }



                if (createQusetion_UserControl1s[7].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[7].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[7].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[7].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[7].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[7].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[7].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[7].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[7].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[7].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[7].TrueFalseguna2ComboBox4.Enabled = false;
                }



                if (createQusetion_UserControl1s[8].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[8].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[8].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[8].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[8].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[8].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[8].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[8].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[8].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[8].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[8].TrueFalseguna2ComboBox4.Enabled = false;
                }


                if (createQusetion_UserControl1s[9].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[9].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[9].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[9].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[9].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[9].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[9].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[9].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[9].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[9].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[9].TrueFalseguna2ComboBox4.Enabled = false;
                }



                if (createQusetion_UserControl1s[10].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[10].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[10].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[10].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[10].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[10].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[10].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[10].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[10].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[10].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[10].TrueFalseguna2ComboBox4.Enabled = false;
                }




                if (createQusetion_UserControl1s[11].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[11].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[11].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[11].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[11].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[11].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[11].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[11].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[11].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[11].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[11].TrueFalseguna2ComboBox4.Enabled = false;
                }


                if (createQusetion_UserControl1s[12].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[12].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[12].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[12].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[12].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[12].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[12].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[12].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[12].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[12].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[12].TrueFalseguna2ComboBox4.Enabled = false;
                }


                if (createQusetion_UserControl1s[13].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[13].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[13].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[13].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[13].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[13].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[13].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[13].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[13].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[13].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[13].TrueFalseguna2ComboBox4.Enabled = false;
                }



                if (createQusetion_UserControl1s[14].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[14].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[14].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[14].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[14].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[14].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[14].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[14].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[14].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[14].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[14].TrueFalseguna2ComboBox4.Enabled = false;
                }



                if (createQusetion_UserControl1s[15].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[15].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[15].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[15].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[15].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[15].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[15].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[15].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[15].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[15].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[15].TrueFalseguna2ComboBox4.Enabled = false;
                }



                if (createQusetion_UserControl1s[16].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[16].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[16].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[16].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[16].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[16].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[16].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[16].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[16].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[16].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[16].TrueFalseguna2ComboBox4.Enabled = false;
                }



                if (createQusetion_UserControl1s[17].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[17].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[17].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[17].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[17].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[17].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[17].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[17].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[17].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[17].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[17].TrueFalseguna2ComboBox4.Enabled = false;
                }



                if (createQusetion_UserControl1s[18].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[18].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[18].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[18].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[18].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[18].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[18].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[18].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[18].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[18].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[18].TrueFalseguna2ComboBox4.Enabled = false;
                }


                if (createQusetion_UserControl1s[19].QuestionrichTextBox1.Text == "" || String.IsNullOrEmpty(createQusetion_UserControl1s[19].QuestionrichTextBox1.Text))
                {
                    createQusetion_UserControl1s[19].QuestionrichTextBox1.Enabled = false;

                    createQusetion_UserControl1s[19].Variant_ArichTextBox.Enabled = false;
                    createQusetion_UserControl1s[19].Variant_BrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[19].Variant_CrichTextBox.Enabled = false;
                    createQusetion_UserControl1s[19].Variant_DrichTextBox.Enabled = false;

                    createQusetion_UserControl1s[19].TrueFalseguna2ComboBox1.Enabled = false;
                    createQusetion_UserControl1s[19].TrueFalseguna2ComboBox2.Enabled = false;
                    createQusetion_UserControl1s[19].TrueFalseguna2ComboBox3.Enabled = false;
                    createQusetion_UserControl1s[19].TrueFalseguna2ComboBox4.Enabled = false;
                }





            }


        }





        private void Resultguna2Button1_Click(object sender, EventArgs e)
        {
            int plus = 0;
            int minus = 0;

            if (!File.Exists($@"Answer\SelectQuestion2.xml"))
            {
                File.Create($@"Answer\SelectQuestion2.xml").Close();

            }


            XmlNodeList xmlNodeList2 = doc2.GetElementsByTagName("Answer");


            xmlNodeList2[0].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[0].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[1].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[0].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[2].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[0].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[3].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[0].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[4].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[1].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[5].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[1].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[6].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[1].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[7].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[1].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[8].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[2].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[9].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[2].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[10].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[2].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[11].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[2].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[12].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[3].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[13].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[3].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[14].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[3].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[15].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[3].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[16].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[4].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[17].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[4].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[18].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[4].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[19].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[4].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[20].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[5].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[21].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[5].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[22].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[5].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[23].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[5].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[24].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[6].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[25].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[6].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[26].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[6].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[27].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[6].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[28].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[7].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[29].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[7].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[30].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[7].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[31].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[7].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[32].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[8].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[33].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[8].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[34].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[8].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[35].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[8].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[36].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[9].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[37].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[9].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[38].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[9].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[39].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[9].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[40].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[10].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[41].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[10].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[42].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[10].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[43].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[10].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[44].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[11].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[45].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[11].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[46].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[11].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[47].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[11].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[48].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[12].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[49].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[12].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[50].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[12].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[51].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[12].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[52].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[13].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[53].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[13].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[54].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[13].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[55].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[13].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[56].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[14].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[57].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[14].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[58].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[14].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[59].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[14].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[60].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[15].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[61].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[15].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[62].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[15].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[63].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[15].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[64].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[16].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[65].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[16].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[66].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[16].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[67].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[16].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[68].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[17].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[69].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[17].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[70].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[17].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[71].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[17].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[72].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[18].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[73].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[18].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[74].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[18].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[75].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[18].TrueFalseguna2ComboBox4.Text;

            xmlNodeList2[76].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[19].TrueFalseguna2ComboBox1.Text;
            xmlNodeList2[77].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[19].TrueFalseguna2ComboBox2.Text;
            xmlNodeList2[78].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[19].TrueFalseguna2ComboBox3.Text;
            xmlNodeList2[79].Attributes["IsCorrect"].Value = createQusetion_UserControl1s[19].TrueFalseguna2ComboBox4.Text;

            doc2.Save($@"Answer\SelectQuestion2.xml");












            XmlDocument doc4 = new XmlDocument();
            doc4.Load($@"Answer\SelectQuestion.xml");

            XmlNodeList xmlNodeList3 = doc4.GetElementsByTagName("Answer");


            if (createQusetion_UserControl1s[0].TrueFalseguna2ComboBox1.Text != "")
            {

                if (xmlNodeList2[0].Attributes["IsCorrect"].Value == xmlNodeList3[0].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[0].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[0].X1pictureBox1.Hide();

                    plus += 1;



                }

                if (xmlNodeList2[0].Attributes["IsCorrect"].Value != xmlNodeList3[0].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[0].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[0].X1pictureBox1.Show();

                    if (xmlNodeList3[0].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox1.Show();

                    }


                    if (xmlNodeList3[1].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox2.Show();

                    }


                    if (xmlNodeList3[2].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox3.Show();

                    }

                    if (xmlNodeList3[3].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[1].Attributes["IsCorrect"].Value == xmlNodeList3[1].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[0].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[0].X1pictureBox1.Hide();

                    plus += 1;


                }


                if (xmlNodeList2[1].Attributes["IsCorrect"].Value != xmlNodeList3[1].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[0].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[0].X1pictureBox1.Show();




                    if (xmlNodeList3[0].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox1.Show();

                    }


                    if (xmlNodeList3[1].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox2.Show();

                    }


                    if (xmlNodeList3[2].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox3.Show();

                    }

                    if (xmlNodeList3[3].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox4.Show();

                    }

                    minus += 1;
                }


                if (xmlNodeList2[2].Attributes["IsCorrect"].Value == xmlNodeList3[2].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[0].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[0].X1pictureBox1.Hide();

                    plus += 1;


                }


                if (xmlNodeList2[2].Attributes["IsCorrect"].Value != xmlNodeList3[2].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[0].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[0].X1pictureBox1.Show();


                    if (xmlNodeList3[0].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox1.Show();

                    }


                    if (xmlNodeList3[1].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox2.Show();

                    }


                    if (xmlNodeList3[2].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox3.Show();

                    }

                    if (xmlNodeList3[3].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox4.Show();

                    }

                    minus += 1;
                }


                if (xmlNodeList2[3].Attributes["IsCorrect"].Value == xmlNodeList3[3].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[0].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[0].X1pictureBox1.Hide();

                    plus += 1;

                }



                if (xmlNodeList2[3].Attributes["IsCorrect"].Value != xmlNodeList3[3].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[0].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[0].X1pictureBox1.Show();



                    if (xmlNodeList3[0].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox1.Show();

                    }


                    if (xmlNodeList3[1].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox2.Show();

                    }


                    if (xmlNodeList3[2].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox3.Show();

                    }

                    if (xmlNodeList3[3].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[0].pictureBox4.Show();

                    }
                    minus += 1;
                }

                plus = plus - 3;
                if (plus < 0)
                {
                    plus = 0;
                }
            }


            if (createQusetion_UserControl1s[1].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[4].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[4].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[1].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[1].X1pictureBox1.Hide();

                    plus += 1;

                }

                if (xmlNodeList2[4].Attributes["IsCorrect"].Value != xmlNodeList3[4].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[1].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[1].X1pictureBox1.Show();



                    if (xmlNodeList3[4].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox1.Show();

                    }


                    if (xmlNodeList3[5].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox2.Show();

                    }


                    if (xmlNodeList3[1].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox3.Show();

                    }

                    if (xmlNodeList3[7].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox4.Show();

                    }

                    minus += 1;
                }

                if (xmlNodeList2[5].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[5].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[1].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[1].X1pictureBox1.Hide();

                    plus += 1;

                }


                if (xmlNodeList2[5].Attributes["IsCorrect"].Value != xmlNodeList3[5].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[1].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[1].X1pictureBox1.Show();



                    if (xmlNodeList3[4].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox1.Show();

                    }


                    if (xmlNodeList3[5].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox2.Show();

                    }


                    if (xmlNodeList3[6].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox3.Show();

                    }

                    if (xmlNodeList3[7].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox4.Show();

                    }

                    minus += 1;
                }


                if (xmlNodeList2[6].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[6].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[1].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[1].X1pictureBox1.Hide();

                    plus += 1;


                }


                if (xmlNodeList2[6].Attributes["IsCorrect"].Value != xmlNodeList3[6].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[1].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[1].X1pictureBox1.Show();



                    if (xmlNodeList3[4].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox1.Show();

                    }


                    if (xmlNodeList3[5].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox2.Show();

                    }


                    if (xmlNodeList3[6].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox3.Show();

                    }

                    if (xmlNodeList3[7].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox4.Show();

                    }

                    minus += 1;
                }


                if (xmlNodeList2[7].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[7].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[1].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[1].X1pictureBox1.Hide();

                    plus += 1;


                }


                if (xmlNodeList2[7].Attributes["IsCorrect"].Value != xmlNodeList3[7].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[1].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[1].X1pictureBox1.Show();






                    if (xmlNodeList3[4].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox1.Show();

                    }


                    if (xmlNodeList3[5].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox2.Show();

                    }


                    if (xmlNodeList3[6].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox3.Show();

                    }

                    if (xmlNodeList3[7].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[1].pictureBox4.Show();

                    }

                    minus += 1;
                }

            }


            if (createQusetion_UserControl1s[2].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[8].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[8].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[2].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[2].X1pictureBox1.Hide();

                    plus += 1;



                }

                if (xmlNodeList2[8].Attributes["IsCorrect"].Value != xmlNodeList3[8].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[2].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[2].X1pictureBox1.Show();




                    if (xmlNodeList3[8].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox1.Show();

                    }


                    if (xmlNodeList3[9].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox2.Show();

                    }


                    if (xmlNodeList3[10].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox3.Show();

                    }

                    if (xmlNodeList3[11].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox4.Show();

                    }

                    minus += 1;
                }

                if (xmlNodeList2[9].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[9].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[2].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[2].X1pictureBox1.Hide();

                    plus += 1;


                }


                if (xmlNodeList2[9].Attributes["IsCorrect"].Value != xmlNodeList3[9].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[2].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[2].X1pictureBox1.Show();



                    if (xmlNodeList3[8].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox1.Show();

                    }


                    if (xmlNodeList3[9].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox2.Show();

                    }


                    if (xmlNodeList3[10].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox3.Show();

                    }

                    if (xmlNodeList3[11].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox4.Show();

                    }

                    minus += 1;
                }


                if (xmlNodeList2[10].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[10].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[2].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[2].X1pictureBox1.Hide();

                    plus += 1;



                }


                if (xmlNodeList2[10].Attributes["IsCorrect"].Value != xmlNodeList3[10].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[2].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[2].X1pictureBox1.Show();


                    if (xmlNodeList3[8].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox1.Show();

                    }


                    if (xmlNodeList3[9].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox2.Show();

                    }


                    if (xmlNodeList3[10].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox3.Show();

                    }

                    if (xmlNodeList3[11].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox4.Show();

                    }


                    minus += 1;

                }


                if (xmlNodeList2[11].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[11].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[2].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[2].X1pictureBox1.Hide();

                    plus += 1;


                }


                if (xmlNodeList2[11].Attributes["IsCorrect"].Value != xmlNodeList3[11].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[2].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[2].X1pictureBox1.Show();




                    if (xmlNodeList3[8].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox1.Show();

                    }


                    if (xmlNodeList3[9].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox2.Show();

                    }


                    if (xmlNodeList3[10].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox3.Show();

                    }

                    if (xmlNodeList3[11].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[2].pictureBox4.Show();

                    }

                    minus += 1;

                }

            }




            if (createQusetion_UserControl1s[3].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[12].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[12].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[3].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[3].X1pictureBox1.Hide();

                    plus += 1;


                }

                if (xmlNodeList2[12].Attributes["IsCorrect"].Value != xmlNodeList3[12].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[3].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[3].X1pictureBox1.Show();



                    if (xmlNodeList3[12].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox1.Show();

                    }


                    if (xmlNodeList3[13].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox2.Show();

                    }


                    if (xmlNodeList3[14].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox3.Show();

                    }

                    if (xmlNodeList3[15].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox4.Show();

                    }


                    minus += 1;

                }

                if (xmlNodeList2[13].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[13].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[3].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[3].X1pictureBox1.Hide();

                    plus += 1;

                }


                if (xmlNodeList2[13].Attributes["IsCorrect"].Value != xmlNodeList3[13].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[3].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[3].X1pictureBox1.Show();

                    if (xmlNodeList3[12].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox1.Show();

                    }


                    if (xmlNodeList3[13].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox2.Show();

                    }


                    if (xmlNodeList3[14].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox3.Show();

                    }

                    if (xmlNodeList3[15].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox4.Show();

                    }


                    minus += 1;

                }


                if (xmlNodeList2[14].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[14].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[3].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[3].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[14].Attributes["IsCorrect"].Value != xmlNodeList3[14].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[3].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[3].X1pictureBox1.Show();

                    if (xmlNodeList3[12].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox1.Show();

                    }


                    if (xmlNodeList3[13].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox2.Show();

                    }


                    if (xmlNodeList3[14].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox3.Show();

                    }

                    if (xmlNodeList3[15].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox4.Show();

                    }


                    minus += 1;

                }


                if (xmlNodeList2[15].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[15].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[3].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[3].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[15].Attributes["IsCorrect"].Value != xmlNodeList3[15].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[3].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[3].X1pictureBox1.Show();




                    if (xmlNodeList3[12].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox1.Show();

                    }


                    if (xmlNodeList3[13].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox2.Show();

                    }


                    if (xmlNodeList3[14].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox3.Show();

                    }

                    if (xmlNodeList3[15].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[3].pictureBox4.Show();

                    }


                    minus += 1;


                }

            }



            if (createQusetion_UserControl1s[4].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[16].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[16].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[4].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[4].X1pictureBox1.Hide();


                    plus += 1;



                }

                if (xmlNodeList2[16].Attributes["IsCorrect"].Value != xmlNodeList3[16].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[4].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[4].X1pictureBox1.Show();



                    if (xmlNodeList3[16].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox1.Show();

                    }


                    if (xmlNodeList3[17].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox2.Show();

                    }


                    if (xmlNodeList3[18].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox3.Show();

                    }

                    if (xmlNodeList3[19].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[17].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[17].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[4].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[4].X1pictureBox1.Hide();


                    plus += 1;


                }


                if (xmlNodeList2[17].Attributes["IsCorrect"].Value != xmlNodeList3[17].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[4].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[4].X1pictureBox1.Show();


                    if (xmlNodeList3[16].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox1.Show();

                    }


                    if (xmlNodeList3[17].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox2.Show();

                    }


                    if (xmlNodeList3[18].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox3.Show();

                    }

                    if (xmlNodeList3[19].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[18].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[18].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[4].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[4].X1pictureBox1.Hide();


                    plus += 1;


                }


                if (xmlNodeList2[18].Attributes["IsCorrect"].Value != xmlNodeList3[18].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[4].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[4].X1pictureBox1.Show();



                    if (xmlNodeList3[16].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox1.Show();

                    }


                    if (xmlNodeList3[17].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox2.Show();

                    }


                    if (xmlNodeList3[18].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox3.Show();

                    }

                    if (xmlNodeList3[19].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox4.Show();

                    }


                    minus += 1;

                }


                if (xmlNodeList2[19].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[19].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[4].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[4].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[19].Attributes["IsCorrect"].Value != xmlNodeList3[19].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[4].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[4].X1pictureBox1.Show();



                    if (xmlNodeList3[16].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox1.Show();

                    }


                    if (xmlNodeList3[17].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox2.Show();

                    }


                    if (xmlNodeList3[18].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox3.Show();

                    }

                    if (xmlNodeList3[19].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[4].pictureBox4.Show();

                    }

                    minus += 1;

                }

            }





            if (createQusetion_UserControl1s[5].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[20].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[20].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[5].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[5].X1pictureBox1.Hide();



                    plus += 1;



                }

                if (xmlNodeList2[20].Attributes["IsCorrect"].Value != xmlNodeList3[20].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[5].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[5].X1pictureBox1.Show();


                    if (xmlNodeList3[20].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox1.Show();

                    }


                    if (xmlNodeList3[21].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox2.Show();

                    }


                    if (xmlNodeList3[22].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox3.Show();

                    }

                    if (xmlNodeList3[23].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[21].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[21].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[5].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[5].X1pictureBox1.Hide();

                    plus += 1;


                }


                if (xmlNodeList2[21].Attributes["IsCorrect"].Value != xmlNodeList3[21].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[5].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[5].X1pictureBox1.Show();



                    if (xmlNodeList3[20].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox1.Show();

                    }


                    if (xmlNodeList3[21].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox2.Show();

                    }


                    if (xmlNodeList3[22].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox3.Show();

                    }

                    if (xmlNodeList3[23].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[22].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[22].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[5].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[5].X1pictureBox1.Hide();


                    plus += 1;




                }


                if (xmlNodeList2[22].Attributes["IsCorrect"].Value != xmlNodeList3[22].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[5].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[5].X1pictureBox1.Show();



                    if (xmlNodeList3[20].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox1.Show();

                    }


                    if (xmlNodeList3[21].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox2.Show();

                    }


                    if (xmlNodeList3[22].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox3.Show();

                    }

                    if (xmlNodeList3[23].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox4.Show();

                    }


                    minus += 1;

                }


                if (xmlNodeList2[23].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[23].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[5].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[5].X1pictureBox1.Hide();



                    plus += 1;


                }


                if (xmlNodeList2[23].Attributes["IsCorrect"].Value != xmlNodeList3[23].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[5].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[5].X1pictureBox1.Show();


                    if (xmlNodeList3[20].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox1.Show();

                    }


                    if (xmlNodeList3[21].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox2.Show();

                    }


                    if (xmlNodeList3[22].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox3.Show();

                    }

                    if (xmlNodeList3[23].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[5].pictureBox4.Show();

                    }

                    minus += 1;

                }

            }



            if (createQusetion_UserControl1s[6].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[24].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[24].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[6].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[6].X1pictureBox1.Hide();


                    plus += 1;




                }

                if (xmlNodeList2[24].Attributes["IsCorrect"].Value != xmlNodeList3[24].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[6].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[6].X1pictureBox1.Show();


                    if (xmlNodeList3[24].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox1.Show();

                    }


                    if (xmlNodeList3[25].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox2.Show();

                    }


                    if (xmlNodeList3[26].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox3.Show();

                    }

                    if (xmlNodeList3[27].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[25].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[25].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[6].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[6].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[25].Attributes["IsCorrect"].Value != xmlNodeList3[25].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[6].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[6].X1pictureBox1.Show();


                    if (xmlNodeList3[24].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox1.Show();

                    }


                    if (xmlNodeList3[25].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox2.Show();

                    }


                    if (xmlNodeList3[26].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox3.Show();

                    }

                    if (xmlNodeList3[27].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[26].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[26].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[6].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[6].X1pictureBox1.Hide();


                    plus += 1;


                }


                if (xmlNodeList2[26].Attributes["IsCorrect"].Value != xmlNodeList3[26].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[6].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[6].X1pictureBox1.Show();




                    if (xmlNodeList3[24].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox1.Show();

                    }


                    if (xmlNodeList3[25].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox2.Show();

                    }


                    if (xmlNodeList3[26].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox3.Show();

                    }

                    if (xmlNodeList3[27].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[27].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[27].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[6].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[6].X1pictureBox1.Hide();


                    plus += 1;





                }


                if (xmlNodeList2[27].Attributes["IsCorrect"].Value != xmlNodeList3[27].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[6].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[6].X1pictureBox1.Show();




                    if (xmlNodeList3[24].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox1.Show();

                    }


                    if (xmlNodeList3[25].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox2.Show();

                    }


                    if (xmlNodeList3[26].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox3.Show();

                    }

                    if (xmlNodeList3[27].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[6].pictureBox4.Show();

                    }

                    minus += 1;

                }

            }



            if (createQusetion_UserControl1s[7].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[28].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[28].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[7].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[7].X1pictureBox1.Hide();


                    plus += 1;




                }

                if (xmlNodeList2[28].Attributes["IsCorrect"].Value != xmlNodeList3[28].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[7].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[7].X1pictureBox1.Show();




                    if (xmlNodeList3[28].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox1.Show();

                    }


                    if (xmlNodeList3[29].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox2.Show();

                    }


                    if (xmlNodeList3[30].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox3.Show();

                    }

                    if (xmlNodeList3[31].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[29].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[29].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[7].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[7].X1pictureBox1.Hide();

                    plus += 1;


                }


                if (xmlNodeList2[29].Attributes["IsCorrect"].Value != xmlNodeList3[29].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[7].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[7].X1pictureBox1.Show();



                    if (xmlNodeList3[28].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox1.Show();

                    }


                    if (xmlNodeList3[29].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox2.Show();

                    }


                    if (xmlNodeList3[30].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox3.Show();

                    }

                    if (xmlNodeList3[31].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[30].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[30].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[7].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[7].X1pictureBox1.Hide();


                    plus += 1;


                }


                if (xmlNodeList2[30].Attributes["IsCorrect"].Value != xmlNodeList3[30].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[7].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[7].X1pictureBox1.Show();



                    if (xmlNodeList3[28].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox1.Show();

                    }


                    if (xmlNodeList3[29].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox2.Show();

                    }


                    if (xmlNodeList3[30].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox3.Show();

                    }

                    if (xmlNodeList3[31].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[31].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[31].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[7].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[7].X1pictureBox1.Hide();



                    plus += 1;




                }


                if (xmlNodeList2[31].Attributes["IsCorrect"].Value != xmlNodeList3[31].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[7].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[7].X1pictureBox1.Show();


                    if (xmlNodeList3[28].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox1.Show();

                    }


                    if (xmlNodeList3[29].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox2.Show();

                    }


                    if (xmlNodeList3[30].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox3.Show();

                    }

                    if (xmlNodeList3[31].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[7].pictureBox4.Show();

                    }

                    minus += 1;

                }

            }




            if (createQusetion_UserControl1s[8].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[32].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[32].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[8].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[8].X1pictureBox1.Hide();


                    plus += 1;


                }

                if (xmlNodeList2[32].Attributes["IsCorrect"].Value != xmlNodeList3[32].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[8].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[8].X1pictureBox1.Show();



                    if (xmlNodeList3[32].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox1.Show();

                    }


                    if (xmlNodeList3[33].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox2.Show();

                    }


                    if (xmlNodeList3[34].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox3.Show();

                    }

                    if (xmlNodeList3[35].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[33].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[33].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[8].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[8].X1pictureBox1.Hide();



                    plus += 1;



                }


                if (xmlNodeList2[33].Attributes["IsCorrect"].Value != xmlNodeList3[33].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[8].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[8].X1pictureBox1.Show();



                    if (xmlNodeList3[32].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox1.Show();

                    }


                    if (xmlNodeList3[33].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox2.Show();

                    }


                    if (xmlNodeList3[34].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox3.Show();

                    }

                    if (xmlNodeList3[35].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[34].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[34].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[8].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[8].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[34].Attributes["IsCorrect"].Value != xmlNodeList3[34].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[8].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[8].X1pictureBox1.Show();


                    if (xmlNodeList3[32].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox1.Show();

                    }


                    if (xmlNodeList3[33].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox2.Show();

                    }


                    if (xmlNodeList3[34].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox3.Show();

                    }

                    if (xmlNodeList3[35].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[35].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[35].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[8].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[8].X1pictureBox1.Hide();



                    plus += 1;



                }


                if (xmlNodeList2[35].Attributes["IsCorrect"].Value != xmlNodeList3[35].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[8].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[8].X1pictureBox1.Show();


                    if (xmlNodeList3[32].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox1.Show();

                    }


                    if (xmlNodeList3[33].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox2.Show();

                    }


                    if (xmlNodeList3[34].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox3.Show();

                    }

                    if (xmlNodeList3[35].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[8].pictureBox4.Show();

                    }

                    minus += 1;

                }

            }



            if (createQusetion_UserControl1s[9].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[36].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[36].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[9].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[9].X1pictureBox1.Hide();



                    plus += 1;



                }

                if (xmlNodeList2[36].Attributes["IsCorrect"].Value != xmlNodeList3[36].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[9].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[9].X1pictureBox1.Show();



                    if (xmlNodeList3[36].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox1.Show();

                    }


                    if (xmlNodeList3[37].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox2.Show();

                    }


                    if (xmlNodeList3[38].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox3.Show();

                    }

                    if (xmlNodeList3[39].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[37].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[37].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[9].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[9].X1pictureBox1.Hide();



                    plus += 1;


                }


                if (xmlNodeList2[37].Attributes["IsCorrect"].Value != xmlNodeList3[37].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[9].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[9].X1pictureBox1.Show();



                    if (xmlNodeList3[36].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox1.Show();

                    }


                    if (xmlNodeList3[37].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox2.Show();

                    }


                    if (xmlNodeList3[38].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox3.Show();

                    }

                    if (xmlNodeList3[39].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[38].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[38].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[9].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[9].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[38].Attributes["IsCorrect"].Value != xmlNodeList3[38].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[9].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[9].X1pictureBox1.Show();



                    if (xmlNodeList3[36].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox1.Show();

                    }


                    if (xmlNodeList3[37].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox2.Show();

                    }


                    if (xmlNodeList3[38].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox3.Show();

                    }

                    if (xmlNodeList3[39].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox4.Show();

                    }


                    minus += 1;

                }


                if (xmlNodeList2[39].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[39].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[9].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[9].X1pictureBox1.Hide();


                    plus += 1;







                }


                if (xmlNodeList2[39].Attributes["IsCorrect"].Value != xmlNodeList3[39].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[9].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[9].X1pictureBox1.Show();



                    if (xmlNodeList3[36].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox1.Show();

                    }


                    if (xmlNodeList3[37].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox2.Show();

                    }


                    if (xmlNodeList3[38].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox3.Show();

                    }

                    if (xmlNodeList3[39].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[9].pictureBox4.Show();

                    }

                    minus += 1;

                }

            }





            if (createQusetion_UserControl1s[10].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[40].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[40].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[10].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[10].X1pictureBox1.Hide();


                    plus += 1;





                }

                if (xmlNodeList2[40].Attributes["IsCorrect"].Value != xmlNodeList3[40].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[10].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[10].X1pictureBox1.Show();





                    if (xmlNodeList3[40].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox1.Show();

                    }


                    if (xmlNodeList3[41].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox2.Show();

                    }


                    if (xmlNodeList3[42].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox3.Show();

                    }

                    if (xmlNodeList3[43].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[41].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[41].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[10].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[10].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[41].Attributes["IsCorrect"].Value != xmlNodeList3[41].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[10].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[10].X1pictureBox1.Show();





                    if (xmlNodeList3[40].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox1.Show();

                    }


                    if (xmlNodeList3[41].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox2.Show();

                    }


                    if (xmlNodeList3[42].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox3.Show();

                    }

                    if (xmlNodeList3[43].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[42].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[42].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[10].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[10].X1pictureBox1.Hide();


                    plus += 1;






                }


                if (xmlNodeList2[42].Attributes["IsCorrect"].Value != xmlNodeList3[42].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[10].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[10].X1pictureBox1.Show();





                    if (xmlNodeList3[40].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox1.Show();

                    }


                    if (xmlNodeList3[41].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox2.Show();

                    }


                    if (xmlNodeList3[42].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox3.Show();

                    }

                    if (xmlNodeList3[43].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox4.Show();

                    }


                    minus += 1;

                }


                if (xmlNodeList2[43].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[43].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[10].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[10].X1pictureBox1.Hide();


                    plus += 1;





                }


                if (xmlNodeList2[43].Attributes["IsCorrect"].Value != xmlNodeList3[43].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[10].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[10].X1pictureBox1.Show();





                    if (xmlNodeList3[40].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox1.Show();

                    }


                    if (xmlNodeList3[41].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox2.Show();

                    }


                    if (xmlNodeList3[42].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox3.Show();

                    }

                    if (xmlNodeList3[43].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[10].pictureBox4.Show();

                    }

                    minus += 1;

                }

            }




            if (createQusetion_UserControl1s[11].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[44].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[44].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[11].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[11].X1pictureBox1.Hide();


                    plus += 1;





                }

                if (xmlNodeList2[44].Attributes["IsCorrect"].Value != xmlNodeList3[44].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[11].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[11].X1pictureBox1.Show();





                    if (xmlNodeList3[44].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox1.Show();

                    }


                    if (xmlNodeList3[45].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox2.Show();

                    }


                    if (xmlNodeList3[46].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox3.Show();

                    }

                    if (xmlNodeList3[47].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[45].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[45].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[11].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[11].X1pictureBox1.Hide();

                    plus += 1;



                }


                if (xmlNodeList2[45].Attributes["IsCorrect"].Value != xmlNodeList3[45].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[11].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[11].X1pictureBox1.Show();






                    if (xmlNodeList3[44].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox1.Show();

                    }


                    if (xmlNodeList3[45].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox2.Show();

                    }


                    if (xmlNodeList3[46].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox3.Show();

                    }

                    if (xmlNodeList3[47].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[46].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[46].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[11].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[11].X1pictureBox1.Hide();


                    plus += 1;





                }


                if (xmlNodeList2[46].Attributes["IsCorrect"].Value != xmlNodeList3[46].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[11].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[11].X1pictureBox1.Show();






                    if (xmlNodeList3[44].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox1.Show();

                    }


                    if (xmlNodeList3[45].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox2.Show();

                    }


                    if (xmlNodeList3[46].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox3.Show();

                    }

                    if (xmlNodeList3[47].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox4.Show();

                    }


                    minus += 1;

                }


                if (xmlNodeList2[47].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[47].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[11].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[11].X1pictureBox1.Hide();

                    plus += 1;








                }


                if (xmlNodeList2[47].Attributes["IsCorrect"].Value != xmlNodeList3[47].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[11].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[11].X1pictureBox1.Show();





                    if (xmlNodeList3[44].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox1.Show();

                    }


                    if (xmlNodeList3[45].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox2.Show();

                    }


                    if (xmlNodeList3[46].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox3.Show();

                    }

                    if (xmlNodeList3[47].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[11].pictureBox4.Show();

                    }


                    minus += 1;

                }

            }



            if (createQusetion_UserControl1s[12].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[48].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[48].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[12].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[12].X1pictureBox1.Hide();


                    plus += 1;




                }

                if (xmlNodeList2[48].Attributes["IsCorrect"].Value != xmlNodeList3[48].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[12].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[12].X1pictureBox1.Show();




                    if (xmlNodeList3[48].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox1.Show();

                    }


                    if (xmlNodeList3[49].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox2.Show();

                    }


                    if (xmlNodeList3[50].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox3.Show();

                    }

                    if (xmlNodeList3[51].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[49].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[49].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[12].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[12].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[49].Attributes["IsCorrect"].Value != xmlNodeList3[49].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[12].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[12].X1pictureBox1.Show();




                    if (xmlNodeList3[48].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox1.Show();

                    }


                    if (xmlNodeList3[49].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox2.Show();

                    }


                    if (xmlNodeList3[50].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox3.Show();

                    }

                    if (xmlNodeList3[51].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox4.Show();

                    }


                    minus += 1;

                }


                if (xmlNodeList2[50].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[50].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[12].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[12].X1pictureBox1.Hide();

                    plus += 1;




                }


                if (xmlNodeList2[50].Attributes["IsCorrect"].Value != xmlNodeList3[50].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[12].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[12].X1pictureBox1.Show();




                    if (xmlNodeList3[48].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox1.Show();

                    }


                    if (xmlNodeList3[49].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox2.Show();

                    }


                    if (xmlNodeList3[50].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox3.Show();

                    }

                    if (xmlNodeList3[51].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[51].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[51].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[12].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[12].X1pictureBox1.Hide();


                    plus += 1;




                }


                if (xmlNodeList2[51].Attributes["IsCorrect"].Value != xmlNodeList3[51].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[12].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[12].X1pictureBox1.Show();




                    if (xmlNodeList3[48].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox1.Show();

                    }


                    if (xmlNodeList3[49].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox2.Show();

                    }


                    if (xmlNodeList3[50].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox3.Show();

                    }

                    if (xmlNodeList3[51].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[12].pictureBox4.Show();

                    }


                    minus += 1;

                }

            }




            if (createQusetion_UserControl1s[13].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[52].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[52].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[13].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[13].X1pictureBox1.Hide();


                    plus += 1;



                }

                if (xmlNodeList2[52].Attributes["IsCorrect"].Value != xmlNodeList3[52].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[13].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[13].X1pictureBox1.Show();




                    if (xmlNodeList3[52].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox1.Show();

                    }


                    if (xmlNodeList3[53].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox2.Show();

                    }


                    if (xmlNodeList3[54].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox3.Show();

                    }

                    if (xmlNodeList3[55].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[53].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[53].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[13].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[13].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[53].Attributes["IsCorrect"].Value != xmlNodeList3[53].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[13].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[13].X1pictureBox1.Show();





                    if (xmlNodeList3[52].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox1.Show();

                    }


                    if (xmlNodeList3[53].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox2.Show();

                    }


                    if (xmlNodeList3[54].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox3.Show();

                    }

                    if (xmlNodeList3[55].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox4.Show();

                    }

                    minus += 1;

                }


                if (xmlNodeList2[54].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[54].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[13].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[13].X1pictureBox1.Hide();


                    plus += 1;




                }


                if (xmlNodeList2[54].Attributes["IsCorrect"].Value != xmlNodeList3[54].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[13].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[13].X1pictureBox1.Show();




                    if (xmlNodeList3[52].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox1.Show();

                    }


                    if (xmlNodeList3[53].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox2.Show();

                    }


                    if (xmlNodeList3[54].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox3.Show();

                    }

                    if (xmlNodeList3[55].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[55].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[55].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[13].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[13].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[55].Attributes["IsCorrect"].Value != xmlNodeList3[55].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[13].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[13].X1pictureBox1.Show();





                    if (xmlNodeList3[52].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox1.Show();

                    }


                    if (xmlNodeList3[53].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox2.Show();

                    }


                    if (xmlNodeList3[54].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox3.Show();

                    }

                    if (xmlNodeList3[55].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[13].pictureBox4.Show();

                    }


                    minus += 1;

                }

            }




            if (createQusetion_UserControl1s[14].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[56].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[56].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[14].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[14].X1pictureBox1.Hide();


                    plus += 1;



                }

                if (xmlNodeList2[56].Attributes["IsCorrect"].Value != xmlNodeList3[56].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[14].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[14].X1pictureBox1.Show();


                    if (xmlNodeList3[56].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox1.Show();

                    }


                    if (xmlNodeList3[57].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox2.Show();

                    }


                    if (xmlNodeList3[58].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox3.Show();

                    }

                    if (xmlNodeList3[59].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[57].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[57].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[14].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[14].X1pictureBox1.Hide();


                    plus += 1;

                }


                if (xmlNodeList2[57].Attributes["IsCorrect"].Value != xmlNodeList3[57].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[14].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[14].X1pictureBox1.Show();




                    if (xmlNodeList3[56].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox1.Show();

                    }


                    if (xmlNodeList3[57].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox2.Show();

                    }


                    if (xmlNodeList3[58].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox3.Show();

                    }

                    if (xmlNodeList3[59].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[58].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[58].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[14].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[14].X1pictureBox1.Hide();

                    plus += 1;



                }


                if (xmlNodeList2[58].Attributes["IsCorrect"].Value != xmlNodeList3[58].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[14].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[14].X1pictureBox1.Show();




                    if (xmlNodeList3[56].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox1.Show();

                    }


                    if (xmlNodeList3[57].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox2.Show();

                    }


                    if (xmlNodeList3[58].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox3.Show();

                    }

                    if (xmlNodeList3[59].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[59].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[59].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[14].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[14].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[59].Attributes["IsCorrect"].Value != xmlNodeList3[59].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[14].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[14].X1pictureBox1.Show();


                    if (xmlNodeList3[56].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox1.Show();

                    }


                    if (xmlNodeList3[57].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox2.Show();

                    }


                    if (xmlNodeList3[58].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox3.Show();

                    }

                    if (xmlNodeList3[59].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[14].pictureBox4.Show();

                    }


                    minus += 1;


                }

            }






            if (createQusetion_UserControl1s[15].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[60].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[60].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[15].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[15].X1pictureBox1.Hide();

                    plus += 1;



                }

                if (xmlNodeList2[60].Attributes["IsCorrect"].Value != xmlNodeList3[60].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[15].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[15].X1pictureBox1.Show();


                    if (xmlNodeList3[60].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox1.Show();

                    }


                    if (xmlNodeList3[61].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox2.Show();

                    }


                    if (xmlNodeList3[62].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox3.Show();

                    }

                    if (xmlNodeList3[63].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox4.Show();

                    }

                    minus += 1;

                }

                if (xmlNodeList2[61].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[61].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[15].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[15].X1pictureBox1.Hide();

                    plus += 1;



                }


                if (xmlNodeList2[61].Attributes["IsCorrect"].Value != xmlNodeList3[61].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[15].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[15].X1pictureBox1.Show();


                    if (xmlNodeList3[60].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox1.Show();

                    }


                    if (xmlNodeList3[61].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox2.Show();

                    }


                    if (xmlNodeList3[62].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox3.Show();

                    }

                    if (xmlNodeList3[63].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[62].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[62].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[15].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[15].X1pictureBox1.Hide();

                    plus += 1;





                }


                if (xmlNodeList2[62].Attributes["IsCorrect"].Value != xmlNodeList3[62].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[15].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[15].X1pictureBox1.Show();




                    if (xmlNodeList3[60].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox1.Show();

                    }


                    if (xmlNodeList3[61].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox2.Show();

                    }


                    if (xmlNodeList3[62].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox3.Show();

                    }

                    if (xmlNodeList3[63].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[63].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[63].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[15].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[15].X1pictureBox1.Hide();

                    plus += 1;






                }


                if (xmlNodeList2[63].Attributes["IsCorrect"].Value != xmlNodeList3[63].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[15].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[15].X1pictureBox1.Show();





                    if (xmlNodeList3[60].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox1.Show();

                    }


                    if (xmlNodeList3[61].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox2.Show();

                    }


                    if (xmlNodeList3[62].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox3.Show();

                    }

                    if (xmlNodeList3[63].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[15].pictureBox4.Show();

                    }
                    minus += 1;


                }

            }




            if (createQusetion_UserControl1s[16].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[64].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[64].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[16].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[16].X1pictureBox1.Hide();

                    plus += 1;



                }

                if (xmlNodeList2[64].Attributes["IsCorrect"].Value != xmlNodeList3[64].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[16].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[16].X1pictureBox1.Show();



                    if (xmlNodeList3[64].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox1.Show();

                    }


                    if (xmlNodeList3[65].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox2.Show();

                    }


                    if (xmlNodeList3[66].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox3.Show();

                    }

                    if (xmlNodeList3[67].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox4.Show();

                    }
                    minus += 1;


                }

                if (xmlNodeList2[65].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[65].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[16].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[16].X1pictureBox1.Hide();

                    plus += 1;



                }


                if (xmlNodeList2[65].Attributes["IsCorrect"].Value != xmlNodeList3[65].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[16].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[16].X1pictureBox1.Show();



                    if (xmlNodeList3[64].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox1.Show();

                    }


                    if (xmlNodeList3[65].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox2.Show();

                    }


                    if (xmlNodeList3[66].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox3.Show();

                    }

                    if (xmlNodeList3[67].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[66].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[66].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[16].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[16].X1pictureBox1.Hide();

                    plus += 1;





                }


                if (xmlNodeList2[66].Attributes["IsCorrect"].Value != xmlNodeList3[66].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[16].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[16].X1pictureBox1.Show();



                    if (xmlNodeList3[64].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox1.Show();

                    }


                    if (xmlNodeList3[65].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox2.Show();

                    }


                    if (xmlNodeList3[66].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox3.Show();

                    }

                    if (xmlNodeList3[67].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[67].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[67].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[16].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[16].X1pictureBox1.Hide();

                    plus += 1;






                }


                if (xmlNodeList2[67].Attributes["IsCorrect"].Value != xmlNodeList3[67].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[16].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[16].X1pictureBox1.Show();




                    if (xmlNodeList3[64].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox1.Show();

                    }


                    if (xmlNodeList3[65].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox2.Show();

                    }


                    if (xmlNodeList3[66].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox3.Show();

                    }

                    if (xmlNodeList3[67].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[16].pictureBox4.Show();

                    }
                    minus += 1;


                }

            }



            if (createQusetion_UserControl1s[17].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[68].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[68].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[17].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[17].X1pictureBox1.Hide();

                    plus += 1;




                }

                if (xmlNodeList2[68].Attributes["IsCorrect"].Value != xmlNodeList3[68].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[17].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[17].X1pictureBox1.Show();




                    if (xmlNodeList3[68].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox1.Show();

                    }


                    if (xmlNodeList3[69].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox2.Show();

                    }


                    if (xmlNodeList3[70].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox3.Show();

                    }

                    if (xmlNodeList3[71].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox4.Show();

                    }
                    minus += 1;


                }

                if (xmlNodeList2[69].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[69].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[17].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[17].X1pictureBox1.Hide();

                    plus += 1;



                }


                if (xmlNodeList2[69].Attributes["IsCorrect"].Value != xmlNodeList3[69].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[17].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[17].X1pictureBox1.Show();


                    if (xmlNodeList3[68].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox1.Show();

                    }


                    if (xmlNodeList3[69].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox2.Show();

                    }


                    if (xmlNodeList3[70].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox3.Show();

                    }

                    if (xmlNodeList3[71].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[70].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[70].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[17].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[17].X1pictureBox1.Hide();


                    plus += 1;


                }


                if (xmlNodeList2[70].Attributes["IsCorrect"].Value != xmlNodeList3[70].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[17].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[17].X1pictureBox1.Show();




                    if (xmlNodeList3[68].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox1.Show();

                    }


                    if (xmlNodeList3[69].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox2.Show();

                    }


                    if (xmlNodeList3[70].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox3.Show();

                    }

                    if (xmlNodeList3[71].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[71].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[71].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[17].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[17].X1pictureBox1.Hide();


                    plus += 1;



                }


                if (xmlNodeList2[71].Attributes["IsCorrect"].Value != xmlNodeList3[71].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[17].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[17].X1pictureBox1.Show();




                    if (xmlNodeList3[68].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox1.Show();

                    }


                    if (xmlNodeList3[69].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox2.Show();

                    }


                    if (xmlNodeList3[70].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox3.Show();

                    }

                    if (xmlNodeList3[71].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[17].pictureBox4.Show();

                    }
                    minus += 1;


                }

            }




            if (createQusetion_UserControl1s[18].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[72].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[72].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[18].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[18].X1pictureBox1.Hide();

                    plus += 1;



                }

                if (xmlNodeList2[72].Attributes["IsCorrect"].Value != xmlNodeList3[72].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[18].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[18].X1pictureBox1.Show();



                    if (xmlNodeList3[72].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox1.Show();

                    }


                    if (xmlNodeList3[73].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox2.Show();

                    }


                    if (xmlNodeList3[74].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox3.Show();

                    }

                    if (xmlNodeList3[75].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox4.Show();

                    }
                    minus += 1;


                }

                if (xmlNodeList2[73].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[73].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[18].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[18].X1pictureBox1.Hide();

                    plus += 1;



                }


                if (xmlNodeList2[73].Attributes["IsCorrect"].Value != xmlNodeList3[73].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[18].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[18].X1pictureBox1.Show();



                    if (xmlNodeList3[72].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox1.Show();

                    }


                    if (xmlNodeList3[73].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox2.Show();

                    }


                    if (xmlNodeList3[74].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox3.Show();

                    }

                    if (xmlNodeList3[75].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[74].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[74].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[18].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[18].X1pictureBox1.Hide();

                    plus += 1;



                }


                if (xmlNodeList2[74].Attributes["IsCorrect"].Value != xmlNodeList3[74].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[18].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[18].X1pictureBox1.Show();



                    if (xmlNodeList3[72].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox1.Show();

                    }


                    if (xmlNodeList3[73].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox2.Show();

                    }


                    if (xmlNodeList3[74].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox3.Show();

                    }

                    if (xmlNodeList3[75].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[75].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[75].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[18].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[18].X1pictureBox1.Hide();

                    plus += 1;



                }


                if (xmlNodeList2[75].Attributes["IsCorrect"].Value != xmlNodeList3[75].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[18].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[18].X1pictureBox1.Show();



                    if (xmlNodeList3[72].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox1.Show();

                    }


                    if (xmlNodeList3[73].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox2.Show();

                    }


                    if (xmlNodeList3[74].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox3.Show();

                    }

                    if (xmlNodeList3[75].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[18].pictureBox4.Show();

                    }

                    minus += 1;

                }

            }




            if (createQusetion_UserControl1s[19].TrueFalseguna2ComboBox1.Text != "")
            {
                if (xmlNodeList2[76].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[76].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[19].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[19].X1pictureBox1.Hide();


                    plus += 1;



                }

                if (xmlNodeList2[76].Attributes["IsCorrect"].Value != xmlNodeList3[76].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[19].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[19].X1pictureBox1.Show();





                    if (xmlNodeList3[76].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox1.Show();

                    }


                    if (xmlNodeList3[77].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox2.Show();

                    }


                    if (xmlNodeList3[78].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox3.Show();

                    }

                    if (xmlNodeList3[79].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox4.Show();

                    }
                    minus += 1;


                }

                if (xmlNodeList2[77].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[77].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[19].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[19].X1pictureBox1.Hide();

                    plus += 1;


                }


                if (xmlNodeList2[77].Attributes["IsCorrect"].Value != xmlNodeList3[77].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[19].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[19].X1pictureBox1.Show();




                    if (xmlNodeList3[76].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox1.Show();

                    }


                    if (xmlNodeList3[77].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox2.Show();

                    }


                    if (xmlNodeList3[78].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox3.Show();

                    }

                    if (xmlNodeList3[79].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[78].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[78].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[19].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[19].X1pictureBox1.Hide();


                    plus += 1;


                }


                if (xmlNodeList2[78].Attributes["IsCorrect"].Value != xmlNodeList3[78].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[19].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[19].X1pictureBox1.Show();




                    if (xmlNodeList3[76].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox1.Show();

                    }


                    if (xmlNodeList3[77].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox2.Show();

                    }


                    if (xmlNodeList3[78].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox3.Show();

                    }

                    if (xmlNodeList3[79].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox4.Show();

                    }
                    minus += 1;

                }


                if (xmlNodeList2[79].Attributes["IsCorrect"].Value == "Yes" && xmlNodeList3[79].Attributes["IsCorrect"].Value == "Yes")
                {
                    createQusetion_UserControl1s[19].OK1pictureBox1.Show();

                    createQusetion_UserControl1s[19].X1pictureBox1.Hide();


                    plus += 1;




                }


                if (xmlNodeList2[79].Attributes["IsCorrect"].Value != xmlNodeList3[79].Attributes["IsCorrect"].Value)
                {
                    createQusetion_UserControl1s[19].OK1pictureBox1.Hide();

                    createQusetion_UserControl1s[19].X1pictureBox1.Show();




                    if (xmlNodeList3[76].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox1.Show();

                    }


                    if (xmlNodeList3[77].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox2.Show();

                    }


                    if (xmlNodeList3[78].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox3.Show();

                    }

                    if (xmlNodeList3[79].Attributes["IsCorrect"].Value == "Yes")
                    {

                        createQusetion_UserControl1s[19].pictureBox4.Show();

                    }
                    minus += 1;


                }

            }




            minus = minus / 2;
            TPlabel12.Text = plus.ToString();
            FPlabel13.Text = minus.ToString();




            createQusetion_UserControl1s[0].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[0].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[0].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[0].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[1].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[1].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[1].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[1].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[2].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[2].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[2].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[2].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[3].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[3].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[3].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[3].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[4].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[4].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[4].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[4].TrueFalseguna2ComboBox4.Enabled = false;


            createQusetion_UserControl1s[5].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[5].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[5].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[5].TrueFalseguna2ComboBox4.Enabled = false;



            createQusetion_UserControl1s[6].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[6].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[6].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[6].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[7].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[7].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[7].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[7].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[8].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[8].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[8].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[8].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[9].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[9].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[9].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[9].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[10].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[10].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[10].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[10].TrueFalseguna2ComboBox4.Enabled = false;


            createQusetion_UserControl1s[11].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[11].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[11].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[11].TrueFalseguna2ComboBox4.Enabled = false;


            createQusetion_UserControl1s[12].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[12].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[12].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[12].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[13].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[13].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[13].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[13].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[14].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[14].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[14].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[14].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[15].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[15].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[15].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[15].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[16].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[16].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[16].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[16].TrueFalseguna2ComboBox4.Enabled = false;


            createQusetion_UserControl1s[17].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[17].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[17].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[17].TrueFalseguna2ComboBox4.Enabled = false;


            createQusetion_UserControl1s[18].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[18].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[18].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[18].TrueFalseguna2ComboBox4.Enabled = false;

            createQusetion_UserControl1s[19].TrueFalseguna2ComboBox1.Enabled = false;
            createQusetion_UserControl1s[19].TrueFalseguna2ComboBox2.Enabled = false;
            createQusetion_UserControl1s[19].TrueFalseguna2ComboBox3.Enabled = false;
            createQusetion_UserControl1s[19].TrueFalseguna2ComboBox4.Enabled = false;




            Resultguna2Button1.Enabled = false;


            string true_ = TPlabel12.Text;
            string false_ = FPlabel13.Text;

            string docpath = $@"Result.docx";
            string pdfpath = $@"Result.pdf";
            string shadowpath = $@"Shadow.docx";


            XmlDocument xmldoc2 = new XmlDocument();
            xmldoc2.Load($@"Data\RegisterStudent {d.ToString("MM dd yyyy")}.xml");
            XmlNodeList xmlNodeList_ = xmldoc2.GetElementsByTagName("_StudentName");
            XmlNodeList xmlNodeList2_ = xmldoc2.GetElementsByTagName("StudentSurname");

             xmlNodeList_[0].InnerText.ToString();
            xmlNodeList2_[0].InnerText.ToString();

            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF file|*.pdf", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Document doc = new Document(PageSize.A4.Rotate());
                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();

                        doc.Add(new Paragraph($"Name:     \t{ xmlNodeList_[0].InnerText.ToString()} \n"));
                        doc.Add(new Paragraph($"Surname: \t{   xmlNodeList2_[0].InnerText.ToString()} \n"));
                        doc.Add(new Paragraph($"True:    \t{TPlabel12.Text} \n"));
                        doc.Add(new Paragraph($"False:   \t{FPlabel13.Text} \n"));

                    }
                    catch (Exception)
                    {

                        MessageBox.Show($"Error");
                    }

                    finally
                    {
                        doc.Close();
                    }
                }
            }



        }
    }
}
