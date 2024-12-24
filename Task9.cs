using CombLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iz11
{
    internal class Task9
    {
        public Task9() {
            Graphs graphs = new Graphs();
            long answer = graphs.NumberOfEulerianGraphs(9, 11);
            Console.WriteLine($"Количество связных графов из 9 вершин и 11 ребер: {answer}");
        }
    }
}
