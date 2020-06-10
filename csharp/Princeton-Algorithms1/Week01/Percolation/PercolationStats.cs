using System;
using System.Linq;
using System.Threading.Tasks;
using Princeton_Algorithms1.Common;

namespace Princeton_Algorithms1.Week01.Percolation
{
    public sealed class PercolationStats
    {
        private double[] results;
        private readonly int _totalNumberOfElements;

        public Func<IUnionFindImplementation> UnionFindImplementationFactory { get; set; }
            = () => new WeightedPathCompressingQuickUnionFind();

        /// <summary>
        ///     Performs independent trials on an n-by-n grid
        /// </summary>
        /// <param name="n"></param>
        /// <param name="trials"></param>
        public PercolationStats(int n, int trials)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException(nameof(n), n, "Value cannot be less than 1");
            if (trials <= 0)
                throw new ArgumentOutOfRangeException(nameof(trials), trials, "Value cannot be less than 1");

            _totalNumberOfElements = n * n;
            RunTrials(n, trials);
        }

        private void RunTrials(int n, int trials)
        {
            results = new double[trials];
            Parallel.For(
                fromInclusive: 0,
                toExclusive: trials,
                body: index =>
                {
                    results[index] = RunTrial(n);
                }
            );
        }

        private double RunTrial(int n)
        {
            Percolation percolator = new Percolation(n, UnionFindImplementationFactory());
            while (!percolator.Percolates())
            {
                int row = ThreadLocalRandom.Next(n); // TODO: move random to dependency
                int col = ThreadLocalRandom.Next(n); // TODO: move random to dependency
                percolator.Open(row, col);
            }
            double openCount = percolator.NumberOfOpenSites();
            return openCount / (_totalNumberOfElements);
        }

        // sample mean of percolation threshold
        public double Mean()
        {
            return results.Average();
        }

        // sample standard deviation of percolation threshold
        public double Stddev()
        {
            throw new NotImplementedException();
        }

        // low endpoint of 95% confidence interval
        public double ConfidenceLo()
        {
            throw new NotImplementedException();
        }

        // high endpoint of 95% confidence interval
        public double ConfidenceHi()
        {
            throw new NotImplementedException();
        }
    }
}