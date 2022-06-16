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
    public class StoreMenu : BaseMenuAbstract
    {
        private DBWorker _dbWorker;
        new IIOEditor _consoleEditor;

        public StoreMenu()
        {
            Name = "Меню работы со складами.";
            MenuPoint = new List<string>() {
                "Список складов",
                "Добавить",
                "Удалить",
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
                            if (_dbWorker.TryGetStoresFromDB(out var storeData, out string error))
                            {
                                _consoleEditor.SendMessage("_____________________________________________________");
                                if (storeData.Count() > 0)
                                {
                                    foreach (var store in storeData)
                                    {
                                        _consoleEditor.SendMessage($"\n\r Название: {store.Name} | Аптека: {store.ShopName}");
                                    }
                                }
                                else
                                {
                                    _consoleEditor.SendMessage("Складов в БД не найдено.");
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
                                    "Аптека"
                                });
                            if (_dbWorker.TryAddStoreToDB(shopData, out string error))
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
                            if (_dbWorker.TryDeleteStoreFromDB(shopData, out string error))
                            {
                                _consoleEditor.SendMessage("Успешно удалено.");
                            }
                            else
                            {
                                _consoleEditor.SendMessage($"Ошибка при удалении: \n\r {error}");
                            }
                            break;
                        }
                    case 4:
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
