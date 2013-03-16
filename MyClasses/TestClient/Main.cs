using System;
using MyClasses.Data_structures;
using MyClasses.SortingAlgorithms;

namespace TestClient
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            HashTable<int> tableOfIntegers = new HashTable<int>(80, new FNVHash());
            for (int i = 0; i < 100; i++)
            {
                tableOfIntegers.Insert(i);
            }
            int[] values = new int[100];
            int cnt = 0;
            foreach (int item in tableOfIntegers)
            {
                values[cnt++] = item;
            }
            QSort<int>.Sort(ref values);  
        }
    }
}
