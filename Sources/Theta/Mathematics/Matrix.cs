// Theta
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.md" in th root project directory.
// SUPPORT: See "SUPPORT.md" in the root project directory.

using Theta.Measurements;
namespace Theta.Mathematics
{
	/// <summary>A matrix of arbitrary dimensions implemented as a flattened array.</summary>
	/// <typeparam name="T">The numeric type of this Matrix.</typeparam>
	[System.Serializable]
	public class Matrix<T>
	{
		#region delegates

		public class Delegates
		{
			/// <summary>Creates a zero matrix of the given dimensions.</summary>
			/// <param name="rows">The row count of the desired matrix.</param>
			/// <param name="columns">The column count of the desired matrix.</param>
			/// <returns>A zero matrix of the desired dimensions.</returns>
			public delegate Matrix<T> Matrix_FactoryZero(int rows, int columns);
			/// <summary>Creates a ones matrix of the given dimensions.</summary>
			/// <param name="rows">The row count of the desired matrix.</param>
			/// <param name="columns">The column count of the desired matrix.</param>
			/// <returns>A ones matrix of the desired dimensions.</returns>
			public delegate Matrix<T> Matrix_FactoryOne(int rows, int columns);
			/// <summary>Creates an identity (ones along diagnol, zeros otherwise) matrix of the given dimensions.</summary>
			/// <param name="rows">The row count of the desired matrix.</param>
			/// <param name="columns">The column count of the desired matrix.</param>
			/// <returns>An identity matrix of the desired dimensions.</returns>
			public delegate Matrix<T> Matrix_FactoryIdentity(int rows, int columns);
			/// <summary>Determines if a matrix is symetric.</summary>
			/// <param name="matrix">The matrix to determine symetry on.</param>
			/// <returns>True if the matrix is symetric; false if not.</returns>
			public delegate bool Matrix_IsSymetric(Matrix<T> matrix);
			/// <summary>Negates all the values in a matrix.</summary>
			/// <param name="matrix">The matrix to have its values negated.</param>
			/// <returns>The resulting matrix after the negations.</returns>
			public delegate Matrix<T> Matrix_Negate(Matrix<T> matrix);
			/// <summary>Does standard addition of two matrices.</summary>
			/// <param name="left">The left matrix of the addition.</param>
			/// <param name="right">The right matrix of the addition.</param>
			/// <returns>The resulting matrix after the addition.</returns>
			public delegate Matrix<T> Matrix_Add(Matrix<T> left, Matrix<T> right);
			/// <summary>Subtracts a scalar from all the values in a matrix.</summary>
			/// <param name="left">The matrix to have the values subtracted from.</param>
			/// <param name="right">The scalar to subtract from all the matrix values.</param>
			/// <returns>The resulting matrix after the subtractions.</returns>
			public delegate Matrix<T> Matrix_Subtract(Matrix<T> left, Matrix<T> right);
			/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
			/// <param name="left">The left matrix of the multiplication.</param>
			/// <param name="right">The right matrix of the multiplication.</param>
			/// <returns>The resulting matrix of the multiplication.</returns>
			public delegate Matrix<T> Matrix_Multiply(Matrix<T> left, Matrix<T> right);
			/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
			/// <param name="matrix">The left matrix of the multiplication.</param>
			/// <param name="right">The right matrix of the multiplication.</param>
			/// <returns>The resulting matrix of the multiplication.</returns>
			public delegate Vector<T> Matrix_Multiply_vector(Matrix<T> matrix, Vector<T> right);
			/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
			/// <param name="matrix">The matrix to have the values multiplied.</param>
			/// <param name="right">The scalar to multiply the values by.</param>
			/// <returns>The resulting matrix after the multiplications.</returns>
			public delegate Matrix<T> Matrix_Multiply_scalar(Matrix<T> matrix, T right);
			/// <summary>Divides all the values in the matrix by a scalar.</summary>
			/// <param name="left">The matrix to divide the values of.</param>
			/// <param name="right">The scalar to divide all the matrix values by.</param>
			/// <returns>The resulting matrix with the divided values.</returns>
			public delegate Matrix<T> Matrix_Divide(Matrix<T> left, T right);
			/// <summary>Applies a power to a square matrix.</summary>
			/// <param name="matrix">The matrix to be powered by.</param>
			/// <param name="power">The power to apply to the matrix.</param>
			/// <returns>The resulting matrix of the power operation.</returns>
			public delegate Matrix<T> Matrix_Power(Matrix<T> matrix, int power);
			/// <summary>Gets the minor of a matrix.</summary>
			/// <param name="matrix">The matrix to get the minor of.</param>
			/// <param name="row">The restricted row to form the minor.</param>
			/// <param name="column">The restricted column to form the minor.</param>
			/// <returns>The minor of the matrix.</returns>
			public delegate Matrix<T> Matrix_Minor(Matrix<T> matrix, int row, int column);
			/// <summary>Combines two matrices from left to right 
			/// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
			/// <param name="left">The left matrix of the concatenation.</param>
			/// <param name="right">The right matrix of the concatenation.</param>
			/// <returns>The resulting matrix of the concatenation.</returns>
			public delegate Matrix<T> Matrix_ConcatenateRowWise(Matrix<T> left, Matrix<T> right);
			/// <summary>Calculates the determinent of a square matrix.</summary>
			/// <param name="matrix">The matrix to calculate the determinent of.</param>
			/// <returns>The determinent of the matrix.</returns>
			public delegate T Matrix_Determinent(Matrix<T> matrix);
			/// <summary>Calculates the echelon of a matrix (aka REF).</summary>
			/// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
			/// <returns>The echelon of the matrix (aka REF).</returns>
			public delegate Matrix<T> Matrix_Echelon(Matrix<T> matrix);
			/// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
			/// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
			/// <returns>The reduced echelon of the matrix (aka RREF).</returns>
			public delegate Matrix<T> Matrix_ReducedEchelon(Matrix<T> matrix);
			/// <summary>Calculates the inverse of a matrix.</summary>
			/// <param name="matrix">The matrix to calculate the inverse of.</param>
			/// <returns>The inverse of the matrix.</returns>
			public delegate Matrix<T> Matrix_Inverse(Matrix<T> matrix);
			/// <summary>Calculates the adjoint of a matrix.</summary>
			/// <param name="matrix">The matrix to calculate the adjoint of.</param>
			/// <returns>The adjoint of the matrix.</returns>
			public delegate Matrix<T> Matrix_Adjoint(Matrix<T> matrix);
			/// <summary>Returns the transpose of a matrix.</summary>
			/// <param name="matrix">The matrix to transpose.</param>
			/// <returns>The transpose of the matrix.</returns>
			public delegate Matrix<T> Matrix_Transpose(Matrix<T> matrix);
			/// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
			/// <param name="matrix">The matrix to decompose.</param>
			/// <param name="lower">The computed lower triangular matrix.</param>
			/// <param name="upper">The computed upper triangular matrix.</param>
			public delegate void Matrix_DecomposeLU(Matrix<T> matrix, out Matrix<T> lower, out Matrix<T> upper);
			/// <summary>Does a value equality check.</summary>
			/// <param name="left">The first matrix to check for equality.</param>
			/// <param name="right">The second matrix to check for equality.</param>
			/// <returns>True if values are equal, false if not.</returns>
			public delegate bool Matrix_EqualsByValue(Matrix<T> left, Matrix<T> right);
			/// <summary>Does a value equality check with leniency.</summary>
			/// <param name="left">The first matrix to check for equality.</param>
			/// <param name="right">The second matrix to check for equality.</param>
			/// <param name="leniency">How much the values can vary but still be considered equal.</param>
			/// <returns>True if values are equal, false if not.</returns>
			public delegate bool Matrix_EqualsByValue_leniency(Matrix<T> left, Matrix<T> right, T leniency);
		}

		#endregion

		#region field

		/// <summary>The flattened array of this matrix.</summary>
		public readonly T[] _matrix;
		private int _rows;
		private int _columns;

		#endregion

		#region properties

		/// <summary>The number of rows in the matrix.</summary>
		public int Rows { get { return this._rows; } }
		/// <summary>The number of columns in the matrix.</summary>
		public int Columns { get { return this._columns; } }
		/// <summary>Determines if the matrix is square.</summary>
		public bool IsSquare { get { return this._rows == this._columns; } }
		/// <summary>Determines if the matrix is a vector.</summary>
		public bool IsVector { get { return this._columns == 1; } }
		/// <summary>Determines if the matrix is a 2 component vector.</summary>
		public bool Is2x1 { get { return this._rows == 2 && this._columns == 1; } }
		/// <summary>Determines if the matrix is a 3 component vector.</summary>
		public bool Is3x1 { get { return this._rows == 3 && this._columns == 1; } }
		/// <summary>Determines if the matrix is a 4 component vector.</summary>
		public bool Is4x1 { get { return this._rows == 4 && this._columns == 1; } }
		/// <summary>Determines if the matrix is a 2 square matrix.</summary>
		public bool Is2x2 { get { return this._rows == 2 && this._columns == 2; } }
		/// <summary>Determines if the matrix is a 3 square matrix.</summary>
		public bool Is3x3 { get { return this._rows == 3 && this._columns == 3; } }
		/// <summary>Determines if the matrix is a 4 square matrix.</summary>
		public bool Is4x4 { get { return this._rows == 4 && this._columns == 4; } }

		/// <summary>Standard row-major matrix indexing.</summary>
		/// <param name="row">The row index.</param>
		/// <param name="column">The column index.</param>
		/// <returns>The value at the given indeces.</returns>
		public T this[int row, int column]
		{
			get
			{
				if (row < 0 || row > this._rows)
					throw new System.ArgumentOutOfRangeException("index out of bounds: row");
				if (column < 0 || column > this._columns)
					throw new System.ArgumentOutOfRangeException("index out of bounds: column");
				return (this._matrix)[row * this._columns + column];
			}
			set
			{
				if (row < 0 || row > this._rows)
					throw new System.ArgumentOutOfRangeException("index out of bounds: row");
				if (column < 0 || column > this._columns)
					throw new System.ArgumentOutOfRangeException("index out of bounds: column");
				(this._matrix)[row * this._columns + column] = value;
			}
		}

		#endregion

		#region constructors

		/// <summary>Constructs a new zero-matrix of the given dimensions (using FlattenedArray format).</summary>
		/// <param name="rows">The number of row dimensions.</param>
		/// <param name="columns">The number of column dimensions.</param>
		public Matrix(int rows, int columns)
		{
			if (rows < 1)
				throw new System.ArgumentException("Invalid rows on matrix contruction");
			if (columns < 1)
				throw new System.ArgumentException("Invalid columns on matrix contruction");

			this._matrix = new T[rows * columns];
			this._rows = rows;
			this._columns = columns;
		}

		/// <summary>Constructs a matrix from a T[,] (using FlattenedArray format).</summary>
		/// <param name="matrix">The float[,] to wrap in a matrix class.</param>
		public Matrix(T[,] matrix)
		{
			this._rows = matrix.GetLength(0);
			this._columns = matrix.GetLength(1);
			this._matrix = new T[this._rows * this._columns];
			T[] this_matrix_flat = this._matrix;
            int current = 0;
            for (int i = 0; i < this._rows; i++)
                for (int j = 0; j < this._columns; j++)
                    this_matrix_flat[current++] = matrix[i, j];
		}

		private Matrix(Matrix<T> matrix)
		{
			this._rows = matrix._rows;
			this._columns = matrix.Columns;
			this._matrix = (matrix._matrix).Clone() as T[];
		}

		internal Matrix(Vector<T> vector)
		{
			this._rows = vector.Dimensions;
			this._columns = 1;
			this._matrix = vector._vector.Clone() as T[];
		}

		#endregion

		#region factories

		/// <summary>Constructs a new zero-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed zero-matrix.</returns>
		public static Matrix<T> FactoryZero(int rows, int columns)
		{
			return Matrix<T>.Matrix_FactoryZero(rows, columns);
		}

		/// <summary>Constructs a new identity-matrix of the given dimensions.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed identity-matrix.</returns>
		public static Matrix<T> FactoryIdentity(int rows, int columns)
		{
			return Matrix<T>.Matrix_FactoryIdentity(rows, columns);
		}

		/// <summary>Constructs a new matrix where every entry is 1.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <returns>The newly constructed matrix filled with 1's.</returns>
		public static Matrix<T> FactoryOne(int rows, int columns)
		{
			return Matrix<T>.Matrix_FactoryOne(rows, columns);
		}

		/// <summary>Constructs a new matrix where every entry is the same uniform value.</summary>
		/// <param name="rows">The number of rows of the matrix.</param>
		/// <param name="columns">The number of columns of the matrix.</param>
		/// <param name="uniform">The value to assign every spot in the matrix.</param>
		/// <returns>The newly constructed matrix filled with the uniform value.</returns>
		public static Matrix<T> FactoryUniform(int rows, int columns, T uniform)
		{
			Matrix<T> matrix;
			if (!(rows > 0))
				throw new System.ArgumentOutOfRangeException("rows");
			if (!(columns > 0))
				throw new System.ArgumentOutOfRangeException("columns");
			matrix = new Matrix<T>(rows, columns);
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < columns; j++)
					matrix[i, j] = uniform;
			return matrix;
		}

