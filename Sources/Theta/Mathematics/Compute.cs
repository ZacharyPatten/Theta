// Theta
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.md" in th root project directory.
// SUPPORT: See "SUPPORT.md" in the root project directory.

using Theta;
using Theta.Structures;
using Theta.Measurements;

using System;
using System.Linq.Expressions;

namespace Theta.Mathematics
{
	/// <summary>Primary class for generic mathematics in the SevenFramework.</summary>
	/// <typeparam name="T">The generic type to perform mathematics on (expected to be numeric).</typeparam>
	public static class Compute<T>
	{
		#region Delegates

		/// <summary>Static storage class for all the Delegates used in the Compute class.</summary>
		public static class Delegates
		{
			public delegate T FromInt32(int value);
			public delegate T ComputePi();
			/// <summary>Negates a value.</summary>
			/// <param name="value">The value to negate.</param>
			/// <returns>The result of the negation.</returns>
			public delegate T Negate(T value);
			/// <summary>Adds two operands together.</summary>
			/// <typeparam name="T">The type of the values to be added.</typeparam>
			/// <param name="left">The left operand of the addition.</param>
			/// <param name="right">The right operand of the addition.</param>
			/// <returns>The result of the addition.</returns>
			public delegate T Add(T left, T right);
			/// <summary>Compuates the algebraic summation [ Σ (stepper) ].</summary>
			/// <param name="stepper">The items to perform the summation on.</param>
			/// <returns>The summation of the provided items.</returns>
			public delegate T Summation(Stepper<T> stepper);
			/// <summary>Subtracts two operands.</summary>
			/// <param name="left">The left operand of the subtraction.</param>
			/// <param name="right">The right operand of the subtraction.</param>
			/// <returns>The result of the subtraction.</returns>
			public delegate T Subtract(T left, T right);
			/// <summary>Multiplies two operands together.</summary>
			/// <param name="left">The left operand of the multiplication.</param>
			/// <param name="right">The right operand of the multiplication.</param>
			/// <returns>The result of the multiplication.</returns>
			public delegate T Multiply(T left, T right);
			/// <summary>Divides two operands.</summary>
			/// <param name="left">The left operand of the division.</param>
			/// <param name="right">The right operand of the division.</param>
			/// <returns>The result of the division.</returns>
			public delegate T Divide(T left, T right);
			/// <summary>Finds the remainder between two operands.</summary>
			/// <param name="left">The left operand of the modulus.</param>
			/// <param name="right">The right operand of the modulus.</param>
			/// <returns>The result of the modulus.</returns>
			public delegate T Modulus(T left, T right);
			/// <summary>Takes one operand to the power of another.</summary>
			/// <param name="left">The base of the power operaition.</param>
			/// <param name="right">The exponent of the power operation.</param>
			/// <returns>The result of the power operation.</returns>
			public delegate T Power(T left, T right);
			/// <summary>Solves for "x": [ x ^ 2 = b ].</summary>
			/// <param name="b">The value to be square rooted.</param>
			/// <returns>x from: [ x ^ 2 = b ]</returns>
			public delegate T SquareRoot(T b);
			/// <summary>Solves for "x": [ x ^ r = b ].</summary>
			/// <param name="b">The number to be rooted.</param>
			/// <param name="r">The root to find of b.</param>
			/// <returns>x from: [ x ^ r = b ]</returns>
			public delegate T Root(T b, T r);
			/// <summary>Computes: [ log_b(n) ].</summary>
			/// <param name="n">The value to be log-ed.</param>
			/// <param name="b">The base of the log operation.</param>
			/// <returns>[ log_b(n) ]</returns>
			public delegate T Logarithm(T n, T b);
			/// <summary>Computes the absolute value of a value.</summary>
			/// <param name="n">The value to be absolut-ed.</param>
			/// <returns>The result of the absolute value operation.</returns>
			public delegate T AbsoluteValue(T n);
            /// <summary>Finds the max value in a set.</summary>
            /// <returns>The max value in the set.</returns>
            public delegate T Maximum2(T a, T b);
            /// <summary>Finds the min value in a set.</summary>
            /// <returns>The min value in the set.</returns>
            public delegate T Minimum2(T a, T b);
			/// <summary>Finds the max value in a set.</summary>
			/// <param name="stepper">The set to find the max value within.</param>
			/// <returns>The max value in the set.</returns>
			public delegate T Maximum(Stepper<T> stepper);
			/// <summary>Finds the min value in a set.</summary>
			/// <param name="stepper">The set to find the min value within.</param>
			/// <returns>The min value in the set.</returns>
			public delegate T Minimum(Stepper<T> stepper);
			/// <summary>Restricts a value to a min-max range.</summary>
			/// <param name="value">The value to be clamped.</param>
			/// <param name="minimum">The minimum value allowed.</param>
			/// <param name="maximum">The maximum value allowed.</param>
			/// <returns>The possibly clamped value.</returns>
			public delegate T Clamp(T value, T minimum, T maximum);
			/// <summary>Checks for equality by value with a leniency.</summary>
			/// <param name="left">The left operand of the equate operation.</param>
			/// <param name="right">The right operand of the equate operation.</param>
			/// <param name="leniency">The leniency of the equate operation.</param>
			/// <returns>True if the operand are within the leniency of each other.</returns>
			public delegate bool EqualsLeniency(T left, T right, T leniency);
			/// <summary>Determines if one operand is less than another.</summary>
			/// <param name="left">Left hand operand.</param>
			/// <param name="right">Right hand operand.</param>
			/// <returns>Returns (left < right).</returns>
			public delegate bool LessThan(T left, T right);
			/// <summary>Determines if one operand is greater than another.</summary>
			/// <param name="left">Left hand operand.</param>
			/// <param name="right">Right hand operand.</param>
			/// <returns>Returns (left > right).</returns>
			public delegate bool GreaterThan(T left, T right);
			public delegate bool LessThanOrEqualTo(T left, T right);
			public delegate bool GreaterThanOrEqualTo(T left, T right);
			/// <summary>Finds the greatest common factor between multiple integers.</summary>
			/// <param name="stepper">The stepper function for the set.</param>
			/// <returns>The greatest common factor.</returns>
			public delegate T GreatestCommonFactor(Stepper<T> stepper);
			/// <summary>Finds the least common multiple between multiple integers.</summary>
			/// <param name="stepper">The stepper function for the set.</param>
			/// <returns>The least common multiple.</returns>
			public delegate T LeastCommonMultiple(Stepper<T> stepper);
			/// <summary>Computes the prime factors of n.</summary>
			/// <typeparam name="T">The numeric type of the operation.</typeparam>
			/// <param name="n">The value to find the prime roots of.</param>
			/// <returns>The prime factors.</returns>
			public delegate void FactorPrimes(T n, Step<T> step);
			/// <summary>Computes: [ e ^ x ].</summary>
			/// <param name="x">The exponent.</param>
			/// <returns>[ e ^ x ]</returns>
			public delegate T Exponential(T x);
			/// <summary>Computes (natrual log): [ ln(n) ].</summary>
			/// <param name="n">The value to compute the natural log of.</param>
			/// <returns>The result of the natrual log operation.</returns>
			public delegate T NaturalLogarithm(T n);
			/// <summary>Computes: [ 1 / n ].</summary>
			/// <param name="n">The value to be inverted.</param>
			/// <returns>The result of the inversion.</returns>
			public delegate T Invert(T n);
			/// <summary>Determines if a number is a prime number.</summary>
			/// <param name="n">The number to determine the prime status of.</param>
			/// <returns>True if prime. False if not prime.</returns>
			public delegate bool IsPrime(T n);
			public delegate T LinearInterpolation(T x, T x0, T x1, T y0, T y1);
			/// <summary>Computes: [ N! ].</summary>
			/// <typeparam name="T">The numeric type of the operation.</typeparam>
			/// <param name="N">The number to compute the factorial of.</param>
			/// <returns>[ N! ]</returns>
			public delegate T Factorial(T N);
			/// <summary>Computes: [ N! / (n[0]! + n[1]! + n[3]! ...) ].</summary>
			/// <typeparam name="T">The numeric type of the operation.</typeparam>
			/// <param name="N">The total number of items in the set.</param>
			/// <param name="n">The number of items in the respective sub-sets.</param>
			/// <returns>[ N! / (n[0]! + n[1]! + n[3]! ...) ]</returns>
			public delegate T Combinations(T N, T[] n);
			/// <summary>Computes: [ N! / (N - n)! ]</summary>
			/// <typeparam name="T">The numeric type of the operation.</typeparam>
			/// <param name="N">The total number of items.</param>
			/// <param name="n">The number of items to choose.</param>
			/// <returns>[ N! / (N - n)! ]</returns>
			public delegate T Choose(T N, T n);
			/// <summary>Finds the number of occurences for each item and sorts them into a heap.</summary>
			/// <param name="stepper">The values to find the mode(s) of.</param>
			/// <returns>A heap containing all the values sorted on their occurence count.</returns>
			public delegate Heap<Link<T, int>> Mode(Stepper<T> stepper);
			/// <summary>Computes the mean (or average) between multiple values.</summary>
			/// <param name="stepper">The operands in the mean operation.</param>
			/// <returns>The mean (or average between the operands).</returns>
			public delegate T Mean(Stepper<T> stepper);
			/// <summary>Computes the mean (or average) between multiple values.</summary>
			/// <param name="stepper">The operands in the mean operation.</param>
			/// <returns>The mean (or average between the operands).</returns>
			public delegate T Mean2(T a, T b);
			/// <summary>Computes the median of a set of values.</summary>
			/// <param name="stepper">The values to compute the median of.</param>
			/// <returns>The computed median from the set of values.</returns>
			public delegate T Median(Stepper<T> stepper);
			/// <summary>Computes the geometric mean of a set of values.</summary>
			/// <param name="stepper">The values to compute the geometric mean of.</param>
			/// <returns>The computed geometric mean from the set of values.</returns>
			public delegate T GeometricMean(Stepper<T> stepper);
			/// <summary>Computes the variance of a set of values.</summary>
			/// <param name="stepper">The values to compute the variance of.</param>
			/// <returns>The computed variance from the set of values.</returns>
			public delegate T Variance(Stepper<T> stepper);
			/// <summary>Computes the standard deviation of a set of values.</summary>
			/// <param name="stepper">The values to compute the standard deviation of.</param>
			/// <returns>The computed standard deviation from the set of values.</returns>
			public delegate T StandardDeviation(Stepper<T> stepper);
			/// <summary>Computes the mean deviation of a set of values.</summary>
			/// <param name="stepper">The values to compute the mean deviation of.</param>
			/// <returns>The computed mean deviation from the set of values.</returns>
			public delegate T MeanDeviation(Stepper<T> stepper);
			/// <summary>Computes the range of a set of values.</summary>
			/// <param name="stepper">The values to compute the range of.</param>
			/// <param name="min">The found minimum value.</param>
			/// <param name="max">The found maximum value.</param>
			/// <returns>The computed range from the set of values.</returns>
			public delegate Range<T> Range(Stepper<T> stepper);
			/// <summary>Computes the quantiles of a set of values.</summary>
			/// <param name="quantiles">The number of quntiles to split the set by.</param>
			/// <param name="stepper">The values to compute the quantiles of.</param>
			/// <returns>The computed quantiles from the set of values.</returns>
			public delegate T[] Quantiles(int quantiles, Stepper<T> stepper);
			/// <summary>Relationship between two random variables or two sets of data.</summary>
			/// <param name="a">One of the two sets to find the correlation of.</param>
			/// <param name="b">One of the two sets to find the correlation of.</param>
			/// <returns>The correlation value.</returns>
			public delegate T Correlation(Stepper<T> a, Stepper<T> b);
			/// <summary>Computes the ratio [length of the side opposite to the angle / hypotenuse] in a right triangle.</summary>
			/// <param name="angle">The angle to compute the trigonometric function of.</param>
			/// <returns>The computed ratio.</returns>
			public delegate T Sine(Angle<T> angle);
			/// <summary>Computes the ratio [length of the side adjacent to the angle / hypotenuse] in a right triangle.</summary>
			/// <param name="angle">The angle to compute the trigonometric function of.</param>
			/// <returns>The computed ratio.</returns>
			public delegate T Cosine(Angle<T> angle);
			/// <summary>Computes the ratio [length of the side opposite to the angle / length of the side adjacent to the angle] in a right triangle.</summary>
			/// <param name="angle">The angle to compute the trigonometric function of.</param>
			/// <returns>The computed ratio.</returns>
			public delegate T Tangent(Angle<T> angle);
			/// <summary>Computes the ratio [hypotenuse / length of the side opposite to the angle] in a right triangle.</summary>
			/// <param name="angle">The angle to compute the trigonometric function of.</param>
			/// <returns>The computed ratio.</returns>
			public delegate T Cosecant(Angle<T> angle);
			/// <summary>Computes the ratio [hypotenuse / length of the side adjacent to the angle] in a right triangle.</summary>
			/// <param name="angle">The angle to compute the trigonometric function of.</param>
			/// <returns>The computed ratio.</returns>
			public delegate T Secant(Angle<T> angle);
			/// <summary>Computes the ratio [length of the side adjacent to the angle / length of the side opposite to the angle] in a right triangle.</summary>
			/// <param name="angle">The angle to compute the trigonometric function of.</param>
			/// <returns>The computed ratio.</returns>
			public delegate T Cotangent(Angle<T> angle);
			public delegate Angle<T> InverseSine(T ratio);
			public delegate Angle<T> InverseCosine(T ratio);
			public delegate Angle<T> InverseTangent(T ratio);
			public delegate Angle<T> InverseCosecant(T ratio);
			public delegate Angle<T> InverseSecant(T ratio);
			public delegate Angle<T> InverseCotangent(T ratio);
			public delegate T HyperbolicSine(Angle<T> angle);
			public delegate T HyperbolicCosine(Angle<T> angle);
			public delegate T HyperbolicTangent(Angle<T> angle);
			public delegate T HyperbolicCosecant(Angle<T> angle);
			public delegate T HyperbolicSecant(Angle<T> angle);
			public delegate T HyperbolicCotangent(Angle<T> angle);
			public delegate Angle<T> InverseHyperbolicSine(T ratio);
			public delegate Angle<T> InverseHyperbolicCosine(T ratio);
			public delegate Angle<T> InverseHyperbolicTangent(T ratio);
			public delegate Angle<T> InverseHyperbolicCosecant(T ratio);
			public delegate Angle<T> InverseHyperbolicSecant(T ratio);
			public delegate Angle<T> InverseHyperbolicCotangent(T ratio);
		}

