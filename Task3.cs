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
            var A = new List<char> { 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a' };
            CombObject<char> obj = new CombObject<char>(A);
            List<List<char>[]> part = obj.GetUnNamedPartitionsWithOneElement(5, 11, 'a').ToList();
            foreach (List<char>[] partition in part)
            {
                foreach (List<char> set in partition)
                {
                    Console.Write("{");
                    foreach (char c in set) Console.Write(c);
                    Console.Write("} ");
                }
                Console.WriteLine();
            }
        }
    }
}
