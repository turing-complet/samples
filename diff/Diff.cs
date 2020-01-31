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
        }

        private string traceback()
        {
            string result = string.Empty;
            // dunno
            return result;
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

        public void Print() {}

    }
}