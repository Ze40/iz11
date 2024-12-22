using CombLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iz11
{
    internal class Task4
    {
        public Task4()
        {
            List<char> alphabet = new List<char> { 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a' };
            CombObject<char> obj = new CombObject<char>(alphabet);
            List<List<char>[]> results = obj.GetNamedPartitionsWithOneElement(5, 11, 'a').ToList();
            Console.WriteLine($"Всего разбиений: {results.Count}");

            obj.WriteToFile("out.txt", results);
        }
    }
}
