using System;
using MyHashTable;
using MyGenerator;

public class Program
{
    private static readonly int N = 53;

    static void Main()
    {
        Console.Out.WriteLine("\nSTART");

        var randomUniqueSet = GeneratorValues.GetUniqueRandomValues(N, 1000, 9999);
        var hashTable = new HashTable(N);
        foreach (var randomValue in randomUniqueSet)
        {
            hashTable.Add(randomValue, randomValue.ToString());
        }
        
        OutputArr(randomUniqueSet, true, true);
        hashTable.OutputHashTable();
        hashTable.OutputInfo();

        #region MENU
        bool exit = false;
        do
        {
            //.Clear();
            Console.Out.Write("\n\nМеню\n" +
                              "1. Показать сгенерированный набор данных\n" +
                              "2. Показать хеш таблицу\n" +
                              "3. Показать данные по хеш таблице\n" +
                              "4. Поиск\n" +
                              "5. Удаление\n" +
                              "6. Добавление\n" +
                              "7. Замена\n" +
                              "0. Exit\n" +
                              "=============\n" +
                              "Выберите пункт меню: ");
            string key = Console.ReadLine();
            switch (key)
            {
                
                case "1":
                    OutputArr(randomUniqueSet, true, true);
                    break;
                case "2":
                    hashTable.OutputHashTable();
                    break;
                case "3":
                    hashTable.OutputInfo();
                    break;
                case "4":
                    Console.Out.Write("Введите значение для поиска: ");
                    int valueToSearch;
                    int.TryParse(Console.ReadLine(), out valueToSearch);
                    hashTable.Find(valueToSearch, out _);
                    break;
                case "5":
                    Console.Out.Write("Введите значение для удаления: ");
                    int valueToDelete;
                    int.TryParse(Console.ReadLine(), out valueToDelete);
                    hashTable.Dlt(valueToDelete);
                    break;
                case "6":
                    Console.Out.Write("Введите значение для добавления: ");
                    int valueToAdd;
                    int.TryParse(Console.ReadLine(), out valueToAdd);
                    hashTable.Add(valueToAdd, valueToAdd.ToString());
                    break;
                case "7":
                    Console.Out.Write("Введите старое значение: ");
                    int valueToDelete2;
                    int.TryParse(Console.ReadLine(), out valueToDelete);
                    Console.Out.Write("Введите новое значение: ");
                    int valueToAdd2;
                    int.TryParse(Console.ReadLine(), out valueToAdd);
                    if(hashTable.Dlt(valueToDelete))
                        hashTable.Add(valueToAdd, valueToAdd.ToString());
                    break;
                case "0":
                    exit = true;
                    break;
            }
            Console.ReadLine();
        }while (!exit);
        #endregion

        Console.Out.WriteLine("\nSTOP");
    }
    
    private static void OutputArr(int[] arr, bool hrz = false, bool needIndex = false)
    {
        Console.Out.WriteLine("\nData:");
        for (var i = 0; i < arr.Length; i++)
        {
            if (hrz)
                Console.Out.Write(needIndex ? $"{i + 1}-{arr[i]} " : $"{arr[i]}");
            else
                Console.Out.WriteLine(needIndex ? $"\t{i + 1} - \t{arr[i]}" : $"\t{arr[i]}");
        }

        Console.Out.WriteLine("");
    }
}