		/// <summary>Constructs a 2-component vector matrix with all values being 0.</summary>
		/// <returns>The constructed 2-component vector matrix.</returns>
		public static Matrix<T> Factory2x1() { return new Matrix<T>(2, 1); }
		/// <summary>Constructs a 3-component vector matrix with all values being 0.</summary>
		/// <returns>The constructed 3-component vector matrix.</returns>
		public static Matrix<T> Factory3x1() { return new Matrix<T>(3, 1); }
		/// <summary>Constructs a 4-component vector matrix with all values being 0.</summary>
		/// <returns>The constructed 4-component vector matrix.</returns>
		public static Matrix<T> Factory4x1() { return new Matrix<T>(4, 1); }

		/// <summary>Constructs a 2x2 matrix with all values being 0.</summary>
		/// <returns>The constructed 2x2 matrix.</returns>
		public static Matrix<T> Factory2x2() { return new Matrix<T>(2, 2); }
		/// <summary>Constructs a 3x3 matrix with all values being 0.</summary>
		/// <returns>The constructed 3x3 matrix.</returns>
		public static Matrix<T> Factory3x3() { return new Matrix<T>(3, 3); }
		/// <summary>Constructs a 4x4 matrix with all values being 0.</summary>
		/// <returns>The constructed 4x4 matrix.</returns>
		public static Matrix<T> Factory4x4() { return new Matrix<T>(4, 4); }

		///// <param name="angle">Angle of rotation in radians.</param>
		//public static Matrix<T> Factory3x3RotationX(T angle)
		//{
		//	T cos = _cos(angle);
		//	T sin = _sin(angle);
		//	return new Matrix<T>(new T[,] {
		//		{ _one, _zero, _zero },
		//		{ _zero, cos, sin },
		//		{ _zero, _negate(sin), cos }});
		//}

		///// <param name="angle">Angle of rotation in radians.</param>
		//public static Matrix<T> Factory3x3RotationY(T angle)
		//{
		//	T cos = _cos(angle);
		//	T sin = _sin(angle);
		//	return new Matrix<T>(new T[,] {
		//		{ cos, _zero, _negate(sin) },
		//		{ _zero, _one, _zero },
		//		{ sin, _zero, cos }});
		//}

		///// <param name="angle">Angle of rotation in radians.</param>
		//public static Matrix<T> Factory3x3RotationZ(T angle)
		//{
		//	T cos = _cos(angle);
		//	T sin = _sin(angle);
		//	return new Matrix<T>(new T[,] {
		//		{ cos, _negate(sin), _zero },
		//		{ sin, cos, _zero },
		//		{ _zero, _zero, _zero }});
		//}

		///// <param name="angleX">Angle about the X-axis in radians.</param>
		///// <param name="angleY">Angle about the Y-axis in radians.</param>
		///// <param name="angleZ">Angle about the Z-axis in radians.</param>
		//public static Matrix<T> Factory3x3RotationXthenYthenZ(T angleX, T angleY, T angleZ)
		//{
		//	T xCos = _cos(angleX), xSin = _sin(angleX),
		//		yCos = _cos(angleY), ySin = _sin(angleY),
		//		zCos = _cos(angleZ), zSin = _sin(angleZ);
		//	return new Matrix<T>(new T[,] {
		//		{ _multiply(yCos, zCos), _negate(_multiply(yCos, zSin)), ySin },
		//		{ _add(_multiply(xCos, zSin), _multiply(_multiply(xSin, ySin), zCos)), _add(_multiply(xCos, zCos), _multiply(_multiply(xSin, ySin), zSin)), _negate(_multiply(xSin, yCos)) },
		//		{ _subtract(_multiply(xSin, zSin), _multiply(_multiply(xCos, ySin), zCos)), _add(_multiply(xSin, zCos), _multiply(_multiply(xCos, ySin), zSin)), _multiply(xCos, yCos) }});
		//}

		///// <param name="angleX">Angle about the X-axis in radians.</param>
		///// <param name="angleY">Angle about the Y-axis in radians.</param>
		///// <param name="angleZ">Angle about the Z-axis in radians.</param>
		//public static Matrix<T> Factory3x3RotationZthenYthenX(T angleX, T angleY, T angleZ)
		//{
		//	T xCos = _cos(angleX), xSin = _sin(angleX),
		//		yCos = _cos(angleY), ySin = _sin(angleY),
		//		zCos = _cos(angleZ), zSin = _sin(angleZ);
		//	return new Matrix<T>(new T[,] {
		//		{ _multiply(yCos, zCos), _subtract(_multiply(_multiply(zCos, xSin), ySin), _multiply(xCos, zSin)), _add(_multiply(_multiply(xCos, zCos), ySin), _multiply(xSin, zSin)) },
		//		{ _multiply(yCos, zSin), _add(_multiply(xCos, zCos), _multiply(_multiply(xSin, ySin), zSin)), _add(_multiply(_negate(zCos), xSin), _multiply(_multiply(xCos, ySin), zSin)) },
		//		{ _negate(ySin), _multiply(yCos, xSin), _multiply(xCos, yCos) }});
		//}

		///// <summary>Creates a 3x3 matrix initialized with a shearing transformation.</summary>
		///// <param name="shearXbyY">The shear along the X-axis in the Y-direction.</param>
		///// <param name="shearXbyZ">The shear along the X-axis in the Z-direction.</param>
		///// <param name="shearYbyX">The shear along the Y-axis in the X-direction.</param>
		///// <param name="shearYbyZ">The shear along the Y-axis in the Z-direction.</param>
		///// <param name="shearZbyX">The shear along the Z-axis in the X-direction.</param>
		///// <param name="shearZbyY">The shear along the Z-axis in the Y-direction.</param>
		///// <returns>The constructed shearing matrix.</returns>
		//public static Matrix<T> Factory3x3Shear(
		//	T shearXbyY, T shearXbyZ, T shearYbyX,
		//	T shearYbyZ, T shearZbyX, T shearZbyY)
		//{
		//	return new Matrix<T>(new T[,] {
		//		{ _one, shearYbyX, shearZbyX },
		//		{ shearXbyY, _one, shearYbyZ },
		//		{ shearXbyZ, shearYbyZ, _one }});
		//}

		#endregion

		#region operators

		/// <summary>Negates all the values in a matrix.</summary>
		/// <param name="matrix">The matrix to have its values negated.</param>
		/// <returns>The resulting matrix after the negations.</returns>
		public static Matrix<T> operator -(Matrix<T> matrix)
		{ return Matrix<T>.Matrix_Negate(matrix); }
		/// <summary>Does a standard matrix addition.</summary>
		/// <param name="left">The left matrix of the addition.</param>
		/// <param name="right">The right matrix of the addition.</param>
		/// <returns>The resulting matrix after teh addition.</returns>
		public static Matrix<T> operator +(Matrix<T> left, Matrix<T> right)
		{ return Matrix<T>.Matrix_Add(left, right); }
		/// <summary>Does a standard matrix subtraction.</summary>
		/// <param name="left">The left matrix of the subtraction.</param>
		/// <param name="right">The right matrix of the subtraction.</param>
		/// <returns>The result of the matrix subtraction.</returns>
		public static Matrix<T> operator -(Matrix<T> left, Matrix<T> right)
		{ return Matrix<T>.Matrix_Subtract(left, right); }
		/// <summary>Multiplies a vector by a matrix.</summary>
		/// <param name="left">The matrix of the multiplication.</param>
		/// <param name="right">The vector of the multiplication.</param>
		/// <returns>The resulting vector after the multiplication.</returns>
		public static Vector<T> operator *(Matrix<T> left, Vector<T> right)
		{ return Matrix<T>.Matrix_Multiply_vector(left, right); }
		/// <summary>Does a standard matrix multiplication.</summary>
		/// <param name="left">The left matrix of the multiplication.</param>
		/// <param name="right">The right matrix of the multiplication.</param>
		/// <returns>The resulting matrix after the multiplication.</returns>
		public static Matrix<T> operator *(Matrix<T> left, Matrix<T> right)
		{ return Matrix<T>.Matrix_Multiply(left, right); }
		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="left">The matrix to have its values multiplied.</param>
		/// <param name="right">The scalar to multiply the values by.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public static Matrix<T> operator *(Matrix<T> left, T right)
		{ return Matrix<T>.Matrix_Multiply_scalar(left, right); }
		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="left">The scalar to multiply the values by.</param>
		/// <param name="right">The matrix to have its values multiplied.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public static Matrix<T> operator *(T left, Matrix<T> right)
		{ return Matrix<T>.Matrix_Multiply_scalar(right, left); }
		/// <summary>Divides all the values in a matrix by a scalar.</summary>
		/// <param name="left">The matrix to have its values divided.</param>
		/// <param name="right">The scalar to divide the values by.</param>
		/// <returns>The resulting matrix after the divisions.</returns>
		public static Matrix<T> operator /(Matrix<T> left, T right)
		{ return Matrix<T>.Matrix_Divide(left, right); }
		/// <summary>Applies a power to a matrix.</summary>
		/// <param name="left">The matrix to apply a power to.</param>
		/// <param name="right">The power to apply to the matrix.</param>
		/// <returns>The result of the power operation.</returns>
		public static Matrix<T> operator ^(Matrix<T> left, int right)
		{ return Matrix<T>.Matrix_Power(left, right); }
		/// <summary>Checks for equality by value.</summary>
		/// <param name="left">The left matrix of the equality check.</param>
		/// <param name="right">The right matrix of the equality check.</param>
		/// <returns>True if the values of the matrices are equal, false if not.</returns>
		public static bool operator ==(Matrix<T> left, Matrix<T> right)
		{ return Matrix<T>.Matrix_EqualsByValue(left, right); }
		/// <summary>Checks for false-equality by value.</summary>
		/// <param name="left">The left matrix of the false-equality check.</param>
		/// <param name="right">The right matrix of the false-equality check.</param>
		/// <returns>True if the values of the matrices are not equal, false if they are.</returns>
		public static bool operator !=(Matrix<T> left, Matrix<T> right)
		{ return !Matrix<T>.Matrix_EqualsByValue(left, right); }
		/// <summary>Automatically converts a float[,] into a matrix if necessary.</summary>
		/// <param name="matrix">The float[,] to convert to a matrix.</param>
		/// <returns>The reference to the matrix representing the T[,].</returns>
		public static explicit operator Matrix<T>(T[,] matrix)
		{ return new Matrix<T>(matrix); }
		///// <summary>Automatically converts a matrix into a T[,] if necessary.</summary>
		///// <param name="matrix">The matrix to convert to a T[,].</param>
		///// <returns>The reference to the T[,] representing the matrix.</returns>
		//public static explicit operator T[,](Matrix<T> matrix)
		//{ 
		//	T[,] array = new T[matrix.Rows, matrix.Columns];
		//	for (int i = 0; i < i )
		//	return matrix; }
		#endregion

		#region instance

		/// <summary>Negates all the values in this matrix.</summary>
		/// <returns>The resulting matrix after the negations.</returns>
		public Matrix<T> Negate()
		{ return Matrix<T>.Matrix_Negate(this); }
		/// <summary>Does a standard matrix addition.</summary>
		/// <param name="right">The matrix to add to this matrix.</param>
		/// <returns>The resulting matrix after the addition.</returns>
		public Matrix<T> Add(Matrix<T> right)
		{ return Matrix<T>.Matrix_Add(this, right); }
		/// <summary>Does a standard matrix multiplication (triple for loop).</summary>
		/// <param name="right">The matrix to multiply this matrix by.</param>
		/// <returns>The resulting matrix after the multiplication.</returns>
		public Matrix<T> Multiply(Matrix<T> right)
		{ return Matrix<T>.Matrix_Multiply(this, right); }
		/// <summary>Multiplies all the values in this matrix by a scalar.</summary>
		/// <param name="right">The scalar to multiply all the matrix values by.</param>
		/// <returns>The retulting matrix after the multiplications.</returns>
		public Matrix<T> Multiply(T right)
		{ return Matrix<T>.Matrix_Multiply_scalar(this, right); }
		/// <summary>Divides all the values in this matrix by a scalar.</summary>
		/// <param name="right">The scalar to divide the matrix values by.</param>
		/// <returns>The resulting matrix after teh divisions.</returns>
		public Matrix<T> Divide(T right)
		{ return Matrix<T>.Matrix_Divide(this, right); }
		/// <summary>Gets the minor of a matrix.</summary>
		/// <param name="row">The restricted row of the minor.</param>
		/// <param name="column">The restricted column of the minor.</param>
		/// <returns>The minor from the row/column restrictions.</returns>
		public Matrix<T> Minor(int row, int column)
		{ return Matrix<T>.Matrix_Minor(this, row, column); }
		/// <summary>Combines two matrices from left to right 
		/// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
		/// <param name="right">The matrix to combine with on the right side.</param>
		/// <returns>The resulting row-wise concatination.</returns>
		public Matrix<T> ConcatenateRowWise(Matrix<T> right)
		{ return Matrix<T>.Matrix_ConcatenateRowWise(this, right); }
		/// <summary>Matrixs the determinent if this matrix is square.</summary>
		/// <returns>The computed determinent if this matrix is square.</returns>
		public T Determinent()
		{ return Matrix<T>.Matrix_Determinent(this); }
		/// <summary>Matrixs the echelon form of this matrix (aka REF).</summary>
		/// <returns>The computed echelon form of this matrix (aka REF).</returns>
		public Matrix<T> Echelon()
		{ return Matrix<T>.Matrix_Echelon(this); }
		/// <summary>Matrixs the reduced echelon form of this matrix (aka RREF).</summary>
		/// <returns>The computed reduced echelon form of this matrix (aka RREF).</returns>
		public Matrix<T> ReducedEchelon()
		{ return Matrix<T>.Matrix_ReducedEchelon(this); }
		/// <summary>Matrixs the inverse of this matrix.</summary>
		/// <returns>The inverse of this matrix.</returns>
		public Matrix<T> Inverse()
		{ return Matrix<T>.Matrix_Inverse(this); }
		/// <summary>Gets the adjoint of this matrix.</summary>
		/// <returns>The adjoint of this matrix.</returns>
		public Matrix<T> Adjoint()
		{ return Matrix<T>.Matrix_Adjoint(this); }
		/// <summary>Transposes this matrix.</summary>
		/// <returns>The transpose of this matrix.</returns>
		public Matrix<T> Transpose()
		{ return Matrix<T>.Matrix_Transpose(this); }
		/// <summary>Copies this matrix.</summary>
		/// <returns>The copy of this matrix.</returns>
		public Matrix<T> Clone()
		{ return Matrix<T>.Clone(this); }

