using CombLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iz11
{
    internal class Task7
    {
        public Task7() {
            Graphs graphs = new Graphs();
            long answer = graphs.NumberOfEulerianGraphs(9);
            Console.WriteLine($"Количество эйлеровых графов из 9 вершин: {answer}");
        }
    }
}
