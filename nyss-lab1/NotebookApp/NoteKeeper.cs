using System.Collections.Generic;

namespace NotebookApp
{
    public class NoteKeeper
    {
        private List<Note> noteList;

        public NoteKeeper()
        {
            noteList = new List<Note>();
        }

        public string GetInfo()
        {
            if (noteList.Count == 0) return "Записей нет!";
            string result = "";
            foreach (Note note in noteList)
            {
                result += "Запись #" + note.Id;
                result += "\n Фамилия: " + note.Surname;
                result += "\n Имя: " + note.Name;
                result += "\n Номер телефона: " + note.PhoneNumber;
                result += "\n\n";
            }
            return result.Trim();
        }

        public string GetInfox()
        {
            if (noteList.Count == 0) return "Записей нет!";
            string result = "";
            foreach (Note note in noteList)
            {
                result += "Запись #" + note.Id;
                result += "\n Фамилия: " + note.Surname;
                result += "\n Имя: " + note.Name;
                result += "\n Отчество: " + note.FatherName;
                result += "\n Номер телефона: " + note.PhoneNumber;
                result += "\n Страна: " + note.Country;
                result += "\n Дата рождения: " + note.Birthdate;
                result += "\n Организация: " + note.Company;
                result += "\n Должность: " + note.Position;
                result += "\n Прочие заметки: " + note.Notes;
                result += "\n\n";
            }
            return result.Trim();
        }

        public void AddNote(Note note)
        {
            noteList.Add(note);
        }

        public bool CheckById(long id)
        {
            foreach (Note note in noteList)
            {
                if (note.Id == id) return true;
            }
            return false;
        }

        public bool RemoveById(long id)
        {
            Note noteToRemove = null;
            for (int i = 0; i < noteList.Count; i++)
            {
                if (noteList[i].Id == id)
                    noteToRemove = noteList[i];
            }

            if (noteToRemove != null)
                noteList.Remove(noteToRemove);
            return noteToRemove != null;
        }

        public bool EditById(long id, Note note)
        {
            Note noteToEdit = null;
            for (int i = 0; i < noteList.Count; i++)
            {
                if (noteList[i].Id == id)
                    noteToEdit = noteList[i];
            }

            if (noteToEdit != null)
            {
                noteToEdit.Surname = note.Surname == null ? noteToEdit.Surname : note.Surname;
                noteToEdit.Name = note.Name == null ? noteToEdit.Name : note.Name;
                noteToEdit.FatherName = note.FatherName == null ? noteToEdit.FatherName : note.FatherName;
                noteToEdit.PhoneNumber = note.PhoneNumber == null ? noteToEdit.PhoneNumber : note.PhoneNumber;
                noteToEdit.Country = note.Country == null ? noteToEdit.Country : note.Country;
                noteToEdit.Birthdate = note.Birthdate == null ? noteToEdit.Birthdate : note.Birthdate;
                noteToEdit.Company = note.Company == null ? noteToEdit.Company : note.Company;
                noteToEdit.Position = note.Position == null ? noteToEdit.Position : note.Position;
                noteToEdit.Notes = note.Notes == null ? noteToEdit.Notes : note.Notes;
            }
            return noteToEdit != null;
        }
    }
}
