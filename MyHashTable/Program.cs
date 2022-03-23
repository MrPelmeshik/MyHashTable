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
        OutputArr(randomUniqueSet, true, true);
        
        var hashTable = new HashTable();
        hashTable.InitTableWithSet(randomUniqueSet);
        hashTable.OutputHashTable();
        Console.Out.WriteLine($"Коэффициент заполнения: {hashTable.GetFilling()}");
        Console.Out.WriteLine($"Среднее число шагов: {hashTable.GetAverageNumberSteps()}");

        Console.Out.WriteLine("\nSTOP");
    }

    private static void OutputArr(int [] arr, bool hrz = false, bool needIndex = false)
    {
        Console.Out.WriteLine("\nData:");
        for (var i = 0; i < arr.Length; i++)
        {
            if (hrz)
                Console.Out.Write(needIndex ? $"{i}-{arr[i]} " : $"{arr[i]}");
            else
                Console.Out.WriteLine(needIndex ? $"\t{i} - \t{arr[i]}" : $"\t{arr[i]}");
        }
        Console.Out.WriteLine("");
    }
}
