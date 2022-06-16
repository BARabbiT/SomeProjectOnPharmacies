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
    class BatchMenu : BaseMenuAbstract
    {
        private DBWorker _dbWorker;
        IIOEditor _consoleEditor;

        public BatchMenu()
        {
            Name = "Меню работы с партиями.";
            MenuPoint = new List<string>() {
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
                            Dictionary<string, string> shopData = _consoleEditor.GetDataFromConsole(
                                new List<string>()
                                {
                                    "Товар",
                                    "Склад",
                                    "Количество"
                                });
                            if (_dbWorker.TryAddBatchToDB(shopData, out string error))
                            {
                                _consoleEditor.SendMessage("Успешно добавлено.");
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
                                    "Товар",
                                    "Склад"
                                });
                            if (_dbWorker.TryDeleteBatchFromDB(shopData, out string error))
                            {
                                _consoleEditor.SendMessage("Успешно удалено.");
                            }
                            else
                            {
                                _consoleEditor.SendMessage($"Ошибка при добавлении: \n\r {error}");
                            }
                            break;
                        }
                    case 3:
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
