>- [Home](https://github.com/53V3N1X/SevenFramework/wiki)<br />
>  - [Mathematics](https://github.com/53V3N1X/SevenFramework/wiki/Mathematics)
>    - Linear Algebra

##### Summary

>Linear Algebra is a branch of mathematics that deals with spatial computations
>and solving systems of equations.

##### Code Map (publicly visible)

> - Seven.Mathematics (namespace)<br />
>   - LinearAlgebra (static class)<br />
>     - LinearAlgebra_generic<T> (class)<br />
>   - LinearAlgebra_Extensions (static class)<br />
>   - LinearAlgebra<T> (interface)<br />
>   - Vector<T> (class)<br />
>   - Matrix<T> (class)<br />
>   - Quaternion<T> (class)<br />

##### Class Summaries

> - LinearAlgebra (static class)<br />
>Contains all the linear algebra functionality in static functions.<br />
> - LinearAlgebra_generic<T> (class)<br />
>Allows creation of a linear algebra library for the given type at runtime instead of compile time.<br />
> - LinearAlgebra_Extensions (static class)<br />
>Includes extension methods for C# arrays to perform linear algebra.<br />
> - LinearAlgebra<T> (interface)<br />
>Provides the linear algebra delegates for generic types (such as vector, quaternion, and matrix).<br />
> - Vector<T> (class)<br />
>A wrapper around a 1D array to perform vector operations on.<br />
> - Matrix<T> (class)<br />
>A wrapper around a 2D array to perform matrix operations on.<br />
> - Quaternion<T> (class)<br />
>A 4D vector using complex numbers to represent rotations in 3d space.<br />

##### Adding Additional Types

>If you want to use linear algebra on a non-supported type. All you have to do
>is add a "LinearAlgebra<YOUR_TYPE>" to the static dictionary "LinearAlgebra._linearAlgebras"
>before any linear algebra object or functions are called on that type. If you don't
>want to build your own Linear Algebra library, you can use the "LinearAlgebra.LinearAlgebra_generic"
>class to build a library at runtime.

##### Functionality

> - Vector Operations
><pre>
>/// <summary>Adds two vectors together.</summary><br />
>LinearAlgebra.delegates.Vector_Add<T> Vector_Add<br />
>/// <summary>Negates all the values in a vector.</summary><br />
>LinearAlgebra.delegates.Vector_Negate<T> Vector_Negate<br />
>/// <summary>Subtracts two vectors.</summary><br />
>LinearAlgebra.delegates.Vector_Subtract<T> Vector_Subtract<br />
>/// <summary>Multiplies all the components of a vecotr by a scalar.</summary><br />
>LinearAlgebra.delegates.Vector_Multiply<T> Vector_Multiply<br />
>/// <summary>Divides all the components of a vector by a scalar.</summary><br />
>LinearAlgebra.delegates.Vector_Divide<T> Vector_Divide<br />
>/// <summary>Computes the dot product between two vectors.</summary><br />
>LinearAlgebra.delegates.Vector_DotProduct<T> Vector_DotProduct<br />
>/// <summary>Computes teh cross product of two vectors.</summary><br />
>LinearAlgebra.delegates.Vector_CrossProduct<T> Vector_CrossProduct<br />
>/// <summary>Normalizes a vector.</summary><br />
>LinearAlgebra.delegates.Vector_Normalize<T> Vector_Normalize<br />
>/// <summary>Computes the length of a vector.</summary><br />
>LinearAlgebra.delegates.Vector_Magnitude<T> Vector_Magnitude<br />
>/// <summary>Computes the length of a vector but doesn't square root it for efficiency (length remains squared).</summary><br />
>LinearAlgebra.delegates.Vector_MagnitudeSquared<T> Vector_MagnitudeSquared<br />
>/// <summary>Computes the angle between two vectors.</summary><br />
>LinearAlgebra.delegates.Vector_Angle<T> Vector_Angle<br />
>/// <summary>Rotates a vector by the specified axis and rotation values.</summary><br />
>LinearAlgebra.delegates.Vector_RotateBy<T> Vector_RotateBy<br />
>/// <summary>Computes the linear interpolation between two vectors.</summary><br />
>LinearAlgebra.delegates.Vector_Lerp<T> Vector_Lerp<br />
>/// <summary>Sphereically interpolates between two vectors.</summary><br />
>LinearAlgebra.delegates.Vector_Slerp<T> Vector_Slerp<br />
>/// <summary>Interpolates between three vectors using barycentric coordinates.</summary><br />
>LinearAlgebra.delegates.Vector_Blerp<T> Vector_Blerp<br />
>/// <summary>Checks for equality between two vectors.</summary><br />
>LinearAlgebra.delegates.Vector_EqualsValue<T> Vector_EqualsValue<br />
>/// <summary>Checks for equality between two vectors with a leniency.</summary><br />
>LinearAlgebra.delegates.Vector_EqualsValue_leniency<T> Vector_EqualsValue_leniency<br />
>/// <summary>Rotates a vector by a quaternion.</summary><br />
>LinearAlgebra.delegates.Vector_RotateBy_quaternion<T> Vector_RotateBy_quaternion<br />
></pre>
> - Matrix Operations
><pre>
>/// <summary>Negates all the values in this matrix.</summary><br />
>LinearAlgebra.delegates.Matrix_Negate<T> Matrix_Negate<br />
>/// <summary>Does a standard matrix addition.</summary><br />
>LinearAlgebra.delegates.Matrix_Add<T> Matrix_Add<br />
>/// <summary>Does a standard matrix subtraction.</summary><br />
>LinearAlgebra.delegates.Matrix_Subtract<T> Matrix_Subtract<br />
>/// <summary>Does a standard matrix multiplication (triple for loop).</summary><br />
>LinearAlgebra.delegates.Matrix_Multiply<T> Matrix_Multiply<br />
>/// <summary>Multiplies all the values in this matrix by a scalar.</summary><br />
>LinearAlgebra.delegates.Matrix_Multiply_scalar<T> Matrix_Multiply_scalar<br />
>/// <summary>Premultiplies a vector by a matrix.</summary><br />
>LinearAlgebra.delegates.Matrix_Multiply_vector<T> Matrix_Multiply_vector<br />
>/// <summary>Divides all the values in this matrix by a scalar.</summary><br />
>LinearAlgebra.delegates.Matrix_Divide<T> Matrix_Divide<br />
>/// <summary>Takes the matrix to the given int power.</summary><br />
>LinearAlgebra.delegates.Matrix_Power<T> Matrix_Power<br />
>/// <summary>Gets the minor of a matrix.</summary><br />
>LinearAlgebra.delegates.Matrix_Minor<T> Matrix_Minor<br />
>/// <summary>Combines two matrices from left to right (result.Rows = left.Rows && result.Columns = left.Columns + right.Columns).</summary><br />
>LinearAlgebra.delegates.Matrix_ConcatenateRowWise<T> Matrix_ConcatenateRowWise<br />
>/// <summary>Computes the determinent if this matrix is square.</summary><br />
>LinearAlgebra.delegates.Matrix_Determinent<T> Matrix_Determinent<br />
>/// <summary>Computes the echelon form of this matrix (aka REF).</summary><br />
>LinearAlgebra.delegates.Matrix_Echelon<T> Matrix_Echelon<br />
>/// <summary>Computes the reduced echelon form of this matrix (aka RREF).</summary><br />
>LinearAlgebra.delegates.Matrix_ReducedEchelon<T> Matrix_ReducedEchelon<br />
>/// <summary>Computes the inverse of this matrix.</summary><br />
>LinearAlgebra.delegates.Matrix_Inverse<T> Matrix_Inverse<br />
>/// <summary>Gets the adjoint of this matrix.</summary><br />
>LinearAlgebra.delegates.Matrix_Adjoint<T> Matrix_Adjoint<br />
>/// <summary>Transposes this matrix.</summary><br />
>LinearAlgebra.delegates.Matrix_Transpose<T> Matrix_Transpose<br />
>/// <summary>Decomposes a matrix to lower/upper components.</summary><br />
>LinearAlgebra.delegates.Matrix_DecomposeLU<T> Matrix_DecomposeLU<br />
>/// <summary>Dtermines equality but value.</summary><br />
>LinearAlgebra.delegates.Matrix_EqualsByValue<T> Matrix_EqualsByValue<br />
>/// <summary>Determines equality by value with leniency.</summary><br />
>LinearAlgebra.delegates.Matrix_EqualsByValue_leniency<T> Matrix_EqualsByValue_leniency<br />
></pre>
> - Quaternion Operations
><pre>
>/// <summary>Computes the magnitude of quaternion.</summary><br />
>LinearAlgebra.delegates.Quaternion_Magnitude<T> Quaternion_Magnitude<br />
>/// <summary>Computes the magnitude of a quaternion, but doesn't square root it.</summary><br />
>LinearAlgebra.delegates.Quaternion_MagnitudeSquared<T> Quaternion_MagnitudeSquared<br />
>/// <summary>Gets the conjugate of the quaternion.</summary><br />
>LinearAlgebra.delegates.Quaternion_Conjugate<T> Quaternion_Conjugate<br />
>/// <summary>Adds two quaternions together.</summary><br />
>LinearAlgebra.delegates.Quaternion_Add<T> Quaternion_Add<br />
>/// <summary>Subtracts two quaternions.</summary><br />
>LinearAlgebra.delegates.Quaternion_Subtract<T> Quaternion_Subtract<br />
>/// <summary>Multiplies two quaternions together.</summary><br />
>LinearAlgebra.delegates.Quaternion_Multiply<T> Quaternion_Multiply<br />
>/// <summary>Multiplies all the values of the quaternion by a scalar value.</summary><br />
>LinearAlgebra.delegates.Quaternion_Multiply_scalar<T> Quaternion_Multiply_scalar<br />
>/// <summary>Pre-multiplies a 3-component vector by a quaternion.</summary><br />
>LinearAlgebra.delegates.Quaternion_Multiply_Vector<T> Quaternion_Multiply_Vector<br />
>/// <summary>Normalizes the quaternion.</summary><br />
>LinearAlgebra.delegates.Quaternion_Normalize<T> Quaternion_Normalize<br />
>/// <summary>Inverts a quaternion.</summary><br />
>LinearAlgebra.delegates.Quaternion_Invert<T> Quaternion_Invert<br />
>/// <summary>Lenearly interpolates between two quaternions.</summary><br />
>LinearAlgebra.delegates.Quaternion_Lerp<T> Quaternion_Lerp<br />
>/// <summary>Sphereically interpolates between two quaternions.</summary><br />
>LinearAlgebra.delegates.Quaternion_Slerp<T> Quaternion_Slerp<br />
>/// <summary>Rotates a vector by a quaternion [v' = qvq'].</summary><br />
>LinearAlgebra.delegates.Quaternion_Rotate<T> Quaternion_Rotate<br />
>/// <summary>Does a value equality check.</summary><br />
>LinearAlgebra.delegates.Quaternion_EqualsValue<T> Quaternion_EqualsValue<br />
>/// <summary>Does a value equality check with leniency.</summary><br />
>LinearAlgebra.delegates.Quaternion_EqualsValue_leniency<T> Quaternion_EqualsValue_leniency { get; }<br />
></pre>
