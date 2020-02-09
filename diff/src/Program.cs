using System;
using System.Collections.Generic;
using System.Linq;

namespace diff
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("WRONG");
                return;
            }
            var diff = new FileDiff(args[0], args[1]);
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
