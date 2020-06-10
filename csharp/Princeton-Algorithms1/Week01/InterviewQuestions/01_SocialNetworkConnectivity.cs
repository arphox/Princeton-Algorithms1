using System.Collections.Generic;

namespace Princeton_Algorithms1.Week01.InterviewQuestions
{
    /// <summary>
    ///     Social network connectivity.
    ///     Given
    ///     - a social network containing n members and
    ///     - a log file containing m timestamps at which times pairs of members formed friendships,
    ///     design an algorithm to determine the earliest time at which all members are connected
    ///     (i.e., every member is a friend of a friend of a friend ... of a friend).
    ///     
    ///     Assume that the log file is sorted by timestamp and that friendship is an equivalence relation.
    ///     The running time of your algorithm should be 'm log n' or better and use extra space proportional to n.
    /// </summary>
    public class _01_SocialNetworkConnectivity
    {
        private readonly int _memberCount;
        private readonly IEnumerable<(int, int)> _relations;

        public _01_SocialNetworkConnectivity(
            int memberCount,
            IEnumerable<(int, int)> relations)
        {
            _memberCount = memberCount;
            _relations = relations;
        }

        public int Run()
        {
            WeightedPathCompressingQuickUnionUFLegacy algo = new WeightedPathCompressingQuickUnionUFLegacy();
            algo.Initialize(_memberCount);

            using var enumerator = _relations.GetEnumerator();

            int i = 0;
            while (algo.SizeOf(0) != _memberCount)
            {
                if (!enumerator.MoveNext())
                    return -1;

                (int a, int b) = enumerator.Current;
                algo.Union(a, b);
                i++;
            }

            return i;
        }
    }
}