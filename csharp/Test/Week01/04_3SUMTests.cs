using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Princeton_Algorithms1.Week01.InterviewQuestions;
using Xunit;

namespace Test.Week01
{
    public sealed class _04_3SUMTests
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateSeparated_Test(int[] input, string rawExpectedOutput)
        {
            // Arrange
            List<int[]> expectedOutput = ParseExpectedOutput(rawExpectedOutput);

            // Act
            List<int[]> lists = _04_3SUM.CalculateSeparated(input);

            // Assert
            lists.Should().BeEquivalentTo(expectedOutput);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Calculate_Test(int[] input, string rawExpectedOutput)
        {
            // Arrange
            List<int[]> expectedOutput = ParseExpectedOutput(rawExpectedOutput);

            // Act
            List<int[]> lists = Separate(_04_3SUM.Calculate(input));

            // Assert
            lists.Should().BeEquivalentTo(expectedOutput);
        }

        public static IEnumerable<object[]> TestData => new List<object[]>
        {
            new object[] { new int[] { 0, 1, -1 }, "-1,0,1" },
            new object[] { new int[] { 0, -10, 10, 12 }, "-10, 0, 10" },
            new object[] { new int[] { 2, 1, -4, -1, 0, }, "-1, 0, 1" },
            new object[] { new int[] { -12, 10, -10, 0, 12 }, "-10, 0, 10; -12, 0, 12" },
            new object[] { new int[] { 2, 3, -2, 1, -4, -5, 4, -1, -3, 0, 5 }, "-5,0,5; -5,1,4; -5,2,3; -4,-1,5; -4,0,4; -4,1,3; -3,-2,5; -3,-1,4; -3,0,3; -3,1,2; -2,-1,3; -2,0,2; -1,0,1" }
        };

        private List<int[]> Separate(List<int> list)
        {
            List<int[]> result = new List<int[]>();
            for (int i = 0; i < list.Count - 2; i += 3)
            {
                int[] arr = new int[] { list[i], list[i + 1], list[i + 2] };
                result.Add(arr);
            }
            return result;
        }

        private static List<int[]> ParseExpectedOutput(string rawExpectedSolution)
        {
            return rawExpectedSolution
                .Replace(" ", "")
                .Split(";", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(",").Select(int.Parse).ToArray())
                .ToList();
        }
    }
}