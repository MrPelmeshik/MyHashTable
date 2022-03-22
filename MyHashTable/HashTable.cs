using System;

namespace MyHashTable
{
    public class HashTable
    {
        /// <summary>
        /// Максимальный размер таблицы
        /// </summary>
        private static readonly int _maxSize = 255;
        
        /// <summary>
        /// Ограничение по количеству попыток разрешить коллизию
        /// </summary>
        private static readonly int _numberAttempts = 50;
        
        /// <summary>
        /// Постоянный коэфициент для разрешения коллизий
        /// </summary>
        private static readonly int _k = 3;
        
        /// <summary>
        /// Количество занятых ячеек
        /// </summary>
        private int _m = 0;

        /// <summary>
        /// Размер исходной таблицы со сзначениями
        /// </summary>
        private int _sizeTable = 0;
        
        /// <summary>
        /// Размер полученной хеш-таблицы
        /// </summary>
        private int _sizeHashTable = 0;

        /// <summary>
        /// Сумма шаго потребовавшихся для определения ключа
        /// </summary>
        private int _counterAttempts = 0;

        /// <summary>
        /// Список элементов
        /// </summary>
        private Item[] _items;

        /// <summary>
        /// Метод вывода хеш таблицы на экран
        /// </summary>
        public void OutputHashTable(int numColumn = 1)
        {
            Console.Out.WriteLine("\nHashTable:");
            int n = _items.Length / 3;
            for (int i = 0; i < n + 1; i++)
            {
                Console.Out.Write(i < _items.Length
                    ? $"\t{i} > \t[{_items[i].Key}]\t[{_items[i].Value}]" 
                    : $"\t\t\t");
                var k = n + 1;
                Console.Out.Write(k + i < _items.Length
                    ? $"\t\t{k + i} > \t[{_items[k + i].Key}]\t[{_items[k + i].Value}]"
                    : $"\t\t\t");
                k = n + n + 2;
                Console.Out.Write(k + i  < _items.Length
                    ? $"\t\t{k + i} > \t[{_items[k + i].Key}]\t[{_items[k + i].Value}]"
                    : $"\t\t\t");
                Console.Out.Write("\n");
            }
        }

        /// <summary>
        /// Инициализация хеш таблицы значениями из заданного набора
        /// </summary>
        public void InitTableWithSet(int[] set)
        {
            _sizeTable = set.Length;
            _sizeHashTable = _sizeTable * 3 / 2;
            var items = new Item[_sizeHashTable];

            for (var i = 0; i < _sizeHashTable; i++)
            {
                items[i] = new Item();
            }
            
            for (var i = 0; i < set.Length; i++)
            {
                int counterAttempts = 0;
                bool resolvedCollision = false;
                do
                {
                    int key = (GetHash(set[i]) + counterAttempts * counterAttempts) % _sizeHashTable;

                    if (key > _sizeHashTable)
                        throw new NotImplementedException($"ERR>>\tПолученный ключ ({key}) больше размера таблицы ({_sizeHashTable})");

                    if (items[key].Value == null)
                    {
                        items[key].Key = i;
                        items[key].Value = set[i];
                        resolvedCollision = true;
                        _m++;
                        _counterAttempts += counterAttempts + 1;
                        //Console.Out.WriteLine($"LOG>>\tДля значения {set[i]} за {counterAttempts} шагов найден ключ: Key = {key}");
                    }
                    
                    if (counterAttempts > _numberAttempts)
                        throw new Exception($"ERR>>\tНеудалось разрешить коллизию (методом квадратичного пробирования) за {_numberAttempts} шагов");

                    counterAttempts++;

                } while (!resolvedCollision);
            }
            _items = items;
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
                if (item.Value == null)
                    counter++;
            }
            _m = counter;
        }

        /// <summary>
        /// Получение коэффициента заполнения
        /// </summary>
        /// <returns></returns>
        public double GetFilling()
        {
            UpdateOccupiedCell();
            return _sizeHashTable == 0 ? 0 : (double)_m / (double)_sizeHashTable;
        }

        /// <summary>
        /// Получение среднего числа шагов потребовавшихся для поиска ключа
        /// </summary>
        public double GetAverageNumberSteps()
        {
            return _sizeHashTable == 0 ? 0 : (double)_counterAttempts / (double)_sizeTable;
        }

        /// <summary>
        /// Базовый класс для элементов
        /// </summary>
        private class Item
        {
            public int? Key { get; internal set; }
            public int? Value { get; internal set; }

            public Item()
            {
            }

            public Item(int key, int value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}