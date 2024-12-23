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
            Graphs graphs = new Graphs();
            long answer = graphs.NumberOfConnectedGraphs(9);
            Console.WriteLine($"Количество связных графов из 9 вершин: {answer}");
        }
    }
}
