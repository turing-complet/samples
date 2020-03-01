using System;
using System.Diagnostics;
using System.Linq;

namespace diff
{
    public class StringDiff : Diff
    {
        public StringDiff(string left, string right)
        {
            this.left = left.ToCharArray().Select(c => c.ToString()).ToArray();
            this.right = right.ToCharArray().Select(c => c.ToString()).ToArray();
            tally = new Tally[left.Length + 1, right.Length + 1];
        }
    }
}