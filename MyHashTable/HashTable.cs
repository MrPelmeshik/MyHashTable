using System;

namespace MyHashTable
{
    /*public class HashTable
    {
        public int? key { set; get; }
        
        public int? value { set; get; }

        public static HashTable[] GetHashTable(int n)
        {
            var originalTable = InitHashTable(n);
            OutputTable(originalTable, false);

            var hashTable = OriginalTableToHashTable(originalTable, n);

            Console.Out.WriteLine("");
            return hashTable;
        }
        

        public static void OutputTable(HashTable[] hashTable, bool vertical = true)
        {
            for (int i = 0; i < hashTable.Length; i++)
            {
                if (vertical)
                {
                    Console.Out.WriteLine($"\t{i} > \t{hashTable[i].key} > \t{hashTable[i].value}");
                }
                else
                {
                    Console.Out.Write($"{i}-{hashTable[i].value} ");
                }
            }
        }



        public static HashTable[] OriginalTableToHashTable(HashTable[] originalTable, int n)
        {
            var hashtable = new HashTable[n * 3 / 2];

            for (int i = 0; i < hashtable.Length; i++)
            {
                // if (hashtable[originalTable[i].key].value != 0)
                // {
                //     // Коллизия
                // }
                //
                // hashtable[originalTable[i].key].key = i;
                // hashtable[originalTable[i].key].value = originalTable[i].value;

            }
            
            return hashtable;
        }


        public static void ResolveCollision()
        {
            throw new NotImplementedException();
        }
        
    }*/
}