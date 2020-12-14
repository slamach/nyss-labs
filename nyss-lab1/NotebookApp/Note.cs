namespace NotebookApp
{
    public class Note
    {
        public static long curId = 1;

        public long Id { get; private set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Birthdate { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Notes { get; set; }

        public Note(string surname, string name, string fatherName, string phoneNumber,
            string country, string birhdate, string company, string position, string notes)
        {
            Id = curId;
            Surname = surname;
            Name = name;
            FatherName = fatherName;
            PhoneNumber = phoneNumber;
            Country = country;
            Birthdate = birhdate;
            Company = company;
            Position = position;
            Notes = notes;

            curId++;
        }

        public Note(string surname, string name, string fatherName, string phoneNumber,
            string country, string birhdate, string company, string position, string notes, long id)
        {
            Id = id;
            Surname = surname;
            Name = name;
            FatherName = fatherName;
            PhoneNumber = phoneNumber;
            Country = country;
            Birthdate = birhdate;
            Company = company;
            Position = position;
            Notes = notes;
        }
    }
}
