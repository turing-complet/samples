using System;
using System.Collections.Generic;
using System.Linq;

namespace diff
{
    static class Program
    {
        static void Main(string[] args)
        {
            string first = "abcdfghjqz";
            string second = "abcdefgijkrxyz";

            var longestCommon = LCS(first, second);
        }

        static IEnumerable<string> CustomSort(this IEnumerable<string> words) 
            => words.OrderByDescending(s => s, new CustomCompare()) as IEnumerable<string>;

        static string LCS(string versionOne, string versionTwo)
        {
            var seq1 = Utility.Subsequences(versionOne).CustomSort();
            var seq2 = Utility.Subsequences(versionTwo).CustomSort();

            string result;
            while (true)
            {
                string a = seq1.First();
                string b = seq2.First();
                if (a == b)
                {
                    result = a;
                    break;
                }
                if (CustomCompare.Instance.Compare(a, b) > 0)
                {
                    seq1 = seq1.Skip(1);
                }
                else
                {
                    seq2 = seq2.Skip(1);
                }
            }

            return result;
        }
    }

}
