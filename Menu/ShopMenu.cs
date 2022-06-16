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
    public class ShopMenu : BaseMenuAbstract
    {
        private DBWorker _dbWorker;
        new IIOEditor _consoleEditor;

        public ShopMenu()
        {
            Name = "Меню работы с аптеками.";
            MenuPoint = new List<string>() { 
                "Список аптек",
                "Добавить",
                "Удалить",
                "Показать список товаров",
                "Вернуться назад"
            };

            _dbWorker = new DBWorker();
            _consoleEditor = new ConsoleEditor();
        }

        public override bool TryGetSubAction(int itemNumber, out BaseMenuAbstract someMenu)
        {
            if (MenuPoint.Count() >= itemNumber)
            {
                someMenu = this;
                switch (itemNumber)
                {
                    case 1:
                        {
                            if (_dbWorker.TryGetShopsFromDB(out var shopData, out string error))
                            {
                                _consoleEditor.SendMessage("_____________________________________________________");
                                if (shopData.Count() > 0)
                                {
                                    foreach (var shop in shopData)
                                    {
                                        _consoleEditor.SendMessage($"\n\r Название: {shop.Name} | Адрес: {shop.Addres} | Телефон: {shop.Phone}");
                                    }
                                }
                                else
                                {
                                    _consoleEditor.SendMessage("Аптек в БД не найдено.");
                                }
                                _consoleEditor.SendMessage("_____________________________________________________");
                            }
                            else
                            {
                                _consoleEditor.SendMessage($"Ошибка при добавлении: \n\r {error}");
                            }
                            break;
                        }
                    case 2:
                        {
                            Dictionary<string, string> shopData = _consoleEditor.GetDataFromConsole(
                                new List<string>()
                                {
                                    "Название",
                                    "Адрес",
                                    "Телефон"
                                });
                            if (_dbWorker.TryAddShopToDB(shopData, out string error))
                            {
                                _consoleEditor.SendMessage("Успешно добавлено.");
                            }
                            else
                            {
                                _consoleEditor.SendMessage($"Ошибка при добавлении: \n\r {error}");
                            }
                            break;
                        }
                    case 3:
                        {
                            Dictionary<string, string> shopData = _consoleEditor.GetDataFromConsole(
                                new List<string>()
                                {
                                    "Название"
                                });
                            if (_dbWorker.TryDeleteShopFromDB(shopData, out string error))
                            {
                                _consoleEditor.SendMessage("Успешно удалено.");
                            }
                            else
                            {
                                _consoleEditor.SendMessage($"Ошибка при добавлении: \n\r {error}");
                            }
                            break;
                        }
                    case 4:
                        {
                            Dictionary<string, string> shopData = _consoleEditor.GetDataFromConsole(
                                new List<string>()
                                {
                                    "Название"
                                });
                            if (_dbWorker.TryShowNomenclatureFromShop(shopData,out string showData, out string error))
                            {
                                _consoleEditor.SendMessage("_____________________________________________________");
                                if (!string.IsNullOrEmpty(showData))
                                {
                                    _consoleEditor.SendMessage(showData);
                                }
                                else
                                {
                                    _consoleEditor.SendMessage("По данной аптеке партий не указано");
                                }
                                _consoleEditor.SendMessage("_____________________________________________________");

                            }
                            else
                            {
                                _consoleEditor.SendMessage($"Ошибка при удалении: \n\r {error}");
                            }

                            break;
                        }
                    case 5:
                        {
                            return false;
                        }
                }
                _consoleEditor.SendMessage("Для продолжения нажмите любую клавишу....");
                _consoleEditor.GetData();
                return true;
            }
            else
            {
                someMenu = this;
                return true;
            }
        }
    }
}
