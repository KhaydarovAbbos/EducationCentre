using System;
using System.IO;

namespace EducationalCenter
{
    internal class GroupType : Adminstration
    {
        public string Name { get; set; }
        public TeacherType Teacher { get; set; }
        public decimal Cost { get; set; }

        public readonly static string groupPath = ("Groups.txt");
        public readonly static string studentPath = "Students.txt";

        public static void AddGroup(GroupType group, string FirstName, string LastName, int Age)
        {
            //saving file data into string array and checking if it is already exsit            
            bool result = CheckIfAlreadyExist(groupPath, group);

            //adding new group to file
            if (result == false)
            {
                using (StreamWriter sw = File.AppendText(groupPath))
                {
                    sw.Write($"\n{group.Name} {FirstName} {LastName} {Age} {group.Cost}");
                    sw.Close();
                    //sharing result with user
                    //chaning text color of color
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nKurs muaffaqiyatli qo'shildi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                //chaning text color of color
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nKurs mavjutligi aniqlandi, iltimos qaytadan urunib ko'ring\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }//Done

        public static void DeleteGroup(string GroupName)
        {
            int succesChecker = 0;
            //saving file data into string array and deleting file 
            string[] Lines = File.ReadAllLines(groupPath);
            File.Delete(groupPath);

            //checking if data equal with input, than true continuing, if false saving them into new file
            using (StreamWriter sw = File.AppendText(groupPath))
            {
                foreach (string line in Lines)
                {
                    if (line != "")
                    {
                        string[] data = line.Split(" ");
              
                        if (data[0] == GroupName)
                        {
                            //opening students file end delating who studys in the group
                            string[] DataOfStudents = File.ReadAllLines(studentPath);
                            File.Delete(studentPath);

                            //checking if data equal with input, than true continuing, if false saving them into new file
                            foreach (string LineOfData in DataOfStudents)
                            {
                                string[] Data = LineOfData.Split(" ");
                                if (Data[0] != "")
                                {
                                    if (Data[4] == GroupName) continue;
                                    //writing data into file
                                    else
                                    {
                                        using (StreamWriter sw2 = File.AppendText(studentPath))
                                            sw2.Write($"\n{Data[0]} {Data[1]} {Data[2]} {Data[3]} {Data[4]} {Data[5]}");
                                    }
                                }
                            }
                            succesChecker++;
                        }
                        else sw.Write($"\n{line}");
                    }
                }
            }
            //sharing result with user

            if (succesChecker == 0)
            {   
                //chaning color of console text
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nKurs topilmadi\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (succesChecker > 0)
            {
                //chaning color of console text
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nKurs muaffaqiylatli o'chirildi");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }//Done

        public static bool CheckIfAlreadyExist(string path, GroupType group)
        {
            string[] Data = File.ReadAllLines(path);
            foreach (string DataItem in Data)
            {
                if (DataItem != "")
                {
                    string[] sortData = DataItem.Split();
                    if (sortData[0] == group.Name) return true;
                }
            }
            return false;
        } //Done
    }
}
