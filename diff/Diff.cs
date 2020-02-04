using System;
using System.Diagnostics;

namespace diff
{
    [DebuggerDisplay("{IsMatch}, {LocalMax}")]
    public class Tally
    {
        public Tally(bool isMatch, int localMax)
        {
            IsMatch = isMatch;
            LocalMax = localMax;
        }
        public bool IsMatch { get; set;} // not necessary for strings but will help for harder-to-compare objects
        public int LocalMax { get; set;}
    }

    // works on string, should abstract and implement for files, other types?
    public class Diff
    {
        private char[] left;
        private char[] right;

        private Tally[,] tally;

        public Diff(string left, string right)
        {
            this.left = left.ToCharArray();
            this.right = right.ToCharArray();
            tally = new Tally[left.Length, right.Length];
        }

        public void Compute()
        {
            populateGrid();
            string lcs = traceback();
            Console.WriteLine(lcs); // getting "ana", should be "aana"
        }

        private string traceback()
        {
            return _tracebackFrom(left.Length-1, right.Length-1);
        }

        private string _tracebackFrom(int i, int j)
        {
            if (i == 0)
            {
                return edgeCaseMatch(new string(right), new string(left), j);
            }
            if (j == 0)
            {
                return edgeCaseMatch(new string(left), new string(right), i);
            }

            if (left[i] == right[j]) return _tracebackFrom(i-1, j-1) + left[i];
            if (tally[i-1, j].LocalMax > tally[i, j-1].LocalMax)
            {
                return _tracebackFrom(i-1, j);
            } else {
                return _tracebackFrom(i, j-1);
            }
        }

        private string edgeCaseMatch(string first, string other, int index)
        {
            if (first.Substring(0, Math.Max(1, index)).Contains(other[0]))
            {
                return other[0].ToString();
            }
            return string.Empty;
        }

        private void populateGrid()
        {
            for (int i = 0; i < left.Length; i++)
            {
                for (int j = 0; j < right.Length; j++)
                {
                    bool isMatch;
                    int localMax;
                    if (left[i] == right[j])
                    {
                        isMatch = true;
                        localMax = 1 + areaMax(i-1, j-1);
                    }
                    else
                    {
                        isMatch = false;
                        localMax = areaMax(i, j);
                    }
                    tally[i, j] = new Tally(isMatch, localMax);
                }
            }
        }

        // inefficient for strings with no common chars
        private int areaMax(int row, int col)
        {
            if (row < 0 || col < 0) return 0;

            int curr = tally[row, col]?.LocalMax ?? 0;
            if (curr > 0) return curr;

            return Math.Max(
                areaMax(row-1, col),
                areaMax(row, col-1)
            );
        }

        public DiffResult Result() 
        { 
            return new DiffResult();
        }

    }
}