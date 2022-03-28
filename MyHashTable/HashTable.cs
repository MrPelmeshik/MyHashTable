using System;
using System.Globalization;

namespace MyHashTable
{
    public class HashTable
    {
        #region Поля
        
        /// <summary>
        /// Максимальный размер таблицы
        /// </summary>
        //private static readonly int _maxSize = 255;

        /// <summary>
        /// Ограничение по количеству попыток разрешить коллизию
        /// </summary>
        private static readonly int _numberAttempts = 50;

        /// <summary>
        /// Количество занятых ячеек
        /// </summary>
        private int _counterOccupiedCell = 0;

        /// <summary>
        /// Размер исходной таблицы со сзначениями
        /// </summary>
        private int _sizeTable = 0;

        /// <summary>
        /// Размер полученной хеш-таблицы
        /// </summary>
        private int _sizeHashTable = 0;

        /// <summary>
        /// Сумма шагов, потребовавшихся для определения ключа
        /// </summary>
        private int _counterAttempts = 0;

        /// <summary>
        /// Список элементов
        /// </summary>
        private Item[] _items;
        
        /// <summary>
        /// Базовый класс для элементов
        /// </summary>
        internal class Item
        {
            /// <summary>
            /// Ключ
            /// </summary>
            public int? Key { get; internal set; }
            
            /// <summary>
            /// Значение
            /// </summary>
            public string? Value { get; internal set; }

            /// <summary>
            /// По этому индексу возникали коллизии
            /// </summary>
            public bool isCollision { get; internal set; }

            /// <summary>
            /// Пустой конструктор
            /// </summary>
            public Item()
            {
                Key = null;
                Value = null;
                isCollision = false;
            }
            
            /// <summary>
            /// Конструктор с инициализацией
            /// </summary>
            public Item(int key, string value)
            {
                Key = key;
                Value = value;
                isCollision = false;
            }
        }
        
        #endregion

        #region Публичные методы

        /// <summary>
        /// Конструктор 
        /// </summary>
        /// <param name="sizeTable">Размер исходных данных</param>
        public HashTable(int sizeTable)
        {
            _sizeTable = sizeTable;
            _sizeHashTable = _sizeTable * 3 / 2;
            _items = new Item[_sizeHashTable];

            for (int i = 0; i < _sizeHashTable; i++)
            {
                _items[i] = new Item();
            }
        }

        /// <summary>
        /// Метод вывода хеш-таблицы на экран
        /// </summary>
        public void OutputHashTable()
        {
            Console.Out.WriteLine("\nHashTable:");
            int n = _items.Length / 3;
            for (int i = 0; i < n + 1; i++)
            {
                OutputColumn(i);
                OutputColumn(n + 1 + i);
                OutputColumn(n + n + 2 + i);

                Console.Out.Write("\n");
            }

            void OutputColumn(int i)
            {
                Console.Out.Write(
                    i < _items.Length
                        ? _items[i].Value != null
                            //? $"\t{i} > \t{_items[i].Key} ({_items[i].Value})"
                            ? $"\t{i} > \t{_items[i].Key}"
                            : $"\t{i} > \t\t"
                        : $"\t\t\t");
            }
        }
        
        /// <summary>
        /// Метод вывода информации информации о ХТ
        /// </summary>
        public void OutputInfo()
        {
            Console.Out.WriteLine();
            Console.Out.WriteLine($"Коэффициент заполнения: {_GetFilling()}");
            Console.Out.WriteLine($"Среднее число шагов: {_GetAverageNumberSteps()}");
        }

        /// <summary>
        /// Поиск индекса элемента по значению 
        /// </summary>
        public bool Find(int targetKey, out int? key)
        {
            key = null;
            var counterAttempts = 0; // Счетчик шагов
            var isFound = false;
            int hTargetValue = 0;
            do
            {
                hTargetValue = (_GetHash(targetKey) + counterAttempts * counterAttempts) % _sizeHashTable;
                counterAttempts++;

                if (_items[hTargetValue].Key == targetKey)
                {
                    isFound = true;
                    Console.Out.WriteLine(
                        $"RES>>\tПо ключу {targetKey} найдено значение \"{_items[hTargetValue].Value}\" за {counterAttempts} шагов по индексу {hTargetValue}");
                    key = hTargetValue;
                    return true;
                }

                if (counterAttempts >= _numberAttempts)
                {
                    Console.Out.WriteLine($"RES>>\tПо ключу {targetKey} не получилось найти элемент за {_numberAttempts} шагов");
                    return false;
                }
                    
            } while (_items[hTargetValue].isCollision);

            Console.Out.WriteLine($"RES>>\tПо ключу {targetKey} не получилось найти элемент");
            return false;
        }

        /// <summary>
        /// Добавить элемент
        /// </summary>
        public bool Add(int addkey, string addValue)
        {
            if(_GetFilling() > 0.9)
                Console.Out.WriteLine("WRN>>\tХеш-таблица заполнена более чем на 90%");
            
            int counterAttempts = 0;
            bool resolvedCollision = false;
            do
            {
                int key = (_GetHash(addkey) + counterAttempts * counterAttempts) % _sizeHashTable;

                if (_items[key].Key == null)
                {
                    _items[key].Key = addkey;
                    _items[key].Value = addValue;
                    _items[key].isCollision = true;
                    resolvedCollision = true;
                    _counterOccupiedCell++;
                    _counterAttempts += counterAttempts + 1;
                    Console.Out.WriteLine(
                        $"RES>>\tПо ключу {addkey} добавлено значение \"{addValue}\" за {counterAttempts} шагов по индексу {key}");
                    return true;
                }

                counterAttempts++;

            } while (counterAttempts < _numberAttempts && !resolvedCollision);

            Console.Out.WriteLine(
                $"RES>>\tПо ключу {addkey} неудалось добавить значение \"{addValue}\" (неудалось разрешить коллизию методом квадратичного пробирования за {_numberAttempts} шагов)");
            return false;
        }

        /// <summary>
        /// Удалить элемент по значению
        /// </summary>
        public bool Dlt(int dltKey)
        {
            if (Find(dltKey, out int? keyDltValue))
            {
                string dltValue = _items[(int) keyDltValue].Value;
                _items[(int) keyDltValue].Key = null;
                _items[(int) keyDltValue].Value = null;
                _counterOccupiedCell--;
                Console.Out.WriteLine($"RES>>\tПо ключу {dltKey} удалена запись со значением \"{dltValue}\" по индексу {keyDltValue}");
                return true;
            }

            return false;
        }
        
        #endregion

        #region Скрытые методы

        /// <summary>
        /// Хеш функция
        /// </summary>
        private static int _GetHash(int value)
        {
            return value / 1000 + value % 10;

        }
        
        /// <summary>
        /// Пересчитать количество занятых ячеек
        /// </summary>
        private void _UpdateOccupiedCell()
        {
            int counter = 0;
            foreach (var item in _items)
            {
                if (item.Value != null)
                    counter++;
            }

            _counterOccupiedCell = counter;
        }
        
        /// <summary>
        /// Получение коэффициента заполнения
        /// </summary>
        private double _GetFilling()
        {
            _UpdateOccupiedCell();
            return _sizeHashTable == 0 ? 0 : (double) _counterOccupiedCell / (double) _sizeHashTable;
        }

        /// <summary>
        /// Получение среднего числа шагов, потребовавшихся для поиска ключа
        /// </summary>
        private double _GetAverageNumberSteps()
        {
            return _sizeTable == 0 ? 0 : (double) _counterAttempts / (double) _sizeTable;
        }
        
        #endregion
    }
}