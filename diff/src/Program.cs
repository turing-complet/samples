using System;
using System.Collections.Generic;
using System.Linq;

namespace diff
{
    static class Program
    {
        static void Main(string[] args)
        {
            // var longestCommon = LCS(first, second);

            string first = "banana";
            string second = "atana";
            var diff = new StringDiff(first, second);
            var result = diff.Compute();
            result.Print();
        }

        static string LCS(string versionOne, string versionTwo)
        {
            var seq1 = Utility.Subsequences(versionOne);
            var seq2 = Utility.Subsequences(versionTwo);
            return Utility.LongestCommonElement(seq1, seq2);
        }
    }

}
