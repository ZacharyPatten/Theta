// Theta
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.md" in th root project directory.
// SUPPORT: See "SUPPORT.md" in the root project directory.

using System;

namespace Theta
{

	/// <summary>Just syntax sugar. Not recommended for use outside the Theta Framework.</summary>
	internal class Code
	{
		/// <summary>Performs an assignment and returns the value of the assignment.</summary>
		/// <typeparam name="T">The type of the assignment and return.</typeparam>
		/// <param name="a">The variable to assign a value to.</param>
		/// <param name="b">The value of the assignment and return.</param>
		/// <returns>The value of th assignment.</returns>
        public static T ReturnAssign<T>(ref T a, T b)
		{
			a = b;
			return b;
		}

        public static void Assert<Exception>(bool assertion, string message)
			where Exception : System.Exception
		{
			if (!assertion)
				throw new System.Exception(message);
		}

        public static void Assert<Exception>(bool assertion)
			where Exception : System.Exception
		{
			if (!assertion)
				throw new System.Exception();
		}

		/// <summary>Asserts that an argument not be null valued.</summary>
		/// <param name="obj">The argument to check.</param>
        /// <param name="parameterName">The name of the variable.</param>
        public static void AssertArgNonNull<T>(T obj, string parameterName)
		{
			if (obj == null)
                throw new System.ArgumentNullException(parameterName);
		}

        /// <summary>Asserts that an array argument not contain null values.</summary>
        /// <param name="array">The array argument to check for nulls in.</param>
        /// <param name="parameterName">The name of the array variable.</param>
        public static void AssertArgArrayNonNulls<T>(T[] array, string parameterName)
        {
            // null check for array
            AssertArgNonNull(array, parameterName);
            // null check for array contents
            for (int i = 0; i < array.Length; i++)
                if (array[i] == null)
                    throw new System.ArgumentNullException("The array argument contains null values.", parameterName);
        }
        
        /// <summary>Performs an "is" type comparison followed by as "as" conversion and performs an operation on the converted value.</summary>
        /// <typeparam name="T">The generic type to convert the object to.</typeparam>
        /// <param name="obj">The object to convert.</param>
        /// <param name="function">The function to perform if the "is" check returns true.</param>
        /// <returns>The result of the "is" check.</returns>
        public static bool IfAs<T>(object obj, Step<T> function)
            where T : class
        {
            if (obj is T)
            {
                function(obj as T);
                return true;
            }
            else
            {
                return false;
            }
        }
	}
}