		#endregion

        #region Constants

        #region Pi
        /// <summary>The mathematical constant for pi. [3.14159265...]</summary>
		/// <remarks>The accuracy can be adjusted with the "pi_maximum_iterations" constant.</remarks>
		public static T Pi
		{
			get
			{
				if (!_pi_computed)
					_pi = ComputePi();
				return _pi;
			}
		}

		private static bool _pi_computed = false;
		private static T _pi;

		private static T ComputePi()
		{
			const int pi_maximum_iterations = 100; // NOTE: decimal accuracy for pi requires pi_maximum_iterations = 92

			// Series: PI = 2 * (1 + 1/3 * (1 + 2/5 * (1 + 3/7 * (...))))
			// more terms in computation inproves accuracy

			Compute<T>.Delegates.ComputePi computation =
					Meta.Compile<Compute<T>.Delegates.ComputePi>(
					string.Concat(
@"() =>
{
	int j = 1, max = 1;
	// the actual computation
	Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Delegates.ComputePi function = null;
	function = () =>
		{
			if (j > max)
				return 1;
			return (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")(1 + (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")j / (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")(2 * (j++) + 1) * function());
		};
	// continually compute with higher accuracy
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" pi = 1, previous = 0;
	for (int i = 1; previous != pi && i < ", pi_maximum_iterations, @"; i++)
	{
		previous = pi;
		j = 1;
		max = i;
		pi = ((", Meta.ConvertTypeToCsharpSource(typeof(T)), @")2) * function();
	}
	return (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")pi;
}"));

            _pi_computed = true;
            
            return computation();
		}
		#endregion

		#region FromInt32
		public static T FromInt32(int value)
		{
            if (FromInt32_private == null)
            {
                FromInt32_private = (int __value) =>
                {
                    // compile checks
                    if (!Meta.ValidateConvert<T>())
                        throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.IntCast; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks explicit casting from int operator.");
                    // shared expressions
                    ParameterExpression _value = Expression.Parameter(typeof(int));
                    LabelTarget _label = Expression.Label(typeof(T));
                    // code builder
                    ListLinked<Expression> expressions = new ListLinked<Expression>();
                    // null checks
                    if (!typeof(T).IsValueType) // is nullable?
                    {
                        expressions.Add(
                            Expression.IfThen(
                                Expression.Equal(_value, Expression.Constant(null, typeof(T))),
                                Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("value")))));
                    }
                    // code
                    expressions.Add(Expression.Return(_label, Expression.Convert(_value, typeof(T))));
                    expressions.Add(Expression.Label(_label, Expression.Constant(default(T))));
                    // compilation
                    Compute<T>.FromInt32_private = Expression.Lambda<Compute<T>.Delegates.FromInt32>(
                        Expression.Block(expressions.ToArray()),
                        _value).Compile();
                    // invocation
                    return Compute<T>.FromInt32_private(__value);
                };
            }

			return Compute<T>.FromInt32_private(value);
		}

		internal static Compute<T>.Delegates.FromInt32 FromInt32_private;

		#endregion

		#region Zero
        private static bool zero_computed = false;
        private static T zero_value;
		public static T Zero
        {
            get
            {
                if (!zero_computed)
                {
                    zero_value = Compute<T>.FromInt32(0);
                }
                return zero_value;
            }
        }
		#endregion

		#region One
        private static bool one_computed = false;
        private static T one_value;
        public static T One
        {
            get
            {
                if (!one_computed)
                {
                    one_value = Compute<T>.FromInt32(1);
                }
                return one_value;
            }
        }
        #endregion
        
        #region Epsilon
        /// <summary>The mathematical constant for pi. [3.14159265...]</summary>
        /// <remarks>The accuracy can be adjusted with the "pi_maximum_iterations" constant.</remarks>
        public static T Epsilon
        {
            get
            {
                if (!_epsilon_computed)
                    _epsilon = ComputeEpsilon();
                return _epsilon;
            }
        }

        private static bool _epsilon_computed = false;
        private static T _epsilon;

        private static T ComputeEpsilon()
        {
            #region Optimizations
            if (typeof(T) == typeof(float)) // float optimization
            {
                Compute<float>._epsilon = 1.192092896e-012f;
                Compute<float>._epsilon_computed = true;
                return Compute<T>._epsilon;
            }
            #endregion
            throw new System.NotImplementedException();
        }
        #endregion
        
        #endregion

        #region Properties

        #region IsInteger

        internal static bool IsInteger(T value)
        {
            return Equate(Modulus(value, FromInt32(1)), FromInt32(0));
        }

        #endregion

        #region IsNonNegative

        internal static bool IsNonNegative(T value)
        {
            return GreaterThanOrEqualTo(value, FromInt32(0));
        }

        #endregion

        #region IsPrime
        /// <summary>Computes (natrual log): [ ln(n) ].</summary>
        internal static Compute<T>.Delegates.IsPrime IsPrime_private = (T value) =>
        {
            Compute<T>.IsPrime_private =
    Meta.Compile<Compute<T>.Delegates.IsPrime>(
        string.Concat(
@"(", Meta.ConvertTypeToCsharpSource(typeof(T)), @" candidate) =>
{
	if (candidate % (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")1 == 0)
	{
		if (candidate == 2)
			return true;
		", Meta.ConvertTypeToCsharpSource(typeof(T)), @" squareRoot = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.SquareRoot(candidate);
		for (int divisor = 3; divisor <= squareRoot; divisor += 2)
			if ((candidate % divisor) == 0)
				return false;
		return true;
	}
	else
		return false;
}"));

            return Compute<T>.IsPrime_private(value);
        };

        public static bool IsPrime(T value)
        {
            return IsPrime_private(value);
        }
        #endregion
        
        #endregion

        #region Fundamental Operations

        #region AbsoluteValue
        /// <summary>Computes the absolute value of a value.</summary>
        internal static Compute<T>.Delegates.AbsoluteValue AbsoluteValue_private = (T value) =>
        {
            // compile checks
            if (!Meta.ValidateEqual<T>())
                throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.AbsoluteValue; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks equality (==) operator.");
            if (!Meta.ValidateLessThan<T>())
                throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.AbsoluteValue; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
            if (!Meta.ValidateNegate<T>())
                throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.AbsoluteValue; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks unary negation (-) operator.");
            if (!Meta.ValidateConvert<T>())
                throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.AbsoluteValue; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks explicit casting from int operator.");
            // compile the operation
            Compute<T>.AbsoluteValue_private = Meta.UnaryOperationHelper<Compute<T>.Delegates.AbsoluteValue, T, T>(
                (Expression _operand, LabelTarget _returnLabel) =>
                {
                    return Expression.IfThenElse(
                        Expression.LessThan(_operand, Expression.Constant(Compute<T>.FromInt32(0), typeof(T))),
                        Expression.Return(_returnLabel, Expression.Negate(_operand), typeof(T)),
                        Expression.Return(_returnLabel, _operand, typeof(T)));
                });
            // invoke (recursion)
            return Compute<T>.AbsoluteValue_private(value);

            #region Alternate Version
            //			Compute<T>.AbsoluteValueOLD =
            //				Meta.Compile<Compute<T>.Delegates.AbsoluteValue>(
            //					string.Concat(
            //"(", Type_String, @" _n) =>
            //{
            //	if (_n < (", Type_String, @")0)
            //		return -_n;
            //	else
            //		return _n;
            //}"));

            //			return Compute<T>.AbsoluteValueOLD(n);
            #endregion
        };

        public static T AbsoluteValue(T operand)
        {
            return Compute<T>.AbsoluteValue_private(operand);
        }
        #endregion

        #region Negate
        /// <summary>Negates a value.</summary>
		public static T Negate(T operand)
		{
			return Compute<T>.Negate_private(operand);
		}

		internal static Compute<T>.Delegates.Negate Negate_private = (T value) =>
		{
			// compile checks
			if (!Meta.ValidateNegate<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Negate; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks unary negation (-) operator.");
			// shared expressions
			ParameterExpression _value = Expression.Parameter(typeof(T));
			LabelTarget _label = Expression.Label(typeof(T));
			// code builder
			ListLinked<Expression> expressions = new ListLinked<Expression>();
			// null checks
			if (!typeof(T).IsValueType) // is nullable?
			{
				expressions.Add(
						Expression.IfThen(
								Expression.Equal(_value, Expression.Constant(null, typeof(T))),
								Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("value")))));
			}
			// code
			expressions.Add(Expression.Return(_label, Expression.Negate(_value), typeof(T)));
			expressions.Add(Expression.Label(_label, Expression.Constant(default(T))));
			// compilation
			Compute<T>.Negate_private = Expression.Lambda<Compute<T>.Delegates.Negate>(
					Expression.Block(expressions.ToArray()),
					_value).Compile();
			// invocation
			return Compute<T>.Negate_private(value);
		};
		#endregion

		#region Add
		internal static Compute<T>.Delegates.Add Add_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateAdd<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Add; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks addition (+) operator.");
			// compile the operation
			Compute<T>.Add_private = Meta.BinaryOperationHelper<Compute<T>.Delegates.Add, T, T, T>(
					(Expression _left, Expression _right, LabelTarget _returnLabel) =>
					{
						return Expression.Return(_returnLabel, Expression.Add(_left, _right), typeof(T));
					});
			// invoke (recursion)
			return Compute<T>.Add_private(left, right);
		};

		/// <summary>Adds two operands together.</summary>
		public static T Add(T operand_left, T operand_right)
		{
			return Compute<T>.Add_private(operand_left, operand_right);
		}

		/// <summary>Compuates the algebraic summation [ Σ (stepper) ].</summary>
		internal static Compute<T>.Delegates.Summation Summation_private = (Stepper<T> stepper) =>
		{
			if (!Meta.ValidateAdd<T>()) { throw new System.ArithmeticException(string.Concat("computation requires an addition operator: ", Meta.ConvertTypeToCsharpSource(typeof(T)), " +(", Meta.ConvertTypeToCsharpSource(typeof(T)), ", ", Meta.ConvertTypeToCsharpSource(typeof(T)), ")")); }

			Compute<T>.Summation_private =
				Meta.Compile<Compute<T>.Delegates.Summation>(
					string.Concat(
					"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> _stepper) => { ", Meta.ConvertTypeToCsharpSource(typeof(T)), " sum = 0; _stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), " i) => { sum += i; }); return sum; }"));

			return Compute<T>.Summation_private(stepper);
		};

		public static T Add(Stepper<T> stepper)
		{
			return Compute<T>.Summation_private(stepper);
		}

		public static T Add(System.Collections.Generic.IEnumerable<T> enumerable)
		{
			return Compute<T>.Summation_private(enumerable.Stepper());
		}

        public static T Add(params T[] values)
        {
            return Add(values as System.Collections.Generic.IEnumerable<T>);
        }
		#endregion

		#region Subtract
		internal static Compute<T>.Delegates.Subtract Subtract_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateSubtract<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Subtract; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks subtraction (-) operator.");
			// compile the operation
			Compute<T>.Subtract_private = Meta.BinaryOperationHelper<Compute<T>.Delegates.Subtract, T, T, T>(
					(Expression _left, Expression _right, LabelTarget _returnLabel) =>
					{
						return Expression.Return(_returnLabel, Expression.Subtract(_left, _right), typeof(T));
					});
			// invoke (recursion)
			return Compute<T>.Subtract_private(left, right);
		};

		/// <summary>Subtracts two operands.</summary>
		public static T Subtract(T operand_left, T operand_right)
		{
			return Compute<T>.Subtract_private(operand_left, operand_right);
		}
		#endregion

		#region Multiply
		internal static Compute<T>.Delegates.Multiply Multiply_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateMultiply<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Multiply; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks multiplication (*) operator.");
			// compile the operation
			Compute<T>.Multiply_private = Meta.BinaryOperationHelper<Compute<T>.Delegates.Multiply, T, T, T>(
					(Expression _left, Expression _right, LabelTarget _returnLabel) =>
					{
						return Expression.Return(_returnLabel, Expression.Multiply(_left, _right), typeof(T));
					});
			// invoke (recursion)
			return Compute<T>.Multiply_private(left, right);
		};

		/// <summary>Multiplies two operands together.</summary>
		public static T Multiply(T operand_left, T operand_right)
		{
			return Compute<T>.Multiply_private(operand_left, operand_right);
		}

		public static T Multiply(T operand_left, T operand_right, params T[] operands)
		{
			T result = Compute<T>.Multiply_private(operand_left, operand_right);
			for (int i = 0; i < operands.Length; i++)
				result = Compute<T>.Multiply_private(result, operands[i]);
			return result;
		}
		#endregion

		#region Divide
		/// <summary>Divides two operands.</summary>
		internal static Compute<T>.Delegates.Divide Divide_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateDivide<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Divide; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks division (/) operator.");
			// compile the operation
			Compute<T>.Divide_private = Meta.BinaryOperationHelper<Compute<T>.Delegates.Divide, T, T, T>(
				(Expression _left, Expression _right, LabelTarget _returnLabel) =>
				{
					return Expression.Return(_returnLabel, Expression.Divide(_left, _right), typeof(T));
				});
			// invoke (recursion)
			return Compute<T>.Divide_private(left, right);
		};

