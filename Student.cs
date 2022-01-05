using System;
using System.IO;
using System.Linq;

namespace EducationalCenter
{
    internal class StudentType : Adminstration, IPeronalData
    {
        public GroupType Group { get; set; }
        public decimal Balance { get; set; }

        public readonly static string groupPath = "Groups.txt";
        public readonly static string studentPath = "Students.txt";


        public void AddStudent(StudentType student)
        {
            //checking if user's contact already exist
            bool result = CheckIfAlreadyExist(studentPath, Contact);

            //saving data into file
            if (result == false)
            {
                student.Balance = 0;
                //specifying which course user will study
                string GroupName = Reception.ShowGroupsToNewStudent();
                using (StreamWriter sw = File.AppendText(studentPath))
                {
                    sw.Write($"\n{student.FirstName} {student.LastName} {student.Age} {student.Contact} {GroupName} {student.Balance} ");
                    sw.Close();
                    //changing console text color
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nO'quvchi muaffaqiyatli qo'shildi");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                //changing console text color
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Telfon raqam mavjutligi aniqlandi, iltimos qaytadan urunib ko'ring");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }//Done

        public bool DeleteStudent()
        {
            //input data
            bool result = false;
            Console.Write("\nO'quvchining telefon raqamini kiritng: ");
            Contact = Console.ReadLine();

            int succesChecker = 0;
            //saving file data to string array and deleating file
            string[] Lines = File.ReadAllLines(studentPath);
            File.Delete(studentPath);

            //checking if data equal with input, than true continuing, if false saving them into new file
            foreach (string Line in Lines)
            {
                if (Line != "")
                {
                    //separating data of file
                    string[] data = Line.Split(" ");

                    //missing data if true
                    if (data[3] == Contact) succesChecker++;

                    else
                    {
                        //updating false data
                        using (StreamWriter sw = File.AppendText(studentPath))
                        {
                            sw.Write($"\n{data[0]} {data[1]} {data[2]} {data[3]} {data[4]} {data[5]}");
                        }
                    }
                }
            }
            //sharing result with user
            if (succesChecker == 0)
            {
                //changing console text color
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nO'quvchi topilmadi\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nO'quvchi muaffaqiyatli o'chirildi\n");
                result = true;
                Console.ForegroundColor = ConsoleColor.White;
            }
            return result;
        } //Done

        public void UpdateStudent()
        {
            StudentType student = new StudentType();
            bool result = DeleteStudent();
            if (result == true)
            {
                #region input data
                Console.Write("Yangi ism kiriting: ");
                student.FirstName = Console.ReadLine();
                student.FirstName = student.FirstName.Capitalize();

                Console.Write($"{student.FirstName}ning familyasini kiriting: ");
                student.LastName = Console.ReadLine();
                student.LastName = student.LastName.Capitalize();

                Console.Write($"{student.FirstName} {student.LastName}ning yoshni kiriting: ");
                student.Age = int.Parse(Console.ReadLine());

                Console.Write($"{student.FirstName} {student.LastName}ning telfon raqamini kiriting: ");
                student.Contact = Console.ReadLine();
                #endregion
                AddStudent(student);
            }
        } //Done

        public static void SearchStudent(StudentType student)
        {
            //saving file data into string array
            int succesChecker = 0;
            //getting file data into string array
            string[] Data = File.ReadAllLines(studentPath);

            foreach (string DataItem in Data)
            {
                if (DataItem != "")
                {
                    string[] sortData = DataItem.Split();
                    //checking if data equal
                    if (student.Contact == sortData[3])
                    {
                        //sharing result with user
                        //changing console text color
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nIsm: {sortData[0]}\n" +
                            $"Familya: {sortData[1]}\n" +
                            $"Yosh: {sortData[2]}\n" +
                            $"Kontakt: {sortData[3]}\n" +
                            $"Kurs: {sortData[4]}\n" +
                            $"To'langan summa: {sortData[5]} so'm\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        succesChecker++;
                        break;
                    }
                }
            }

            if (succesChecker == 0)
            {
                //changing console text color
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("O'quvchi topilmadi");
                Console.ForegroundColor = ConsoleColor.White;
            }
        } //Done

        public void PayForStudy(StudentType student)
        {
            string[] Data = File.ReadAllLines(studentPath);
            int succesChecker = 0;

            foreach (string DataItem in Data)
            {
                if (DataItem != "")
                {
                    string[] sortData = DataItem.Split();
                    //checking if data found
                    if (student.Contact == sortData[3])
                    {
                        succesChecker++;

                        //getting group cost
                        decimal GroupCost = SpecifyerOfGroupCost(sortData[4]);

                        //getting data into obeject
                        student.FirstName = sortData[0];
                        student.LastName = sortData[1];
                        student.Balance = Convert.ToDecimal(sortData[5]);
                        //informing if student has already payed for the course
                        if (GroupCost <= student.Balance)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n{student.FirstName} {student.LastName} uchun to'lov amalga oshirilgan");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        //informing how much user has to pay
                        else if (student.Balance < GroupCost)
                        {
                            decimal MustPay = GroupCost - student.Balance;
                            Console.WriteLine($"\n{student.FirstName} {student.LastName} {sortData[4]} kursi uchun {MustPay} so'm to'lashi lozim");
                            Console.Write("\nSummani kiriting: ");
                            try
                            {
                                decimal payment = decimal.Parse(Console.ReadLine());
                                if (payment > MustPay)
                                {
                                    //informing if user payed more than needed
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"\n{MustPay} so'mdan ortiq summa kirita olmaysiz, iltimos qaytadan urunib ko'ring");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                                else if (payment <= MustPay)
                                {
                                    //counting bill of the user
                                    MustPay = GroupCost - (student.Balance + payment);

                                    student.Balance = student.Balance + payment;
                                    sortData[5] = Convert.ToString(student.Balance);

                                    File.Delete(studentPath);
                                    foreach (string Line in Data)
                                    {
                                        if (Line != "")
                                        {
                                            string[] data = Line.Split(" ");

                                            if (data[3] == student.Contact)
                                            {
                                                //updating data of file
                                                using (StreamWriter sw = File.AppendText(studentPath))
                                                {
                                                    sw.Write($"\n{sortData[0]} {sortData[1]} {sortData[2]} {sortData[3]} {sortData[4]} {sortData[5]}");
                                                }
                                            }

                                            else
                                            {
                                                //writing data into file
                                                using (StreamWriter sw = File.AppendText(studentPath))
                                                {
                                                    sw.Write($"\n{data[0]} {data[1]} {data[2]} {data[3]} {data[4]} {data[5]}");
                                                }
                                            }
                                        }
                                    }
                                    //sharing result with user
                                    //changing color of console text
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"\nTo'lov muaffaqiyatli amlaga oshirldi, o'quvchida {MustPay} so'm miqdorda qarzdorlik qoldi");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                }
                            }
                            catch
                            {
                                Console.Clear();
                                //sharing result with user
                                //changing color of console text
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nXatolik aniqlandi, iltimos qaytadan urunib ko'ring");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }

                    }
                }
            }
            if (succesChecker < 1)
            {
                //sharing result with user
                //changing color of console text
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nO'quvchi topilmadi");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }//Done

        public static bool CheckIfAlreadyExist(string path, string Contact)
        {
            string[] Data = File.ReadAllLines(path);
            foreach (string DataItem in Data)
            {
                if (DataItem != "")
                {
                    string[] sortData = DataItem.Split();
                    if (sortData[3] == Contact) return true;
                }
            }
            return false;
        } //Done

        public static decimal SpecifyerOfGroupCost(string NameOfGroup)
        {
            decimal result = 0;
            string[] groups = File.ReadAllLines(groupPath).ToArray();
            foreach (string group in groups)
            {
                if (group != "")
                {
                    string[] groupData = group.Split(" ");
                    if (groupData[0] == NameOfGroup) result = decimal.Parse(groupData[4]);
                }
            }
            return result;
        }//Done
    }
}
