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
    class NomenclatureMenu : BaseMenuAbstract
    {
        private DBWorker _dbWorker;
        new IIOEditor _consoleEditor;

        public NomenclatureMenu()
        {
            Name = "Меню работы с товарами.";
            MenuPoint = new List<string>() {
                "Список товаров",
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
                            if (_dbWorker.TryGetNomenclaturesFromDB(out var nomenclaturesData, out string error))
                            {
                                _consoleEditor.SendMessage("_____________________________________________________");
                                if (nomenclaturesData.Count() > 0)
                                {
                                    foreach (var nomenclature in nomenclaturesData)
                                    {
                                        _consoleEditor.SendMessage($"\n\r Название: {nomenclature.Name}");
                                    }
                                }
                                else
                                {
                                    _consoleEditor.SendMessage("Товаров в БД не найдено.");
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
                                    "Название"
                                });
                            if (_dbWorker.TryAddNomenclatureToDB(shopData, out string error))
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
                            if (_dbWorker.TryDeleteNomenclatureFromDB(shopData, out string error))
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
                _consoleEditor.SendMessage($"некорретный пункт меню, выберите от 1 до {MenuPoint.Count}");
                return true;
            }
        }
    }
}
