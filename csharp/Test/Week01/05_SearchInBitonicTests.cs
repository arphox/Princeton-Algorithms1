using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Princeton_Algorithms1.Week01.InterviewQuestions;
using Xunit;

namespace Test.Week01
{
    public sealed class _05_SearchInBitonicTests
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void SearchAndReturnIndex_StandardTest(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int searchedValue = array[i];
                int result = _05_SearchInBitonic.SearchAndReturnIndex_Standard(array, searchedValue);
                result.Should().Be(i);
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void SearchAndReturnIndex_ImprovedTest(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int searchedValue = array[i];
                int result = _05_SearchInBitonic.SearchAndReturnIndex_Improved(array, searchedValue);
                result.Should().Be(i);
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void FindMaxIndexTest(int[] array)
        {
            int result = _05_SearchInBitonic.FindMaxIndex(array);
            result.Should().Be(Array.IndexOf(array, array.Max()));
        }

        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            new object[] { new int[] { 1, 3, 2 } }, // max in middle, odd count
            new object[] { new int[] { 6, 7, 5, 4 } }, // max on index 1, then 2 elements
            new object[] { new int[] { 8, 9, 7, 6, 5 } }, // max on index 1, then 3 elements
            new object[] { new int[] { 10, 11, 12, 9 } }, // max on index LAST-1, 2 elements before
            new object[] { new int[] { 13, 14, 15, 16, 12 } }, // max on index LAST-1, 3 elements before
            new object[] { new int[] { 17, 18, 19, 16, -15 } }, // max on middle, 2 elements before and 2 after
            new object[] { new int[] { 20, 21, 22, 23, 18, 15, 10 } }, // max on middle, 3 elements before and 3 after
            new object[] { new int[] { 21, 22, 24, 19, 16, 11 } }, // max on middle, 2 elements before and 3 after
            new object[] { new int[] { 22, 23, 24, 25, 17, -10 } }, // max on middle, 3 elements before and 2 after
            new object[] { new int[] { 23, 24, 25, 26, 15, -10 } }, // max on middle-ish, 10 elements
            new object[] { new int[] { 24, 25, 26, 33, 32, 31, 30, 29, 28, 27 } }, // max on middle-ish, 10 elements
            new object[] { new int[] { 25, 26, 99, 98, 95, 93, 90, 88, 83, 63, 60 } }, // max on middle-ish, 11 elements
        };
    }
}