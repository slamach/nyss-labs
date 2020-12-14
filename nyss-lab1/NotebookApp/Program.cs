using System;

namespace NotebookApp
{
    public class Program
    {
        public const string PS1 = "$ ";
        public const string PS2 = "> ";
        public static NoteKeeper keeper;

        public static void Main(string[] args)
        {
            keeper = new NoteKeeper();
            InteractiveMode();
        }

        private static void InteractiveMode()
        {
            string[] userCommand;
            int commandStatus;
            do
            {
                Console.Write(PS1);
                userCommand = (Console.ReadLine().Trim() + " ").Split(' ');
                userCommand[1] = userCommand[1].Trim();
                commandStatus = LaunchCommand(userCommand);
                if (commandStatus == 1) Console.WriteLine("Неверный аргумент!");
            } while (commandStatus != 2);
        }

        private static int LaunchCommand(string[] userCommand)
        {
            switch (userCommand[0])
            {
                case "help":
                    if (userCommand[1] != "") return 1;
                    Console.WriteLine(GetHelp());
                    break;
                case "show":
                    if (userCommand[1] != "") return 1;
                    Console.WriteLine(keeper.GetInfo());
                    break;
                case "showx":
                    if (userCommand[1] != "") return 1;
                    Console.WriteLine(keeper.GetInfox());
                    break;
                case "add":
                    if (userCommand[1] != "") return 1;
                    keeper.AddNote(NoteAsker.AskNote());
                    Console.WriteLine("Запись успешно добавлена.");
                    break;
                case "remove":
                    long removeId;
                    if (userCommand[1] == "" || !Int64.TryParse(userCommand[1], out removeId)) return 1;
                    if (!keeper.RemoveById(removeId))
                        Console.WriteLine("Записи с таким номером не существует!");
                    else
                        Console.WriteLine("Запись успешно удалена.");
                    break;
                case "edit":
                    long editId;
                    if (userCommand[1] == "" || !Int64.TryParse(userCommand[1], out editId)) return 1;
                    if (!keeper.CheckById(editId) || !keeper.EditById(Int64.Parse(userCommand[1]), NoteAsker.AskEditNote()))
                        Console.WriteLine("Записи с таким номером не существует!");
                    else
                        Console.WriteLine("Запись успешно изменена.");
                    break;
                case "exit":
                    if (userCommand[1] != "") return 1;
                    else return 2;
                default:
                    if (userCommand[0] == "") Console.WriteLine("Введите команду.");
                    else Console.WriteLine("Команда '" + userCommand[0] + "' не найдена! Введите 'help' для просмотра списка команд.");
                    break;
            }
            return 0;
        }

        private static string GetHelp()
        {
            return "show - Краткий просмотр всех записей\n" +
                "showx - Полный просмотр всех записей\n" +
                "add - Добавить запись\n" +
                "remove <id> - Удалить запись\n" +
                "edit <id> - Изменить запись\n" +
                "exit - Выйти";
        }
    }
}
