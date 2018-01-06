// Theta
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.md" in th root project directory.
// SUPPORT: See "SUPPORT.md" in the root project directory.

namespace Theta
{
	/// <summary>Unary operator for criteria testing.</summary>
	/// <typeparam name="T">The type of item for the predicate.</typeparam>
	/// <param name="item">The item of the predicate.</param>
	/// <returns>True if the item passes the criteria test. False if not.</returns>
	[System.Serializable]
	public delegate bool Predicate<T>(T item);
}
