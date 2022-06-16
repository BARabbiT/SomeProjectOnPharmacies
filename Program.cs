using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeProjectOnPharmacies.Menu;
using SomeProjectOnPharmacies.Interfaces;
using SomeProjectOnPharmacies.DB;
using SomeProjectOnPharmacies.IO;

namespace SomeProjectOnPharmacies
{
    class Program
    {
        static void Main(string[] args)
        {
            IIOEditor _consoleEditor = new ConsoleEditor();
            BaseMenuAbstract mainMenu = new MainMenu();
            BaseMenuAbstract currentMenu = mainMenu;
            _consoleEditor.SendMessage(currentMenu.GetMenuVisual());

            DBWorker dbWorker = new DBWorker();
            if (!dbWorker.TryInitialiseTable(out string error))
            {
                _consoleEditor.SendMessage(error);
            }

            bool needExit = false;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    if (currentMenu.TryGetSubAction(number, out currentMenu))
                    {
                        _consoleEditor.SendMessage(currentMenu.GetMenuVisual());
                    }
                    else
                    {
                        currentMenu = mainMenu;
                        _consoleEditor.SendMessage(currentMenu.GetMenuVisual());
                    }
                }
                else
                {
                    _consoleEditor.SendMessage("Некорретный ввод, используйте цифры для указания пункта меню.");
                    _consoleEditor.SendMessage("Выберите пункт меню:");
                }
                
            }
            while (!needExit);
        }
    }
}