		#endregion

		#region static

		/// <summary>Negates all the values in a matrix.</summary>
		/// <param name="matrix">The matrix to have its values negated.</param>
		/// <returns>The resulting matrix after the negations.</returns>
		public static Matrix<T> Negate(Matrix<T> matrix)
		{ return Matrix<T>.Matrix_Negate(matrix); }
		/// <summary>Does standard addition of two matrices.</summary>
		/// <param name="left">The left matrix of the addition.</param>
		/// <param name="right">The right matrix of the addition.</param>
		/// <returns>The resulting matrix after the addition.</returns>
		public static Matrix<T> Add(Matrix<T> left, Matrix<T> right)
		{ return Matrix<T>.Matrix_Add(left, right); }
		/// <summary>Subtracts a scalar from all the values in a matrix.</summary>
		/// <param name="left">The matrix to have the values subtracted from.</param>
		/// <param name="right">The scalar to subtract from all the matrix values.</param>
		/// <returns>The resulting matrix after the subtractions.</returns>
		public static Matrix<T> Subtract(Matrix<T> left, Matrix<T> right)
		{ return Matrix<T>.Matrix_Subtract(left, right); }
		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
		/// <param name="left">The left matrix of the multiplication.</param>
		/// <param name="right">The right matrix of the multiplication.</param>
		/// <returns>The resulting matrix of the multiplication.</returns>
		public static Matrix<T> Multiply(Matrix<T> left, Matrix<T> right)
		{ return Matrix<T>.Matrix_Multiply(left, right); }
		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
		/// <param name="left">The left matrix of the multiplication.</param>
		/// <param name="right">The right matrix of the multiplication.</param>
		/// <returns>The resulting matrix of the multiplication.</returns>
		public static Vector<T> Multiply(Matrix<T> left, Vector<T> right)
		{ return Matrix<T>.Matrix_Multiply_vector(left, right._vector); }
		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		/// <param name="left">The matrix to have the values multiplied.</param>
		/// <param name="right">The scalar to multiply the values by.</param>
		/// <returns>The resulting matrix after the multiplications.</returns>
		public static Matrix<T> Multiply(Matrix<T> left, T right)
		{ return Matrix<T>.Matrix_Multiply_scalar(left, right); }
		/// <summary>Applies a power to a square matrix.</summary>
		/// <param name="matrix">The matrix to be powered by.</param>
		/// <param name="power">The power to apply to the matrix.</param>
		/// <returns>The resulting matrix of the power operation.</returns>
		public static Matrix<T> Power(Matrix<T> matrix, int power)
		{ return Matrix<T>.Matrix_Power(matrix, power); }
		/// <summary>Divides all the values in the matrix by a scalar.</summary>
		/// <param name="matrix">The matrix to divide the values of.</param>
		/// <param name="right">The scalar to divide all the matrix values by.</param>
		/// <returns>The resulting matrix with the divided values.</returns>
		public static Matrix<T> Divide(Matrix<T> matrix, T right)
		{ return Matrix<T>.Matrix_Divide(matrix, right); }
		/// <summary>Gets the minor of a matrix.</summary>
		/// <param name="matrix">The matrix to get the minor of.</param>
		/// <param name="row">The restricted row to form the minor.</param>
		/// <param name="column">The restricted column to form the minor.</param>
		/// <returns>The minor of the matrix.</returns>
		public static Matrix<T> Minor(Matrix<T> matrix, int row, int column)
		{ return Matrix<T>.Matrix_Minor(matrix, row, column); }
		/// <summary>Combines two matrices from left to right 
		/// (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
		/// <param name="left">The left matrix of the concatenation.</param>
		/// <param name="right">The right matrix of the concatenation.</param>
		/// <returns>The resulting matrix of the concatenation.</returns>
		public static Matrix<T> ConcatenateRowWise(Matrix<T> left, Matrix<T> right)
		{ return Matrix<T>.Matrix_ConcatenateRowWise(left, right); }
		/// <summary>Calculates the determinent of a square matrix.</summary>
		/// <param name="matrix">The matrix to calculate the determinent of.</param>
		/// <returns>The determinent of the matrix.</returns>
		public static T Determinent(Matrix<T> matrix)
		{ return Matrix<T>.Matrix_Determinent(matrix); }
		/// <summary>Calculates the echelon of a matrix (aka REF).</summary>
		/// <param name="matrix">The matrix to calculate the echelon of (aka REF).</param>
		/// <returns>The echelon of the matrix (aka REF).</returns>
		public static Matrix<T> Echelon(Matrix<T> matrix)
		{ return Matrix<T>.Matrix_Echelon(matrix); }
		/// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
		/// <param name="matrix">The matrix matrix to calculate the reduced echelon of (aka RREF).</param>
		/// <returns>The reduced echelon of the matrix (aka RREF).</returns>
		public static Matrix<T> ReducedEchelon(Matrix<T> matrix)
		{ return Matrix<T>.Matrix_ReducedEchelon(matrix); }
		/// <summary>Calculates the inverse of a matrix.</summary>
		/// <param name="matrix">The matrix to calculate the inverse of.</param>
		/// <returns>The inverse of the matrix.</returns>
		public static Matrix<T> Inverse(Matrix<T> matrix)
		{ return Matrix<T>.Matrix_Inverse(matrix); }
		/// <summary>Calculates the adjoint of a matrix.</summary>
		/// <param name="matrix">The matrix to calculate the adjoint of.</param>
		/// <returns>The adjoint of the matrix.</returns>
		public static Matrix<T> Adjoint(Matrix<T> matrix)
		{ return Matrix<T>.Matrix_Adjoint(matrix); }
		/// <summary>Returns the transpose of a matrix.</summary>
		/// <param name="matrix">The matrix to transpose.</param>
		/// <returns>The transpose of the matrix.</returns>
		public static Matrix<T> Transpose(Matrix<T> matrix)
		{ return Matrix<T>.Matrix_Transpose(matrix); }
		/// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
		/// <param name="matrix">The matrix to decompose.</param>
		/// <param name="lower">The computed lower triangular matrix.</param>
		/// <param name="upper">The computed upper triangular matrix.</param>
		/// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
		/// <param name="matrix">The matrix to decompose.</param>
		/// <param name="lower">The computed lower triangular matrix.</param>
		/// <param name="upper">The computed upper triangular matrix.</param>
		public static void DecomposeLU(Matrix<T> matrix, out Matrix<T> lower, out Matrix<T> upper)
		{ Matrix<T>.Matrix_DecomposeLU(matrix, out lower, out upper); }
		/// <summary>Creates a copy of a matrix.</summary>
		/// <param name="matrix">The matrix to copy.</param>
		/// <returns>A copy of the matrix.</returns>
		public static Matrix<T> Clone(Matrix<T> matrix)
		{ return new Matrix<T>(matrix); }
		/// <summary>Does a value equality check.</summary>
		/// <param name="left">The first matrix to check for equality.</param>
		/// <param name="right">The second matrix to check for equality.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Matrix<T> left, Matrix<T> right)
		{ return Matrix<T>.Matrix_EqualsByValue(left, right); }
		/// <summary>Does a value equality check with leniency.</summary>
		/// <param name="left">The first matrix to check for equality.</param>
		/// <param name="right">The second matrix to check for equality.</param>
		/// <param name="leniency">How much the values can vary but still be considered equal.</param>
		/// <returns>True if values are equal, false if not.</returns>
		public static bool EqualsValue(Matrix<T> left, Matrix<T> right, T leniency)
		{ return Matrix<T>.Matrix_EqualsByValue_leniency(left, right, leniency); }
		/// <summary>Checks if two matrices are equal by reverences.</summary>
		/// <param name="left">The left matric of the equality check.</param>
		/// <param name="right">The right matrix of the equality check.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public static bool EqualsReference(Matrix<T> left, Matrix<T> right)
		{ return object.ReferenceEquals(left, right); }

		#endregion

		#region implementations

		#region T_Source

		private static string t_source = null;
		private static string T_Source
		{
			get
			{
				if (t_source != null)
					return t_source;
				return t_source = Meta.ConvertTypeToCsharpSource(typeof(T));
			}
		}

		#endregion

		#region Private Only

		#region RowMultiplication
		// Macro for runtime compilation
		private static string RowMultiplication(string scope_variable, string matrix_variable, string row_variable, string scalar_variable)
		{
			return string.Concat(
				"for (int ", scope_variable, " = 0; ", scope_variable, " < ", matrix_variable, ".Columns; ", scope_variable, "++) ",
				"{ ",
					matrix_variable, "[", row_variable, ", ", scope_variable, "] *= ", scalar_variable, ";",
				" }");
		}

		private static void RowMultiplication(Matrix<T> matrix, int row, T scalar)
		{
			for (int i = 0; i < matrix.Columns; i++)
			{
				matrix[row, i] = Compute<T>.Multiply(matrix[row, i], scalar);
			}
		}
		#endregion

		#region RowAddition
		// Macro for runtime compilation
		private static string RowAddition(string scope_variable, string matrix_variable, string target_variable, string second_variable, string scalar_variable)
		{
			return string.Concat(
				"for (int ", scope_variable, " = 0; ", scope_variable, " < ", matrix_variable, ".Columns; ", scope_variable, "++) ",
				"{ ",
					matrix_variable, "[", target_variable, ", ", scope_variable, "] += (", matrix_variable, "[", second_variable, ", ", scope_variable, "] * ", scalar_variable, "); ",
				" }");
		}

		private static void RowAddition(Matrix<T> matrix, int target, int second, T scalar)
		{
			for (int i = 0; i < matrix.Columns; i++)
			{
				matrix[target, i] = Compute<T>.Add(matrix[target, i], Compute<T>.Multiply(matrix[second, i], scalar));
			}
		}
		#endregion

