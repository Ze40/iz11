using CombLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iz11
{
    internal class Task8
    {
        public Task8() { 
            Graphs graphs = new Graphs();
            long answer = graphs.NumberOfConnectedGraphsWithColors(9, 5);
            Console.WriteLine($"Количество графов раскрашенных в 5 цветов: {answer}");
        }
    }
}
