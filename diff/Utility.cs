using System.Collections.Generic;

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
    }
}