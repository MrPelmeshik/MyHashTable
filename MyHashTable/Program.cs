using System;
using MyHashTable;
using MyGenerator;

public class Program
{
    private static readonly int N = 53;

    static void Main()
    {
        Console.Out.WriteLine("START");
        var randomUniqueSet = GeneratorValues.GetUniqueRandomValues(N, 1000, 9999);
        OutputArr(randomUniqueSet, true, true);
        
        try
        {
            var hashTable = new HashTable();
            hashTable.InitTableWithSet(randomUniqueSet);
            hashTable.OutputHashTable(true);
            Console.Out.WriteLine($"Коэффициент заполнения: {hashTable.GetFilling()}");
            Console.Out.WriteLine($"Среднее число шагов: {hashTable.GetAverageNumberSteps()}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        Console.Out.WriteLine("STOP");
    }

    private static void OutputArr(int [] arr, bool hrz = false, bool needIndex = false)
    {
        Console.Out.WriteLine("Data:");
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
