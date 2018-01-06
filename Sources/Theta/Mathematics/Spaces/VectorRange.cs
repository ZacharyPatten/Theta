using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theta.Mathematics.Spaces
{
    public class VectorRange<T>
    {
        private Vector<T> _min;
		private Vector<T> _max;

		#region validation

		private static bool Validate(Vector<T> a, Vector<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			if (a.Dimensions != b.Dimensions)
				throw new System.ArithmeticException("dimension mismatch on range validation");
			for (int i = 0; i < a.Dimensions; i++)
				if (Compute<T>.LessThan(b[i], a[i]) || Compute<T>.GreaterThan(a[i], b[i]))
					return false;
			return true;
		}

		#endregion

		#region properties

		public Vector<T> Min
		{
			get { return this._min; }
			set
			{
				if (object.ReferenceEquals(null, value))
					throw new System.ArgumentNullException("value");
				if (this._min.Dimensions != value.Dimensions)
					throw new System.ArithmeticException("dimension mismatch while setting range minimum");
				if (!Validate(value, this._max))
					throw new System.ArithmeticException("invalid vectors when setting range minimum !(min <= max)");
				this._min = value;
			}
		}

		public Vector<T> Max
		{
			get { return this._max; }
			set
			{
				if (object.ReferenceEquals(null, value))
					throw new System.ArgumentNullException("value");
				if (this._max.Dimensions != value.Dimensions)
					throw new System.ArithmeticException("dimension mismatch while setting range maximum");
				if (!Validate(this._min, value))
					throw new System.ArithmeticException("invalid vectors when setting range maximum !(min <= max)");
				this._max = value;
			}
		}

		public int Dimensions
		{
			get
			{
				return this._min.Dimensions;
			}
		}

		public Vector<T> MidPoint
		{
			get
			{
				Vector<T> midpoint = new Vector<T>(this.Dimensions);
				for (int i = 0; i < this.Dimensions; i++)
					midpoint[i] = Compute<T>.Mean((Step<T> step) => { step(this._min[i]); step(this._max[i]); });
				return midpoint;
			}
		}

		#endregion

		#region constructor

		public VectorRange(Vector<T> min, Vector<T> max)
		{
			if (object.ReferenceEquals(null, min))
				throw new System.ArgumentNullException("min");
			if (object.ReferenceEquals(null, max))
				throw new System.ArgumentNullException("max");
			if (min.Dimensions != max.Dimensions)
					throw new System.ArithmeticException("dimension mismatch during range construction");
			if (!VectorRange<T>.Validate(min, max))
				throw new System.ArithmeticException("invalid vectors during range construction !(min <= max)");
			this._min = min;
			this._max = max;
		}

		#endregion

		#region static
		
		public static bool Contains(VectorRange<T> range, Vector<T> vector)
		{
			if (object.ReferenceEquals(null, range))
				throw new System.ArgumentNullException("range");
			if (object.ReferenceEquals(null, vector))
				throw new System.ArgumentNullException("vector");
			if (range.Dimensions != vector.Dimensions)
				throw new System.ArithmeticException("dimension mismatch during range construction");
			for (int i = 0; i < range.Dimensions; i++)
				if (Compute<T>.LessThan(vector[i], range._min[i]) || Compute<T>.GreaterThan(vector[i], range._max[i]))
					return false;
			return true;
		}

		public static bool Contains(VectorRange<T> a, VectorRange<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			if (a.Dimensions != b.Dimensions)
				throw new System.ArithmeticException("dimension mismatch during range construction");
			for (int i = 0; i < a.Dimensions; i++)
				if (Compute<T>.LessThan(b._min[i], a._min[i]) || Compute<T>.GreaterThan(b._max[i], a._max[i]))
					return false;
			return true;
		}

		public static bool Overlaps(VectorRange<T> a, VectorRange<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			if (a.Dimensions != b.Dimensions)
				throw new System.ArithmeticException("dimension mismatch during range construction");
			for (int i = 0; i < a.Dimensions; i++)
				if (Compute<T>.LessThan(a._max[i], b._min[i]) || Compute<T>.GreaterThan(a._min[i], b._max[i]))
					return false;
			return true;
		}

		public static VectorRange<T> Intersect(VectorRange<T> a, VectorRange<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			if (a.Dimensions != b.Dimensions)
				throw new System.ArithmeticException("dimension mismatch during range intersect");
			if (!VectorRange<T>.Overlaps(a, b))
				//throw new System.ArithmeticException("attempting to intersect non-overlapping ranges");
				return null;
			Vector<T> min = new Vector<T>(a.Dimensions);
			Vector<T> max = new Vector<T>(a.Dimensions);
			for (int i = 0; i < a.Dimensions; i++)
			{
				if (Compute<T>.GreaterThan(a._min[i], b._min[i]))
					min[i] = a._min[i];
				else
					min[i] = b._min[i];
				if (Compute<T>.LessThan(a._max[i], b._max[i]))
					max[i] = a._max[i];
				else
					max[i] = b._max[i];
			}
			return new VectorRange<T>(min, max);
		}

		public static VectorRange<T>[] Union(VectorRange<T> a, VectorRange<T> b)
		{
			throw new System.NotImplementedException();
			//if (object.ReferenceEquals(null, a))
			//	throw new System.ArgumentNullException("a");
			//if (object.ReferenceEquals(null, b))
			//	throw new System.ArgumentNullException("b");
			//if (a.Dimensions != b.Dimensions)
			//	throw new System.ArithmeticException("dimension mismatch during range union");
			//if (!VectorRange<T>.Overlaps(a, b))
			//	throw new System.ArithmeticException("attempting to union non-overlapping ranges");
			//if (!VectorRange<T>.IsContiguous(a, b))
			//	throw new System.ArithmeticException("attempting to union non-contiguous ranges");
			//Vector<T> min = new Vector<T>(a.Dimensions);
			//Vector<T> max = new Vector<T>(a.Dimensions);
			//for (int i = 0; i < a.Dimensions; i++)
			//{
			//	if (Compute<T>.LessThan(a._min[i], b._min[i]))
			//		min[i] = a._min[i];
			//	else
			//		min[i] = b._min[i];
			//	if (Compute<T>.GreaterThan(a._max[i], b._max[i]))
			//		max[i] = a._max[i];
			//	else
			//		max[i] = b._max[i];
			//}
			//return new VectorRange<T>(min, max);
		}

		public static Space<T> Complement(VectorRange<T> a, VectorRange<T> b)
		{
            throw new System.NotImplementedException();
            //if (object.ReferenceEquals(null, a))
            //    throw new System.ArgumentNullException("a");
            //if (object.ReferenceEquals(null, b))
            //    throw new System.ArgumentNullException("b");
            //if (a.Dimensions != b.Dimensions)
            //    throw new System.ArithmeticException("dimension mismatch during range complement");
            //if (!VectorRange<T>.Overlaps(a, b))
            //    return a;
            //if (a._min == b._min && a._max == b._max)
            //    //throw new System.ArithmeticException("attempting to complement identical ranges");
            //    return null;
            //Vector<T> min = new Vector<T>(a.Dimensions);
            //Vector<T> max = new Vector<T>(a.Dimensions);
            //for (int i = 0; i < a.Dimensions; i++)
            //{
            //    if (Compute<T>.LessThan(a._min[i], b._min[i]) && Compute<T>.GreaterThan(a._max[i], b._max[i]))
            //        throw new System.ArithmeticException("range compement has a sandwiched complement");
            //    if (Compute<T>.LessThan(a._min[i], b._min[i]))
            //    {
            //        min[i] = a._min[i];
            //        max[i] = b._min[i];
            //    }
            //    else
            //    {
            //        min[i] = b._max[i];
            //        max[i] = a._max[i];
            //    }
            //}
            //return new VectorRange<T>(min, max);
		}

		public static VectorRange<T>[] Split(VectorRange<T> range, bool inclusiveEdges, params Vector<T>[] vectors)
		{
			throw new System.NotImplementedException();
			// how to handle the edges? include them in each split or not? make enum for a parameter?
		}
		
		public static bool IsContiguous(VectorRange<T> a, VectorRange<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			if (a.Dimensions != b.Dimensions)
				throw new System.ArithmeticException("dimension mismatch during range contiguous check");
			if (a.Dimensions == 1 && VectorRange<T>.Overlaps(a, b))
				return true;
			return VectorRange<T>.Contains(a, b) || VectorRange<T>.Contains(b, a) || a._min == b._max || a._max == b._min;
		}

		public new static bool Equals(VectorRange<T> a, VectorRange<T> b)
		{
			if (object.ReferenceEquals(null, a))
				throw new System.ArgumentNullException("a");
			if (object.ReferenceEquals(null, b))
				throw new System.ArgumentNullException("b");
			if (a.Dimensions != b.Dimensions)
				throw new System.ArithmeticException("dimension mismatch during range contiguous check");
			return object.ReferenceEquals(a, b) || (a._min == b._min && a._max == b._max);
		}

		#endregion

		#region instance

		public bool Contains(Vector<T> vector)
		{ return VectorRange<T>.Contains(this, vector); }
		public bool Contains(VectorRange<T> b)
		{ return VectorRange<T>.Contains(this, b); }
		public bool Overlaps(VectorRange<T> b)
		{ return VectorRange<T>.Overlaps(this, b); }
		public VectorRange<T> Intersect(VectorRange<T> b)
		{ return VectorRange<T>.Intersect(this, b); }
		public VectorRange<T>[] Union(VectorRange<T> b)
		{ return VectorRange<T>.Union(this, b); }
		public Space<T> Complement(VectorRange<T> b)
		{ return VectorRange<T>.Complement(this, b); }
		public VectorRange<T>[] Split(bool inclusiveEdges, params Vector<T>[] vectors)
		{ return VectorRange<T>.Split(this, inclusiveEdges, vectors); }
		public bool IsContiguous(VectorRange<T> b)
		{ return VectorRange<T>.IsContiguous(this, b); }

		#endregion

		#region operators
		
        public static implicit operator VectorRange<T>(Range<T> range)
        { return new VectorRange<T>(range.Min, range.Max); }
		public static bool operator ==(VectorRange<T> a, VectorRange<T> b)
		{ return Equals(a, b); }
		public static bool operator !=(VectorRange<T> a, VectorRange<T> b)
		{ return !Equals(a, b); }
		/// <summary>Complement</summary>
        public static Space<T> operator ^(VectorRange<T> a, VectorRange<T> b)
		{ return VectorRange<T>.Complement(a, b); }
		/// <summary>Union</summary>
		public static VectorRange<T>[] operator |(VectorRange<T> a, VectorRange<T> b)
		{ return VectorRange<T>.Union(a, b); }
		/// <summary>Intersection</summary>
		public static VectorRange<T> operator &(VectorRange<T> a, VectorRange<T> b)
		{ return VectorRange<T>.Intersect(a, b); }

		#endregion

		#region overrides

		public override string ToString()
		{
			return string.Concat(this._min, "->", this._max);
		}

		public override bool Equals(object b)
		{
			if (!(b is VectorRange<T>))
				return false;
			return VectorRange<T>.Equals(this, b as VectorRange<T>);
		}

		public override int GetHashCode()
		{
			return this._min.GetHashCode() ^ this._max.GetHashCode();
		}
		#endregion
    }
}
