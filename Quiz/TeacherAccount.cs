using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    [Serializable]
    public class TeacherAccount
    {
        public TeacherAccount(string name, string surname, string email, string password, string confirmPassword, string username)
        {
            _Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            Username = username;
        }

        public TeacherAccount() { }
        public string _Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Username { get; set; }
    }
}
