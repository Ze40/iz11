using CombLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iz11
{
    internal class Task2
    {
        public Task2() {
            var A = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k' };
            CombObject<char> obj = new CombObject<char>(A);
            List<HashSet<char>[]> results = obj.GetNamedPartitions(5).ToList();

            Console.WriteLine($"Всего разбиений: {results.Count}");

            obj.WriteToFile("out.txt", results);
        }
    }
}
