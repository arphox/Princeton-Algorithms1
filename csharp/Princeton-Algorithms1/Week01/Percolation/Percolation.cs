using System;

namespace Princeton_Algorithms1.Week01.Percolation
{
    /// <summary>
    ///     Percolation data type to model a percolation system.
    ///     Indexing is 0-based!
    /// </summary>
    /// <remarks>
    ///     See specification at:
    ///     https://coursera.cs.princeton.edu/algs4/assignments/percolation/specification.php
    /// </remarks>
    public class Percolation
    {
        private readonly IUnionFindImplementation _unionFind;
        private int _openSiteCounter;
        private readonly bool[,] siteStates;
        private readonly int _virtualTopIndex;
        private readonly int _virtualBottomIndex;

        public int Size { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Percolation"/> class,
        ///     by creating an n-by-n grid, with all sites initially blocked/full
        /// </summary>
        /// <param name="n">Size of the grid (will be n-by-n)</param>
        /// <param name="unionFind">Union find algorithm implementation that is NOT initialized (and its indexing is 0-based).</param>
        public Percolation(int n, IUnionFindImplementation unionFind)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException(nameof(n), n, "Value cannot be less than 1.");

            Size = n;
            _unionFind = unionFind;
            _unionFind.Initialize(Size * Size + 2);
            siteStates = new bool[Size, Size];

            // Virtual top element
            _virtualTopIndex = Size * Size;
            InitializeVirtualTop();

            // Virtual bottom element
            _virtualBottomIndex = Size * Size + 1;
            InitializeVirtualBottom();
        }

        private void InitializeVirtualTop()
        {
            for (int i = 0; i < Size; i++)
            {
                int index = CalculateUnionFindIndex(0, i);
                _unionFind.Union(index, _virtualTopIndex);
            }
        }

        private void InitializeVirtualBottom()
        {
            for (int i = 0; i < Size; i++)
            {
                int index = CalculateUnionFindIndex(Size - 1, i);
                _unionFind.Union(index, _virtualBottomIndex);
            }
        }

        /// <summary>
        ///     Opens the given site
        /// </summary>
        public void Open(int row, int col)
        {
            CheckCoordinates(row, col);
            if (IsOpen(row, col))
                return;

            siteStates[row, col] = true;
            _openSiteCounter++;
            int index = CalculateUnionFindIndex(row, col);

            UnionIfOpen(row, col + 1);
            UnionIfOpen(row, col - 1);
            UnionIfOpen(row + 1, col);
            UnionIfOpen(row - 1, col);

            // ------------------------------------------
            void UnionIfOpen(int row, int col)
            {
                bool isOpen = IsOpenSafe(row, col);
                if (isOpen)
                    _unionFind.Union(index, CalculateUnionFindIndex(row, col));
            }
        }

        /// <summary>
        ///     Returns whether the given site is open.
        /// </summary>
        public bool IsOpen(int row, int col)
        {
            CheckCoordinates(row, col);
            return IsOpenNonChecking(row, col);
        }

        private bool IsOpenSafe(int row, int col)
        {
            if (IsInvalidCoordinatePart(row) || IsInvalidCoordinatePart(col))
                return false;
            return IsOpenNonChecking(row, col);
        }

        private bool IsOpenNonChecking(int row, int col)
        {
            return siteStates[row, col];
        }

        /// <summary>
        ///     Returns whether the given site is full (not open).
        /// </summary>
        public bool IsFull(int row, int col) => !IsOpen(row, col);

        /// <summary>
        ///     Returns the number of open sites
        /// </summary>
        public int NumberOfOpenSites() => _openSiteCounter;

        /// <summary>
        ///     Determines whether the system percolates.
        /// </summary>
        public bool Percolates()
        {
            if (Size == 1)
                return _openSiteCounter == 1;

            return _unionFind.Find(_virtualBottomIndex) == _unionFind.Find(_virtualTopIndex);
        }

        private void CheckCoordinates(int row, int col)
        {
            if (IsInvalidCoordinatePart(row))
                throw new ArgumentOutOfRangeException(nameof(row), row, "Value has to be between the interval [0;N[.");
            if (IsInvalidCoordinatePart(col))
                throw new ArgumentOutOfRangeException(nameof(col), col, "Value has to be between the interval [0;N[.");
        }

        private bool IsInvalidCoordinatePart(int x)
        {
            return x < 0 || x >= Size;
        }

        private int CalculateUnionFindIndex(int row, int col)
        {
            return row * Size + col;
        }
    }
}