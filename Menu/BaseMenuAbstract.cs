using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeProjectOnPharmacies.Interfaces;
using SomeProjectOnPharmacies.DB;
using SomeProjectOnPharmacies.IO;

namespace SomeProjectOnPharmacies.Menu
{
    public abstract class BaseMenuAbstract
    {
        public string Name { get; set; }
        public List<string> MenuPoint { get; set; }
        public IIOEditor _consoleEditor { get; set; }

        public BaseMenuAbstract()
        {
            _consoleEditor = new ConsoleEditor();
        }

        public virtual string GetMenuVisual()
        {
            _consoleEditor.Clear();
            _consoleEditor.SendMessage(Name);
            _consoleEditor.SendMessage("_____________________________________________________");
            string result = string.Empty;
            for (int i = 1; i <= MenuPoint.Count(); i++)
            {
                result += $"{i}){MenuPoint[i - 1]}\n\r";
            }
            result += "_____________________________________________________ \n\r";
            return result += "Выберите пункт меню:";
        }

        public abstract bool TryGetSubAction(int itemNumber, out BaseMenuAbstract someMenu);
    }
}
