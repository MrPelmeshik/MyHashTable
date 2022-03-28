using System;
using System.Globalization;

namespace MyHashTable
{
    public class HashTable
    {
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
                            ? $"\t{i} > \t{_items[i].Value} ({_items[i].Key})"
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
            Console.Out.WriteLine($"Коэффициент заполнения: {GetFilling()}");
            Console.Out.WriteLine($"Среднее число шагов: {GetAverageNumberSteps()}");
        }

        /// <summary>
        /// Инициализация хеш таблицы значениями из заданного набора
        /// </summary>
        public void InitTableWithSet(int[] set)
        {
            _sizeTable = set.Length;
            _sizeHashTable = _sizeTable * 3 / 2;
            _items = new Item[_sizeHashTable];

            for (var i = 0; i < _sizeHashTable; i++)
            {
                _items[i] = new Item();
            }

            for (var i = 0; i < set.Length; i++)
            {
                if (!Add(set[i], i))
                {
                    Console.Out.WriteLine($"ERR\tОшибка инициализации хеш-таблицы набором данных");
                    return;
                }
            }
        }

        /// <summary>
        /// Хеш функция
        /// </summary>
        public static int GetHash(int value)
        {
            return value / 1000 + value % 10;

        }

        /// <summary>
        /// Пересчитать количество занятых ячеек
        /// </summary>
        private void UpdateOccupiedCell()
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
        public double GetFilling()
        {
            UpdateOccupiedCell();
            return _sizeHashTable == 0 ? 0 : (double) _counterOccupiedCell / (double) _sizeHashTable;
        }

        /// <summary>
        /// Получение среднего числа шагов, потребовавшихся для поиска ключа
        /// </summary>
        public double GetAverageNumberSteps()
        {
            return _sizeTable == 0 ? 0 : (double) _counterAttempts / (double) _sizeTable;
        }

        /// <summary>
        /// Поиск индекса элемента по значению 
        /// </summary>
        public bool Find(int targetValue, out int? key)
        {
            key = null;
            var counterAttempts = 0; // Счетчик шагов
            var isFound = false;
            int hTargetValue = 0;
            do
            {
                hTargetValue = (GetHash(targetValue) + counterAttempts * counterAttempts) % _sizeHashTable;
                counterAttempts++;

                if (_items[hTargetValue].Value == targetValue)
                {
                    isFound = true;
                    Console.Out.WriteLine(
                        $"RES>>\tВведенное значение ({targetValue}) найдено за {counterAttempts} шагов по ключу {hTargetValue}");
                    key = hTargetValue;
                    return true;
                }

            } while (_items[hTargetValue].isCollision);

            Console.Out.WriteLine($"RES>>\tВведенное значение ({targetValue}) не найдено");
            return false;
        }

        /// <summary>
        /// Добавить элемент
        /// </summary>
        public bool Add(int addValue, int? index = null)
        {
            int counterAttempts = 0;
            bool resolvedCollision = false;
            do
            {
                int key = (GetHash(addValue) + counterAttempts * counterAttempts) % _sizeHashTable;

                if (_items[key].Value == null)
                {
                    _items[key].Key = index;
                    _items[key].Value = addValue;
                    _items[key].isCollision = true;
                    resolvedCollision = true;
                    _counterOccupiedCell++;
                    _counterAttempts += counterAttempts + 1;
                    Console.Out.WriteLine(
                        $"RES>>\tЗначение {addValue} добавлено за {counterAttempts} шагов по ключу {key}");
                    return true;
                }

                counterAttempts++;

            } while (counterAttempts < _numberAttempts && !resolvedCollision);

            Console.Out.WriteLine(
                $"RES>>\tНеудалось добавить занчение {addValue} (неудалось разрешить коллизию методом квадратичного пробирования за {_numberAttempts} шагов)");
            return false;
        }

        /// <summary>
        /// Удалить элемент по значению
        /// </summary>
        public bool Dlt(int dltValue)
        {
            if (Find(dltValue, out int? keyDltValue))
            {
                _items[(int) keyDltValue].Key = null;
                _items[(int) keyDltValue].Value = null;
                _counterOccupiedCell--;
                Console.Out.WriteLine($"RES>>\tВведенное значение ({dltValue}) удалено по ключу {keyDltValue}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Базовый класс для элементов
        /// </summary>
        internal class Item
        {
            public int? Key { get; internal set; }
            public int? Value { get; internal set; }

            public bool isCollision { get; internal set; }

            public Item()
            {
                Key = null;
                Value = null;
                isCollision = false;
            }

            /*public Item(int key, int value)
            {
                Key = key;
                Value = value;
            }*/
        }
    }
}