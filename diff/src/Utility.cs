using System.Collections.Generic;
using System.Linq;

namespace diff
{
    public static class Utility
    {
        public static List<string> Subsequences(string input)
        {
            var result = new List<string>();
            _subsequences(input, result);
            return result;
        }

        private static void _subsequences(string input, List<string> result)
        {
            if (string.IsNullOrEmpty(input)) return;
            
            string first = input[0].ToString();
            string remaining = input.Substring(1);
            _subsequences(remaining, result);
            
            var tmp = new List<string>();
            foreach (var seq in result)
            {
                tmp.Add(first + seq);
            }
            result.AddRange(tmp);
            result.Add(first);
        }

        public static IEnumerable<string> CustomSort(this IEnumerable<string> words) 
            => words.OrderByDescending(s => s, new CustomCompare()) as IEnumerable<string>;

        public static string LongestCommonElement(IEnumerable<string> seq1, IEnumerable<string> seq2)
        {
            seq1 = seq1.CustomSort();
            seq2 = seq2.CustomSort();

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