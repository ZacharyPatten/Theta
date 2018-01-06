// Theta
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.md" in th root project directory.
// SUPPORT: See "SUPPORT.md" in the root project directory.

namespace Theta.Mathematics.Spaces
{
	/// <summary>Represents a point in N-D space and a radius.</summary>
	/// <typeparam name="T">The generic numeric type for computations.</typeparam>
	public class PointRadius<T>
	{
		private Vector<T> _center;
		private T _radius;

		#region constructor

		public PointRadius(Vector<T> center, T radius)
		{
			if (object.ReferenceEquals(null, center))
				throw new System.ArgumentNullException("center");
			if (object.ReferenceEquals(null, radius))
				throw new System.ArgumentNullException("radius");
			if (!Compute<T>.GreaterThan(radius, Compute<T>.Zero))
				throw new System.ArithmeticException("invalid radius on PointRadius construction !(radius > 0)");
			this._center = center;
			this._radius = radius;
		}

		#endregion

		#region static

		public static bool Contains(PointRadius<T> range, Vector<T> vector)
		{
            throw new System.NotImplementedException();
		}

		public static bool Contains(PointRadius<T> a, PointRadius<T> b)
		{
			throw new System.NotImplementedException();
		}

		public static bool Overlaps(PointRadius<T> a, PointRadius<T> b)
		{
			throw new System.NotImplementedException();
		}

		public static Space<T> Intersect(PointRadius<T> a, PointRadius<T> b)
		{
			throw new System.NotImplementedException();
		}

		public static Space<T> Union(PointRadius<T> a, PointRadius<T> b)
		{
			throw new System.NotImplementedException();
		}

		public static Space<T> Complement(PointRadius<T> a, PointRadius<T> b)
		{
			throw new System.NotImplementedException();
		}

		public new static bool Equals(PointRadius<T> a, PointRadius<T> b)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region instance

		public bool Contains(Vector<T> vector)
		{ return PointRadius<T>.Contains(this, vector); }
		public bool Contains(PointRadius<T> b)
		{ return PointRadius<T>.Contains(this, b); }
		public bool Overlaps(PointRadius<T> b)
		{ return PointRadius<T>.Overlaps(this, b); }
		public Space<T> Intersect(PointRadius<T> b)
		{ return PointRadius<T>.Intersect(this, b); }
		public Space<T> Union(PointRadius<T> b)
		{ return PointRadius<T>.Union(this, b); }
		public Space<T> Complement(PointRadius<T> b)
		{ return PointRadius<T>.Complement(this, b); }

		#endregion

		#region operators

		/// <summary>Checks for equality.</summary>
		/// <param name="a">First operand.</param>
		/// <param name="b">Second operand.</param>
		/// <returns>True if equal; False if not.</returns>
		public static bool operator ==(PointRadius<T> a, PointRadius<T> b)
		{ return Equals(a, b); }
		/// <summary>Checks for inequality.</summary>
		/// <param name="a">First operand.</param>
		/// <param name="b">Second operand.</param>
		/// <returns>False if equal; True if not.</returns>
		public static bool operator !=(PointRadius<T> a, PointRadius<T> b)
		{ return !Equals(a, b); }
		/// <summary>Complement</summary>
		public static Space<T> operator ^(PointRadius<T> a, PointRadius<T> b)
		{ return PointRadius<T>.Complement(a, b); }
		/// <summary>Union</summary>
		public static Space<T> operator |(PointRadius<T> a, PointRadius<T> b)
		{ return PointRadius<T>.Union(a, b); }
		/// <summary>Intersection</summary>
		public static Space<T> operator &(PointRadius<T> a, PointRadius<T> b)
		{ return PointRadius<T>.Intersect(a, b); }

		#endregion

		/// <summary>Checks for equality.</summary>
		/// <param name="obj">The other operand to check for equality.</param>
		/// <returns>True if equal; False if not.</returns>
		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(this, obj))
				return true;
			else if (object.ReferenceEquals(null, obj))
				return false;
			else if (obj is PointRadius<T>)
				return this == (obj as PointRadius<T>);
			else
				return false;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>The hash code for this instance.</returns>
		public override int GetHashCode()
		{
			return _center.GetHashCode() ^ _radius.GetHashCode();
		}
	}
}
