using System;
using System.Collections.Generic;
using System.Linq;

namespace diff
{
    static class Program
    {
        static void Main(string[] args)
        {
            // string first = "abcdfghjqz";
            // string second = "abcdefgijkrxyz";

            // var longestCommon = LCS(first, second);

            string first = "banana";
            string second = "atana";
            var diff = new Diff(first, second);
            diff.Compute();
        }

        static string LCS(string versionOne, string versionTwo)
        {
            var seq1 = Utility.Subsequences(versionOne);
            var seq2 = Utility.Subsequences(versionTwo);
            return Utility.LongestCommonElement(seq1, seq2);
        }
    }

}
