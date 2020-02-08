using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
}