using System;

namespace Princeton_Algorithms1.Week01.InterviewQuestions
{
    /// <summary>
    ///     Search in a bitonic array.
    ///         
    ///     An array is bitonic if it is comprised of an increasing sequence of integers 
    ///     followed immediately by a decreasing sequence of integers.
    ///     Write a program that, given a bitonic array of n distinct integer values,
    ///     determines whether a given integer is in the array.
    ///         
    ///     Standard version:
    ///         Use ~3 lg⁡(n) compares in the worst case.
    ///         
    ///     Signing bonus:
    ///         Use ~2 lg⁡(n) compares in the worst case
    ///         (and prove that no algorithm can guarantee to perform fewer than ~2 lg⁡(n) compares in the worst case).
    ///         
    ///     NOTES:
    ///     To be clear: from the beginning, numbers will increase, then at some point, they start to decrease.
    ///     But there is no more increasing part! This means there is a maximum element somewhere and it is easy to find.
    /// </summary>
    public sealed class _05_SearchInBitonic
    {
        public static int SearchAndReturnIndex_Standard(int[] array, int value)
        {
            if (array.Length < 3)
                throw new ArgumentOutOfRangeException(nameof(array.Length), array.Length, "Array's length cannot be less than 3");

            int maxIndex = FindMaxIndex(array);

            int foundIndex = BinarySearchInIncreasing(array, 0, maxIndex, value);
            if (foundIndex >= 0)
                return foundIndex;

            return BinarySearchInDecreasing(array, maxIndex + 1, array.Length - 1, value);
        }

        public static int FindMaxIndex(int[] arr)
        {
            int low = 1;
            int high = arr.Length - 2;

            while (low != high)
            {
                int mid = low + (high - low) / 2;

                if (arr[mid - 1] > arr[mid])
                    high = mid - 1;
                else if (arr[mid + 1] > arr[mid])
                    low = mid + 1;
                else
                    return mid;
            }

            return low;
        }

        public static int SearchAndReturnIndex_Improved(int[] array, int searchedValue)
        {
            if (array.Length < 3)
                throw new ArgumentOutOfRangeException(nameof(array.Length), array.Length, "Array's length cannot be less than 3");

            return BitonicSearchImproved(array, 0, array.Length - 1, searchedValue);
        }

        private static int BitonicSearchImproved(int[] array, int fromIndex, int toIndex, int searchedValue)
        {
            if (fromIndex >= toIndex)
                return array[fromIndex] == searchedValue ? fromIndex : -1;

            int midIndex = fromIndex + (toIndex - fromIndex) / 2;
            int midValue = array[midIndex];
            if (midValue == searchedValue)
                return midIndex;

            bool middleElementSmallerThanValue = false;
            if (midValue < searchedValue)
                middleElementSmallerThanValue = true;

            if (middleElementSmallerThanValue)
            {
                bool maxIsOnLeftSide = midIndex > 0 && array[midIndex - 1] > midValue;
                if (maxIsOnLeftSide)
                    return BitonicSearchImproved(array, fromIndex, midIndex - 1, searchedValue);
                else
                    return BitonicSearchImproved(array, midIndex + 1, toIndex, searchedValue);
            }
            else
            {
                int leftResult = BinarySearchInIncreasing(array, fromIndex, midIndex - 1, searchedValue);
                if (leftResult >= 0)
                    return leftResult;

                return BinarySearchInDecreasing(array, midIndex + 1, toIndex, searchedValue);
            }
        }

        public static int BinarySearchInIncreasing(int[] arr, int fromIndex, int toIndex, int value)
        {
            int low = fromIndex;
            int high = toIndex;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                if (arr[mid] > value)
                    high = mid - 1;
                else if (arr[mid] < value)
                    low = mid + 1;
                else
                    return mid;
            }

            return -1;
        }

        public static int BinarySearchInDecreasing(int[] arr, int fromIndex, int toIndex, int value)
        {
            int low = fromIndex;
            int high = toIndex;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                if (arr[mid] < value)
                    high = mid - 1;
                else if (arr[mid] > value)
                    low = mid + 1;
                else
                    return mid;
            }

            return -1;
        }
    }
}