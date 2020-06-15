using System;
using System.Collections.Generic;
using System.Linq;

namespace Princeton_Algorithms1.Week01.InterviewQuestions
{
    public sealed class _04_3SUM
    {
        public static List<int[]> CalculateSeparated(int[] input)
        {
            input = input.OrderBy(x => x).ToArray(); // replace this with Array.Sort if it is okay to modify input

            List<int[]> result = new List<int[]>();

            for (int i = 0; i < input.Length; i++)
            {
                int x = input[i];
                for (int j = i + 1; j < input.Length; j++)
                {
                    int y = input[j];
                    int target = -(x + y);
                    bool isFound = Array.BinarySearch(input, j + 1, input.Length - j - 1, target) >= 0;
                    if (isFound)
                        result.Add(new int[] { x, y, target });
                }
            }

            return result;
        }

        public static List<int> Calculate(int[] input)
        {
            input = input.OrderBy(x => x).ToArray(); // replace this with Array.Sort if it is okay to modify input

            List<int> result = new List<int>(input.Length);

            for (int i = 0; i < input.Length; i++)
            {
                int x = input[i];
                for (int j = i + 1; j < input.Length; j++)
                {
                    int y = input[j];
                    int target = -(x + y);
                    bool isFound = Array.BinarySearch(input, j + 1, input.Length - j - 1, target) >= 0;
                    if (isFound)
                    {
                        result.Add(x);
                        result.Add(y);
                        result.Add(target);
                    }
                }
            }

            return result;
        }
    }
}