using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeProjectOnPharmacies.Menu;
using SomeProjectOnPharmacies.Interfaces;

namespace SomeProjectOnPharmacies
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseMenuAbstract mainMenu = new MainMenu();
            BaseMenuAbstract currentMenu = mainMenu;
            Console.WriteLine(currentMenu.GetMenuVisual());

            bool needExit = false;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    if (currentMenu.TryGetSubAction(number, out currentMenu))
                    {
                        Console.WriteLine(currentMenu.GetMenuVisual());
                    }
                    else
                    {
                        currentMenu = mainMenu;
                        Console.WriteLine(currentMenu.GetMenuVisual());
                    }
                }
                else
                {
                    Console.WriteLine("Некорретный ввод, используйте цифры для указания пункта меню.");
                    Console.WriteLine("Выберите пункт меню:");
                }
                
            }
            while (!needExit);
        }
    }
}
