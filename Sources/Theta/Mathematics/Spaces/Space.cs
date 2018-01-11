using Theta.Structures;

namespace Theta.Mathematics.Spaces
{
	public class Space<T>
	{
		private object _storage;
		private bool _complemented;

        public enum Type
        {
            Range,
            VectorRange,
            PointRadius,
        }

		#region constructors

		public Space(Stepper<T> stepper)
		{
			this._storage = stepper;
		}

		public Space(Stepper<Vector<T>> stepper)
		{
			this._storage = stepper;
		}

		public Space(Structure<T> structure)
		{
			this._storage = structure;
		}

		public Space(Structure<Vector<T>> structure)
		{
			this._storage = structure;
		}

		public Space(Range<T> range)
		{
			this._storage = range;
		}

		public Space(PointRadius<T> pointRadius)
		{

		}

		#endregion

		#region static

		public static bool Contains(Space<T> space, Vector<T> vector)
		{
			throw new System.NotImplementedException();

			if (space._storage is Stepper<T>)
			{
				throw new System.NotImplementedException();
			}
			else if (space._storage is Structure<T>)
			{
				// The vector must be 1D for structures
				if (vector.Dimensions == 1)
				{
					// Does the structure have an optimized "Contains" function
					if (space._storage is Structure.Auditable<T>)
					{
						Structure.Auditable<T> structure = space._storage as Structure.Auditable<T>;
						return structure.Contains(vector[0]);
					}
					else
					{
						throw new System.NotImplementedException();
						//Stepper.
					}
				}
			}
			else if (space._storage is Range<T>)
			{

			}
		}

		public static bool Contains(Space<T> a, Space<T> b)
		{
			throw new System.NotImplementedException();
		}

		public static bool Overlaps(Space<T> a, Space<T> b)
		{
			throw new System.NotImplementedException();
		}

		public static Space<T> Intersect(Space<T> a, Space<T> b)
		{
			throw new System.NotImplementedException();
		}

		public static Space<T> Union(Space<T> a, Space<T> b)
		{
			throw new System.NotImplementedException();
		}

		public static Space<T> Complement(Space<T> a, Space<T> b)
		{
			throw new System.NotImplementedException();
		}

		public new static bool Equals(Space<T> a, Space<T> b)
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region instance

		public bool Contains(Vector<T> vector)
		{ return Space<T>.Contains(this, vector); }
		public bool Contains(Space<T> b)
		{ return Space<T>.Contains(this, b); }
		public bool Overlaps(Space<T> b)
		{ return Space<T>.Overlaps(this, b); }
		public Space<T> Intersect(Space<T> b)
		{ return Space<T>.Intersect(this, b); }
		public Space<T> Union(Space<T> b)
		{ return Space<T>.Union(this, b); }
		public Space<T> Complement(Space<T> b)
		{ return Space<T>.Complement(this, b); }

		#endregion

		#region operators

		public static bool operator ==(Space<T> a, Space<T> b)
		{ return Equals(a, b); }
		public static bool operator !=(Space<T> a, Space<T> b)
		{ return !Equals(a, b); }
		/// <summary>Complement</summary>
		public static Space<T> operator ^(Space<T> a, Space<T> b)
		{ return Space<T>.Complement(a, b); }
		/// <summary>Union</summary>
		public static Space<T> operator |(Space<T> a, Space<T> b)
		{ return Space<T>.Union(a, b); }
		/// <summary>Intersection</summary>
		public static Space<T> operator &(Space<T> a, Space<T> b)
		{ return Space<T>.Intersect(a, b); }

		#endregion
	}
}
