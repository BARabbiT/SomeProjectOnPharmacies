using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeProjectOnPharmacies.Interfaces;

namespace SomeProjectOnPharmacies.Menu
{
    public class MainMenu: BaseMenuAbstract
    {
        private List<BaseMenuAbstract> _subMenus;

        public MainMenu()
        {
            Name = "Основное меню.";
            _subMenus = new List<BaseMenuAbstract>()
            {
                new ShopMenu(),
                new StoreMenu(),
                new BatchMenu(),
                new NomenclatureMenu()
            };
        }
        public override string GetMenuVisual()
        {
            Console.Clear();
            Console.WriteLine(Name);
            Console.WriteLine("_____________________________________________________");
            string result = string.Empty;
            for (int i = 1; i <= _subMenus.Count(); i++)
            {
                result += $"{i}){_subMenus[i-1].Name} \n\r";
            }
            result += "_____________________________________________________ \n\r";
            return result += "Выберите пункт меню:";
        }

        public override bool TryGetSubAction(int itemNumber, out BaseMenuAbstract subMenu)
        {
            if (_subMenus.Count() >= itemNumber)
            {
                subMenu = _subMenus[itemNumber-1];
                return true;
            }
            else
            {
                subMenu = null;
                return false;
            }
        }
    }
}
