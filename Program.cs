using System;

namespace EducationalCenter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("EduCenter Manager [Version 0.0.1]\n" +
                "(c) Najot ta'lim Corparation. All rights reserved.");
            Console.ForegroundColor = ConsoleColor.White;
            MainMenu.Menu();
        }
    }
    
}
