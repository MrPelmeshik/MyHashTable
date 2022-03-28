using System;
using System.Linq;

namespace MyGenerator
{
    public static  class GeneratorValues
    {
        public static int[] GetRandomValues(int n, int min, int max)
        {
            Random rnd = new Random();
            var set = new int[n];

            for (var i = 0; i < n; i++)
            {
                set[i] = rnd.Next(min, max);
            }

            return set;
        }

        public static int[] GetUniqueRandomValues(int n, int min, int max)
        {
            Random rnd = new Random();
            var set = new int[n];
            
            int i = 0;
            do
            {
                int rndValue = rnd.Next(min, max);
                if (!IsUniqueValueInSet(set, rndValue)) continue;
                set[i] = rndValue;
                i++;
            } while (i < n);

            return set;
        }
        
        private static bool IsUniqueValueInSet(int[] set, int rndValue)
        {
            if (set == null) throw new ArgumentNullException(nameof(set));
            return set.All(value => value != rndValue);
        }
    }
}