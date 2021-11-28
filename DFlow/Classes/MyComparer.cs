using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPortal
{
    class MyComparer : IComparer<string>
    {
        [System.Runtime.InteropServices.DllImport("shlwapi.dll")]
        static extern Int32 StrCmpLogicalW(string s1, string s2);

        public int Compare(string x, string y)
        {
            return StrCmpLogicalW(x, y);
        }
    }
}