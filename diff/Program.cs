using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace diff
{
    public class CustomCompare : IComparer<string>
    {
        private static IComparer<string>_baseCompare = StringComparer.CurrentCulture;
        public int Compare([AllowNull] string x, [AllowNull] string y)
        {
            if (x.Length == y.Length) return _baseCompare.Compare(y, x);
            return x.Length > y.Length ? 1 : -1;
        }

        public static CustomCompare Instance = new CustomCompare();
    }

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
            var seq1 = Subsequences(versionOne).CustomSort();
            var seq2 = Subsequences(versionTwo).CustomSort();

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
    }

    public enum Edit
    {
        Add, Remove
    }

    public class LineItem
    {
        public Edit Change { get; set; }
        public string Text { get; set; }
    }

    public class DiffResult
    {
        public LineItem[] Changes { get; set; }

        public void Display()
        {
            if (Changes?.Length == 0)
            {
                Console.WriteLine("No differences found");
                return;
            }

            foreach (var change in Changes)
            {
                if (change.Change == Edit.Add)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($">  {change.Text}{Environment.NewLine}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<  {change.Text}{Environment.NewLine}");
                }
            }
        }
    }
}
