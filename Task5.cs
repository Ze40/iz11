using CombLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iz11
{
    internal class Task5
    {
        public Task5() {
            List<char> alphabet = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k' };
            CombObject<char> obj = new CombObject<char>(alphabet);
            List<List<char>[]> results = obj.GetAllCycles(5).ToList();

            Console.WriteLine($"Всего разбиений: {results.Count}");

            //obj.WriteToFile("out.txt", results);
        }
    }
}
