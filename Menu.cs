using System;

namespace EducationalCenter
{
    internal class MainMenu
    {
        public static void Menu()
        {
            

            Console.ForegroundColor = ConsoleColor.White;
            //ensuring program never ends
            while (true)
            {
                Console.Write("\nAdminstratsiya(1) | Kurslar haqida ma'lumot(2) | Dasturdan chiqish(3)\n" +
                    ">>> ");
                string mainChoice = Console.ReadLine();

                if (mainChoice == "1")
                {
                    Console.Clear();
                    Adminstration admin = new Adminstration();
                    #region input data

                    Console.Write("\nLoginingizni kiriting: ");
                    admin.Login = Console.ReadLine();


                    Console.Write("Parolingizni kiriting: ");
                    admin.Password = Adminstration.ReadPassword();

                    #endregion
                    //checking if login and password exsist
                    bool result = Adminstration.IsAdmin(admin);
                    if (result == true)
                    {
                        Console.Clear();
                        //chaning color of console text
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nLogin va parol tasdiqlandi\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        OnlyAdmin();
                    }

                    else
                    {
                        Console.Clear();

                        //changing color of console text
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLogin yoki parol xato kiritildi\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }


                }

                else if (mainChoice == "2")
                {
                    Console.Clear();
                    Reception.ShowGroups();
                }

                else if (mainChoice == "3")
                {
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine("Dastur tugatildi");
                    Console.ForegroundColor = ConsoleColor.White;
                    Environment.Exit(0);
                }

                else
                {
                    Console.Clear();
                    //chaning text of console color
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHech narsa topilmadi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

        }

        public static void StudentMenu()
        {
            StudentType student = new StudentType();
            Console.Clear();
            while (true)
            {
                Console.Write("\nTo'lovni qabul qilish(1)   |    O'quvchi haqidagi ma'lumotlar(2)   |     Ma'lumotlarini yangilash(3)\n" +
                              "O'quvchi qo'shish(4)       |    O'quvchini o'chirish(5)            |     Ortga qaytish(6)\n" +
                    ">>> ");
                string StudentChoice = Console.ReadLine();
                if (StudentChoice == "1")
                {
                    Console.Clear();
                    Console.Write($"\nO'quvchining telefon raqamini kiritng: ");
                    student.Contact = Console.ReadLine();
                    student.PayForStudy(student);
                }
                else if (StudentChoice == "2")
                {
                    Console.Clear();
                    Console.Write("O'quvchining telefon raqamini kiriting: ");
                    student.Contact = Console.ReadLine();
                    StudentType.SearchStudent(student);
                }
                else if (StudentChoice == "3")
                {
                    Console.Clear();
                    student.UpdateStudent();
                }
                else if (StudentChoice == "4")
                {
                    Console.Clear();
                    try
                    {
                        #region input data
                        Console.Write("\nO'quvchning ismni kiriting: ");
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

                        student.AddStudent(student);
                    }
                    catch
                    {
                        //chaging text color of console
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nXatolik aniqlandi, iltimos qaytadan kiriting\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else if (StudentChoice == "5")
                {
                    Console.Clear();
                    student.DeleteStudent();
                }
                else if (StudentChoice == "6")
                {
                    Console.Clear();
                    OnlyAdmin();
                }

                else
                {
                    Console.Clear();
                    //chaging text color of console
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHech narsa topilamdi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public static void GroupMenu()
        {
            GroupType group = new GroupType();
            Console.Clear();
            while (true)
            {
                Console.Write("Yangi kurs qo'shish(1) | Kursni o'chirish(2) | Ortga qaytish(3) \n" +
                    ">>> ");
                string GroupChoice = Console.ReadLine();
                if (GroupChoice == "1")
                {
                    Console.Clear();
                    try
                    {
                        #region input data
                        Console.Write("\nKurs nomini kiriting: ");
                        group.Name = Console.ReadLine();
                        group.Name = group.Name.Capitalize();

                        Console.Write("Kurs narxini kiriting: ");
                        group.Cost = decimal.Parse(Console.ReadLine());

                        Console.Write("Kurs o'qituvchisining ismini kiriting: ");
                        string FirstName = Console.ReadLine();
                        FirstName = FirstName.Capitalize();

                        Console.Write($"{FirstName}ning familyasini kiriting: ");
                        string LastName = Console.ReadLine();
                        LastName = LastName.Capitalize();

                        Console.Write($"{FirstName} {LastName}ning yoshini kiriting: ");
                        int Age = int.Parse(Console.ReadLine());
                        #endregion
                        GroupType.AddGroup(group, FirstName, LastName, Age);
                    }
                    catch
                    {
                        //changing color of console text
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nXatolik aniqlandi, iltimos qaytadan urinib ko'ring\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                }

                else if (GroupChoice == "2")
                {
                    Console.Clear();

                    //changing color of console text
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nEslatma: Kursni o'chirish, kurs o'qituvchisi va kurs o'quvchilari ham o'chirilishiga olib keladi!\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write("Kurs nomini kiriting: ");
                    string GroupName = Console.ReadLine();
                    GroupType.DeleteGroup(GroupName);

                }
                else if (GroupChoice == "3")
                {
                    Console.Clear();
                    OnlyAdmin();
                }
                else
                {
                    Console.Clear();
                    //changing color of console text
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHech narsa topilmadi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public static void AdminMenu()
        {

            Adminstration admin = new Adminstration();
            while (true)
            {
                Console.Write("Admin qo'shish(1) | Adminni o'chirish(2) | Adminlar ro'yhati(3) | Ortga qaytish(4)\n" +
                                   ">>> ");
                string adminChoice = Console.ReadLine();
                if (adminChoice == "1")
                {
                    Console.Clear();
                    try
                    {
                        #region input data
                        Console.Write("Yangi adminstratorning ismni kiriting: ");
                        admin.FirstName = Console.ReadLine();
                        admin.FirstName = admin.FirstName.Capitalize();

                        Console.Write($"{admin.FirstName}ning familyasini kiriting: ");
                        admin.LastName = Console.ReadLine();
                        admin.LastName = admin.LastName.Capitalize();

                        Console.Write($"{admin.FirstName} {admin.LastName}ning yoshni kiriting: ");
                        admin.Age = int.Parse(Console.ReadLine());

                        Console.Write($"{admin.FirstName} {admin.LastName}ning telfon raqamini kiriting: ");
                        admin.Contact = Console.ReadLine();

                        place:
                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine("\n1.Asosiy admin\n" +
                            "2.Yordamchi admin");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.Write($"{admin.FirstName} {admin.LastName}ning adminstratorlik darajasini kiriting: ");
                        string role = Console.ReadLine();
                        if(role != "1" && role != "2") 
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nHech narsa toplimadi\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            goto place;
                        }

                        Console.Write("Login yarating: ");
                        admin.Login = Console.ReadLine();

                        Console.Write("Parol yarating: ");
                        admin.Password = Console.ReadLine();
                        #endregion
                        Adminstration.AddAdmistrator(admin, role);
                    }
                    catch
                    {
                        //chaning text of console color
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nXatolik aniqlandi, iltimos qaytadan urunib ko'ring\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else if (adminChoice == "2")
                {
                    Console.Clear();
                    Adminstration.DeleteAdmin();
                }

                else if (adminChoice == "3")
                {
                    Console.Clear();
                    Adminstration.ShowAdmins();
                }

                else if (adminChoice == "4")
                {
                    Console.Clear();
                    OnlyAdmin();
                }

                else
                {
                    Console.Clear();
                    //chaning text of console color
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHech narsa topilmadi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public  static void OnlyAdmin()
        {   
            Adminstration admin = new Adminstration();
            while (true)
            {

                Console.Write("\nO'quvchilar parametrlari(1) | Kurslar parametrlari(2) | Adminstratorlar parametrlari(3) | Asosiy menuga qaytish(4) \n" +
                    ">>> ");

                string adminChoice = Console.ReadLine();
                if (adminChoice == "1")
                {
                    Console.Clear();
                    StudentMenu();
                }

                else if (adminChoice == "2")
                {
                    Console.Clear();
                    GroupMenu();
                }

                else if (adminChoice == "3")
                {
                    Console.Clear();
                    #region input data
                    Console.ForegroundColor = ConsoleColor.Red;
                    //Console.WriteLine("\nEslatma: Admistratorlar parametrlariga kirish uchun Asosiy admin bo'lishingiz lozim!\n");
                    Console.WriteLine("Admistratorlar parametrlariga faqat asosiy adminlar kira oladi!");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Write("\nLoginingizni kiriting: ");
                    admin.Login = Console.ReadLine();

                    Console.Write("Parolingizni kiriting: ");
                    admin.Password = Adminstration.ReadPassword();
                    #endregion 

                    bool result = Adminstration.IsMainAdmin(admin);
                    if (result == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nLogin va Parol tasdiqlandi\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        AdminMenu();
                    }

                    else if (result == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nAdmistratorlar parametrlariga faqat Asosiy admin kira oladi!\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                else if (adminChoice == "4")
                {
                    Console.Clear();
                    Menu();
                }

                else
                {
                    Console.Clear();
                    //changing color of console text
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nHech narsa topilmadi\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}

