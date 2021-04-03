using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Quiz
{
    [Serializable]
    public class QuestionBlock
    {
        public QuestionBlock() { }
        public QuestionBlock(int _id, string text)
        {
            id = _id;
            Text = text;

        }

        public QuestionBlock(string text)
        {
          
            Text = text;

        }

        [XmlAttribute]
        public int id { get; set; }
        [XmlElement]
        public string Text { get; set; }
        [XmlArray]
        public List<Answer> Answers { get; set; } = new List<Answer>();


        public override string ToString()
        {
            foreach (var item in Answers)
            {
                Console.WriteLine($"{item.id} {item.Text}");
            }
            return $" ";
        }
    }
}