		public static T Divide(T operand_left, T operand_right)
		{
			return Compute<T>.Divide_private(operand_left, operand_right);
		}
		#endregion

        #region Invert
        /// <summary>Computes: [ 1 / n ].</summary>
        public static Compute<T>.Delegates.Invert Invert_private = (T value) =>
        {
            if (!Meta.ValidateDivide<T>()) { throw new System.ArithmeticException(string.Concat("computation requires a division operator: ", Meta.ConvertTypeToCsharpSource(typeof(T)), " /(", Meta.ConvertTypeToCsharpSource(typeof(T)), ", ", Meta.ConvertTypeToCsharpSource(typeof(T)), ")")); }

            Compute<T>.Invert_private =
        Meta.Compile<Compute<T>.Delegates.Invert>(
        string.Concat("(", Meta.ConvertTypeToCsharpSource(typeof(T)), " _value) => { return 1 / _value; }"));

            return Compute<T>.Invert_private(value);
        };

        public static T Invert(T value)
        {
            return Invert_private(value);
        }
        #endregion

		#region Modulus
		/// <summary>Modulus of two operands.</summary>
		internal static Compute<T>.Delegates.Modulus Modulus_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateModulo<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Modulus; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks modulus (%) operator.");
			// compile the operation
			Compute<T>.Modulus_private = Meta.BinaryOperationHelper<Compute<T>.Delegates.Modulus, T, T, T>(
				(Expression _left, Expression _right, LabelTarget _returnLabel) =>
				{
					return Expression.Return(_returnLabel, Expression.Modulo(_left, _right), typeof(T));
				});
			// invoke (recursion)
			return Compute<T>.Modulus_private(left, right);
		};

		public static T Modulus(T operand_left, T operand_right)
		{
			return Compute<T>.Modulus_private(operand_left, operand_right);
		}
		#endregion

		#region Power
		/// <summary>Takes one operand to the power of another.</summary>
		private static Compute<T>.Delegates.Power Power_private;

		public static T Power(T operand_left, T operand_right)
		{
            if (Power_private == null)
            {
                Power_private = (T left, T right) =>
                {
                    #region Optimizations
                    if (typeof(T) == typeof(short))
                    {
                        Compute<short>.Power_private = (short _left, short _right) => { return (short)System.Math.Pow(_left, _right); };
                        return Compute<T>.Power_private(left, right);
                    }
                    if (typeof(T) == typeof(ushort))
                    {
                        Compute<ushort>.Power_private = (ushort _left, ushort _right) => { return (ushort)System.Math.Pow(_left, _right); };
                        return Compute<T>.Power_private(left, right);
                    }
                    if (typeof(T) == typeof(int))
                    {
                        Compute<int>.Power_private = (int _left, int _right) =>
                        {
                            if (_right < 0)
                                return 0;
                            int result = 1;
                            while (_right > 0)
                            {
                                if ((_right & 1) > 0)
                                    result *= _left;
                                _right >>= 1;
                                _left *= _left;
                            }
                            return result;
                        };
                        return Compute<T>.Power_private(left, right);
                    }
                    if (typeof(T) == typeof(uint))
                    {
                        Compute<uint>.Power_private = (uint _left, uint _right) => { return (uint)System.Math.Pow(_left, _right); };
                        return Compute<T>.Power_private(left, right);
                    }
                    if (typeof(T) == typeof(long))
                    {
                        Compute<long>.Power_private = (long _left, long _right) => { return (long)System.Math.Pow(_left, _right); };
                        return Compute<T>.Power_private(left, right);
                    }
                    if (typeof(T) == typeof(ulong))
                    {
                        Compute<ulong>.Power_private = (ulong _left, ulong _right) => { return (ulong)System.Math.Pow(_left, _right); };
                        return Compute<T>.Power_private(left, right);
                    }
                    if (typeof(T) == typeof(float))
                    {
                        Compute<float>.Power_private = (float _left, float _right) => { return (float)System.Math.Pow(_left, _right); };
                        return Compute<T>.Power_private(left, right);
                    }
                    if (typeof(T) == typeof(double))
                    {
                        Compute<double>.Power_private = (double _left, double _right) => { return System.Math.Pow(_left, _right); };
                        return Compute<T>.Power_private(left, right);
                    }
                    #endregion

                    throw new System.NotImplementedException();

                    //#if math_debug
                    //			ConstantExpression operand_left = Expression.Constant(left, typeof(T));
                    //			ConstantExpression operand_right = Expression.Constant(right, typeof(T));
                    //#else
                    //			ParameterExpression operand_left = Expression.Parameter(typeof(T), "operand_left");
                    //			ParameterExpression operand_right = Expression.Parameter(typeof(T), "operand_right");
                    //#endif
                    //			ParameterExpression result = Expression.Variable(typeof(T), "result");
                    //			ParameterExpression i = Expression.Variable(typeof(T), "i");
                    //			LabelTarget label = Expression.Label(typeof(int));
                    //			Expression code = 
                    //				Expression.Loop(
                    //				);
                    //			Expression loop = Expression.Loop(
                    //					 Expression.IfThenElse(
                    //							 Expression.GreaterThan(value, Expression.Constant(1)),
                    //							 Expression.MultiplyAssign(result,
                    //									 Expression.PostDecrementAssign(value)),
                    //							 Expression.Break(label, result)
                    //					 ),
                    //				 label
                    //					);
                    //			Expression code = Expression.Power(operand_left, operand_right);
                    //#if math_debug
                    //			return Expression.Lambda<System.Func<T>>(code).Compile()();
                    //#else
                    //			Compute<T>.Power = Expression.Lambda<Compute<T>.Delegates.Power>(code, operand_left, operand_right).Compile();
                    //			return Compute<T>.Power(left, right);
                    //#endif
                };
            }
			return Compute<T>.Power_private(operand_left, operand_right);
		}
		#endregion

		#region SquareRoot
		/// <summary>Solves for "x": [ x ^ 2 = b ].</summary>
		internal static Compute<T>.Delegates.SquareRoot SquareRoot_private = (T value) =>
		{
			Compute<T>.SquareRoot_private = Meta.Compile<Compute<T>.Delegates.SquareRoot>(
				string.Concat("(", Meta.ConvertTypeToCsharpSource(typeof(T)), " _value) => { return (", Meta.ConvertTypeToCsharpSource(typeof(T)), ")System.Math.Sqrt((double)_value); }"));

			return Compute<T>.SquareRoot_private(value);
		};

		public static T SquareRoot(T operand)
		{
			return Compute<T>.SquareRoot_private(operand);
		}

		#endregion

		#region Root

		/// <summary>Solves for "x": [ x ^ r = b ].</summary>
		internal static Compute<T>.Delegates.Root Root_private = (T _base, T root) =>
		{
			Compute<T>.Root_private = Meta.Compile<Compute<T>.Delegates.Root>(
								string.Concat("(", Meta.ConvertTypeToCsharpSource(typeof(T)), " __base, ", Meta.ConvertTypeToCsharpSource(typeof(T)), " _root) => { return (", Meta.ConvertTypeToCsharpSource(typeof(T)), ")System.Math.Pow((double)__base, (1D / (double)_root)); }"));

			return Compute<T>.Root_private(_base, root);
		};

		public static T Root(T operand_base, T operand_root)
		{
			return Compute<T>.Root_private(operand_base, operand_root);
		}
		#endregion

		#region Logarithm
		/// <summary>Computes: [ log_b(n) ].</summary>
		public static Compute<T>.Delegates.Logarithm Logarithm_private = (T value, T _base) =>
		{
			Compute<T>.Logarithm_private =
				Meta.Compile<Compute<T>.Delegates.Logarithm>(
					string.Concat("(" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " _value, " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " __base) => { return (" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ")System.Math.Log((double)_value, (double)__base); }"));

			return Compute<T>.Logarithm_private(value, _base);
		};

		public static T Logarithm(T operand_value, T operand_base)
		{
			return Compute<T>.Logarithm_private(operand_value, operand_base);
		}
		#endregion

        #endregion

        #region Mathematic Logic

        #region Maximum

        /// <summary>Finds the max value in a set.</summary>
        internal static Compute<T>.Delegates.Maximum2 Maximum2_private = (T a, T b) =>
        {
            // compile checks
            if (!Meta.ValidateEqual<T>())
                throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Maximum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks equality (==) operator.");
            if (!Meta.ValidateLessThan<T>())
                throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Maximum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
            // compilation
            if (!typeof(T).IsValueType)
            {
                Compute<T>.Maximum2_private =
                (T _a, T _b) =>
                {
                    if (Compute<T>.LessThan(_a, _b))
                        return _b;
                    else
                        return _a;
                };
            }
            else
            {
                Compute<T>.Maximum2_private =
                (T _a, T _b) =>
                {
                    Code.AssertArgNonNull(_a, "a");
                    Code.AssertArgNonNull(_b, "b");

                    if (Compute<T>.LessThan(_a, _b))
                        return _b;
                    else
                        return _a;
                };
            }
            // invocation
            return Compute<T>.Maximum2_private(a, b);
        };

