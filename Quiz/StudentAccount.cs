using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    [Serializable]
    public class StudentAccount
    {
        public StudentAccount() { }

        public StudentAccount(string studentName, string studentSurname, string studentEmail, string studentPassword, string studentConfirmPassword, string studentUsername)
        {
            _StudentName = studentName;
            StudentSurname = studentSurname;
            StudentEmail = studentEmail;
            StudentPassword = studentPassword;
            StudentConfirmPassword = studentConfirmPassword;
            StudentUsername = studentUsername;
        }

        public string _StudentName { get; set; }
        public string StudentSurname { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPassword { get; set; }
        public string StudentConfirmPassword { get; set; }
        public string StudentUsername { get; set; }
    }
}
