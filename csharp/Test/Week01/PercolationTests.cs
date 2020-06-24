using FluentAssertions;
using Princeton_Algorithms1.Week01;
using Princeton_Algorithms1.Week01.Percolation;
using Xunit;

namespace Test.Week01
{
    public class PercolationTests
    {
        public sealed class FunctionTests
        {
            [Fact]
            public void If_a_site_is_opened_then_NumberOfOpenSites_should_return_valid_value()
            {
                const int Size = 5;
                Percolation sut = CreateSut(Size);
                int expectedCountOfOpenSites = 0;
                sut.NumberOfOpenSites().Should().Be(expectedCountOfOpenSites);

                sut.Open(0, 0);
                sut.Open(0, 1);
                sut.Open(0, 2);
                expectedCountOfOpenSites += 3;
                sut.NumberOfOpenSites().Should().Be(expectedCountOfOpenSites);

                for (int i = 1; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        sut.Open(i, j);
                        expectedCountOfOpenSites++;
                        sut.NumberOfOpenSites().Should().Be(expectedCountOfOpenSites);
                    }
                }
            }

            [Fact]
            public void If_nothing_is_opened_then_IsOpen_should_return_false_for_everything()
            {
                const int Size = 7;
                Percolation sut = CreateSut(Size);
                for (int i = 0; i < Size; i++)
                    for (int j = 0; j < Size; j++)
                        sut.IsOpen(i, j).Should().BeFalse();
            }

            private Percolation CreateSut(int size)
            {
                return new Percolation(size, new WeightedPathCompressingQuickUnionFind());
            }
        }

        public sealed class AcceptanceTests
        {
            private Percolation sut;

            [Fact]
            public void Size1()
            {
                InitializePercolation(1);
                OpenSiteAndExpectPercolates(0, 0);
            }

            [Fact]
            public void Size2_top_opened_first()
            {
                InitializePercolation(2);
                OpenSite(0, 0);
                OpenSite(1, 1);
                OpenSiteAndExpectPercolates(0, 1);
                OpenSiteAndExpectPercolates(1, 1);
            }

            [Fact]
            public void Size2_bottom_opened_first()
            {
                InitializePercolation(2);
                OpenSite(1, 1);
                OpenSite(0, 0);
                OpenSiteAndExpectPercolates(0, 1);
            }

            [Fact]
            public void Size2_opened_in_one_column()
            {
                InitializePercolation(2);
                OpenSite(0, 1);
                OpenSiteAndExpectPercolates(1, 1);
            }

            [Fact]
            public void Size3()
            {
                InitializePercolation(3);
                OpenSite(1, 1);
                OpenSite(2, 2);
                OpenSite(0, 0);
                OpenSite(2, 1);
                OpenSite(0, 2);
                OpenSiteAndExpectPercolates(1, 0);
                OpenSiteAndExpectPercolates(0, 1);
                OpenSiteAndExpectPercolates(1, 2);
                OpenSiteAndExpectPercolates(2, 0);
            }

            [Fact]
            public void Size4()
            {
                InitializePercolation(4);
                OpenSite(3, 0);
                OpenSite(2, 3);
                OpenSite(0, 1);
                OpenSite(0, 2);
                OpenSite(2, 1);
                OpenSite(3, 1);
                OpenSite(1, 3);
                OpenSite(1, 2);
                OpenSite(1, 0);
                OpenSite(3, 2);
                OpenSiteAndExpectPercolates(2, 2);
                OpenSiteAndExpectPercolates(3, 3);
                OpenSiteAndExpectPercolates(2, 0);
                OpenSiteAndExpectPercolates(1, 1);
                OpenSiteAndExpectPercolates(0, 0);
                OpenSiteAndExpectPercolates(0, 3);
            }

            [Fact]
            public void Size5()
            {
                InitializePercolation(5);
                OpenSite(2, 0);
                OpenSite(3, 0);
                OpenSite(2, 4);
                OpenSite(2, 2);
                OpenSite(0, 4);
                OpenSite(2, 3);
                OpenSite(1, 1);
                OpenSite(4, 0);
                OpenSite(3, 1);
                OpenSite(3, 2);
                OpenSiteAndExpectPercolates(1, 4);
            }

            [Fact]
            public void Size6()
            {
                InitializePercolation(6);
                OpenSite(1, 1);
                OpenSite(2, 5);
                OpenSite(3, 2);
                OpenSite(2, 2);
                OpenSite(5, 4);
                OpenSite(5, 0);
                OpenSite(3, 0);
                OpenSite(0, 4);
                OpenSite(1, 5);
                OpenSite(0, 2);
                OpenSite(3, 3);
                OpenSite(4, 1);
                OpenSite(1, 4);
                OpenSite(3, 4);
                OpenSite(4, 2);
                OpenSite(4, 5);
                OpenSite(2, 3);
                OpenSite(2, 4);
                OpenSite(5, 3);
                OpenSiteAndExpectPercolates(5, 2);
            }

            #region [ Helpers ]

            private void InitializePercolation(int size)
            {
                sut = new Percolation(size, new WeightedPathCompressingQuickUnionFind()); ;
                sut.Percolates().Should().BeFalse();
            }

            private void OpenSiteAndExpectPercolates(int row, int col)
            {
                sut.Open(row, col);
                sut.Percolates().Should().BeTrue();
            }

            private void OpenSite(int row, int col)
            {
                sut.Open(row, col);
                sut.Percolates().Should().BeFalse();
            }

            #endregion
        }
    }
}