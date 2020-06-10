using System;
using System.Diagnostics;
using Princeton_Algorithms1.Week01.Percolation;

namespace Princeton_Algorithms1
{
    internal class Program
    {
        private static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            PercolationStats ps = new PercolationStats(770, 100); // with size 770, 100 trials take ~10s (in DEBUG)
            Console.WriteLine(ps.Mean());
            Console.WriteLine(sw.Elapsed);
            Console.ReadLine();
        }
    }
}