using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatskivLab4.Tools.Exceptions
{
        public class PersonException : Exception
        {
            public PersonException(string message)
                : base(message)
            {
            }
        }

        public class NullPersonException : PersonException
        {
            public NullPersonException()
                : base($"Person does not exist!")
            {
            }
        }

        public class InvalidEmailException : PersonException
        {
            public InvalidEmailException(string email)
                : base($"Email {email} is not valid!")
            {
            }
        }

        public class InvalidNameException : PersonException
        {
            public InvalidNameException(string name)
                : base($"{name} is not valid name")
            {
            }
        }

        public class InvalidSurnameException : PersonException
        {
            public InvalidSurnameException(string surname)
                : base($"{surname} is not valid surname")
            {
            }
        }

        public class NotBornPersonException : PersonException
        {
            public NotBornPersonException(DateTime birthday)
                : base($"{birthday.ToShortDateString()} is in the Future!")
            {
            }
        }

        public class DeadPersonException : PersonException
        {
            public DeadPersonException(DateTime birthday)
                : base($"{birthday.ToShortDateString()} was more than 135 years ago!")
            {
            }
        }

}
