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

    public class Diff
    {
        protected string[] left;
        protected string[] right;

        protected Tally[,] tally;

        public DiffResult Compute()
        {
            Initialize();
            populateGrid();
            string lcs = traceback();
            Console.WriteLine($"LCS = {lcs}");
            return traceResult();
        }

        protected virtual void Initialize() {}

        public DiffResult traceResult() 
        { 
            var result = new DiffResult();
            traceResult(result, left.Length-1, right.Length-1);
            return result;
        }

        private void traceResult(DiffResult result, int i, int j)
        {
            if (i < 0 && j < 0) return;

            if (i >= 0 && j >= 0 && tally[i, j].IsMatch)
            {
                traceResult(result, i-1, j-1);
                result.Unchanged(left[i]);
                return;
            }

            i = Math.Max(0, i);
            j = Math.Max(0, j);

            if (i > 0 && (j == 0 || tally[i-1, j].LocalMax > tally[i, j-1].LocalMax))
            {
                traceResult(result, i-1, j);
                result.Deletion(left[i]);
            }
            else if (j > 0 && (i == 0 || tally[i-1, j].LocalMax <= tally[i, j-1].LocalMax)) 
            {
                traceResult(result, i, j-1);
                result.Addition(right[j]);
            }
            else if (i == 0 && (j == 0 || tally[0, j].LocalMax > tally[i, j-1].LocalMax))
            {
                result.Deletion(left[0]);
            }
            else if (j == 0 && (i == 0 || tally[i-1, j].LocalMax <= tally[i, 0].LocalMax))
            {
                result.Addition(right[0]);
            }
        }

        private string traceback()
        {
            return _tracebackFrom(left.Length-1, right.Length-1);
        }

        private string _tracebackFrom(int i, int j)
        {
            if (i == 0)
            {
                return edgeCaseMatch(string.Join("", right), string.Join("", left), j);
            }
            if (j == 0)
            {
                return edgeCaseMatch(string.Join("", left), string.Join("", right), i);
            }

            if (tally[i, j].IsMatch) return _tracebackFrom(i-1, j-1) + left[i];
            if (tally[i-1, j].LocalMax > tally[i, j-1].LocalMax)
            {
                return _tracebackFrom(i-1, j);
            } else {
                return _tracebackFrom(i, j-1);
            }
        }

        private static string edgeCaseMatch(string first, string other, int index)
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

    }
}