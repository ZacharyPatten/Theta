using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Theta.RegularExpressions
{
    internal static class RegularExpressionLibrary
    {
        internal static string spacePattern = @"[\s]*";
        internal static string identPattern = @"[_a-zA-Z][_a-zA-Z0-9]*";
        internal static string paramPattern = @"(ref|out)?" + spacePattern + identPattern + spacePattern + identPattern;
        internal static string openParenPattern = @"\(";
        internal static string closeParenPattern = @"\)";

        public static string methodPattern =
                  identPattern + spacePattern
                + identPattern + spacePattern
                + openParenPattern + spacePattern
                + "(" + paramPattern + spacePattern + "," + spacePattern + ")*" + spacePattern
                + "(" + paramPattern + spacePattern + ")?" + spacePattern
                + closeParenPattern;

        //internal static string Date
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //}

        //internal static string Date
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //}
    }
}