        /// <summary>Finds the max value in a set.</summary>
		internal static Compute<T>.Delegates.Maximum Maximum_private = (Stepper<T> stepper) =>
		{
			// compile checks
			if (!Meta.ValidateEqual<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Maximum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks equality (==) operator.");
			if (!Meta.ValidateLessThan<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Maximum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
			// compilation
			if (!typeof(T).IsValueType)
			{
				Compute<T>.Maximum_private =
				(Stepper<T> _stepper) =>
				{
					if (_stepper == null)
						throw new System.Exception();
					bool assigned = false;
					T max = default(T);
					_stepper((T step) =>
					{
						if (step == null)
							throw new System.ArgumentNullException("step");
						if (!assigned)
						{
							max = step;
							assigned = true;
						}
						else if (Compute<T>.LessThan(max, step))
							max = step;
					});
					return max;
				};
			}
			else
			{
				Compute<T>.Maximum_private =
				(Stepper<T> _stepper) =>
				{
					bool assigned = false;
					T max = default(T);
					_stepper((T step) =>
					{
						if (!assigned)
						{
							max = step;
							assigned = true;
						}
						else if (Compute<T>.LessThan(max, step))
							max = step;
					});
					return max;
				};
			}
			// invocation
			return Compute<T>.Maximum_private(stepper);

			#region Alternate Version
			//// compile checks
			//if (!Meta.ValidateOperator.Equality(typeof(T), typeof(T), typeof(T)))
			//	throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Maximum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks equality (==) operator.");
			//if (!Meta.ValidateOperator.LessThan(typeof(T), typeof(T), typeof(T)))
			//	throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Maximum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
			//// shared expressions
			//ParameterExpression _stepper = Expression.Parameter(typeof(Stepper<T>));
			//ParameterExpression assigned = Expression.Variable(typeof(bool));
			//ParameterExpression max = Expression.Variable(typeof(T));
			//ParameterExpression step = Expression.Parameter(typeof(T));
			//LabelTarget label = Expression.Label(typeof(T));
			//// code builder
			//List_Linked<Expression> expressions = new List_Linked<Expression>();
			//// null checks
			//expressions.Add(
			//	Expression.IfThen(
			//		Expression.Equal(_stepper, Expression.Constant(null, typeof(Stepper<T>))),
			//		Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("stepper")))));
			//// code
			//expressions.Add(
			//	Expression.Assign(assigned, Expression.Constant(false, typeof(bool))));
			//expressions.Add(
			//	Expression.Assign(max, Expression.Constant(default(T), typeof(T))));
			//List_Linked<Expression> inner_expressions = new List_Linked<Expression>();
			//if (!typeof(T).IsValueType) // is nullable?
			//{
			//	expressions.Add(
			//		Expression.IfThen(
			//			Expression.Equal(step, Expression.Constant(null, typeof(T))),
			//			Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("step")))));
			//}
			//inner_expressions.Add(
			//	Expression.IfThen(
			//		Expression.IsFalse(assigned),
			//		Expression.Block(
			//			Expression.Assign(max, step),
			//			Expression.Assign(assigned, Expression.Constant(true)))));
			//inner_expressions.Add(
			//	Expression.IfThen(
			//		Expression.LessThan(max, step),
			//		Expression.Assign(max, step)));
			//expressions.Add(
			//	Expression.Invoke(_stepper,
			//		Expression.Lambda<Step<T>>(
			//			Expression.Block(inner_expressions.ToArray()),
			//			step)));
			//expressions.Add(
			//	Expression.Return(label, max, typeof(T)));
			//expressions.Add(
			//	Expression.Label(label, Expression.Constant(default(T))));
			//// compilation
			//var lamb = Expression.Lambda<Compute<T>.Delegates.Maximum>(
			//	Expression.Block(new ParameterExpression[] { assigned, max }, expressions.ToArray()),
			//	_stepper);
			//Compute<T>.Maximum = lamb.Compile();
			//// invocation
			//return Compute<T>.Maximum(stepper);
			#endregion

			#region Alternate Version
			//			// compile checks
			//			if (!Meta.ValidateOperator.Equality(typeof(T), typeof(T), typeof(T)))
			//				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Maximum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks equality (==) operator.");
			//			if (!Meta.ValidateOperator.LessThan(typeof(T), typeof(T), typeof(T)))
			//				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Maximum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");

			//			if (!typeof(T).IsValueType)
			//			{
			//				Compute<T>.Maximum2 =
			//					Meta.Compile<Compute<T>.Delegates.Maximum>(
			//						string.Concat(
			//@"(Stepper<", Type_String, @"> _stepper) =>
			//{
			//	if (_stepper == null)
			//		throw new System.Exception();
			//	bool assigned = false;
			//	", Type_String, " max = default(", Type_String, @");
			//	_stepper((", Type_String, @" step) =>
			//	{
			//		if (step == null)
			//			throw new System.NullArgumentException(" + "\"step\"" + @");
			//		if (!assigned)
			//			max = step;
			//		else if (step > max)
			//			max = step;
			//	});
			//	return max;
			//}"));
			//			}
			//			else
			//			{
			//				Compute<T>.Maximum =
			//					Meta.Compile<Compute<T>.Delegates.Maximum>(
			//						string.Concat(
			//@"(Stepper<", Type_String, @"> _stepper) =>
			//{
			//	if (_stepper == null)
			//		throw new System.Exception();
			//	bool assigned = false;
			//	", Type_String, " max = default(", Type_String, @");
			//	_stepper((", Type_String, @" step) =>
			//	{
			//		if (!assigned)
			//			max = step;
			//		else if (step > max)
			//			max = step;
			//	});
			//	return max;
			//}"));
			//			}

			//			return Compute<T>.Maximum(stepper);
			#endregion
		};

		/// <summary>Finds the max value in a set.</summary>
		public static T Maximum(Stepper<T> stepper)
		{
			return Compute<T>.Maximum_private(stepper);
		}

		/// <summary>Finds the max value in a set.</summary>
		public static T Maximum(params T[] values)
		{
			return Compute<T>.Maximum(values.Stepper());
		}

        /// <summary>Finds the max value in a set.</summary>
        public static T Maximum(T a, T b)
        {
            return Compute<T>.Maximum2_private(a, b);
        }

		#endregion

		#region Minimum

        /// <summary>Finds the min value in a set.</summary>
        internal static Compute<T>.Delegates.Minimum2 Minimum2_private = (T a, T b) =>
        {
            // compile checks
            if (!Meta.ValidateEqual<T>())
                throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Minimum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks equality (==) operator.");
            if (!Meta.ValidateLessThan<T>())
                throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Minimum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
            // compilation
            if (!typeof(T).IsValueType)
            {
                Compute<T>.Minimum2_private =
                (T _a, T _b) =>
                {
                    if (Compute<T>.LessThan(_b, _a))
                        return _b;
                    else
                        return _a;
                };
            }
            else
            {
                Compute<T>.Minimum2_private =
                (T _a, T _b) =>
                {
                    Code.AssertArgNonNull(_a, "a");
                    Code.AssertArgNonNull(_b, "b");

                    if (Compute<T>.LessThan(_b, _a))
                        return _b;
                    else
                        return _a;
                };
            }
            // invocation
            return Compute<T>.Minimum2_private(a, b);
        };

		/// <summary>Finds the min value in a set.</summary>
		internal static Compute<T>.Delegates.Minimum Minimum_private = (Stepper<T> stepper) =>
		{
			// compile checks
			if (!Meta.ValidateEqual<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Minimum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks equality (==) operator.");
			if (!Meta.ValidateLessThan<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Minimum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
			// compilation
			if (!typeof(T).IsValueType)
			{
				Compute<T>.Minimum_private =
				(Stepper<T> _stepper) =>
				{
					if (_stepper == null)
						throw new System.Exception();
					bool assigned = false;
					T min = default(T);
					_stepper((T step) =>
					{
						if (step == null)
							throw new System.ArgumentNullException("step");
						if (!assigned)
						{
							min = step;
							assigned = true;
						}
						else if (Compute<T>.LessThan(step, min))
							min = step;
					});
					return min;
				};
			}
			else
			{
				Compute<T>.Minimum_private =
				(Stepper<T> _stepper) =>
				{
					bool assigned = false;
					T min = default(T);
					_stepper((T step) =>
					{
						if (!assigned)
						{
							min = step;
							assigned = true;
						}
						else if (Compute<T>.LessThan(step, min))
							min = step;
					});
					return min;
				};
			}
			// invocation
			return Compute<T>.Minimum_private(stepper);

			#region Alternate Version
			//// compile checks
			//if (!Meta.ValidateOperator.Equality(typeof(T), typeof(T), typeof(T)))
			//	throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Minimum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks equality (==) operator.");
			//if (!Meta.ValidateOperator.LessThan(typeof(T), typeof(T), typeof(T)))
			//	throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Minimum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
			//// shared expressions
			//ParameterExpression _stepper = Expression.Parameter(typeof(Stepper<T>));
			//ParameterExpression assigned = Expression.Variable(typeof(bool));
			//ParameterExpression min = Expression.Variable(typeof(T));
			//ParameterExpression step = Expression.Parameter(typeof(T));
			//LabelTarget label = Expression.Label(typeof(T));
			//// code builder
			//List_Linked<Expression> expressions = new List_Linked<Expression>();
			//// null checks
			//expressions.Add(
			//	Expression.IfThen(
			//		Expression.Equal(_stepper, Expression.Constant(null, typeof(Stepper<T>))),
			//		Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("stepper")))));
			//// code
			//expressions.Add(
			//	Expression.Assign(assigned, Expression.Constant(false, typeof(bool))));
			//expressions.Add(
			//	Expression.Assign(min, Expression.Constant(default(T), typeof(T))));
			//List_Linked<Expression> inner_expressions = new List_Linked<Expression>();
			//if (!typeof(T).IsValueType) // is nullable?
			//{
			//	expressions.Add(
			//		Expression.IfThen(
			//			Expression.Equal(step, Expression.Constant(null, typeof(T))),
			//			Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("step")))));
			//}
			//inner_expressions.Add(
			//	Expression.IfThen(
			//		Expression.Not(assigned),
			//		Expression.Block(
			//			Expression.Assign(min, step),
			//			Expression.Assign(assigned, Expression.Constant(true)))));
			//inner_expressions.Add(
			//	Expression.IfThen(
			//		Expression.LessThan(step, min),
			//		Expression.Assign(min, step)));
			//expressions.Add(
			//	Expression.Invoke(_stepper,
			//		Expression.Lambda<Step<T>>(
			//			Expression.Block(inner_expressions.ToArray()),
			//			step)));
			//expressions.Add(
			//	Expression.Return(label, min, typeof(T)));
			//expressions.Add(
			//	Expression.Label(label, Expression.Constant(default(T))));
			//// compilation
			//Compute<T>.Minimum = Expression.Lambda<Compute<T>.Delegates.Minimum>(
			//	Expression.Block(new ParameterExpression[] { assigned, min }, expressions.ToArray()),
			//	_stepper).Compile();
			//// invocation
			//return Compute<T>.Minimum(stepper);
			#endregion

			#region Alternate Version
			//			// compile checks
			//			if (!Meta.ValidateOperator.Equality(typeof(T), typeof(T), typeof(T)))
			//				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Minimum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks equality (==) operator.");
			//			if (!Meta.ValidateOperator.LessThan(typeof(T), typeof(T), typeof(T)))
			//				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Minimum; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");

			//			if (!typeof(T).IsValueType)
			//			{
			//				Compute<T>.Minimum =
			//					Meta.Compile<Compute<T>.Delegates.Minimum>(
			//						string.Concat(
			//@"(Stepper<", Type_String, @"> _stepper) =>
			//{
			//	if (_stepper == null)
			//		throw new System.Exception();
			//	bool assigned = false;
			//	", Type_String, " min = default(", Type_String, @");
			//	_stepper((", Type_String, @" step) =>
			//	{
			//		if (step == null)
			//			throw new System.NullArgumentException(" + "\"step\"" + @");
			//		if (!assigned)
			//			min = step;
			//		else if (min > step)
			//			min = step;
			//	});
			//	return min;
			//}"));
			//			}
			//			else
			//			{
			//				Compute<T>.Minimum =
			//					Meta.Compile<Compute<T>.Delegates.Minimum>(
			//						string.Concat(
			//@"(Stepper<", Type_String, @"> _stepper) =>
			//{
			//	if (_stepper == null)
			//		throw new System.Exception();
			//	bool assigned = false;
			//	", Type_String, " min = default(", Type_String, @");
			//	_stepper((", Type_String, @" step) =>
			//	{
			//		if (!assigned)
			//			min = step;
			//		else if (min > step)
			//			min = step;
			//	});
			//	return min;
			//}"));
			//			}

			//			return Compute<T>.Minimum(stepper);
			#endregion
		};

		/// <summary>Finds the min value in a set.</summary>
		public static T Minimum(Stepper<T> stepper)
		{
			return Compute<T>.Minimum_private(stepper);
		}

		/// <summary>Finds the min value in a set.</summary>
		public static T Minimum(params T[] values)
		{
			return Compute<T>.Minimum(values.Stepper());
		}

        /// <summary>Finds the min value in a set.</summary>
        public static T Minimum(T a, T b)
        {
            return Compute<T>.Minimum2_private(a, b);
        }

		#endregion

		#region Clamp
		/// <summary>Restricts a value to a min-max range.</summary>
		public static Compute<T>.Delegates.Clamp Clamp_private = (T value, T minimum, T maximum) =>
        {
        	// compile checks
        	if (!Meta.ValidateEqual<T>())
        		throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Clamp; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks equality (==) operator.");
        	if (!Meta.ValidateLessThan<T>())
        		throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Clamp; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
        	// shared expressions
        	ParameterExpression _value = Expression.Parameter(typeof(T));
        	ParameterExpression _minimum = Expression.Parameter(typeof(T));
        	ParameterExpression _maximum = Expression.Parameter(typeof(T));
        	LabelTarget _label = Expression.Label(typeof(T));
        	// code builder
        	ListLinked<Expression> expressions = new ListLinked<Expression>();
        	// null checks
        	if (!typeof(T).IsValueType) // is nullable?
        	{
        		expressions.Add(
        			Expression.IfThen(
        				Expression.Equal(_value, Expression.Constant(null, typeof(T))),
        				Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("value")))));
        		expressions.Add(
        			Expression.IfThen(
        				Expression.Equal(_minimum, Expression.Constant(null, typeof(T))),
        				Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("minimum")))));
        		expressions.Add(
        			Expression.IfThen(
        				Expression.Equal(_maximum, Expression.Constant(null, typeof(T))),
        				Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("maximum")))));
        	}
        	// argument checks
        	expressions.Add(
        		Expression.IfThen(
        			Expression.LessThan(_maximum, _minimum),
        			Expression.Throw(Expression.New(typeof(System.ArithmeticException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("Compute.Clamp: !(minimum < maximum)")))));
        	// code
        	expressions.Add(
        		Expression.IfThen(
        			Expression.LessThan(_value, _minimum),
        			Expression.Return(_label, _minimum, typeof(T))));
        	expressions.Add(
        		Expression.IfThen(
        			Expression.LessThan(_maximum, _value),
        			Expression.Return(_label, _maximum, typeof(T))));
        	expressions.Add(
        		Expression.Return(_label, _value, typeof(T)));
        	expressions.Add(
        		Expression.Label(_label, Expression.Constant(default(T), typeof(T))));
        	// compilation
        	Compute<T>.Clamp_private = Expression.Lambda<Compute<T>.Delegates.Clamp>(
        Expression.Block(expressions.ToArray()),
        _value, _minimum, _maximum).Compile();
	    // invocation
	    return Compute<T>.Clamp_private(value, minimum, maximum);
        };

		public static T Clamp(T operand_value, T operand_minimum, T operand_maximum)
		{
			return Compute<T>.Clamp_private(operand_value, operand_minimum, operand_maximum);
		}
		#endregion

		#region EqualsLeniency
		/// <summary>Checks for equality by value with a leniency.</summary>
		internal static Compute<T>.Delegates.EqualsLeniency EqualsLeniency_private = (T left, T right, T leniency) =>
		{
			// compile checks
			if (!Meta.ValidateEqual<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.EqualsLeniency; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (==) operator.");
			if (!Meta.ValidateGreaterThan<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.EqualsLeniency; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks greater than (>) operator.");
			if (!Meta.ValidateAdd<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.EqualsLeniency; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks addition (+) operator.");
			// shared expressions
			ParameterExpression _left = Expression.Parameter(typeof(T));
			ParameterExpression _right = Expression.Parameter(typeof(T));
			ParameterExpression _leniency = Expression.Parameter(typeof(T));
			LabelTarget _label = Expression.Label(typeof(bool));
			// code builder
			ListLinked<Expression> expressions = new ListLinked<Expression>();
			// null checks
			if (!typeof(T).IsValueType) // is nullable?
			{
				expressions.Add(
					Expression.IfThen(
						Expression.Equal(_left, Expression.Constant(null, typeof(T))),
						Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("left")))));
				expressions.Add(
					Expression.IfThen(
						Expression.Equal(_left, Expression.Constant(null, typeof(T))),
						Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("right")))));
				expressions.Add(
					Expression.IfThen(
						Expression.Equal(_leniency, Expression.Constant(null, typeof(T))),
						Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("leniency")))));
			}
			// code
			expressions.Add(
				Expression.IfThenElse(
					Expression.LessThan(_left, _right),
					Expression.Return(_label, Expression.GreaterThanOrEqual(Expression.Add(_left, _leniency), _right)),
					Expression.Return(_label, Expression.GreaterThanOrEqual(Expression.Add(_right, _leniency), _left))));
			expressions.Add(
				Expression.Label(_label, Expression.Constant(default(bool))));
			// compilation
			Compute<T>.EqualsLeniency_private = Expression.Lambda<Compute<T>.Delegates.EqualsLeniency>(
				Expression.Block(expressions.ToArray()),
				_left, _right, _leniency).Compile();
			// invocation
			return Compute<T>.EqualsLeniency_private(left, right, leniency);

			#region Alternate Version
			//			// compile checks
			//			if (!Meta.ValidateEqual<T>())
			//				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.EqualsLeniency; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (==) operator.");
			//			if (!Meta.ValidateLessThan<T>())
			//				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.EqualsLeniency; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
			//			if (!Meta.ValidateSubtract<T>())
			//				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.EqualsLeniency; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks subtraction (-) operator.");
			//			// compilation
			//			Compute<T>.EqualsLeniency2 =
			//				Meta.Compile<Compute<T>.Delegates.EqualsLeniency>(
			//					string.Concat(
			//@"(", Type_String, " _left, ", Type_String, " _right, ", Type_String, @" _leniency) =>
			//{
			//		if (_left < _right)
			//			return (_left + _leniency) > _right;
			//		else
			//			return (_right + _leniency) > _left;
			//}"));
			//			// invocation
			//			return Compute<T>.EqualsLeniency2(left, right, leniency);
			#endregion

			#region Alternate Version
			//// compile checks
			//if (!Meta.ValidateEqual<T>())
			//	throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.EqualsLeniency; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (==) operator.");
			//if (!Meta.ValidateLessThan<T>())
			//	throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.EqualsLeniency; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
			//if (!Meta.ValidateSubtract<T>())
			//	throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.EqualsLeniency; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks subtraction (-) operator.");
			//// compilation
			//Compute<T>.EqualsLeniency3 =
			//	(T _left, T _right, T _leniency) =>
			//	{
			//		if (Compute<T>.LessThan(_left, _right))
			//			return (Compute<T>.GreaterThan(Compute<T>.Add(_left, _leniency), _right));
			//		else
			//			return (Compute<T>.GreaterThan(Compute<T>.Add(_right, _leniency), _left));
			//	};
			//// invocation
			//return Compute<T>.EqualsLeniency3(left, right, leniency);
			#endregion
		};

		public static bool Equals(T left, T right, T leniency)
		{
			return EqualsLeniency_private(left, right, leniency);
		}
		#endregion

		#region Compare
		/// <summary>Compares two operands of </summary>
		internal static Compare<T> Compare_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateLessThan<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Compare; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
			// compile the operation
			Compute<T>.Compare_private = Meta.BinaryOperationHelper<Compare<T>, T, T, Comparison>(
				(Expression _left, Expression _right, LabelTarget _returnLabel) =>
				{
					return Expression.Block(
						Expression.IfThen(
							Expression.LessThan(_left, _right),
							Expression.Return(_returnLabel, Expression.Constant(Comparison.Less))),
						Expression.IfThen(
							Expression.GreaterThan(_left, _right),
							Expression.Return(_returnLabel, Expression.Constant(Comparison.Greater))),
						Expression.Return(_returnLabel, Expression.Constant(Comparison.Equal)));
				});
			// invoke (recursion)
			return Compute<T>.Compare_private(left, right);

			#region Alternate Version
			//			if (!Meta.ValidateLessThan<T>())
			//				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Compare; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
			//			Compute<T>.Compare2 =
			//				Meta.Compile<Compare<T>>(
			//					string.Concat(
			//	@"(", Type_String, " _left, ", Type_String, @" _right) =>
			//{
			//		if (_left < _right)
			//			return Comparison.Less;
			//		else if (_right < _left)
			//			return Comparison.Greater;
			//		else
			//			return Comparison.Equal;
			//}"));
			//			return Compute<T>.Compare2(left, right);
			#endregion

			#region Alternate Version
			//if (!Meta.ValidateLessThan<T>()) { throw new System.ArithmeticException(string.Concat("computation requires a less than operator: ", Type_String, " >(", Type_String, ", ", Type_String, ")")); }

			//Compute<T>.Compare3 =
			//	(T _left, T _right) =>
			//	{
			//		if (Compute<T>.LessThan(_left, _right))
			//			return Comparison.Less;
			//		else if (Compute<T>.LessThan(_right, _left))
			//			return Comparison.Greater;
			//		else
			//			return Comparison.Equal;
			//	};

			//return Compute<T>.Compare3(left, right);
			#endregion
		};

		public static Comparison Compare(T left, T right)
		{
			return Compare_private(left, right);
		}
		#endregion

		#region Equate
		internal static Equate<T> Equate_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateEqual<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Equate; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (==) operator.");
			// compile the operation
			Compute<T>.Equate_private = Meta.BinaryOperationHelper<Equate<T>, T, T, bool>(
				(Expression _left, Expression _right, LabelTarget _returnLabel) =>
				{
					return Expression.Return(_returnLabel, Expression.Equal(_left, _right));
				});
			// invoke (recursion)
			return Compute<T>.Equate_private(left, right);
		};

		public static bool Equate(T left, T right)
		{
			return Equate_private(left, right);
		}
		#endregion

		#region EquateNot
		internal static EquateNot<T> EquateNot_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateEqual<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Equate; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (==) operator.");
			// compile the operation
			Compute<T>.EquateNot_private = Meta.BinaryOperationHelper<EquateNot<T>, T, T, bool>(
					(Expression _left, Expression _right, LabelTarget _returnLabel) =>
					{
						return Expression.Return(_returnLabel, Expression.NotEqual(_left, _right));
					});
			// invoke (recursion)
			return Compute<T>.EquateNot_private(left, right);
		};

		public static bool EquateNot(T left, T right)
		{
			return EquateNot_private(left, right);
		}
		#endregion

		#region LessThan
		public static Compute<T>.Delegates.LessThan LessThan_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateLessThan<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.LessThan; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
			// compile the operation
			Compute<T>.LessThan_private = Meta.BinaryOperationHelper<Compute<T>.Delegates.LessThan, T, T, bool>(
	(Expression _left, Expression _right, LabelTarget _returnLabel) =>
	{
		return Expression.Return(_returnLabel, Expression.LessThan(_left, _right));
	});
			// invoke (recursion)
			return Compute<T>.LessThan_private(left, right);
		};

		public static bool LessThan(T left, T right)
		{
			return LessThan_private(left, right);
		}
		#endregion

		#region Greater
		internal static Compute<T>.Delegates.GreaterThan GreaterThan_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateLessThan<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.LessThan; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<) operator.");
			// compile the operation
			Compute<T>.GreaterThan_private = Meta.BinaryOperationHelper<Compute<T>.Delegates.GreaterThan, T, T, bool>(
				(Expression _left, Expression _right, LabelTarget _returnLabel) =>
				{
					return Expression.Return(_returnLabel, Expression.GreaterThan(_left, _right));
				});
			// invoke (recursion)
			return Compute<T>.GreaterThan_private(left, right);
		};

		public static bool GreaterThan(T left, T right)
		{
			return GreaterThan_private(left, right);
		}
		#endregion

		#region LessThanOrEqualTo
		public static Compute<T>.Delegates.LessThanOrEqualTo LessThanOrEqualTo_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateLessThan<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.LessThanOrEqualTo; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (<=) operator.");
			// compile the operation
			Compute<T>.LessThanOrEqualTo_private = Meta.BinaryOperationHelper<Compute<T>.Delegates.LessThanOrEqualTo, T, T, bool>(
					(Expression _left, Expression _right, LabelTarget _returnLabel) =>
					{
						return Expression.Return(_returnLabel, Expression.LessThanOrEqual(_left, _right));
					});
			// invoke (recursion)
			return Compute<T>.LessThanOrEqualTo_private(left, right);
		};

		public static bool LessThanOrEqualTo(T left, T right)
		{
			return LessThanOrEqualTo_private(left, right);
		}
		#endregion

		#region GreaterOrEqualTo
		internal static Compute<T>.Delegates.GreaterThanOrEqualTo GreaterThanOrEqualTo_private = (T left, T right) =>
		{
			// compile checks
			if (!Meta.ValidateLessThan<T>())
				throw new System.ArithmeticException("Cannot perform Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.LessThanOrEqualTo; " + Meta.ConvertTypeToCsharpSource(typeof(T)) + " lacks less than (>=) operator.");
			// compile the operation
			Compute<T>.GreaterThanOrEqualTo_private = Meta.BinaryOperationHelper<Compute<T>.Delegates.GreaterThanOrEqualTo, T, T, bool>(
					(Expression _left, Expression _right, LabelTarget _returnLabel) =>
					{
						return Expression.Return(_returnLabel, Expression.GreaterThanOrEqual(_left, _right));
					});
			// invoke (recursion)
			return Compute<T>.GreaterThanOrEqualTo_private(left, right);
		};

		public static bool GreaterThanOrEqualTo(T left, T right)
		{
			return GreaterThanOrEqualTo_private(left, right);
		}
		#endregion

		#region GreatestCommonFactor
		/// <summary>Computes (greatest common factor): [ GCF(set) ].</summary>
		internal static Compute<T>.Delegates.GreatestCommonFactor GreatestCommonFactor_private = (Stepper<T> stepper) =>
{
	Compute<T>.GreatestCommonFactor_private =
Meta.Compile<Compute<T>.Delegates.GreatestCommonFactor>(
string.Concat(
@"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _stepper) =>
{
	if (_stepper == null) { throw new System.ArgumentNullException(", "\"stepper\"", @"); }
	bool assigned = false;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), " gcf = (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")0;
	_stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" n) =>
	{
		if (n % (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")1 != 0)
			throw new System.Exception(", "\"Attempting to find the Greatest Common Factor of a non-integer value.\"", @");
		if (!assigned)
		{
			gcf = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.AbsoluteValue(n);
			assigned = true;
		}
		else
		{
			if (gcf > (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")1)
			{
				", Meta.ConvertTypeToCsharpSource(typeof(T)), @" a = gcf;
				", Meta.ConvertTypeToCsharpSource(typeof(T)), @" b = n;
				while (b != 0)
				{
					", Meta.ConvertTypeToCsharpSource(typeof(T)), @" remainder = a % b;
					a = b;
					b = remainder;
				}
				gcf = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.AbsoluteValue(a);
			}
		}
	});
	if (!assigned)
		throw new System.Exception(", "\"No parameters provided in GCF function.\"", @");
	return gcf;
}"));

	return Compute<T>.GreatestCommonFactor_private(stepper);
};

		public static T GreatestCommonFactor(Stepper<T> stepper)
		{
			return GreatestCommonFactor_private(stepper);
		}
		#endregion

		#region LeastCommonMultiple
		/// <summary>Computes (least common multiple): [ LCM(set) ].</summary>
		internal static Compute<T>.Delegates.LeastCommonMultiple LeastCommonMultiple_private = (Stepper<T> stepper) =>
		{
			Compute<T>.LeastCommonMultiple_private =
	Meta.Compile<Compute<T>.Delegates.LeastCommonMultiple>(
		string.Concat(
@"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _stepper) =>
{
	if (_stepper == null) { throw new System.Exception(", "\"Null reference: stepper\"", @"); }
	bool assigned = false;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), " lcm = (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")0;
	_stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" n) =>
	{
		if (n == 0)
		{
			lcm = 0;
			return;
		}
		if (n % (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")1 != 0)
			throw new System.Exception(", "\"Attempting to find the Greatest Common Factor of a non-integer value.\"", @");
		if (!assigned)
		{
			lcm = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.AbsoluteValue(n);
			assigned = true;
		}
		else
		{
			if (lcm > (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")1)
			{
				lcm = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">.AbsoluteValue((lcm / Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">.GreatestCommonFactor((Step<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> step) => { step(lcm); step(n); })) * n);
			}
		}
	});
	if (!assigned)
		throw new System.Exception(", "\"No parameters provided in LCM function.\"", @");
	return lcm;
}"));

			return Compute<T>.LeastCommonMultiple_private(stepper);
		};

		public static T LeastCommonMultiple(Stepper<T> stepper)
		{
			return LeastCommonMultiple_private(stepper);
		}
		#endregion

        #endregion

        #region Regression

        /// <summary>A stepper through individual points in a 2D data set.</summary>
        /// <param name="stepPoint">A step function for an individual point in the 2D data set.</param>
        public delegate void PointCollection2D(StepPoint2D stepPoint);
        /// <summary>A step function for a point in 2D space.</summary>
        /// <param name="x">The x coordinate of the point.</param>
        /// <param name="y">The y coordinate of the point.</param>
        public delegate void StepPoint2D(T x, T y);

        /// <summary>Performs simple 2D linear regression to find a best fit line in the form: "y = m * x + b".</summary>
        /// <typeparam name="T">The numeric type.</typeparam>
        /// <param name="points">The points to estimate a line out of.</param>
        /// <param name="slope">The resulting slope of the best fit line.</param>
        /// <param name="y_intercept">The resulting y axis intercept of the best fit line.</param>
        /// <exception cref="System.InvalidOperationException">Vertical lines are not supported. There must be >2 unique X valued points.</exception>
        public static void LinearRegression2D(PointCollection2D points, out T slope, out T y_intercept)
        {
            Code.AssertArgNonNull(points, "points");
            int count = 0;
            // first find the mean X value (so we can divide the data into two halfs)
            T mean_x = Theta.Mathematics.Compute<T>.Mean((Step<T> step) =>
            {
                points((T x, T y) =>
                {
                    step(x);
                    count++;
                });
            });
            if (count < 2)
                throw new System.InvalidOperationException("Attempting to perform 2D linear regression on an insufficient data set (<2 points provided).");
            // add up all the x & y values while keeping the data segregated 
            // based on the mean X value and keeping a running totals
            int count_1 = 0;
            int count_2 = 0;
            T x_sum_1 = Compute<T>.Zero; T y_sum_1 = Compute<T>.Zero; // represents point 1 of best fit line
            T x_sum_2 = Compute<T>.Zero; T y_sum_2 = Compute<T>.Zero; // represents point 2 of best fit line
            points((T x, T y) =>
            {
                if (Compute<T>.Compare(x, mean_x) == Comparison.Less)
                {
                    count_1++;
                    x_sum_1 = Compute<T>.Add(x_sum_1, x);
                    y_sum_1 = Compute<T>.Add(y_sum_1, y);
                }
                else
                {
                    count_2++;
                    x_sum_2 = Compute<T>.Add(x_sum_2, x);
                    y_sum_2 = Compute<T>.Add(y_sum_2, y);
                }
            });
            if (count_1 == 0 || count_2 == 0)
                throw new System.InvalidOperationException("Not enough unique X values for 2D linear regression (vertical lines are not supported).");
            // divide all summations by the count to calculate means
            T count_1_T = Compute<T>.FromInt32(count_1); // convert int to T
            T x_1 = Compute<T>.Divide(x_sum_1, count_1_T);
            T y_1 = Compute<T>.Divide(y_sum_1, count_1_T);
            T count_2_T = Compute<T>.FromInt32(count_2); // convert int to T
            T x_2 = Compute<T>.Divide(x_sum_2, count_2_T);
            T y_2 = Compute<T>.Divide(y_sum_2, count_2_T);
            // At this point we have two points on the best fit line: (x_1, y_1) & (x_2, y_2)
            // calculate the slope and the y-intercept
            slope = Compute<T>.Divide(Compute<T>.Subtract(y_2, y_1), Compute<T>.Subtract(x_2, x_1)); // m = (y2- y1) / (x2- x1)
            y_intercept = Compute<T>.Subtract(y_1, Compute<T>.Multiply(x_1, slope)); // b = y' - m * x' where (x', y') is a point on the line
        }

        /// <summary>Performs simple 2D linear regression to find a best fit line in the form: "y = m * x + b".</summary>
        /// <typeparam name="T">The numeric type.</typeparam>
        /// <param name="stepper">The vectors to make a line out of.</param>
        /// <param name="slope">The resulting slope of the best fit line.</param>
        /// <param name="y_intercept">The resulting y axis intercept of the best fit line.</param>
        /// <exception cref="System.InvalidOperationException">Vertical lines are not supported. There must be >2 unique X valued points.</exception>
        public static void LinearRegression2D(Stepper<Vector<T>> stepper, out T slope, out T y_intercept)
        {
            Code.AssertArgNonNull(stepper, "stepper");
            LinearRegression2D((StepPoint2D stepPoint) =>
            {
                stepper((Vector<T> vector) =>
                {
                    if (vector.Dimensions != 2)
                        throw new System.InvalidOperationException("Attempting to perform 2D linear regression on vectors that are not 2D.");
                    stepPoint(vector[0], vector[1]);
                });
            }, out slope, out y_intercept);
        }

        /// <summary>Performs simple 2D linear regression to find a best fit line in the form: "y = m * x + b".</summary>
        /// <typeparam name="T">The numeric type.</typeparam>
        /// <param name="x_values">The set of x values for the 2D data set.</param>
        /// <param name="y_values">The set of y values for the 2D data set.</param>
        /// <param name="slope">The resulting slope of the best fit line.</param>
        /// <param name="y_intercept">The resulting y axis intercept of the best fit line.</param>
        /// <exception cref="System.InvalidOperationException">Vertical lines are not supported. There must be >2 unique X valued points.</exception>
        public static void LinearRegression2D(T[] x_values, T[] y_values, out T slope, out T y_intercept)
        {
            Code.AssertArgNonNull(x_values, "x_values");
            Code.AssertArgNonNull(y_values, "y_values");
            if (x_values.Length != y_values.Length)
                throw new System.InvalidOperationException("Invalid 2D data set. (the number of x and y values are not equal)");
            LinearRegression2D((StepPoint2D stepPoint) =>
            {
                for (int i = 0; i < x_values.Length; i++)
                {
                    stepPoint(x_values[0], y_values[1]);
                }
            }, out slope, out y_intercept);
        }

        #endregion

        #region FactorPrimes
        /// <summary>Computes the prime factors of n.</summary>
		internal static Compute<T>.Delegates.FactorPrimes FactorPrimes_private = (T value, Step<T> step) =>
		{
			Compute<T>.FactorPrimes_private =
				Meta.Compile<Compute<T>.Delegates.FactorPrimes>(
					string.Concat(
@"(", Meta.ConvertTypeToCsharpSource(typeof(T)), " _value, Step<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _step) =>
{
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" __value = _value;
	if (__value % (", Meta.ConvertTypeToCsharpSource(typeof(T)), ")1 != (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")0)
		throw new System.Exception(", "\"Attempting to get the pime factors of a non integer\"", @");
	if (__value < 0)
	{
		__value = -__value;
		_step((", Meta.ConvertTypeToCsharpSource(typeof(T)), @")(-1));
	}
	while (__value % (", Meta.ConvertTypeToCsharpSource(typeof(T)), ")2 == (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")0)
	{
		_step((", Meta.ConvertTypeToCsharpSource(typeof(T)), @")2);
		__value /= (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")2;
	}
	for (", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i = 3; i <= Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">.SquareRoot(__value); i += (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")2)
	{
		while (__value % i == 0)
		{
			_step(i);
			__value = __value / i;
		}
	}
	if (__value > ((", Meta.ConvertTypeToCsharpSource(typeof(T)), @")2))
		_step(__value);
}"));

			Compute<T>.FactorPrimes_private(value, step);
		};

		public static void FactorPrimes(T value, Step<T> step)
		{
			FactorPrimes_private(value, step);
		}
		#endregion

		#region LinearInterpolation
		/// <summary>Interpolates in a linear fashion.</summary>
		internal static Compute<T>.Delegates.LinearInterpolation LinearInterpolation_private = (T x, T x0, T x1, T y0, T y1) =>
		{
			Compute<T>.LinearInterpolation_private =
				Meta.Compile<Compute<T>.Delegates.LinearInterpolation>(
					string.Concat(
@"(", Meta.ConvertTypeToCsharpSource(typeof(T)), " _x, ", Meta.ConvertTypeToCsharpSource(typeof(T)), " _x0, ", Meta.ConvertTypeToCsharpSource(typeof(T)), " _x1, ", Meta.ConvertTypeToCsharpSource(typeof(T)), " _y0, ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" _y1) =>
{
	if (_x0 > _x1)
		throw new System.Exception(", "\"invalid arguments: x0 > x1\"", @");
	else if (_x < _x0)
		throw new System.Exception(", "\"invalid arguments: x < x0\"", @");
	else if (_x > _x1)
		throw new System.Exception(", "\"invalid arguments: x > x1\"", @");
	else if (_x0 == _x1)
		if (_y0 != _y1)
			throw new System.Exception(", "\"invalid arguments: _x0 == _x1 && _y0 != _y1\"", @");
		else
			return _y0;
	else
		return _y0 + (_x - _x0) * (_y1 - _y0) / (_x1 - _x0);
}"));

			return Compute<T>.LinearInterpolation_private(x, x0, x1, y0, y1);
		};

		public static T LinearInterpolation(T x, T x0, T x1, T y0, T y1)
		{
			return LinearInterpolation_private(x, x0, x1, y0, y1);
		}
		#endregion

        #region Statistics

        #region Factorial
        /// <summary>Computes: [ N! ].</summary>
		public static Compute<T>.Delegates.Factorial Factorial_private = (T value) =>
		{
			Compute<T>.Factorial_private =
	Meta.Compile<Compute<T>.Delegates.Factorial>(
		string.Concat(
@"(", Meta.ConvertTypeToCsharpSource(typeof(T)), @" N) =>
{
	if (N % 1 != 0)
		throw new System.Exception(", "\"invalid factorial: N must be a whole number.\"", @");
	if (N < 0)
		throw new System.Exception(", "\"invalid factorial: [ N < 0 ] (N = \\\" + N + \\\").\"", @");
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" result = 1;
	for (; N > 1; N--)
		result *= N;
	return result;
}"));

			return Compute<T>.Factorial_private(value);
		};

		public static T Factorial(T value)
		{
			return Factorial_private(value);
		}
		#endregion

		#region Combinations
		/// <summary>Computes: [ N! / (n[0]! + n[1]! + n[3]! ...) ].</summary>
		public static Compute<T>.Delegates.Combinations Combinations_private = (T N, T[] n) =>
		{
			Compute<T>.Combinations_private =
	Meta.Compile<Compute<T>.Delegates.Combinations>(
		string.Concat(
@"(", Meta.ConvertTypeToCsharpSource(typeof(T)), " _N, ", Meta.ConvertTypeToCsharpSource(typeof(T)), @"[] _n) =>
{
	if (_N % 1 != 0)
		throw new System.Exception(", "\"invalid combination: N must be a whole number.\"", @");
	for (int i = 0; i < _n.Length; i++)
		if (_n[i] % 1 != 0)
			throw new System.Exception(", "\"invalid combination: n[\\\" + i + \\\"] must be a whole number.\"", @");
	", Meta.ConvertTypeToCsharpSource(typeof(T)), " result = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Factorial(_N);
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum = 0;
	for (int i = 0; i < _n.Length; i++)
	{
		result /= Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Factorial(_n[i]);
		sum += _n[i];
	}
	if (sum > _N)
		throw new System.Exception(", "\"invalid combination: [ N < Sum(n) ].\"", @");
	return result;
}"));

			return Compute<T>.Combinations_private(N, n);
		};

		public static T Combinations(T N, T[] n)
		{
			return Combinations_private(N, n);
		}
		#endregion

		#region Choose
		/// <summary>Computes: [ N! / (N - n)! ]</summary>
		public static Compute<T>.Delegates.Choose Choose_private = (T N, T n) =>
		{
			Compute<T>.Choose_private =
	Meta.Compile<Compute<T>.Delegates.Choose>(
		string.Concat(
@"(", Meta.ConvertTypeToCsharpSource(typeof(T)), " _N, ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" _n) =>
{
	if (_N % 1 != 0)
		throw new System.Exception(", "\"invalid chose: N must be a whole number.\"", @");
	if (_n % 1 != 0)
		throw new System.Exception(", "\"invalid combination: n must be a whole number.\"", @");
	if (!(_N <= _n || _N >= 0))
		throw new System.Exception(", "\"invalid choose: [ !(N <= n || N >= 0) ].\"", @");
	", Meta.ConvertTypeToCsharpSource(typeof(T)), " factorial_N = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Factorial(_N);
	return Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">.Factorial(_N) / (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">.Factorial(_n) * Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Factorial(_N - _n));
}"));

			return Compute<T>.Choose_private(N, n);
		};

		public static T Choose(T N, T n)
		{
			return Choose_private(N, n);
		}
		#endregion

		#region Mode
		/// <summary>Finds the number of occurences for each item and sorts them into a heap.</summary>
		internal static Compute<T>.Delegates.Mode Mode_private = (Stepper<T> stepper) =>
		{
			string heap_type = typeof(HeapArray<Link<T, int>>).ToCsharpSource();
			string link_type = typeof(Link<T, int>).ToCsharpSource();
			Compute<T>.Mode_private =
	Meta.Compile<Compute<T>.Delegates.Mode>(
		string.Concat(
@"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> stepper) =>
{
	", heap_type, @" heap =
		new ", heap_type, @"(
			(Link<", Meta.ConvertTypeToCsharpSource(typeof(T)), ", int> left, Link<", Meta.ConvertTypeToCsharpSource(typeof(T)), @", int> right) =>
			{
				return Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Compare(left._1, right._1);
			});
	stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" step) =>
	{
		bool contains = false;
		heap.Stepper((", link_type, @" nested_step) =>
		{
			if (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Equate(nested_step._1, step))
			{
				contains = true;
				nested_step._2++;
				heap.Requeue(nested_step);
				return StepStatus.Break;
			}
			else
				return StepStatus.Continue;
		});
		if (!contains)
			heap.Enqueue(new ", link_type, @"(step, 1));
	});
	return heap;
}"));

			return Compute<T>.Mode_private(stepper);
		};

		public static Heap<Link<T, int>> Mode(Stepper<T> stepper)
		{
			return Mode_private(stepper);
		}
		#endregion

		#region Mean
		/// <summary>Computes the mean (or average) between multiple values.</summary>
		internal static Compute<T>.Delegates.Mean Mean_private = (Stepper<T> stepper) =>
		{
			Compute<T>.Mean_private =
	Meta.Compile<Compute<T>.Delegates.Mean>(
		string.Concat(
@"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> stepper) =>
{
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i = 0;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum = 0;
	stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" step) => { i++; sum += step; });
	return sum / i;
}"));

			return Compute<T>.Mean_private(stepper);
		};

		public static T Mean(Stepper<T> stepper)
		{
			return Mean_private(stepper);
		}

		/// <summary>Computes the mean (or average) between multiple values.</summary>
		internal static Compute<T>.Delegates.Mean2 Mean2_private = (T a, T b) =>
		{
			Mean2_private = Meta.BinaryOperationHelper<Compute<T>.Delegates.Mean2, T, T, T>(
					(Expression left, Expression right, LabelTarget returnLabel) =>
					{
						return Expression.Return(returnLabel, Expression.Divide(Expression.Add(left, right), Expression.Constant(Compute<T>.FromInt32(2))), typeof(T));
					});

			return Compute<T>.Mean2_private(a, b);
		};

		public static T Mean(T a, T b)
		{
			return Mean2_private(a, b);
		}
		#endregion

		#region Median
		/// <summary>Computes the median of a set of values.</summary>
		internal static Compute<T>.Delegates.Median Median_private = (Stepper<T> stepper) =>
		{
			Compute<T>.Median_private =
				Meta.Compile<Compute<T>.Delegates.Median>(
					string.Concat(
@"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _stepper) =>
{
	long count =	0;
	_stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" step) => { count++; });
	long half = count / 2;
	if (count % 1 == 0)
	{
		", Meta.ConvertTypeToCsharpSource(typeof(T)), " left = default(", Meta.ConvertTypeToCsharpSource(typeof(T)), @");
		", Meta.ConvertTypeToCsharpSource(typeof(T)), " right = default(", Meta.ConvertTypeToCsharpSource(typeof(T)), @");
		count = 0;
		_stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" step) =>
		{
			if (count == half)
				left = step;
			else if (count == half + 1)
				right = step;
			count++;
		});
		return (left + right) / (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")2;
	}
	else
	{
		", Meta.ConvertTypeToCsharpSource(typeof(T)), " median = default(", Meta.ConvertTypeToCsharpSource(typeof(T)), @");
		_stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) =>
		{
			count = 0;
			_stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" step) =>
			{
				if (count == half)
					median = step;
				count++;
			});
		});
		return median;
	}
}"));

			return Compute<T>.Median_private(stepper);
		};

		public static T Median(Stepper<T> stepper)
		{
			return Median_private(stepper);
		}

		public static T Median(params T[] values)
		{
			return Median(values.Stepper());
		}
		#endregion

		#region GeometricMean
		/// <summary>Computes the median of a set of values.</summary>
		internal static Compute<T>.Delegates.GeometricMean GeometricMean_private = (Stepper<T> stepper) =>
		{
			Compute<T>.GeometricMean_private =
				Meta.Compile<Compute<T>.Delegates.GeometricMean>(
					string.Concat(
@"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _stepper) =>
{
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" multiple = 1;
	int count = 0;
	_stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" current) =>
	{
		count++;
		multiple *= current;
	});
	return Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">.Root(multiple, (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")count);
}"));

			return Compute<T>.GeometricMean_private(stepper);
		};

		public static T GeometricMean(Stepper<T> stepper)
		{
			return GeometricMean_private(stepper);
		}
		#endregion

		#region Variance
		/// <summary>Computes the variance of a set of values.</summary>
		internal static Compute<T>.Delegates.Variance Variance_private = (Stepper<T> stepper) =>
		{
			Compute<T>.Variance_private =
				Meta.Compile<Compute<T>.Delegates.Variance>(
					"(Stepper<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + "> _stepper) =>" +
					"{" +
 "	if (_stepper == null)" +
					"		throw new System.Exception(\"null reference: _stepper\");" +
 "	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " mean = Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Mean(_stepper);" +
					"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " variance = 0;" +
					"	int count = 0;" +
					"	_stepper((" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " i) =>" +
					"		{" +
					"			" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " i_minus_mean = i - mean;" +
					"			variance += i_minus_mean * i_minus_mean;" +
					"			count++;" +
					"		});" +
					"	return variance / (" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ")count;" +
					"}");

			return Compute<T>.Variance_private(stepper);
		};

		public static T Variance(Stepper<T> stepper)
		{
			return Variance_private(stepper);
		}
		#endregion

		#region StandardDeviation
		/// <summary>Computes the standard deviation of a set of values.</summary>
		internal static Compute<T>.Delegates.StandardDeviation StandardDeviation_private = (Stepper<T> stepper) =>
		{
			Compute<T>.StandardDeviation_private =
	Meta.Compile<Compute<T>.Delegates.StandardDeviation>(
		string.Concat(
@"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _stepper) =>
{",
@"	if (_stepper == null)
		throw new System.Exception(", "\"null reference: _stepper\");",
@"	return Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">.SquareRoot(Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Variance(_stepper));
}"));

			return Compute<T>.StandardDeviation_private(stepper);
		};

		public static T StandardDeviation(Stepper<T> stepper)
		{
			return StandardDeviation_private(stepper);
		}
		#endregion

		#region MeanDeviation
		/// <summary>Computes the mean deviation of a set of values.</summary>
		/// <see cref="Theta.Mathematics.Logic<T>.abs"/>
		/// <see cref="Theta.Mathematics.Compute<T>.Mean"/>
		public static Compute<T>.Delegates.MeanDeviation MeanDeviation_private = (Stepper<T> stepper) =>
		{
			Compute<T>.MeanDeviation_private =
	Meta.Compile<Compute<T>.Delegates.MeanDeviation>(
		string.Concat(
@"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _stepper) =>
{
	", Meta.ConvertTypeToCsharpSource(typeof(T)), " mean = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Mean(_stepper);
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" temp = 0;
	int count = 0;
	_stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) =>
	{
		temp += Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.AbsoluteValue(i - mean);
		count++;
	});
	return temp / (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")count;
}"));

			return Compute<T>.MeanDeviation_private(stepper);
		};

		public static T MeanDeviation(Stepper<T> stepper)
		{
			return MeanDeviation_private(stepper);
		}
		#endregion

		#region Range
		/// <summary>Computes the standard deviation of a set of values.</summary>
		internal static Compute<T>.Delegates.Range Range_private = (Stepper<T> stepper) =>
		{
			Compute<T>.Range_private =
				Meta.Compile<Compute<T>.Delegates.Range>(
					string.Concat(
@"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _stepper) =>
{
	bool set = false;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" temp_min = 0;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" temp_max = 0;
	_stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) =>
	{
		if (!set)
		{
			temp_min = i;
			temp_max = i;
			set = true;
		}
		else
		{
			temp_min = i < temp_min ? i : temp_min;
			temp_max = i > temp_max ? i : temp_max;
		}
	});
	return new Range<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">(temp_min, temp_max);
}"));

			return Compute<T>.Range_private(stepper);
		};

		public static Range<T> Range(Stepper<T> stepper)
		{
			return Range_private(stepper);
		}
		#endregion

		#region Quantiles
		/// <summary>Computes the quantiles of a set of values.</summary>
		internal static Compute<T>.Delegates.Quantiles Quantiles_private = (int quantiles, Stepper<T> stepper) =>
{
	Compute<T>.Quantiles_private =
Meta.Compile<Compute<T>.Delegates.Quantiles>(
string.Concat(
@"(int _quantiles, Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _stepper) =>
{",
@"	if (_stepper == null)
		throw new System.Exception(", "\"null reference: _stepper\"", @");
	if (_quantiles < 1)
		throw new System.Exception(", "\"invalid numer of dimensions on Quantile division\");",
@"	int count = 0;
	_stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { count++; });
	", Meta.ConvertTypeToCsharpSource(typeof(T)), "[] ordered = new ", Meta.ConvertTypeToCsharpSource(typeof(T)), @"[count];
	int a = 0;
	_stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { ordered[a++] = i; });
	Algorithms.Sort.Quick<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + @">(Logic.compare, ordered);
	", Meta.ConvertTypeToCsharpSource(typeof(T)), "[] __quantiles = new ", Meta.ConvertTypeToCsharpSource(typeof(T)), @"[_quantiles + 1];
	__quantiles[0] = ordered[0];
	__quantiles[__quantiles.Length - 1] = ordered[ordered.Length - 1];
	for (int i = 1; i < _quantiles; i++)
	{
		", Meta.ConvertTypeToCsharpSource(typeof(T)), " temp = (ordered.Length / (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")(_quantiles + 1)) * i;
		if (temp % 1 == 0)
			__quantiles[i] = ordered[(int)temp];
		else
			__quantiles[i] = (ordered[(int)temp] + ordered[(int)temp + 1]) / (", Meta.ConvertTypeToCsharpSource(typeof(T)), @")2;
	}
	return __quantiles;
}"));

	return Compute<T>.Quantiles_private(quantiles, stepper);
};

		public static T[] Quantiles(int quantiles, Stepper<T> stepper)
		{
			return Quantiles_private(quantiles, stepper);
		}
		#endregion

		#region Correlation
		/// <summary>Computes the median of a set of values.</summary>
		internal static Compute<T>.Delegates.Correlation Correlation_private = (Stepper<T> a, Stepper<T> b) =>
{
	throw new System.NotImplementedException("I introduced an error here when I removed the stepref off of structure. will fix soon - seven");

	Compute<T>.Correlation_private =
Meta.Compile<Compute<T>.Delegates.Correlation>(
string.Concat(
@"(Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> _a, Stepper<", Meta.ConvertTypeToCsharpSource(typeof(T)), @"> _b) =>
{
	", Meta.ConvertTypeToCsharpSource(typeof(T)), " a_mean = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Mean(_a);
	", Meta.ConvertTypeToCsharpSource(typeof(T)), " b_mean = Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Mean(_b);
	List<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> a_temp = new List_Linked<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">();
	_a((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { a_temp.Add(i - b_mean); });
	List<", Meta.ConvertTypeToCsharpSource(typeof(T)), "> b_temp = new List_Linked<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">();
	_b((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { b_temp.Add(i - a_mean); });
	", Meta.ConvertTypeToCsharpSource(typeof(T)), "[] a_cross_b = new ", Meta.ConvertTypeToCsharpSource(typeof(T)), @"[a_temp.Count * b_temp.Count];
	int count = 0;
	a_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i_a) =>
	{
		b_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i_b) =>
		{
			a_cross_b[count++] = i_a * i_b;
		});
	});
	a_temp.Stepper((ref ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { i *= i; });
	b_temp.Stepper((ref ", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { i *= i; });
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_a_cross_b = 0;
	foreach (", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i in a_cross_b)
		sum_a_cross_b += i;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_a_temp = 0;
	a_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { sum_a_temp += i; });
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" sum_b_temp = 0;
	b_temp.Stepper((", Meta.ConvertTypeToCsharpSource(typeof(T)), @" i) => { sum_b_temp += i; });
	return sum_a_cross_b / Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.sqrt(sum_a_temp * sum_b_temp);
}"));

	return Compute<T>.Correlation_private(a, b);
};

		public static T Correlation(Stepper<T> a, Stepper<T> b)
		{
			return Correlation_private(a, b);
		}
		#endregion

        #endregion

        #region Common Defined Functions

        #region Exponential
        /// <summary>Computes: [ e ^ x ].</summary>
        internal static Compute<T>.Delegates.Exponential Exponential_private = (T value) =>
        {
            Compute<T>.Exponential_private =
    Meta.Compile<Compute<T>.Delegates.Exponential>(
        string.Concat("(", Meta.ConvertTypeToCsharpSource(typeof(T)), " _value) => { return (", Meta.ConvertTypeToCsharpSource(typeof(T)), ")System.Math.Sqrt((double)_value); }"));

            return Compute<T>.Exponential_private(value);
        };

        public static T Exponential(T value)
        {
            return Exponential_private(value);
        }
        #endregion

        #region Natural Logarithm
        /// <summary>Computes (natrual log): [ ln(n) ].</summary>
        internal static Compute<T>.Delegates.NaturalLogarithm NaturalLogarithm_private = (T value) =>
        {
            Compute<T>.NaturalLogarithm_private =
    Meta.Compile<Compute<T>.Delegates.NaturalLogarithm>(
        string.Concat("(", Meta.ConvertTypeToCsharpSource(typeof(T)), " _value) => { throw new System.Exception(\"not yet implemented\"); }"));

            return Compute<T>.NaturalLogarithm_private(value);
        };

        public static T NaturalLogarithm(T value)
        {
            return NaturalLogarithm_private(value);
        }
        #endregion

        #region Trigonometric functions

        #region Sine
        /// <summary>Computes the ratio [length of the side opposite to the angle / hypotenuse] in a right triangle.</summary>
		internal static Compute<T>.Delegates.Sine Sine_private = (Angle<T> angle) =>
				{
					#region Optimizations
					if (typeof(T) == typeof(double)) // double optimization
					{
						Compute<double>.Sine_private = (Angle<double> _angle) => { return System.Math.Sin(_angle.Radians); };
                        return Compute<T>.Sine(angle);
                    }
					if (typeof(T) == typeof(float)) // float optimization
					{
						Compute<float>.Sine_private = (Angle<float> _angle) => { return (float)System.Math.Sin(_angle.Radians); };
                        return Compute<T>.Sine(angle);
                    }
					#endregion
					// Series: sin(x) = x - x^3/3! + x^5/5! - x^7/7! ...
					// more terms in computation inproves accuracy

					Compute<T>.Sine_private =
							Meta.Compile<Compute<T>.Delegates.Sine>(
									string.Concat(
@"(", Meta.ConvertTypeToCsharpSource(typeof(T)), @" _angle) =>
{
	// get the angle into the positive unit circle
	_angle = _angle % (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi * 2);
	if (_angle < 0)
		_angle = (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi * 2) + _angle;
	if (_angle <= Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi / 2)
		goto QuandrantSkip;
	else if (_angle <= Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi)
		_angle = (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">.Pi / 2) - (_angle % (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi / 2));
	else if (_angle <= (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi * 3) / 2)
		_angle = _angle % Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi;
	else
		_angle = (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">.Pi / 2) - (_angle % (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi / 2));
QuandrantSkip:
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" three_factorial = 6;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" five_factorial = 120;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" seven_factorial = 5040;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleCubed = _angle * _angle * _angle;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleToTheFifth = angleCubed * _angle * _angle;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleToTheSeventh = angleToTheFifth * _angle * _angle;
	return -(_angle
		- (angleCubed / three_factorial)
		+ (angleToTheFifth / five_factorial)
		- (angleToTheSeventh / seven_factorial));
}"));

					return Compute<T>.Sine(angle);
				};

		public static T Sine(Angle<T> angle)
		{
			return Sine_private(angle);
		}
		#endregion

		#region Cosine
		/// <summary>Computes the ratio [length of the side adjacent to the angle / hypotenuse] in a right triangle.</summary>
		internal static Compute<T>.Delegates.Cosine Cosine_private = (Angle<T> angle) =>
{
	// rather than computing cos, you could do a phase shift and use sin
	// return Sin(angle + (Pi / 2));

	if (typeof(T) == typeof(double)) // double optimization
		Compute<double>.Cosine_private = (Angle<double> _angle) => { return System.Math.Cos(_angle.Radians); };
	else if (typeof(T) == typeof(float)) // float optimization
		Compute<float>.Cosine_private = (Angle<float> _angle) => { return (float)System.Math.Cos(_angle.Radians); };
	else
	{
		// Series: cos(x) = 1 - x^2/2! + x^4/4! - x^6/6! ...
		// more terms in computation inproves accuracy

		Compute<T>.Cosine_private =
Meta.Compile<Compute<T>.Delegates.Cosine>(
string.Concat(
@"(", Meta.ConvertTypeToCsharpSource(typeof(T)), @" _angle) =>
{
	// get the angle into the positive unit circle
	_angle = _angle % (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi * 2);
	if (_angle < 0)
		_angle = (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi * 2) + _angle;
	if (_angle <= Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi / 2)
		goto QuandrantSkip;
	else if (_angle <= Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi)
		_angle = (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">.Pi / 2) - (_angle % (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi / 2));
	else if (_angle <= (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi * 3) / 2)
		_angle = _angle % Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi;
	else
		_angle = (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), ">.Pi / 2) - (_angle % (Compute<", Meta.ConvertTypeToCsharpSource(typeof(T)), @">.Pi / 2));
QuandrantSkip:
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" one = 1;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" two_factorial = 2;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" four_factorial = 24;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" six_factorial = 720;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleSquared = _angle * _angle;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleToTheFourth = angleSquared * _angle * _angle;
	", Meta.ConvertTypeToCsharpSource(typeof(T)), @" angleToTheSixth = angleToTheFourth * _angle * _angle;
	return one
		- (angleSquared / two_factorial)
		+ (angleToTheFourth / four_factorial)
		- (angleToTheSixth / six_factorial);
}"));
	}

	return Compute<T>.Cosine(angle);
};

		public static T Cosine(Angle<T> angle)
		{
			return Cosine_private(angle);
		}
		#endregion

		#region Tangent
		/// <summary>Computes the ratio [length of the side opposite to the angle / length of the side adjacent to the angle] in a right triangle.</summary>
		public static Compute<T>.Delegates.Tangent Tangent_private = (Angle<T> angle) =>
		{
			if (typeof(T) == typeof(double)) // double optimization
				Compute<double>.Tangent_private = (Angle<double> _angle) => { return System.Math.Tan(_angle.Radians); };
			else if (typeof(T) == typeof(float)) // float optimization
				Compute<float>.Tangent_private = (Angle<float> _angle) => { return (float)System.Math.Tan(_angle.Radians); };
			else
			{
				// Series: tan(x) = x + x^3/3 + 2x^5/15 + 17x^7/315 ...
				// more terms in computation inproves accuracy

				Compute<T>.Tangent_private =
	Meta.Compile<Compute<T>.Delegates.Tangent>(
		"(" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " _angle) =>" +
		"{" +
		"	// get the angle into the positive unit circle" +
		"	_angle = _angle % (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi * 2);" +
		"	if (_angle < 0)" +
		"		_angle = (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi * 2) + _angle;" +
		"	if (_angle <= Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi / 2) // quadrant 1" +
		"		goto QuandrantSkip;" +
		"	else if (_angle <= Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi) // quadrant 2" +
		"		_angle = (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi / 2) - (_angle % (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi / 2));" +
		"	else if (_angle <= (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi * 3) / 2) // quadrant 3" +
		"		_angle = _angle % Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi;" +
		"	else // quadrant 4" +
		"		_angle = (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi / 2) - (_angle % (Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Pi / 2));" +
		"QuandrantSkip:" +
		"	// do the computation" +
		"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " two = 2;" +
		"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " three = 3;" +
		"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " fifteen = 15;" +
		"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " seventeen = 17;" +
		"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " threehundredfifteen = 315;" +
		"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " angleCubed = _angle * _angle * _angle; // angle ^ 3" +
		"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " angleToTheFifth = angleCubed * _angle * _angle; // angle ^ 5" +
		"	" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " angleToTheSeventh = angleToTheFifth * _angle * _angle;  // angle ^ 7" +
		"	return angle" +
		"		+ (angleCubed / three)" +
		"		+ (two * angleToTheFifth / fifteen)" +
		"		+ (seventeen * angleToTheSeventh / threehundredfifteen);" +
		"}");
			}

			return Compute<T>.Tangent_private(angle);
		};

		public static T Tangent(Angle<T> angle)
		{
			return Tangent_private(angle);
		}
		#endregion

		#region Cosecant
		/// <summary>Computes the ratio [hypotenuse / length of the side opposite to the angle] in a right triangle.</summary>
		public static Compute<T>.Delegates.Cosecant Cosecant = (Angle<T> angle) =>
		{
			// Series: csc(x) = x^-1 + x/6 + 7x^3/360 + 31x^5/15120 ...
			// more terms in computation inproves accuracy

			Compute<T>.Cosecant =
				Meta.Compile<Compute<T>.Delegates.Cosecant>(
					"(" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " _angle) =>" +
					"{" +
					"	return (" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ")1 / Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Sine(_angle);" +
					"}");

			return Compute<T>.Cosecant(angle);
		};
		#endregion

		#region Secant
		/// <summary>Computes the ratio [hypotenuse / length of the side adjacent to the angle] in a right triangle.</summary>
		public static Compute<T>.Delegates.Secant Secant = (Angle<T> angle) =>
		{
			// Series: sec(x) = ...
			// more terms in computation inproves accuracy

			Compute<T>.Secant =
				Meta.Compile<Compute<T>.Delegates.Secant>(
					"(" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " _angle) =>" +
					"{" +
					"	return (" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ")1 / Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Cosine(_angle);" +
					"}");

			return Compute<T>.Secant(angle);
		};
		#endregion

		#region Cotangent
		/// <summary>Computes the ratio [length of the side adjacent to the angle / length of the side opposite to the angle] in a right triangle.</summary>
		public static Compute<T>.Delegates.Cotangent Cotangent = (Angle<T> angle) =>
		{
			// Series: cot(x) = ...
			// more terms in computation inproves accuracy

			Compute<T>.Cotangent =
				Meta.Compile<Compute<T>.Delegates.Cotangent>(
					"(" + Meta.ConvertTypeToCsharpSource(typeof(T)) + " _angle) =>" +
					"{" +
					"	return (" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ")1 / Compute<" + Meta.ConvertTypeToCsharpSource(typeof(T)) + ">.Tangent(_angle);" +
					"}");

			return Compute<T>.Cotangent(angle);
		};
		#endregion

		#region InverseSine
		public static Compute<T>.Delegates.InverseSine InverseSine = (T ratio) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.InverseSine = (double _ratio) => { return Angle<double>.Factory_Radians(System.Math.Asin(_ratio)); };
			else if (typeof(T) == typeof(float))
				Compute<float>.InverseSine = (float _ratio) => { return Angle<float>.Factory_Radians((float)System.Math.Asin(_ratio)); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.InverseSine(ratio);
		};
		#endregion

		#region InverseCosine
		public static Compute<T>.Delegates.InverseCosine InverseCosine = (T ratio) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.InverseCosine = (double _ratio) => { return Angle<double>.Factory_Radians(System.Math.Acos(_ratio)); };
			else if (typeof(T) == typeof(float))
				Compute<float>.InverseCosine = (float _ratio) => { return Angle<float>.Factory_Radians((float)System.Math.Acos(_ratio)); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.InverseCosine(ratio);
		};
		#endregion

		#region InverseTangent
		public static Compute<T>.Delegates.InverseTangent InverseTangent = (T ratio) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.InverseTangent = (double _ratio) => { return Angle<double>.Factory_Radians(System.Math.Atan(_ratio)); };
			else if (typeof(T) == typeof(float))
				Compute<float>.InverseTangent = (float _ratio) => { return Angle<float>.Factory_Radians((float)System.Math.Atan(_ratio)); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.InverseTangent(ratio);
		};
		#endregion

		#region InverseCosecant
		public static Compute<T>.Delegates.InverseCosecant InverseCosecant = (T ratio) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.InverseCosecant = (double _ratio) => { return Angle<double>.Factory_Radians(System.Math.Asin(1d / _ratio)); };
			else if (typeof(T) == typeof(float))
				Compute<float>.InverseCosecant = (float _ratio) => { return Angle<float>.Factory_Radians((float)System.Math.Asin(1f / _ratio)); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.InverseCosecant(ratio);
		};
		#endregion

		#region InverseSecant
		public static Compute<T>.Delegates.InverseSecant InverseSecant = (T ratio) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.InverseSecant = (double _ratio) => { return Angle<double>.Factory_Radians(System.Math.Acos(1d / _ratio)); };
			else if (typeof(T) == typeof(float))
				Compute<float>.InverseSecant = (float _ratio) => { return Angle<float>.Factory_Radians((float)System.Math.Acos(1f / _ratio)); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.InverseSecant(ratio);
		};
		#endregion

		#region InverseCotangent
		public static Compute<T>.Delegates.InverseCotangent InverseCotangent = (T ratio) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.InverseCotangent = (double _ratio) => { return Angle<double>.Factory_Radians(System.Math.Atan(1d / _ratio)); };
			else if (typeof(T) == typeof(float))
				Compute<float>.InverseCotangent = (float _ratio) => { return Angle<float>.Factory_Radians((float)System.Math.Atan(1f / _ratio)); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.InverseCotangent(ratio);
		};
		#endregion

		#region HyperbolicSine
		public static Compute<T>.Delegates.HyperbolicSine HyperbolicSine = (Angle<T> angle) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.HyperbolicSine = (Angle<double> _angle) => { return System.Math.Sinh(_angle.Radians); };
			else if (typeof(T) == typeof(float))
				Compute<float>.HyperbolicSine = (Angle<float> _angle) => { return (float)System.Math.Sinh(_angle.Radians); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.HyperbolicSine(angle);
		};
		#endregion

		#region HyperbolicCosine
		public static Compute<T>.Delegates.HyperbolicCosine HyperbolicCosine = (Angle<T> angle) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.HyperbolicCosine = (Angle<double> _angle) => { return System.Math.Cosh(_angle.Radians); };
			else if (typeof(T) == typeof(float))
				Compute<float>.HyperbolicCosine = (Angle<float> _angle) => { return (float)System.Math.Cosh(_angle.Radians); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.HyperbolicCosine(angle);
		};
		#endregion

		#region HyperbolicTangent
		public static Compute<T>.Delegates.HyperbolicTangent HyperbolicTangent = (Angle<T> angle) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.HyperbolicTangent = (Angle<double> _angle) => { return System.Math.Tanh(_angle.Radians); };
			else if (typeof(T) == typeof(float))
				Compute<float>.HyperbolicTangent = (Angle<float> _angle) => { return (float)System.Math.Tanh(_angle.Radians); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.HyperbolicTangent(angle);
		};
		#endregion

		#region HyperbolicSecant
		public static Compute<T>.Delegates.HyperbolicSecant HyperbolicSecant = (Angle<T> angle) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.HyperbolicSecant = (Angle<double> _angle) => { return 1d / System.Math.Cosh(_angle.Radians); };
			else if (typeof(T) == typeof(float))
				Compute<float>.HyperbolicSecant = (Angle<float> _angle) => { return 1f / (float)System.Math.Cosh(_angle.Radians); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.HyperbolicSecant(angle);
		};
		#endregion

		#region HyperbolicCosecant
		public static Compute<T>.Delegates.HyperbolicCosecant HyperbolicCosecant = (Angle<T> angle) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.HyperbolicCosecant = (Angle<double> _angle) => { return 1d / System.Math.Sinh(_angle.Radians); };
			else if (typeof(T) == typeof(float))
				Compute<float>.HyperbolicCosecant = (Angle<float> _angle) => { return 1f / (float)System.Math.Sinh(_angle.Radians); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.HyperbolicCosecant(angle);
		};
		#endregion

		#region HyperbolicCotangent
		public static Compute<T>.Delegates.HyperbolicCotangent HyperbolicCotangent = (Angle<T> angle) =>
		{
			if (typeof(T) == typeof(double))
				Compute<double>.HyperbolicCotangent = (Angle<double> _angle) => { return 1d / System.Math.Tanh(_angle.Radians); };
			else if (typeof(T) == typeof(float))
				Compute<float>.HyperbolicCotangent = (Angle<float> _angle) => { return 1f / (float)System.Math.Tanh(_angle.Radians); };
			else
				throw new System.NotImplementedException("unsupported parameter type for Cot function");

			return Compute<T>.HyperbolicCotangent(angle);
		};
		#endregion

		#region InverseHyperbolicSine
		public static Compute<T>.Delegates.InverseHyperbolicSine InverseHyperbolicSine = (T ratio) =>
		{
			throw new System.NotImplementedException();
		};
		#endregion

		#region InverseHyperbolicCosine
		public static Compute<T>.Delegates.InverseHyperbolicCosine InverseHyperbolicCosine = (T ratio) =>
		{
			throw new System.NotImplementedException();
		};
		#endregion

		#region InverseHyperbolicTangent
		public static Compute<T>.Delegates.InverseHyperbolicTangent InverseHyperbolicTangent = (T ratio) =>
		{
			throw new System.NotImplementedException();
		};
		#endregion

		#region InverseHyperbolicCosecant
		public static Compute<T>.Delegates.InverseHyperbolicCosecant InverseHyperbolicCosecant = (T ratio) =>
		{
			throw new System.NotImplementedException();
		};
		#endregion

		#region InverseHyperbolicSecant
		public static Compute<T>.Delegates.InverseHyperbolicSecant InverseHyperbolicSecant = (T ratio) =>
		{
			throw new System.NotImplementedException();
		};
		#endregion

		#region InverseHyperbolicCotangent
		public static Compute<T>.Delegates.InverseHyperbolicCotangent InverseHyperbolicCotangent = (T ratio) =>
		{
			throw new System.NotImplementedException();
		};
		#endregion

        #endregion

        #endregion
    }
}
