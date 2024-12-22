using CombLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iz11
{
    internal class Task3
    {
        public Task3() {
            List<char> alphabet  = new List<char> { 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a' };
            CombObject<char> obj = new CombObject<char>(alphabet);
            List<List<char>[]> results = obj.GetUnNamedPartitionsWithOneElement(5, 11, 'a').ToList();
            Console.WriteLine($"Всего разбиений: {results.Count}");

            obj.WriteToFile("out.txt", results);
        }
    }
}