		#region SwapRows
		// Macro for runtime compilation
		private static string SwapRows(string temp, string looper, string matrix, string row1, string row2)
		{
			return string.Concat(
				"for (int ", looper, " = 0; ", looper, " < ", matrix, ".Columns; ", looper, @"++)
				{
					", T_Source, " ", temp, " = ", matrix, "[", row1, ", ", looper, @"];
					", matrix, "[", row1, ", ", looper, "] = ", matrix, "[", row2, ", ", looper, @"];
					", matrix, "[", row2, ", ", looper, "] = ", temp, @";
				}");
		}

		private static void SwapRows(Matrix<T> matrix, int row1, int row2)
		{
				for (int i = 0; i < matrix.Columns; i++)
				{
					T temp = matrix[row1, i];
					matrix[row1, i] = matrix[row2, i];
					matrix[row2, i] = temp;
				}
		}
		#endregion

		#endregion

		#region FactoryZero
		/// <summary>Creates a zero matrix of the given dimensions.</summary>
		private static Matrix<T>.Delegates.Matrix_FactoryZero Matrix_FactoryZero = (int rows, int columns) =>
		{
			T zero = Compute<T>.FromInt32(0);
			if (Compute<T>.Equate_private(default(T), zero))
			{
				Matrix<T>.Matrix_FactoryZero = (int _rows, int _columns) =>
				{
					if (_rows < 1)
						throw new System.ArithmeticException("invalid dimenions on matrix construction");
					if (_columns < 1)
						throw new System.ArithmeticException("invalid dimenions on matrix construction");
					Matrix<T> result = new Matrix<T>(_rows, _columns);
					return result;
				};
			}
			else
			{
				Matrix<T>.Matrix_FactoryZero = (int _rows, int _columns) =>
				{
					if (_rows < 1)
						throw new System.ArithmeticException("invalid dimenions on matrix construction");
					if (_columns < 1)
						throw new System.ArithmeticException("invalid dimenions on matrix construction");
					Matrix<T> result = new Matrix<T>(_rows, _columns);
					if (Compute<T>.Equate_private(default(T), zero))
						return result;
					else
					{
						T[] flat_matrix = result._matrix;
						for (int i = 0; i < flat_matrix.Length; i++)
							flat_matrix[i] = zero;
						return result;
					}
				};
			}
			return Matrix_FactoryZero(rows, columns);

			#region Alternate Version
//			Matrix<T>.Matrix_FactoryZero2 =
//				Meta.Compile<Matrix<T>.Delegates.Matrix_FactoryZero>(
//					string.Concat(
//@"(int _rows, int _columns) =>
//{
//	Matrix<", T_Source, @"> matrix;
//	try { matrix = new Matrix<", T_Source, @">(_rows, _columns); }
			//	catch { throw new System.Exception(", "\"invalid dimensions.\"", @"); }
//	", T_Source, @"[] array = matrix._matrix;
//	for (int i = 0; i < array.Length; i++)
//			array[0] = 0;
//	return matrix;
//}"));

//			return Matrix<T>.Matrix_FactoryZero2(rows, columns);
			#endregion
		};
		#endregion

		#region FactoryOne
		/// <summary>Creates a ones matrix of the given dimensions.</summary>
		private static Matrix<T>.Delegates.Matrix_FactoryOne Matrix_FactoryOne = (int rows, int columns) =>
		{
			T one = Compute<T>.FromInt32(1);
			Matrix<T>.Matrix_FactoryOne = (int _rows, int _columns) =>
			{
				if (_rows < 1)
					throw new System.ArithmeticException("invalid dimenions on matrix construction");
				if (_columns < 1)
					throw new System.ArithmeticException("invalid dimenions on matrix construction");
				Matrix<T> result = new Matrix<T>(_rows, _columns);
				if (Compute<T>.Equate_private(default(T), one))
					return result;
				else
				{
					T[] flat_matrix = result._matrix;
					for (int i = 0; i < flat_matrix.Length; i++)
						flat_matrix[i] = one;
					return result;
				}
			};
			return Matrix<T>.Matrix_FactoryOne(rows, columns);
		};
		#endregion

		#region FactoryIdentity
		/// <summary>Creates an identity (ones along diagnol, zeros otherwise) matrix of the given dimensions.</summary>
		private static Matrix<T>.Delegates.Matrix_FactoryIdentity Matrix_FactoryIdentity = (int rows, int columns) =>
		{
			T one = Compute<T>.FromInt32(1);
			T zero = Compute<T>.FromInt32(0);
			if (Compute<T>.Equate_private(default(T), zero))
			{
				Matrix<T>.Matrix_FactoryIdentity = (int _rows, int _columns) =>
				{
					Matrix<T> matrix = new Matrix<T>(_rows, _columns);
					if (_rows <= _columns)
						for (int i = 0; i < _rows; i++)
							matrix[i, i] = one;
					else
						for (int i = 0; i < _columns; i++)
							matrix[i, i] = one;
					return matrix;
				};
			}
			else
			{
				Matrix<T>.Matrix_FactoryIdentity = (int _rows, int _columns) =>
				{
					Matrix<T> matrix = new Matrix<T>(_rows, _columns);
					for (int row = 0; row < rows; row++)
						for (int column = 0; column < columns; column++)
							if (row == column)
								matrix[row, column] = one;
							else
								matrix[row, column] = zero;
					return matrix;
				};
			}
			return Matrix<T>.Matrix_FactoryIdentity(rows, columns);
		};
		#endregion

		#region IsSymetric
		/// <summary>Determines if a matrix is symetric.</summary>
		private static Matrix<T>.Delegates.Matrix_IsSymetric Matrix_IsSymetric = (Matrix<T> matrix) =>
		{
			Matrix<T>.Matrix_IsSymetric =
				Meta.Compile<Matrix<T>.Delegates.Matrix_IsSymetric>(
					string.Concat(
"(Matrix<", T_Source, @"> _matrix) =>
{
		if (object.ReferenceEquals(_matrix, null))
				throw new System.ArgumentNullException(", "\"matrix\"", @");
		if (_matrix.Rows != _matrix.Columns)
				return false;
		int size = _matrix.Columns;
		", T_Source, @"[] _matrix_flat = _matrix._matrix;
		for (var row = 0; row < size; row++)
			for (var column = row + 1; column < size; column++)
				if (_matrix_flat[row * size + column] != _matrix_flat[column * size + row])
					return false;
		return true;
}"));

			return Matrix<T>.Matrix_IsSymetric(matrix);

			#region Alternate Version
			//if (matrix.Rows != matrix.Columns)
			//	return false;
			//for (int i = 0; i < matrix.Rows; i++)
			//	for (int j = 0; j < matrix.Columns; j++)
			//		if (!Compute<T>.Equate(matrix[i, j], matrix[j, i]))
			//			return false;
			//return true;
			#endregion
		};
		#endregion

		#region Negate
		/// <summary>Negates all the values in a matrix.</summary>
		private static Matrix<T>.Delegates.Matrix_Negate Matrix_Negate = (Matrix<T> matrix) =>
		{
			Matrix<T>.Matrix_Negate =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Negate>(
					string.Concat(
"(Matrix<", T_Source, @"> matrix) =>
{
	if (object.ReferenceEquals(matrix, null))
		throw new System.ArgumentNullException(", "\"null reference: matirx\"", @");
	Matrix<", T_Source, @"> result =
		new Matrix<", T_Source, @">(matrix.Rows, matrix.Columns);
	int size = result._matrix.Length;
	", T_Source, @"[] result_flat = result._matrix;
	", T_Source, @"[] _matrix_flat = matrix._matrix;
	for (int i = 0; i < size; i++)
			result_flat[i] = -_matrix_flat[i];
	return result;
}"));

			return Matrix<T>.Matrix_Negate(matrix);

			#region Alternate Version
			//if (object.ReferenceEquals(matrix, null))
			//	throw new System.ArgumentNullException("matrix");
			//Matrix<T> result = new Matrix<T>(matrix.Rows, matrix.Columns);
			//T[] flat_matrix = matrix._matrix;
			//T[] flat_result = result._matrix;
			//for (int i = 0; i < flat_matrix.Length; i++)
			//	flat_result[i] = Compute<T>.Negate(flat_matrix[i]);
			//return result;
			#endregion
		};
		#endregion

		#region Add
		/// <summary>Does standard addition of two matrices.</summary>
		private static Matrix<T>.Delegates.Matrix_Add Matrix_Add = (Matrix<T> left, Matrix<T> right) =>
		{
			Matrix<T>.Matrix_Add =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Add>(
			string.Concat(
"(Matrix<" + T_Source + "> _left, Matrix<" + T_Source + @"> _right) =>
{
	if (_left == null)
			throw new System.ArgumentNullException(", "\"left\"", @");
	if (_right == null)
			throw new System.ArgumentNullException(", "\"right\"", @");
	if (_left.Rows != _right.Rows || _left.Columns != _right.Columns)
			throw new System.ArithmeticException(", "\"invalid matrix addition (dimension miss-match).\"", @");
	Matrix<", T_Source, @"> result = new Matrix<", T_Source, @">(_left.Rows, _left.Columns);
	int size = _left.Rows * _left.Columns;
	", T_Source, @"[] _left_flat = _left._matrix;
	", T_Source, @"[] _right_flat = _right._matrix;
	", T_Source, @"[] result_flat = result._matrix;
	for (int i = 0; i < size; i++)
			result_flat[i] = _left_flat[i] + _right_flat[i];
	return result;
}"));

			return Matrix<T>.Matrix_Add(left, right);

			#region Alternate Version
			//Matrix<generic> result =
			//	new Matrix<generic>(left.Rows, left.Columns);
			//for (int i = 0; i < left.Rows; i++)
			//	for (int j = 0; j < left.Columns; j++)
			//		result[i, j] = left[i, j] + right[i, j];
			//return result;
			#endregion
		};
		#endregion

		#region Subtract
		/// <summary>Subtracts a scalar from all the values in a matrix.</summary>
		private static Matrix<T>.Delegates.Matrix_Subtract Matrix_Subtract = (Matrix<T> left, Matrix<T> right) =>
		{
			Matrix<T>.Matrix_Subtract =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Subtract>(
					string.Concat(
"(Matrix<", T_Source, "> _left, Matrix<", T_Source, @"> _right) =>
{
	if (object.ReferenceEquals(_left, null))
			throw new System.ArgumentNullException(", "\"left\"", @");
	if (object.ReferenceEquals(_right, null))
			throw new System.ArgumentNullException(", "\"right\"", @");
	if (_left.Rows != _right.Rows || _left.Columns != _right.Columns)
			throw new System.ArithmeticException(", "\"invalid matrix subtraction (dimension miss-match).\"", @");
	Matrix<", T_Source, @"> result =
		new Matrix<", T_Source, @">(_left.Rows, _left.Columns);
	int size = _left.Rows * _left.Columns;
	", T_Source, @"[] _left_flat = _left._matrix;
	", T_Source, @"[] _right_flat = _right._matrix;
	", T_Source, @"[] result_flat = result._matrix;
	for (int i = 0; i < size; i++)
		result_flat[i] = _left_flat[i] - _right_flat[i];
	return result;
}"));

			return Matrix<T>.Matrix_Subtract(left, right);
			
			#region Alternate Version
			//Matrix<generic> result =
			//	new Matrix<generic>(left.Rows, left.Columns);
			//for (int i = 0; i < left.Rows; i++)
			//	for (int j = 0; j < left.Columns; j++)
			//		result[i, j] = left[i, j] - right[i, j];
			//return result;
			#endregion

		};
		#endregion

		#region Multiply
		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
		private static Matrix<T>.Delegates.Matrix_Multiply Matrix_Multiply = (Matrix<T> left, Matrix<T> right) =>
		{
			Matrix<T>.Matrix_Multiply =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Multiply>(
					string.Concat(
"(Matrix<", T_Source, @"> _left, Matrix<", T_Source, @"> _right) =>
{
	if (_left == null)
		throw new System.ArgumentNullException(", "\"left\"", @");
	if (_right == null)
		throw new System.ArgumentNullException(", "\"right\"", @");
	if (_left.Columns != _right.Rows)
		throw new System.ArithmeticException(", "\"invalid multiplication (size miss-match).\"", @");
	", T_Source, @" sum;
	int result_rows = _left.Rows;
	int _left_cols = _left.Columns;
	int result_cols = _right.Columns;
	Matrix<", T_Source, @"> result =
		new Matrix<", T_Source, @">(result_rows, result_cols);
	", T_Source, @"[] result_flat = result._matrix;
	", T_Source, @"[] _left_flat = _left._matrix;
	", T_Source, @"[] _right_flat = _right._matrix;
	for (int i = 0; i < result_rows; i++)
		for (int j = 0; j < result_cols; j++)
		{
			sum = 0;
			for (int k = 0; k < _left_cols; k++)
				sum += _left_flat[i * _left_cols + k] * _right_flat[k * result_cols + j];
			result_flat[i * result_cols + j] = sum;
		}
	return result;
}"));

			return Matrix<T>.Matrix_Multiply(left, right);

			#region Alternate Version
			//if (_left == null)
			//	throw new System.Exception("null reference: _left");
			//if (_right == null)
			//	throw new System.Exception("null reference: _right");
			//if (_left.Columns != _right.Rows)
			//	throw new System.Exception("invalid multiplication (size miss-match).");
			//Matrix<T> result =
			//	new Matrix<T>(left.Rows, right.Columns);
			//for (int i = 0; i < left.Rows; i++)
			//	for (int j = 0; j < right.Columns; j++)
			//		for (int k = 0; k < left.Columns; k++)
			//			result[i, j] = Matrix<T>.Add(result[i, j], Matrix<T>.Multiply(left[i, k], right[k, j]));
			//return result;
			#endregion
		};
		#endregion

		#region Multiply_vector
		/// <summary>Does a standard (triple for looped) multiplication between matrices.</summary>
		private static Matrix<T>.Delegates.Matrix_Multiply_vector Matrix_Multiply_vector = (Matrix<T> left, Vector<T> right) =>
		{
			Matrix<T>.Matrix_Multiply_vector =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Multiply_vector>(
					string.Concat(
"(Matrix<", T_Source, @"> _left, Vector<", T_Source, @"> _right) =>
{
	if (_left == null)
		throw new System.ArgumentNullException(", "\"left\"", @");
	if (_right == null)
		throw new System.ArgumentNullException(", "\"right\"", @");
	if (_left.Columns != _right.Dimensions)
		throw new System.ArithmeticException(", "\"invalid multiplication (size miss-match).\"", @");
	int _left_row = _left.Rows;
	int _left_col = _left.Columns;
	Vector<", T_Source, @"> result = new Vector<", T_Source, @">(_right.Dimensions);
	", T_Source, @"[] _left_flat = _left._matrix;
	", T_Source, @"[] _right_flat = _right._vector;
	", T_Source, @"[] result_flat = result._vector;
	for (int i = 0; i < _left_row; i++)
		for (int j = 0; j < _left_col; j++)
			result_flat[i] += _left_flat[i * _left_col + j] * _right_flat[j];
	return result;
}"));
			return Matrix<T>.Matrix_Multiply_vector(left, right);

			#region Alternate Version
			//Vector<generic> result = 
			//	new Vector<generic>(right.Dimensions);
			//for (int i = 0; i < left.Rows; i++)
			//	for (int j = 0; j < left.Columns; j++)
			//		result[i] += left[i, j] * right[j];
			//return result;
			#endregion
		};
		#endregion

		#region Multiply_scalar
		/// <summary>Multiplies all the values in a matrix by a scalar.</summary>
		private static Matrix<T>.Delegates.Matrix_Multiply_scalar Matrix_Multiply_scalar = (Matrix<T> left, T right) =>
		{
			Matrix<T>.Matrix_Multiply_scalar =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Multiply_scalar>(
					string.Concat(
"(Matrix<", T_Source, "> _left, ", T_Source, @" _right) =>
{
	if (_left == null)
		throw new System.Exception(", "\"null reference: _left\"", @");
	if (_right == null)
		throw new System.Exception(", "\"null reference: _right\"", @");
	int rows = _left.Rows;
	int columns = _left.Columns;
	Matrix<", T_Source, "> result = new Matrix<", T_Source, @">(rows, columns);
	for (int i = 0; i < rows; i++)
		for (int j = 0; j < columns; j++)
			result[i, j] = _left[i, j] * _right;
	return result;
}"));

			return Matrix<T>.Matrix_Multiply_scalar(left, right);

			#region pre-optimization
			//Matrix<generic> result = 
			//	new Matrix<generic>(left.Rows, left.Columns);
			//for (int i = 0; i < left.Rows; i++)
			//	for (int j = 0; j < left.Columns; j++)
			//		result[i, j] = left[i, j] * right;
			//return result;
			#endregion
		};
		#endregion

		#region Divide
		/// <summary>Divides all the values in the matrix by a scalar.</summary>
		private static Matrix<T>.Delegates.Matrix_Divide Matrix_Divide = (Matrix<T> left, T right) =>
		{
			Matrix<T>.Matrix_Divide =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Divide>(
					string.Concat(
"(Matrix<", T_Source, "> _left, ", T_Source, @" _right) =>
{
	if (_left == null)
		throw new System.Exception(", "\"null reference: matrix\"", @");
	if (_right == null)
			throw new System.Exception(", "\"null reference: matrix\"", @");
	int matrix_row = _left.Rows;
	int matrix_col = _left.Columns;
	Matrix<", T_Source, @"> result =
		new Matrix<", T_Source, @">(matrix_row, matrix_col);
	", T_Source, @"[] matrix_flat = _left._matrix;
	", T_Source, @"[] result_flat = result._matrix;
	for (int i = 0; i < matrix_row; i++)
		for (int j = 0; j < matrix_col; j++)
			result_flat[i * matrix_col + j] = 
				matrix_flat[i * matrix_col + j] / _right;
	return result;
}"));

			return Matrix<T>.Matrix_Divide(left, right);

			#region Alternate Version
			//Matrix<generic> result = 
			//	new Matrix<generic>(matrix.Rows, matrix.Columns);
			//for (int i = 0; i < matrix.Rows; i++)
			//	for (int j = 0; j < matrix.Columns; j++)
			//		result[i, j] = matrix[i, j] / right;
			//return result;
			#endregion
		};
		#endregion

		#region Power
		/// <summary>Applies a power to a square matrix.</summary>
		private static Matrix<T>.Delegates.Matrix_Power Matrix_Power = (Matrix<T> matrix, int power) =>
		{
			Matrix<T>.Matrix_Power =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Power>(
					string.Concat(
"(Matrix<", T_Source, @"> _matrix, int _power) =>
{
	if (!(_matrix.Rows == _matrix.Columns))
		throw new System.Exception(", "\"invalid _power (!_matrix.IsSquare).\"", @");
	if (!(_power >= 0))
		throw new System.Exception(", "\"invalid _power !(_power > -1)\"", @");
	if (_power == 0)
		return Matrix<" + T_Source, @">.FactoryIdentity(_matrix.Rows, _matrix.Columns);
	Matrix<", T_Source, @"> result = _matrix.Clone();
	for (int i = 0; i < _power; i++)
		result = Matrix<", T_Source, @">.Multiply(result, _matrix);
	return result;
}"));

			return Matrix<T>.Matrix_Power(matrix, power);

			#region Alternate Version
			//Matrix<generic> result = _matrix.Clone();
			//for (int i = 0; i < _power; i++)
			//	result = Matrix.Multiply(result, _matrix);
			//return result;
			#endregion
		};
		#endregion

        #region Rotate

        /// <summary>Rotates a matrix around an axis by a specified angle.</summary>
        /// <param name="angle">The angle of rotation around the axis.</param>
        /// <param name="axis">The axis to rotate the matrix around.</param>
        /// <param name="matrix">The matrix to rotate.</param>
        /// <returns>The rotated matrix.</returns>
        public static Matrix<T> Rotate4x4(Angle<T> angle, Vector<T> axis, Matrix<T> matrix)
        {
            Code.AssertArgNonNull(axis, "axis");
            Code.AssertArgNonNull(matrix, "matrix");
            Code.Assert<System.InvalidOperationException>(axis.Dimensions == 3, "Matrix<T>.Rotate4x4 requires a 3 dimensional axis to rotate.");
            Code.Assert<System.InvalidOperationException>(matrix.Rows == 4 && matrix.Columns == 4, "Matrix<T>.Rotate4x4 requires a 4x4 matrix to rotate.");

            // if the angle is zero, no rotation is required
            if (angle == Angle<T>.Factory_Radians(Compute<T>.Zero))
            {
                return matrix.Clone();
            }

            // this function needs optimization (delegate runtime-compilation treatement)

            T cosine = Compute<T>.Cosine(angle);
            T sine = Compute<T>.Sine(angle);
            T oneMinusCosine = Compute<T>.Subtract(Compute<T>.One, cosine);
            T xy = Compute<T>.Multiply(axis.X, axis.Y);
            T yz = Compute<T>.Multiply(axis.Y, axis.Z);
            T xz = Compute<T>.Multiply(axis.X, axis.Z);
            T xs = Compute<T>.Multiply(axis.X, sine);
            T ys = Compute<T>.Multiply(axis.Y, sine);
            T zs = Compute<T>.Multiply(axis.Z, sine);

            T f00 = Compute<T>.Add(Compute<T>.Multiply(Compute<T>.Multiply(axis.X, axis.X), oneMinusCosine), cosine);
            T f01 = Compute<T>.Add(Compute<T>.Multiply(xy, oneMinusCosine), zs);
            T f02 = Compute<T>.Subtract(Compute<T>.Multiply(xz, oneMinusCosine), ys);
            // n[3] not used
            T f10 = Compute<T>.Subtract(Compute<T>.Multiply(xy, oneMinusCosine), zs);
            T f11 = Compute<T>.Add(Compute<T>.Multiply(Compute<T>.Multiply(axis.Y, axis.Y), oneMinusCosine), cosine);
            T f12 = Compute<T>.Add(Compute<T>.Multiply(yz, oneMinusCosine), xs);
            // n[7] not used
            T f20 = Compute<T>.Add(Compute<T>.Multiply(xz, oneMinusCosine), ys);
            T f21 = Compute<T>.Subtract(Compute<T>.Multiply(yz, oneMinusCosine), xs);
            T f22 = Compute<T>.Add(Compute<T>.Multiply(Compute<T>.Multiply(axis.Z, axis.Z), oneMinusCosine), cosine);

            // Row 1
            T _0_0 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 0], f00), Compute<T>.Multiply(matrix[1, 0], f01)), Compute<T>.Multiply(matrix[2, 0], f02));
            T _0_1 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 1], f00), Compute<T>.Multiply(matrix[1, 1], f01)), Compute<T>.Multiply(matrix[2, 1], f02));
            T _0_2 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 2], f00), Compute<T>.Multiply(matrix[1, 2], f01)), Compute<T>.Multiply(matrix[2, 2], f02));
            T _0_3 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 3], f00), Compute<T>.Multiply(matrix[1, 3], f01)), Compute<T>.Multiply(matrix[2, 3], f02));
            // Row 2
            T _1_0 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 0], f10), Compute<T>.Multiply(matrix[1, 0], f11)), Compute<T>.Multiply(matrix[2, 0], f12));
            T _1_1 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 1], f10), Compute<T>.Multiply(matrix[1, 1], f11)), Compute<T>.Multiply(matrix[2, 1], f12));
            T _1_2 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 2], f10), Compute<T>.Multiply(matrix[1, 2], f11)), Compute<T>.Multiply(matrix[2, 2], f12));
            T _1_3 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 3], f10), Compute<T>.Multiply(matrix[1, 3], f11)), Compute<T>.Multiply(matrix[2, 3], f12));
            // Row 3
            T _2_0 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 0], f20), Compute<T>.Multiply(matrix[1, 0], f21)), Compute<T>.Multiply(matrix[2, 0], f22));
            T _2_1 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 1], f20), Compute<T>.Multiply(matrix[1, 1], f21)), Compute<T>.Multiply(matrix[2, 1], f22));
            T _2_2 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 2], f20), Compute<T>.Multiply(matrix[1, 2], f21)), Compute<T>.Multiply(matrix[2, 2], f22));
            T _2_3 = Compute<T>.Add(Compute<T>.Add(Compute<T>.Multiply(matrix[0, 3], f20), Compute<T>.Multiply(matrix[1, 3], f21)), Compute<T>.Multiply(matrix[2, 3], f22));
            // Row 4
            T _3_0 = Compute<T>.Zero;
            T _3_1 = Compute<T>.Zero;
            T _3_2 = Compute<T>.Zero;
            T _3_3 = Compute<T>.One;

            return new Matrix<T>(new T[,]
            {
                { _0_0, _0_1, _0_2, _0_3 },
                { _1_0, _1_1, _1_2, _1_3 },
                { _2_0, _2_1, _2_2, _2_3 },
                { _3_0, _3_1, _3_2, _3_3 }
            });
        }

        #endregion

        #region Minor
        /// <summary>Gets the minor of a matrix.</summary>
		private static Matrix<T>.Delegates.Matrix_Minor Matrix_Minor = (Matrix<T> matrix, int row, int column) =>
		{
			Matrix<T>.Matrix_Minor =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Minor>(
					string.Concat(
"(Matrix<", T_Source, @"> _matrix, int _row, int _column) =>
{
	if (object.ReferenceEquals(_matrix, null))
		throw new System.Exception(", "\"null reference: _matrix\"", @");
	if (_matrix.Rows < 2 || _matrix.Columns < 2)
		throw new System.Exception(", "\"invalid _matrix minor: (_matrix.Rows < 2 || _matrix.Columns < 2)\"", @");
	if (_row < 0 || _row >= _matrix.Rows)
		throw new System.Exception(", "\"invalid _row on _matrix minor: !(0 <= _row < _matrix.Rows)\"", @");
	if (_column < 0 || _row >= _matrix.Columns)
		throw new System.Exception(", "\"invalid _column on _matrix minor: !(0 <= _column < _matrix.Columns)\"", @");
	Matrix<", T_Source, @"> minor =
		new Matrix<", T_Source, @">(_matrix.Rows - 1, _matrix.Columns - 1);
	int _matrix__rows = _matrix.Rows;
	int _matrix_cols = _matrix.Columns;
	int m = 0, n = 0;
	for (int i = 0; i < _matrix.Rows; i++)
	{
			if (i == _row) continue;
			n = 0;
			for (int j = 0; j < _matrix.Columns; j++)
			{
					if (j == _column) continue;
					minor[m, n] = _matrix[i, j];
					n++;
			}
			m++;
	}
	return minor;
}"));

			return Matrix<T>.Matrix_Minor(matrix, row, column);

			#region Alternate Version
			//Matrix<generic> minor = 
			//	new Matrix<generic>(_matrix.Rows - 1, _matrix.Columns - 1);
			//int m = 0, n = 0;
			//for (int i = 0; i < _matrix.Rows; i++)
			//{
			//	if (i == _row) continue;
			//	n = 0;
			//	for (int j = 0; j < _matrix.Columns; j++)
			//	{
			//		if (j == _column) continue;
			//		minor[m, n] = _matrix[i, j];
			//		n++;
			//	}
			//	m++;
			//}
			//return minor;
			#endregion
		};
		#endregion

		#region ConcatenateRowWise
		/// <summary>Combines two matrices from left to right (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary>
		private static Matrix<T>.Delegates.Matrix_ConcatenateRowWise Matrix_ConcatenateRowWise = (Matrix<T> left, Matrix<T> right) =>
		{
			Matrix<T>.Matrix_ConcatenateRowWise =
				Meta.Compile<Matrix<T>.Delegates.Matrix_ConcatenateRowWise>(
					string.Concat(
"(Matrix<", T_Source, "> _left, Matrix<", T_Source, @"> _right) =>
{
	if (object.ReferenceEquals(_left, null))
			throw new System.Exception(", "\"null reference: _left\"", @");
	if (object.ReferenceEquals(_right, null))
			throw new System.Exception(", "\"null reference: _right\"", @");
	if (_left.Rows != _right.Rows)
			throw new System.Exception(", "\"invalid row-wise concatenation !(_left.Rows == _right.Rows).\"", @");
	Matrix<", T_Source, @"> result =
		new Matrix<", T_Source, @">(_left.Rows, _left.Columns + _right.Columns);
	int result_rows = result.Rows;
	int result_cols = result.Columns;
	int _left_cols = _left.Columns;
	int _right_cols = _right.Columns;
	for (int i = 0; i < result_rows; i++)
		for (int j = 0; j < result_cols; j++)
			if (j < _left.Columns)
				result[i, j] = _left[i, j];
			else
				result[i, j] = _right[i, j - _left.Columns];
	return result;
}"));

			return Matrix<T>.Matrix_ConcatenateRowWise(left, right);

			#region Alternate Version
			//Matrix<generic> result =
			//	new Matrix<generic>(left.Rows, left.Columns + right.Columns);
			//for (int i = 0; i < result.Rows; i++)
			//	for (int j = 0; j < result.Columns; j++)
			//		if (j < left.Columns)
			//			result[i, j] = left[i, j];
			//		else
			//			result[i, j] = right[i, j - left.Columns];
			//return result;
			#endregion
		};
		#endregion

		#region Determinent
		/// <summary>Calculates the determinent of a square matrix.</summary>
		private static Matrix<T>.Delegates.Matrix_Determinent Matrix_Determinent = (Matrix<T> matrix) =>
		{
			Matrix<T>.Matrix_Determinent =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Determinent>(
					string.Concat(
"(Matrix<", T_Source, @"> _matrix) =>
{
	if (object.ReferenceEquals(_matrix, null))
		throw new System.ArgumentNullException(", "\"null reference: _matrix\"", @");
	if (_matrix.Rows != _matrix.Columns)
		throw new System.ArithmeticException(", "\"invalid _matrix determinent: !(_matrix.IsSquare)\"", @");
	", T_Source + @" det = 1;
	Matrix<", T_Source, @"> rref = _matrix.Clone();
	for (int i = 0; i < _matrix.Rows; i++)
	{
		if (rref[i, i] == 0)
			for (int j = i + 1; j < rref.Rows; j++)
				if (rref[j, i] != 0)
				{
					", SwapRows("temp", "k", "rref", "i", "j"), @"
					det *= -1;
				}
		det *= rref[i, i];
		", T_Source, @" temp_rowMultiplication = 1 / rref[i, i];
		", RowMultiplication("j", "rref", "i", "temp_rowMultiplication"), @"
		for (int j = i + 1; j < rref.Rows; j++)
		{
			", T_Source, @" scalar = -rref[j, i];
			", RowAddition("k", "rref", "j", "i", "scalar"), @"
		}
		for (int j = i - 1; j >= 0; j--)
		{
			", T_Source, @" scalar = -rref[j, i];
			", RowAddition("k", "rref", "j", "i", "scalar"), @"
		}
	}
	return det;
}"));

			return Matrix<T>.Matrix_Determinent(matrix);

			#region Alternate Version
			//generic det = 1;
			//Matrix<generic> rref = _matrix.Clone();
			//for (int i = 0; i < _matrix.Rows; i++)
			//{
			//	if (rref[i, i] == 0)
			//		for (int j = i + 1; j < rref.Rows; j++)
			//			if (rref[j, i] != 0)
			//			{
			//				Matrix.SwapRows(rref, i, j);
			//				det *= -1;
			//			}
			//	det *= rref[i, i];
			//	Matrix.RowMultiplication(rref, i, 1 / rref[i, i]);
			//	for (int j = i + 1; j < rref.Rows; j++)
			//		Matrix.RowAddition(rref, j, i, -rref[j, i]);
			//	for (int j = i - 1; j >= 0; j--)
			//		Matrix.RowAddition(rref, j, i, -rref[j, i]);
			//}
			//return det;
			#endregion
		};
		#endregion

		#region Echelon
		/// <summary>Calculates the echelon of a matrix (aka REF).</summary>
		private static Matrix<T>.Delegates.Matrix_Echelon Matrix_Echelon = (Matrix<T> matrix) =>
		{
			Matrix<T>.Matrix_Echelon =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Echelon>(
					string.Concat(
"(Matrix<", T_Source, @"> _matrix) =>
{
	if (object.ReferenceEquals(_matrix, null))
		throw new System.Exception(", "\"null reference: _matrix\"", @");
	Matrix<", T_Source, @"> result = _matrix.Clone();
	for (int i = 0; i < _matrix.Rows; i++)
	{
		if (result[i, i] == 0)
			for (int j = i + 1; j < result.Rows; j++)
				if (result[j, i] != 0)
					", SwapRows("temp", "k", "result", "i", "j"), @"
		if (result[i, i] == 0)
			continue;
		if (result[i, i] != 1)
			for (int j = i + 1; j < result.Rows; j++)
				if (result[j, i] == 1)
					", SwapRows("temp", "k", "result", "i", "j"), @"
		", T_Source, @" temp_rowMultiplication = 1 / result[i, i];
		", RowMultiplication("j", "result", "i", "temp_rowMultiplication"), @"
		for (int j = i + 1; j < result.Rows; j++)
		{
			", T_Source, @" scalar = -result[j, i];
			", RowAddition("k", "result", "j", "i", "scalar"), @"
		}
	}
	return result;
}"));

			return Matrix<T>.Matrix_Echelon(matrix);

			#region Alternate Version
			//Matrix<generic> result = _matrix.Clone();
			//for (int i = 0; i < _matrix.Rows; i++)
			//{
			//	if (result[i, i] == 0)
			//		for (int j = i + 1; j < result.Rows; j++)
			//			if (result[j, i] != 0)
			//				Matrix.SwapRows(result, i, j);
			//	if (result[i, i] == 0)
			//		continue;
			//	if (result[i, i] != 1)
			//		for (int j = i + 1; j < result.Rows; j++)
			//			if (result[j, i] == 1)
			//				Matrix.SwapRows(result, i, j);
			//	Matrix.RowMultiplication(result, i, 1 / result[i, i]);
			//	for (int j = i + 1; j < result.Rows; j++)
			//		Matrix.RowAddition(result, j, i, -result[j, i]);
			#endregion
		};
		#endregion

		#region ReducedEchelon
		/// <summary>Calculates the echelon of a matrix and reduces it (aka RREF).</summary>
		private static Matrix<T>.Delegates.Matrix_ReducedEchelon Matrix_ReducedEchelon = (Matrix<T> matrix) =>
		{
			Matrix<T>.Matrix_ReducedEchelon =
				Meta.Compile<Matrix<T>.Delegates.Matrix_ReducedEchelon>(
					string.Concat(
"(Matrix<", T_Source, @"> _matrix) =>
		{
	if (_matrix == null)
		throw new System.Exception(", "\"null reference: _matrix\"", @");
	Matrix<", T_Source, @"> result = _matrix.Clone();
	for (int i = 0; i < _matrix.Rows; i++)
	{
		if (result[i, i] == 0)
			for (int j = i + 1; j < result.Rows; j++)
				if (result[j, i] != 0)
					", SwapRows("temp", "k", "result", "i", "j"), @"
		if (result[i, i] == 0) continue;
		if (result[i, i] != 1)
			for (int j = i + 1; j < result.Rows; j++)
				if (result[j, i] == 1)
					", SwapRows("temp", "k", "result", "i", "j"), @"
		", T_Source, @" temp_rowMultiplication = 1 / result[i, i];
		", RowMultiplication("j", "result", "i", "temp_rowMultiplication"), @"
		for (int j = i + 1; j < result.Rows; j++)
		{
			", T_Source, @" scalar = -result[j, i];
			", RowAddition("k", "result", "j", "i", "scalar"), @"
		}
		for (int j = i - 1; j >= 0; j--)
		{
			", T_Source, @" scalar = -result[j, i];
			", RowAddition("k", "result", "j", "i", "scalar"), @"
		}
	}
	return result;
}"));

			return Matrix<T>.Matrix_ReducedEchelon(matrix);

			#region Alternate Version
			//Matrix<generic> result = matrix.Clone();
			//for (int i = 0; i < matrix.Rows; i++)
			//{
			//	if (result[i, i] == 0)
			//		for (int j = i + 1; j < result.Rows; j++)
			//			if (result[j, i] != 0)
			//				Matrix.SwapRows(result, i, j);
			//	if (result[i, i] == 0) continue;
			//	if (result[i, i] != 1)
			//		for (int j = i + 1; j < result.Rows; j++)
			//			if (result[j, i] == 1)
			//				Matrix.SwapRows(result, i, j);
			//	Matrix.RowMultiplication(result, i, 1 / result[i, i]);
			//	for (int j = i + 1; j < result.Rows; j++)
			//		Matrix.RowAddition(result, j, i, -result[j, i]);
			//	for (int j = i - 1; j >= 0; j--)
			//		Matrix.RowAddition(result, j, i, -result[j, i]);
			#endregion
		};
		#endregion

		#region Inverse
		/// <summary>Calculates the inverse of a matrix.</summary>
		private static Matrix<T>.Delegates.Matrix_Inverse Matrix_Inverse = (Matrix<T> matrix) =>
		{
			Matrix<T>.Matrix_Inverse =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Inverse>(
					string.Concat(
@"(Matrix<", T_Source, @">.Delegates.Matrix_Inverse)(
(Matrix<", T_Source, @"> _matrix) =>
{
	if (object.ReferenceEquals(_matrix, null))
		throw new System.ArgumentNullException(", "\"matrix\"", @");
	if (Matrix<", T_Source, @">.Determinent(_matrix) == 0)
		throw new System.ArithmeticException(", "\"inverse calculation failed.\"", @");
	Matrix<", T_Source, @"> identity = Matrix<", T_Source, @">.FactoryIdentity(_matrix.Rows, _matrix.Columns);
	Matrix<", T_Source, @"> rref = _matrix.Clone();
	for (int i = 0; i < _matrix.Rows; i++)
	{
		if (rref[i, i] == 0)
			for (int j = i + 1; j < rref.Rows; j++)
				if (rref[j, i] != 0)
				{
					", SwapRows("temp", "k", "rref", "i", "j"), @"
					", SwapRows("temp", "k", "identity", "i", "j"), @"
				}
		", T_Source, @" temp_rowMultiplication1 = 1 / rref[i, i];
		", RowMultiplication("j", "identity", "i", "temp_rowMultiplication1"), @"
		", T_Source, @" temp_rowMultiplication2 = 1 / rref[i, i];
		", RowMultiplication("j", "rref", "i", "temp_rowMultiplication2"), @"
		for (int j = i + 1; j < rref.Rows; j++)
		{
			", T_Source, @" scalar1 = -result[j, i];
			", RowAddition("k", "identity", "j", "i", "scalar1"), @"
			", T_Source, @" scalar2 = -result[j, i];
			", RowAddition("k", "rref", "j", "i", "scalar2"), @"
		}
		for (int j = i - 1; j >= 0; j--)
		{
			", T_Source, @" scalar1 = -result[j, i];
			", RowAddition("k", "identity", "j", "i", "scalar1"), @"
			", T_Source, @" scalar2 = -result[j, i];
			", RowAddition("k", "rref", "j", "i", "scalar2"), @"
		}
	}
	return identity;
}"));

			return Matrix<T>.Matrix_Inverse(matrix);

			#region Alternate Version
			//Matrix<T> identity = Matrix<T>.FactoryIdentity(matrix.Rows, matrix.Columns);
			//Matrix<T> rref = matrix.Clone();
			//for (int i = 0; i < matrix.Rows; i++)
			//{
			//	if (Compute<T>.Equate(rref[i, i], Compute<T>.FromInt32(0)))
			//		for (int j = i + 1; j < rref.Rows; j++)
			//			if (!Compute<T>.Equate(rref[j, i], Compute<T>.FromInt32(0)))
			//			{
			//				Matrix<T>.SwapRows(rref, i, j);
			//				Matrix<T>.SwapRows(identity, i, j);
			//			}
			//	Matrix<T>.RowMultiplication(identity, i, Compute<T>.Divide(Compute<T>.FromInt32(1), rref[i, i]));
			//	Matrix<T>.RowMultiplication(rref, i, Compute<T>.Divide(Compute<T>.FromInt32(1), rref[i, i]));
			//	for (int j = i + 1; j < rref.Rows; j++)
			//	{
			//		Matrix<T>.RowAddition(identity, j, i, Compute<T>.Negate(rref[j, i]));
			//		Matrix<T>.RowAddition(rref, j, i, Compute<T>.Negate(rref[j, i]));
			//	}
			//	for (int j = i - 1; j >= 0; j--)
			//	{
			//		Matrix<T>.RowAddition(identity, j, i, Compute<T>.Negate(rref[j, i]));
			//		Matrix<T>.RowAddition(rref, j, i, Compute<T>.Negate(rref[j, i]));
			//	}
			//}
			//return identity;
			#endregion
		};
		#endregion

		#region Adjoint
		/// <summary>Calculates the adjoint of a matrix.</summary>
		private static Matrix<T>.Delegates.Matrix_Adjoint Matrix_Adjoint = (Matrix<T> matrix) =>
		{
			Matrix<T>.Matrix_Adjoint =
				Meta.Compile<Matrix<T>.Delegates.Matrix_Adjoint>(
					string.Concat(
"(Matrix<", T_Source, @"> _matrix) =>
{
	if (object.ReferenceEquals(_matrix, null))
		throw new System.ArgumentNullException(", "\"matrix\"", @");
	if (!(_matrix.Rows == _matrix.Columns))
		throw new System.ArithmeticException(", "\"Adjoint of a non-square _matrix does not exists\"", @");
	Matrix<", T_Source, "> AdjointMatrix = new Matrix<", T_Source, @">(_matrix.Rows, _matrix.Columns);
	for (int i = 0; i < _matrix.Rows; i++)
		for (int j = 0; j < _matrix.Columns; j++)
			if ((i, j) % 2 == 0)
				AdjointMatrix[i, j] = Matrix<", T_Source, ">.Determinent(Matrix<", T_Source, @">.Minor(_matrix, i, j));
			else
				AdjointMatrix[i, j] = -Matrix<", T_Source, ">.Determinent(Matrix<", T_Source, @">.Minor(_matrix, i, j));
	return Matrix<", T_Source, @">.Transpose(AdjointMatrix);
}"));

			return Matrix<T>.Matrix_Adjoint(matrix);

			#region Alternate Version
			//Matrix<T> AdjointMatrix = new Matrix<T>(matrix.Rows, matrix.Columns);
			//for (int i = 0; i < matrix.Rows; i++)
			//	for (int j = 0; j < matrix.Columns; j++)
			//		if ((i + j) % 2 == 0)
			//			AdjointMatrix[i, j] = Matrix<T>.Determinent(Matrix<T>.Minor(matrix, i, j));
			//		else
			//			AdjointMatrix[i, j] = Compute<T>.Negate(Matrix<T>.Determinent(Matrix<T>.Minor(matrix, i, j)));
			//return Matrix<T>.Transpose(AdjointMatrix);
			#endregion
		};
		#endregion

		#region Transpose
		/// <summary>Returns the transpose of a matrix.</summary>
		private static Matrix<T>.Delegates.Matrix_Transpose Matrix_Transpose = (Matrix<T> matrix) =>
		{
			if (object.ReferenceEquals(null, matrix))
				throw new System.ArgumentNullException("matrix");
			Matrix<T> transpose =
				new Matrix<T>(matrix.Columns, matrix.Rows);
			for (int i = 0; i < transpose.Rows; i++)
				for (int j = 0; j < transpose.Columns; j++)
					transpose[i, j] = matrix[j, i];
			return transpose;
		};
		#endregion

		#region DecomposeLU
		/// <summary>Decomposes a matrix into lower-upper reptresentation.</summary>
		private static Matrix<T>.Delegates.Matrix_DecomposeLU Matrix_DecomposeLU = (Matrix<T> matrix, out Matrix<T> lower, out Matrix<T> upper) =>
		{
			Matrix<T>.Matrix_DecomposeLU =
				Meta.Compile<Matrix<T>.Delegates.Matrix_DecomposeLU>(
					string.Concat(
"(Matrix<", T_Source, "> _matrix, out Matrix<", T_Source, "> _Lower, out Matrix<", T_Source, @"> _Upper) =>
{
	if (object.ReferenceEquals(_matrix, null))
		throw new System.Exception(", "\"null reference: _matrix\"", @");
	if (_matrix.Rows != _matrix.Columns)
		throw new System.Exception(", "\"non-square _matrix during DecomposeLU function\"", @");
	_Lower = Matrix<", T_Source, @">.FactoryIdentity(_matrix.Rows, _matrix.Columns);
	_Upper = _matrix.Clone();
	int[] permutation = new int[_matrix.Rows];
	for (int i = 0; i < _matrix.Rows; i++) permutation[i] = i;
	", T_Source, @" p = 0, pom2, detOfP = 1;
	int k0 = 0, pom1 = 0;
	for (int k = 0; k < _matrix.Columns - 1; k++)
	{
		p = 0;
		for (int i = k; i < _matrix.Rows; i++)
				if ((_Upper[i, k] > 0 ? _Upper[i, k] : -_Upper[i, k]) > p)
				{
						p = _Upper[i, k] > 0 ? _Upper[i, k] : -_Upper[i, k];
						k0 = i;
				}
		if (p == 0)
				throw new System.Exception(", "\"The _matrix is singular!\"", @");
		pom1 = permutation[k];
		permutation[k] = permutation[k0];
		permutation[k0] = pom1;
		for (int i = 0; i < k; i++)
		{
				pom2 = _Lower[k, i];
				_Lower[k, i] = _Lower[k0, i];
				_Lower[k0, i] = pom2;
		}
		if (k != k0)
				detOfP *= -1;
		for (int i = 0; i < _matrix.Columns; i++)
		{
				pom2 = _Upper[k, i];
				_Upper[k, i] = _Upper[k0, i];
				_Upper[k0, i] = pom2;
		}
		for (int i = k + 1; i < _matrix.Rows; i++)
		{
				_Lower[i, k] = _Upper[i, k] / _Upper[k, k];
				for (int j = k; j < _matrix.Columns; j++)
					_Upper[i, j] = _Upper[i, j] - _Lower[i, k] * _Upper[k, j];
		}
	}
}"));

			Matrix<T>.Matrix_DecomposeLU(matrix, out lower, out upper);

			#region Alternate Version
			//lower = Matrix<T>.FactoryIdentity(matrix.Rows, matrix.Columns);
			//upper = matrix.Clone();
			//int[] permutation = new int[matrix.Rows];
			//for (int i = 0; i < matrix.Rows; i++) permutation[i] = i;
			//T p = 0, pom2, detOfP = 1;
			//int k0 = 0, pom1 = 0;
			//for (int k = 0; k < matrix.Columns - 1; k++)
			//{
			//	p = 0;
			//	for (int i = k; i < matrix.Rows; i++)
			//		if ((upper[i, k] > 0 ? upper[i, k] : -upper[i, k]) > p)
			//		{
			//			p = upper[i, k] > 0 ? upper[i, k] : -upper[i, k];
			//			k0 = i;
			//		}
			//	if (p == 0)
			//		throw new System.Exception("The matrix is singular!");
			//	pom1 = permutation[k];
			//	permutation[k] = permutation[k0];
			//	permutation[k0] = pom1;
			//	for (int i = 0; i < k; i++)
			//	{
			//		pom2 = lower[k, i];
			//		lower[k, i] = lower[k0, i];
			//		lower[k0, i] = pom2;
			//	}
			//	if (k != k0)
			//		detOfP *= -1;
			//	for (int i = 0; i < matrix.Columns; i++)
			//	{
			//		pom2 = upper[k, i];
			//		upper[k, i] = upper[k0, i];
			//		upper[k0, i] = pom2;
			//	}
			//	for (int i = k + 1; i < matrix.Rows; i++)
			//	{
			//		lower[i, k] = upper[i, k] / upper[k, k];
			//		for (int j = k; j < matrix.Columns; j++)
			//			upper[i, j] = upper[i, j] - lower[i, k] * upper[k, j];
			//	}
			//}
			#endregion
		};
		#endregion

		#region EqualsByValue
		/// <summary>Does a value equality check.</summary>
		private static Matrix<T>.Delegates.Matrix_EqualsByValue Matrix_EqualsByValue = (Matrix<T> left, Matrix<T> right) =>
		{
			Matrix<T>.Matrix_EqualsByValue =
				Meta.Compile<Matrix<T>.Delegates.Matrix_EqualsByValue>(
					string.Concat(
"(Matrix<" + T_Source + "> _left, Matrix<" + T_Source + @"> _right) =>
{
	if (object.ReferenceEquals(_left, null) && object.ReferenceEquals(_right, null))
		return true;
	if (object.ReferenceEquals(_left, null))
		return false;
	if (object.ReferenceEquals(_right, null))
		return false;
	if (_left.Rows != _right.Rows || _left.Columns != _right.Columns)
		return false;
	for (int i = 0; i < _left.Rows; i++)
		for (int j = 0; j < _left.Columns; j++)
			if (_left[i, j] != _right[i, j])
				return false;
	return true;
}"));

			return Matrix<T>.Matrix_EqualsByValue(left, right);

			#region Alternate Version
			//if (object.ReferenceEquals(left, null) && object.ReferenceEquals(right, null))
			//	return true;
			//if (object.ReferenceEquals(left, null))
			//	return false;
			//if (object.ReferenceEquals(right, null))
			//	return false;
			//if (left.Rows != right.Rows || left.Columns != right.Columns)
			//	return false;
			//for (int i = 0; i < left.Rows; i++)
			//	for (int j = 0; j < left.Columns; j++)
			//		if (left[i, j] != right[i, j])
			//			return false;
			//return true;
			#endregion
		};
		#endregion

		#region EqualsByValue_leniency
		/// <summary>Does a value equality check with leniency.</summary>
		private static Matrix<T>.Delegates.Matrix_EqualsByValue_leniency Matrix_EqualsByValue_leniency = (Matrix<T> left, Matrix<T> right, T leniency) =>
		{
			Matrix<T>.Matrix_EqualsByValue_leniency =
				Meta.Compile<Matrix<T>.Delegates.Matrix_EqualsByValue_leniency>(
					string.Concat(
"(Matrix<", T_Source, "> _left, Matrix<", T_Source, "> _right, ", T_Source, @" _leniency) =>
{
	if (object.ReferenceEquals(_left, null) && object.ReferenceEquals(_right, null))
		return true;
	if (object.ReferenceEquals(_left, null))
		return false;
	if (object.ReferenceEquals(_right, null))
		return false;
	if (_left.Rows != _right.Rows || _left.Columns != _right.Columns)
		return false;
	for (int i = 0; i < _left.Rows; i++)
		for (int j = 0; j < _left.Columns; j++)
			if (Compute<", T_Source, @">.AbsoluteValue(_left[i, j] - _right[i, j]) > _leniency)
				return false;
	return true;
}"));

			return Matrix<T>.Matrix_EqualsByValue_leniency(left, right, leniency);

			#region Alternate Version
			//if (object.ReferenceEquals(_left, null) && object.ReferenceEquals(_right, null))
			//	return true;
			//if (object.ReferenceEquals(_left, null))
			//	return false;
			//if (object.ReferenceEquals(_right, null))
			//	return false;
			//if (_left.Rows != _right.Rows || _left.Columns != _right.Columns)
			//	return false;
			//for (int i = 0; i < _left.Rows; i++)
			//	for (int j = 0; j < _left.Columns; j++)
			//		if (Logic.Abs(_left[i, j] - _right[i, j]) > _leniency)
			//			return false;
			//return true;
			#endregion
		};
		#endregion

		#region generic

		//		/// <summary>Negates all the values in a matrix.</summary>
		//		/// <param name="matrix">The matrix to have its values negated.</param>
		//		/// <returns>The resulting matrix after the negations.</returns>
		//		public static Matrix<generic> Negate_parallel(Matrix<generic> matrix)
		//		{
		//			#region error checking

		//#if no_error_checking
		//					// nothing
		//#else
		//			if (object.ReferenceEquals(matrix, null))
		//				throw new System.Exception("null reference: matirx");
		//#endif

		//			#endregion

		//			// At the moment, negation does not benefit from multithreading.
		//			if (false) //matrix.Rows * matrix.Columns > _parallelMinimum)
		//			{
		//				#region flattened array

		//				if (matrix.CurrentFormat == Matrix<generic>.Format.FlattenedArray)
		//				{

		//					Matrix<generic> result =
		//					new Matrix<generic>(matrix.Rows, matrix.Columns);
		//					int size = matrix.Rows * matrix.Columns;

		//#if unsafe_code
		//												unsafe
		//												{
		//														Theta.Parallels.AutoParallel.Divide(
		//															(int current, int max) =>
		//															{
		//																	return () =>
		//																	{
		//																			fixed (generic*
		//																				result_flat = result._matrix as generic[],
		//																				matrix_flat = matrix._matrix as generic[])
		//																					for (int i = current; i < size; i += max)
		//																							result_flat[i] = -matrix_flat[i];
		//																	};
		//															}, Logic.Max(matrix.Rows, matrix.Columns));
		//												}
		//#else
		//					Theta.Parallels.AutoParallel.Divide(
		//						(int current, int max) =>
		//						{
		//							return () =>
		//							{
		//								generic[] matrix_flat = matrix._matrix as generic[];
		//								generic[] result_flat = result._matrix as generic[];
		//								for (int i = current; i < size; i += max)
		//									result_flat[i] = -matrix_flat[i];
		//							};
		//						}, Logic.Max(matrix.Rows, matrix.Columns));
		//#endif
		//					return result;

		//				}

		//				#endregion
		//			}
		//			return LinearAlgebra.Negate(matrix);
		//		}

		//				/// <summary>Does standard addition of two matrices.</summary>
		//				/// <param name="left">The left matrix of the addition.</param>
		//				/// <param name="right">The right matrix of the addition.</param>
		//				/// <returns>The resulting matrix after the addition.</returns>
		//				public static Matrix<generic> Add_parallel(Matrix<generic> left, Matrix<generic> right)
		//				{
		//						#region error checking

		//#if no_error_checking
		//			// nothing
		//#else
		//						if (object.ReferenceEquals(left, null))
		//								throw new System.Exception("null reference: left");
		//						if (object.ReferenceEquals(right, null))
		//								throw new System.Exception("null reference: right");
		//						if (left.Rows != right.Rows || left.Columns != right.Columns)
		//								throw new System.Exception("invalid matrix addition (dimension miss-match).");
		//#endif

		//						#endregion

		//						// At the moment, addition does not benefit from multithreading.
		//						if (false) //matrix.Rows * matrix.Columns > _parallelMinimum)
		//						{
		//								#region flattened array

		//								if (left.CurrentFormat == Matrix<generic>.Format.FlattenedArray)
		//								{

		//										Matrix<generic> result =
		//										new Matrix<generic>(left.Rows, left.Columns);
		//										int size = left.Rows * left.Columns;

		//#if unsafe_code
		//										unsafe
		//										{
		//												Theta.Parallels.AutoParallel.Divide(
		//													(int current, int max) =>
		//													{
		//															return () =>
		//															{
		//																	fixed (generic*
		//																		left_flat = left._matrix as generic[],
		//																		right_flat = right._matrix as generic[],
		//																		result_flat = result._matrix as generic[])
		//																			for (int i = current; i < size; i += max)
		//																					result_flat[i] = left_flat[i] + right_flat[i];
		//															};
		//													}, Logic.Max(left.Rows, left.Columns));
		//										}
		//#else
		//					Theta.Parallels.AutoParallel.Divide(
		//						(int current, int max) =>
		//						{
		//							return () =>
		//							{
		//								generic[] left_flat = left._matrix as generic[];
		//								generic[] right_flat = right._matrix as generic[];
		//								generic[] result_flat = result._matrix as generic[];
		//								for (int i = current; i < size; i += max)
		//									result_flat[i] = left_flat[i] + right_flat[i];
		//							};
		//						}, Logic.Max(left.Rows, left.Columns));
		//#endif
		//										return result;

		//								}

		//								#endregion
		//						}
		//						return LinearAlgebra.Add(left, right);
		//				}

		//				/// <summary>Subtracts a scalar from all the values in a matrix.</summary>
		//				/// <param name="left">The matrix to have the values subtracted from.</param>
		//				/// <param name="right">The scalar to subtract from all the matrix values.</param>
		//				/// <returns>The resulting matrix after the subtractions.</returns>
		//				public static Matrix<generic> Subtract_parallel(Matrix<generic> left, Matrix<generic> right)
		//				{
		//						#region error checking

		//#if no_error_checking
		//			// nothing
		//#else
		//						if (object.ReferenceEquals(left, null))
		//								throw new System.Exception("null reference: left");
		//						if (object.ReferenceEquals(right, null))
		//								throw new System.Exception("null reference: right");
		//						if (left.Rows != right.Rows || left.Columns != right.Columns)
		//								throw new System.Exception("invalid matrix subtraction (dimension miss-match).");
		//#endif

		//						#endregion

		//						// At the moment, subtraction does not benefit from multithreading.
		//						if (false) //matrix.Rows * matrix.Columns > _parallelMinimum)
		//						{
		//								#region flattened arrays

		//								if (left.CurrentFormat == Matrix<generic>.Format.FlattenedArray &&
		//									right.CurrentFormat == Matrix<generic>.Format.FlattenedArray)
		//								{

		//										Matrix<generic> result =
		//										new Matrix<generic>(left.Rows, left.Columns);
		//										int size = left.Rows * left.Columns;

		//#if unsafe_code
		//										unsafe
		//										{
		//												Theta.Parallels.AutoParallel.Divide(
		//													(int current, int max) =>
		//													{
		//															return () =>
		//															{
		//																	fixed (generic*
		//																		left_flat = left._matrix as generic[],
		//																		right_flat = right._matrix as generic[],
		//																		result_flat = result._matrix as generic[])
		//																			for (int i = current; i < size; i += max)
		//																					result_flat[i] = left_flat[i] - right_flat[i];
		//															};
		//													}, Logic.Max(left.Rows, left.Columns));
		//										}
		//#else
		//					Theta.Parallels.AutoParallel.Divide(
		//					(int current, int max) =>
		//					{
		//						return () =>
		//						{
		//							generic[] left_flat = left._matrix as generic[];
		//							generic[] right_flat = right._matrix as generic[];
		//							generic[] result_flat = result._matrix as generic[];
		//								for (int i = current; i < size; i += max)
		//									result_flat[i] = left_flat[i] - right_flat[i];
		//						};
		//					}, Logic.Max(left.Rows, left.Columns));
		//#endif
		//										return result;
		//								}

		//								#endregion
		//						}
		//						return LinearAlgebra.Subtract(left, right);
		//				}

		//				/// <summary>Performs multiplication on two matrices using multi-threading.</summary>
		//				/// <param name="left">The left matrix of the multiplication.</param>
		//				/// <param name="right">The right matrix of the multiplication.</param>
		//				/// <returns>The resulting matrix of the multiplication.</returns>
		//				public static Matrix<generic> Multiply_parrallel(Matrix<generic> left, Matrix<generic> right)
		//				{
		//						#region error checking

		//#if no_error_checking
		//			// nothing
		//#else
		//						if (left == null)
		//								throw new System.Exception("null reference: left");
		//						if (right == null)
		//								throw new System.Exception("null reference: right");
		//						if (left.Columns != right.Rows)
		//								throw new System.Exception("invalid multiplication (dimension miss-match).");
		//#endif

		//						#endregion

		//						int result_rows = left.Rows;
		//						int left_cols = left.Columns;
		//						int result_cols = right.Columns;

		//						// If there are enough rows to warrant multi-threading,
		//						// then multithread the row for-loop.
		//						if (result_rows * result_cols > _parallelMinimum &&
		//							result_rows >= result_cols)
		//						{
		//								#region flattened arrays

		//								if (left.CurrentFormat == Matrix<generic>.Format.FlattenedArray &&
		//									right.CurrentFormat == Matrix<generic>.Format.FlattenedArray)
		//								{

		//										Matrix<generic> result =
		//											new Matrix<generic>(result_rows, result_cols);

		//#if unsafe_code
		//										unsafe
		//										{
		//												Theta.Parallels.AutoParallel.Divide(
		//													(int current, int max) =>
		//													{
		//															return () =>
		//															{
		//																	generic sum;
		//																	int left_i_offest;
		//																	int result_i_offset;

		//																	fixed (generic*
		//																		result_flat = result._matrix as generic[],
		//																		left_flat = left._matrix as generic[],
		//																		right_flat = right._matrix as generic[])
		//																			for (int i = current; i < result_rows; i += max)
		//																			{
		//																					left_i_offest = i * left_cols;
		//																					result_i_offset = i * result_cols;
		//																					for (int j = 0; j < result_cols; j++)
		//																					{
		//																							sum = 0;
		//																							for (int k = 0; k < left_cols; k++)
		//																									sum += left_flat[left_i_offest + k] * right_flat[k * result_cols + j];
		//																							result_flat[result_i_offset + j] = sum;
		//																					}
		//																			}
		//															};
		//													}, result_rows);
		//										}
		//#else
		//				generic[] result_flat = result._matrix as generic[];
		//				generic[] left_flat = left._matrix as generic[];
		//				generic[] right_flat = right._matrix as generic[];

		//				Theta.Parallels.AutoParallel.Divide(
		//						(int current, int max) =>
		//						{
		//							return () =>
		//							{
		//								generic sum;
		//								int left_i_offest;
		//								int result_i_offset;

		//								for (int i = current; i < result_rows; i += max)
		//								{
		//									left_i_offest = i * left_cols;
		//									result_i_offset = i * result_cols;
		//									for (int j = 0; j < result_cols; j++)
		//									{
		//										sum = 0;
		//										for (int k = 0; k < left_cols; k++)
		//											sum += left_flat[left_i_offest + k] * right_flat[k * result_cols + j];
		//										result_flat[result_i_offset + j] = sum;
		//									}
		//								}
		//							};
		//						}, result_rows);

		//#endif

		//										return result;
		//								}

		//								#endregion
		//						}
		//						// If there are enough columns to warrant multi-threading,
		//						// then multithread the column for-loop.
		//						else if (result_rows * result_cols > _parallelMinimum &&
		//							result_rows < result_cols)
		//						{
		//								#region flattened arrays

		//								if (left.CurrentFormat == Matrix<generic>.Format.FlattenedArray &&
		//									right.CurrentFormat == Matrix<generic>.Format.FlattenedArray)
		//								{

		//										Matrix<generic> result =
		//											new Matrix<generic>(result_rows, result_cols);
		//#if unsafe_code
		//										unsafe
		//										{
		//												Theta.Parallels.AutoParallel.Divide(
		//													(int current, int max) =>
		//													{
		//															return () =>
		//															{
		//																	generic sum;
		//																	int left_i_offest;
		//																	int result_i_offset;

		//																	fixed (generic*
		//																		result_flat = result._matrix as generic[],
		//																		left_flat = left._matrix as generic[],
		//																		right_flat = right._matrix as generic[])
		//																			for (int i = 0; i < result_rows; i++)
		//																			{
		//																					left_i_offest = i * left_cols;
		//																					result_i_offset = i * result_cols;
		//																					for (int j = current; j < result_cols; j += max)
		//																					{
		//																							sum = 0;
		//																							for (int k = 0; k < left_cols; k++)
		//																									sum += left_flat[left_i_offest + k] * right_flat[k * result_cols + j];
		//																							result_flat[result_i_offset + j] = sum;
		//																					}
		//																			}
		//															};
		//													}, result_cols);
		//										}
		//#else
		//				generic[] result_flat = result._matrix as generic[];
		//				generic[] left_flat = left._matrix as generic[];
		//				generic[] right_flat = right._matrix as generic[];

		//				Theta.Parallels.AutoParallel.Divide(
		//						(int current, int max) =>
		//						{
		//							return () =>
		//							{
		//								generic sum;
		//								int left_i_offest;
		//								int result_i_offset;

		//								for (int i = 0; i < result_rows; i++)
		//								{
		//									left_i_offest = i * left_cols;
		//									result_i_offset = i * result_cols;
		//									for (int j = current; j < result_cols; j += max)
		//									{
		//										sum = 0;
		//										for (int k = 0; k < left_cols; k++)
		//											sum += left_flat[left_i_offest + k] * right_flat[k * result_cols + j];
		//										result_flat[result_i_offset + j] = sum;
		//									}
		//								}
		//							};
		//						}, result_cols);
		//#endif
		//										return result;

		//								}

		//								#endregion
		//						}
		//						// Multi-threading is not necessary.
		//						return LinearAlgebra.Multiply(left, right);
		//				}

		#endregion

		#endregion

		#region Steppers

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> step_function)
		{
			for (int i = 0; i < this._matrix.Length; i++)
				step_function(this._matrix[i]);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> step_function)
		{
			for (int i = 0; i < this._matrix.Length; i++)
				step_function(ref this._matrix[i]);
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> step_function)
		{
			for (int i = 0; i < this._matrix.Length; i++)
				if (step_function(this._matrix[i]) == StepStatus.Break)
					return StepStatus.Break;
			return StepStatus.Continue;
		}

		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="step_function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> step_function)
		{
			for (int i = 0; i < this._matrix.Length; i++)
				if (step_function(ref this._matrix[i]) == StepStatus.Break)
					return StepStatus.Break;
			return StepStatus.Continue;
		}

		#endregion

		#region overrides

		/// <summary>Prints out a string representation of this matrix.</summary>
		/// <returns>A string representing this matrix.</returns>
		public override string ToString()
		{ return base.ToString(); }
		/// <summary>Matrixs a hash code from the values of this matrix.</summary>
		/// <returns>A hash code for the matrix.</returns>
		public override int GetHashCode()
		{ return this._matrix.GetHashCode(); }
		/// <summary>Does an equality check by value.</summary>
		/// <param name="right">The object to compare to.</param>
		/// <returns>True if the references are equal, false if not.</returns>
		public override bool Equals(object right)
		{
			if (!(right is Matrix<T>))
				return Matrix<T>.EqualsValue(this, (Matrix<T>)right);
			return false;
		}

		#endregion
	}
}
