using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using YatskivLab4.Tools.Exceptions;

namespace YatskivLab4.Models
{
    [Serializable]
    internal class Person
    {
        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthday;
        private string _sunSign;
        private string _chineseSign;



        private enum WestZodiac
        {
            Capricornus = 19,
            Aquarius = 47,
            Pisces = 71,
            Aries = 109,
            Taurus = 134,
            Gemini = 171,
            Cancer = 202,
            Leo = 222,
            Virgo = 259,
            Libra = 205,
            Scorpio = 327,
            Sagittarius = 352
        }

        private enum ChineseZodiac
        {
            Monkey = 0,
            Cock,
            Dog,
            Pig,
            Rat,
            Ox,
            Tiger,
            Heir,
            Dragon,
            Snake,
            Horse,
            Goal
        }


        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                try { NameValidation(value); }
                catch (PersonException ex) { MessageBox.Show(ex.Message); }
                finally { _name = value; }

                //NameValidation(value);
                //_name = value;
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                try { SurnameValidation(value); }
                catch (PersonException ex) { MessageBox.Show(ex.Message); }
                finally { _surname = value; }

                //SurnameValidation(value);
                //_surname = value;
            }

        }

 

        public string Email
        {
            get { return _email; }
            set
            {
                try { EmailValidation(value); }
                catch (PersonException ex) { MessageBox.Show(ex.Message); }
                finally { _email = value; }

                //EmailValidation(value);
                //_email = value;

            }
        }

        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                try { BirthdayValidation(value); }
                catch (PersonException ex) { MessageBox.Show(ex.Message); }
                finally { _birthday = value; }

                //BirthdayValidation(value);
                //_birthday = value;

            }
        }

        public bool IsAdult
        {
            get
            {
                return (DateTime.Today.Year - _birthday.Year) -
                               (DateTime.Today.DayOfYear >= _birthday.DayOfYear ? 0 : 1) >= 18;
            }
        }

        public bool IsBirthday
        {
            get
            {
                DateTime now = DateTime.Today;
                return Birthday.Month == now.Month && Birthday.Day == now.Day;
            }
        }

        public string SunSign
        {
            get
            {
                int counter = 0;

                foreach (int zodiac in Enum.GetValues(typeof(WestZodiac)))
                {
                    if (zodiac >= Birthday.DayOfYear)
                    {
                        SunSign = (Enum.GetNames(typeof(WestZodiac)))[counter];
                        break;
                    }

                    counter++;
                }

                return _sunSign;
            }
            private set
            {
                _sunSign = value;
            }
        }

        public string ChineseSign
        {
            get
            {
                int counter = 0;

                foreach (int zodiac in Enum.GetValues(typeof(ChineseZodiac)))
                {
                    if (Birthday.Year % 12 == zodiac)
                    {
                        ChineseSign = (Enum.GetNames(typeof(ChineseZodiac)))[counter];
                        break;
                    }

                    counter++;
                }

                return _chineseSign;
            }
            private set
            {

                _chineseSign = value;
            }
        }
        #endregion

        #region Constructors
        internal Person()
        {
            Name = "";
            Surname = "";
            Email = "";
            Birthday = DateTime.Today;

        }

        internal Person(string name, string surname, string email, DateTime birthday)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Birthday = birthday;

        }

        public Person(string firstName, string lastName, string email) : this(firstName, lastName, email,
            new DateTime(2001, 1, 1))
        {
        }

        public Person(string firstName, string lastName, DateTime birthDate) : this(firstName, lastName, " ", birthDate)
        {
        }

        #endregion

        #region Validators

        public void NameValidation(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new NullPersonException();

            Regex regex = new Regex(@"^[a-zA-Z'-]+$");
            Match match = regex.Match(value);

            if (!match.Success)
                throw new InvalidNameException(value);
        }

        public void SurnameValidation(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new NullPersonException();

            Regex regex = new Regex(@"^[a-zA-Z'-]+$");
            Match match = regex.Match(value);

            if (!match.Success)
                throw new InvalidSurnameException(value);
        }

        public void EmailValidation(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new NullPersonException();

            Regex regex = new Regex("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`{}|~\\w])*)(?<=[0-9a-z])@))(?([)([(\\d{1,3}.){3}\\d{1,3}])|(([0-9a-z][-0-9a-z]*[0-9a-z]*.)+[a-z0-9][-a-z0-9]{0,22}[a-z0-9]))$"); //(@"^(\w+)+@(\w+)(\.)(\w+)$");
            Match match = regex.Match(value);

            if (!match.Success)
                throw new InvalidEmailException(value);

        }

        public void BirthdayValidation(DateTime value)
        {
            int _age = (DateTime.Today.Year - value.Year) -
                    (DateTime.Today.DayOfYear >= value.DayOfYear ? 0 : 1);

            if (_age > 135) throw new DeadPersonException(value);
            else if (_age < 0) throw new NotBornPersonException(value);
        }

        #endregion

    }
}
