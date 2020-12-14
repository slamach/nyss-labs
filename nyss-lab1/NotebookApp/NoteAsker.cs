using System;
using System.Text.RegularExpressions;

namespace NotebookApp
{
    public static class NoteAsker
    {
        public static string AskSurname()
        {
            string surname = null;
            do
            {
                if (surname != null)
                    Console.WriteLine("Фамилия не может быть пустой!");
                Console.WriteLine("Введите фамилию*:");
                Console.Write(Program.PS2);
                surname = Console.ReadLine().Trim();
            } while (surname == "");
            return surname;
        }

        public static string AskName()
        {
            string name = null;
            do
            {
                if (name != null)
                    Console.WriteLine("Имя не может быть пустым!");
                Console.WriteLine("Введите имя*:");
                Console.Write(Program.PS2);
                name = Console.ReadLine().Trim();
            } while (name == "");
            return name;
        }

        public static string AskFatherName()
        {
            Console.WriteLine("Введите отчество:");
            Console.Write(Program.PS2);
            return Console.ReadLine().Trim();
        }

        public static string AskPhoneNumber()
        {
            string phoneNumber = null;
            do
            {
                if (phoneNumber != null)
                    Console.WriteLine("Номер телефона не может быть пустым и должен состоять из цифр!");
                Console.WriteLine("Введите номер телефона*:");
                Console.Write(Program.PS2);
                phoneNumber = Console.ReadLine().Trim();
            } while (phoneNumber == "" || !Int64.TryParse(phoneNumber, out _));
            return phoneNumber;
        }

        public static string AskCountry()
        {
            string country = null;
            do
            {
                if (country != null)
                    Console.WriteLine("Страна не может быть пустой!");
                Console.WriteLine("Введите страну*:");
                Console.Write(Program.PS2);
                country = Console.ReadLine().Trim();
            } while (country == "");
            return country;
        }

        public static string AskBirthdate()
        {
            Regex birthdateFormat = new Regex(@"\d\d\.\d\d\.\d\d\d\d");
            string birthdate = null;
            do
            {
                if (birthdate != null)
                    Console.WriteLine("Дата рождения должна быть пустой или представлена в формате XX.XX.XXXX!");
                Console.WriteLine("Введите дату рождения в формате XX.XX.XXXX:");
                Console.Write(Program.PS2);
                birthdate = Console.ReadLine().Trim();
            } while (birthdate != "" && !(birthdateFormat.IsMatch(birthdate)));
            return birthdate;
        }

        public static string AskCompany()
        {
            Console.WriteLine("Введите организацию:");
            Console.Write(Program.PS2);
            return Console.ReadLine().Trim();
        }

        public static string AskPosition()
        {
            Console.WriteLine("Введите должность:");
            Console.Write(Program.PS2);
            return Console.ReadLine().Trim();
        }

        public static string AskNotes()
        {
            Console.WriteLine("Введите прочие заметки:");
            Console.Write(Program.PS2);
            return Console.ReadLine().Trim();
        }

        public static Note AskNote()
        {
            return new Note(
                AskSurname(),
                AskName(),
                AskFatherName(),
                AskPhoneNumber(),
                AskCountry(),
                AskBirthdate(),
                AskCompany(),
                AskPosition(),
                AskNotes()
                );
        }

        public static bool AskQuestion(string question)
        {
            string finalQuestion = question + " (+/-):";
            string answer = null;
            do
            {
                if (answer != null)
                    Console.WriteLine("Ответ должен быть представлен знаками '+' или '-'!");
                Console.WriteLine(finalQuestion);
                Console.Write(Program.PS2);
                answer = Console.ReadLine().Trim();
            } while (answer != "+" && answer != "-");
            return answer == "+";
        }

        public static Note AskEditNote()
        {
            string surname = AskQuestion("Хотите изменить фамилию?") ? AskSurname() : null;
            string name = AskQuestion("Хотите изменить имя?") ? AskName() : null;
            string fatherName = AskQuestion("Хотите изменить отчество?") ? AskFatherName() : null;
            string phoneNumber = AskQuestion("Хотите изменить номер телефона?") ? AskPhoneNumber() : null;
            string country = AskQuestion("Хотите изменить страну?") ? AskCountry() : null;
            string birthdate = AskQuestion("Хотите изменить дату рождения?") ? AskBirthdate() : null;
            string company = AskQuestion("Хотите изменить организацию?") ? AskCompany() : null;
            string position = AskQuestion("Хотите изменить должность?") ? AskPosition() : null;
            string notes = AskQuestion("Хотите изменить прочие заметки?") ? AskNotes() : null;

            return new Note(
                surname,
                name,
                fatherName,
                phoneNumber,
                country,
                birthdate,
                company,
                position,
                notes,
                0
                );
        }
    }
}
