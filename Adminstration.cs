using ConsoleTables;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EducationalCenter
{
    internal class Adminstration : IPeronalData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public RoleOfAdmin RoleOfAdmin { get; set; }

        public readonly static string adminsPath = "Adminstrators.txt";

        public static bool IsMainAdmin(Adminstration admin) 
        {
            bool result = false;
            string[] Data = File.ReadAllLines(adminsPath);

            foreach (string line in Data)
            {
                if (line != "")
                {
                    string[] infoOfAdmin = line.Split(" ");
                    if (infoOfAdmin[3] == admin.Login)
                    {
                        //checking if password and login true
                        byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(admin.Password);
                        byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                        admin.Password = ByteArrayToString(tmpHash);
                        if (infoOfAdmin[4] == admin.Password && infoOfAdmin[6] == "MainAdmin") result = true ;
                    }
                }
            }
            return result;
        }

        public static string ReadPassword()
        {
            string password = "";
            while (true)
            {
                place:
                try
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            return null;
                        case ConsoleKey.Enter:
                            return password;
                        case ConsoleKey.Backspace:
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                            break;
                        default:
                            password += key.KeyChar;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("*");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                catch
                {
                    goto place;
                }
            }
        }

        public static bool IsAdmin(Adminstration admin)
        {
            bool result  = false;
            //getting file data into string array
            string[] Data = File.ReadAllLines(adminsPath);
            
            foreach (string line in Data)
            {
                if (line != "")
                {
                    string[] infoOfAdmin = line.Split(" ");
                    if (infoOfAdmin[3] == admin.Login) 
                    {   
                        //checking if password and login true
                        byte [] tmpSource = ASCIIEncoding.ASCII.GetBytes(admin.Password);
                        byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                        admin.Password = ByteArrayToString(tmpHash);
                        if(infoOfAdmin[4] == admin.Password) result = true;
                    }
                }
            }
            return result;
        } //Done

        public static void AddAdmistrator(Adminstration admin, string role)
        {   
            int succesChecker = 0;
            //getting file data into string array
            bool result = admin.CheckIfAlreadyExist(adminsPath, admin);

            if (result == false)
            {
                //hashing input password
                byte[] tmpByteHashedPassword = HashThePassword(admin);
                admin.Password = ByteArrayToString(tmpByteHashedPassword);

                //writing data to file
                using (StreamWriter sw = File.AppendText(adminsPath))
                {
                    if(role == "1") sw.Write($"\n{admin.FirstName} {admin.LastName} {admin.Age} {admin.Login} {admin.Password} {admin.Contact} MainAdmin");
                    else sw.Write($"\n{admin.FirstName} {admin.LastName} {admin.Age} {admin.Login} {admin.Password} {admin.Contact} AssistantAdmin");
                    sw.Close();
                    succesChecker++;
                }

                //sharing result with user
                if (succesChecker == 0)
                {
                    //changing color of console text
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nXatolik aniqlandi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                else
                {
                    //changing color of console text
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nAdminstrator muaffaqiyatli qo'shildi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                //changing color of console text
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nTelfon raqam mavjutligi aniqlandi, iltimos qaytadan urunib ko'ring\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }//Done

        public static void DeleteAdmin()
        {
            Console.Write("\nO'chirmoqchi bo'lgan adminstratorning telefon raqamini kiriting: ");
            string Contact = Console.ReadLine();

            int succesChecker = 0;
            //saving file data to string array and deleating file
            string[] Lines = File.ReadAllLines(adminsPath);
            File.Delete(adminsPath);

            //checking if data equal with input, than true continuing, if false saving them into new file
            foreach (string Line in Lines)
            {
                if (Line != "")
                {
                    string[] data = Line.Split(" ");

                    //ignoring data if true
                    if (data[5] == Contact) succesChecker++;

                    //writng data into file
                    else
                    {
                        using (StreamWriter sw = File.AppendText(adminsPath))
                        {
                            sw.Write($"\n{data[0]} {data[1]} {data[2]} {data[3]} {data[4]} {data[5]} {data[6]}");
                        }
                    }
                }
            }
            //sharing result with user
            if (succesChecker == 0)
            {
                //changing color of console text
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAdminstrator topilmadi\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                //changing color of console text
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAdminstrator muaffaqiyatli o'chirildi\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }//Done

        public static void ShowAdmins()
        {
            string[] admins = File.ReadAllLines("Adminstrators.txt").ToArray();

            var table = new ConsoleTable("Admin", "Yosh","Telefon raqam", "Login", "Adminstratorlik darajasi");
            foreach (string admin in admins)
            {
                if (admin != "")
                {
                    string[] data = admin.Split(" ");
                    table.AddRow(data[0] + " " + data[1], data[2], data[5], data[3], data[6]);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            table.Write();
            Console.ForegroundColor = ConsoleColor.White;

        } //Done

        #region Hashing
        public static byte [] HashThePassword(Adminstration admin)
        {

            //Create a byte array from source data
            byte [] tmpSource = ASCIIEncoding.ASCII.GetBytes(admin.Password);
            //hashing byte array into another byte array
            byte [] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            return tmpHash;
        }

        static string ByteArrayToString(byte[] arrInput)
        {
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (int i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        #endregion

        #region Polymorpism
        public virtual bool CheckIfAlreadyExist(string path, Adminstration admin)
        {
            string[] Data = File.ReadAllLines(path);
            foreach (string DataItem in Data)
            {
                if (DataItem != "")
                {
                    string[] sortData = DataItem.Split();
                    if (sortData[5] == admin.Contact) return true;
                }
            }
            return false;
        } //Done
        #endregion
    }
}
