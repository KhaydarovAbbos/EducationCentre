using ConsoleTables;
using System;
using System.IO;
using System.Linq;

namespace EducationalCenter
{
    internal static class Reception
    {
        public readonly static string groupPath = "Groups.txt";
        public readonly static string studentPath = "Students.txt";
        public static string ShowGroupsToNewStudent()
        {
            int succesChecker = 0;
            while (true) {
                string[] groups = File.ReadAllLines(groupPath).ToArray();
                Console.WriteLine("");
                foreach (string group in groups)
                {
                    if (group != "")
                    {
                        string[] groupData = group.Split(" ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(groupData[0]);
                        Console.ForegroundColor= ConsoleColor.White;
                    }
                }
                Console.Write("\nKurslardan birini kiriting: ");
                string choosenGroup = Console.ReadLine();

                foreach (string group in groups)
                {
                    if (group != "")
                    {
                        string[] groupData = group.Split(" ");
                        if (groupData[0] == choosenGroup)
                        {
                            succesChecker++;
                            return choosenGroup;
                        }
                    }
                }
                if (succesChecker == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Kurs topilmadi");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }//Done

        public static void ShowGroups()
        {
            string [] students = File.ReadAllLines(studentPath).ToArray();
            string[] groups = File.ReadAllLines(groupPath).ToArray();

            Console.WriteLine($"Bizning o'quv markazimizda {students.Count()} ta o'quvchi va {groups.Count()} ta kurs mavjut");
            Console.WriteLine("Quyida har bir kurs bilan yaqindan tanishishingiz mumkun: ");

            var table = new ConsoleTable("Kurs nomi", "Kurs o'qituvchisi", "Kurs narxi");
            foreach (string group in groups)
            {
                if (group != "")
                {
                    string[] data = group.Split(" ");
                    table.AddRow(data[0], data[1] + " " + data[2], data[4]);

                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            table.Write();
            Console.ForegroundColor = ConsoleColor.White;
        }//Done
    }
} 