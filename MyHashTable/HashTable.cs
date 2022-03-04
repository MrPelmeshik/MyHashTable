using System;

namespace MyHashTable
{
    public class HashTable
    {
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
        public int M = 0;
        
        public int? key { set; get; }
        
        public int? value { set; get; }

        public static void OutputHashTable(HashTable[] hashTable, bool vrt = false)
        {
            for (int i = 0; i < hashTable.Length; i++)
            {
                if (vrt)
                {
                    Console.Out.WriteLine($"\t{i} > \t{hashTable[i].key} > \t{hashTable[i].value}");
                }
                else
                {
                    Console.Out.Write($"{i}-{hashTable[i].value} ");
                }
            }
        }

        public HashTable[] InitTableWithSet(int[] set)
        {
            int sizeTable = set.Length * 3 / 2;
            var hashTable = new HashTable[sizeTable];

            for (int i = 0; i < sizeTable; i++)
            {
                hashTable[i] = new HashTable();
            }

            foreach (var value in set)
            {
                int counterAttempts = 0;
                bool resolvedCollision = false;
                do
                {
                    int key = GetHash(value) + ResolveCollision(sizeTable, counterAttempts);

                    if (key > sizeTable)
                        throw new NotImplementedException($"Полученный ключ ({key}) больше размера таблицы ({sizeTable})");

                    Console.Out.WriteLine($">>\tkey - {key}");
                    
                    if (hashTable[key].value == null)
                    {
                        hashTable[key].key = key;
                        hashTable[key].value = value;
                        resolvedCollision = true;
                        M++;
                    }
                    
                    if (counterAttempts > _numberAttempts)
                        throw new Exception($"Неудалось разрешить коллизии за {_numberAttempts} шагов");

                    counterAttempts++;

                } while (resolvedCollision);


            }
            
            return hashTable;
        }


        public static int GetHash(int value)
        {
            return value / 1000 + value % 10;
            
        }

        private static int ResolveCollision(int sizeTable, int index = 0)
        {
            return index * index * _k % sizeTable;
        }
        
    }
}