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

    public class Diff : IDiffUtility
    {
        protected string[] left;
        protected string[] right;

        protected Tally[,] tally;

        public DiffResult Compute()
        {
            Initialize();
            populateGrid();
            return traceResult();
        }

        public string LCS()
        {
            Initialize();
            populateGrid();
            return traceback(left.Length, right.Length);
        }

        protected virtual void Initialize() {}

        public int EditDistance()
        {
            int[,] cost = new int[left.Length + 1, right.Length + 1];
            cost.Initialize();

            for (int i = 0; i < cost.GetLength(0); i++)
            {
                cost[i, 0] = i;
            }

            for (int j = 0; j < cost.GetLength(1); j++)
            {
                cost[0, j] = j;
            }

            int replacementCost;
            for (int i = 1; i < cost.GetLength(0); i++)
            {
                for (int j = 1; j < cost.GetLength(1); j++)
                {
                    replacementCost = (left[i-1] == right[j-1]) ? 0 : 1;
                    cost[i, j] = Math.Min(
                        Math.Min(cost[i-1, j] + 1, cost[i, j-1] + 1),
                        cost[i-1, j-1] + replacementCost
                    );
                }
            }

            return cost[left.Length, right.Length];
        }

        private DiffResult traceResult() 
        { 
            var result = new DiffResult();
            traceResult(result, left.Length, right.Length);
            return result;
        }

        private void traceResult(DiffResult result, int i, int j)
        {
            if (i > 0 && j > 0 && tally[i, j].IsMatch)
            {
                traceResult(result, i-1, j-1);
                result.Unchanged(left[i-1]);
                return;
            }

            if (j > 0 && (i == 0 || compareNext(i, j) > 0)) 
            {
                traceResult(result, i, j-1);
                result.Addition(right[j-1]);
            }
            else if (i > 0 && (j == 0 || compareNext(i, j) < 0))
            {
                traceResult(result, i-1, j);
                result.Deletion(left[i-1]);
            }
        }

        private int compareNext(int i, int j)
        {
            bool cmp = tally[i-1, j].LocalMax < tally[i, j-1].LocalMax;
            return cmp ? 1 : -1;
        }

        private string traceback(int i, int j)
        {
            if (i == 0 || j == 0) 
            {
                return "";
            }

            if (tally[i, j].IsMatch) 
            {
                return traceback(i-1, j-1) + left[i-1];
            }

            if (tally[i-1, j].LocalMax > tally[i, j-1].LocalMax)
            {
                return traceback(i-1, j);
            }

            return traceback(i, j-1);
        }

        private void populateGrid()
        {
            setZeros();
            for (int i = 1; i <= left.Length; i++)
            {
                for (int j = 1; j <= right.Length; j++)
                {
                    bool isMatch;
                    int localMax;
                    if (left[i-1] == right[j-1])
                    {
                        isMatch = true;
                        localMax = 1 + tally[i-1, j-1].LocalMax;
                    }
                    else
                    {
                        isMatch = false;
                        localMax = Math.Max(tally[i, j-1].LocalMax, tally[i-1, j].LocalMax);
                    }
                    tally[i, j] = new Tally(isMatch, localMax);
                }
            }
        }

        private void setZeros()
        {
            for (int i = 0; i < tally.GetLength(0); i++)
            {
                tally[i, 0] = new Tally(false, 0);
            }
            
            for (int j = 0; j < tally.GetLength(1); j++)
            {
                tally[0, j] = new Tally(false, 0);
            }
        }
    }
}