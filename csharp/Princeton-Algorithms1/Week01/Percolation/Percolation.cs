using System;

namespace Princeton_Algorithms1.Week01.Percolation
{
    /// <summary>
    ///     Percolation data type to model a percolation system.
    ///     Indexing is 1-based.
    /// </summary>
    /// <remarks>
    ///     See specification at:
    ///     https://coursera.cs.princeton.edu/algs4/assignments/percolation/specification.php
    /// </remarks>
    public class Percolation
    {
        private readonly IUnionFindImplementation _unionFind;

        public int Size { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Percolation"/> class,
        ///     by creating an n-by-n grid, with all sites initially blocked
        /// </summary>
        /// <param name="n">Size of the grid (will be n-by-n)</param>
        /// <param name="unionFind">Union find algorithm implementation that is already initialized.</param>
        public Percolation(int n, IUnionFindImplementation unionFind)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException(nameof(n), n, "Value cannot be less than 1.");

            Size = n;
            _unionFind = unionFind;
        }

        /// <summary>
        ///     Opens the given site
        /// </summary>
        public void Open(int row, int col)
        {
            CheckCoordinates(row, col);
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns whether the given site is open.
        /// </summary>
        public bool IsOpen(int row, int col)
        {
            CheckCoordinates(row, col);
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns whether the given site is full (not open).
        /// </summary>
        public bool IsFull(int row, int col)
        {
            CheckCoordinates(row, col);
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns the number of open sites
        /// </summary>
        public int NumberOfOpenSites()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Determines whether the system percolates.
        /// </summary>
        public bool Percolates()
        {
            throw new NotImplementedException();
        }

        private void CheckCoordinates(int row, int col)
        {
            if (row < 1 || row > Size)
                throw new ArgumentOutOfRangeException(nameof(row), row, "Value has to be between the interval [1;N].");
            if (col < 1 || col > Size)
                throw new ArgumentOutOfRangeException(nameof(col), col, "Value has to be between the interval [1;N].");
        }
    }
}