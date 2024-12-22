using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombLib;

namespace iz11
{
    internal class Task1
    {
        public Task1()
        {
            var A = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k' };
            CombObject<char> obj = new CombObject<char>(A);
            List<HashSet<char>[]> results = obj.GetUnNamedPartitions(5).ToList();

            Console.WriteLine($"Всего разбиений: {results.Count}");
        }
    }
}
