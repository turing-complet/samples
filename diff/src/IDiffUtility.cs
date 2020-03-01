namespace diff
{
    interface IDiffUtility
    {
        string LCS();
        DiffResult Compute();
        int EditDistance();
    }
}