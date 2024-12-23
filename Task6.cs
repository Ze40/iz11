using CombLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iz11
{
    internal class Task6
    {
        public Task6() {
            Comb comb = new Comb();
            long c = comb.CountOfCombinations(10, 2);
            Console.WriteLine(c);
        }
    }
}
