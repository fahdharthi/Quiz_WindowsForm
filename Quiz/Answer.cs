using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Quiz
{
    [Serializable]
    public class Answer
    {
        public Answer() { }
        public Answer(string ısCorrect, int _id, string text)
        {
            IsCorrect = ısCorrect;
            id = _id;
            Text = text;
        }

        [XmlAttribute]
        public string IsCorrect { get; set; }
        [XmlAttribute]
        public int id { get; set; }
        [XmlElement]
        public string Text { get; set; }
    }
}
