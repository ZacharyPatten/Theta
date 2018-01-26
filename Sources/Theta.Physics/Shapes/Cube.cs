using System;
using Theta.Mathematics;

namespace Theta.Physics.Shapes
{
    public class Cube<T> : XenoScan<T>
    {
        private Vector<T> _position;
        private Quaternion<T> _orientation;
        private T _halfLength;

        public Cube() : this(Compute<T>.One) { }

        public Cube(T halfLength) : this(halfLength, Vector<T>.FactoryZero(3), Quaternion<T>.Identity) { }

        public Cube(T halfLength, Vector<T> position, Quaternion<T> orientation)
        {
            Code.Assert<ArgumentException>(position.Dimensions == 3, "The position vector privided was not 3 dimensional.");

            this._halfLength = halfLength;
            this._position = position;
            this._orientation = orientation;
        }

        /// <summary>Gets the current corner vectors for the cube.</summary>
        public Vector<T>[] Corners
        {
            get
            {
                T x_neg = Compute<T>.Subtract(this._position.X, this._halfLength);
                T y_neg = Compute<T>.Subtract(this._position.Y, this._halfLength);
                T z_neg = Compute<T>.Subtract(this._position.Z, this._halfLength);
                T x_pos = Compute<T>.Add(this._position.X, this._halfLength);
                T y_pos = Compute<T>.Add(this._position.Y, this._halfLength);
                T z_pos = Compute<T>.Add(this._position.Z, this._halfLength);

                return new Vector<T>[]
                {
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_neg, y_neg, z_neg)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_neg, y_neg, z_pos)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_neg, y_pos, z_neg)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_neg, y_pos, z_pos)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_pos, y_neg, z_neg)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_pos, y_neg, z_pos)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_pos, y_pos, z_neg)),
                    Quaternion<T>.Rotate(this._orientation, new Vector<T>(x_pos, y_pos, z_pos)),
                };
            }
        }

        public T HalfLength { get { return this._halfLength; } }

        public Vector<T> Min { get { return GetMinimumVector(this.Corners); } }

        public Vector<T> Max { get { return GetMaximumVector(this.Corners); } }

        public Vector<T> Position
        {
            get { return this._position; }
        }

        public Quaternion<T> Orientation
        {
            get { return this._orientation; }
        }

        public Bounds<T> Bounds
        {
            get
            {
                Vector<T>[] corners = this.Corners;
                return new Bounds<T>(GetMinimumVector(corners), GetMaximumVector(corners));
            }
        }

        public Bounds<T> RoughBounds
        {
            get
            {
                // Optimization Notes:
                // If we don't care about being very accurate, we can just encapsulate
                // this cube in a bounding-cube that is 8x larger. No matter how we
                // rotate the original cube, the 8x cube will always contain it, and an
                // 8x cube is very fast to compute.
                
                T length = Compute<T>.Add(this._halfLength, this._halfLength);
                return new Bounds<T>(
                    // Minimum
                    new Vector<T>(
                        Compute<T>.Subtract(this._position.X, length),
                        Compute<T>.Subtract(this._position.Y, length),
                        Compute<T>.Subtract(this._position.Z, length)),
                    // Maximum
                    new Vector<T>(
                        Compute<T>.Add(this._position.X, length),
                        Compute<T>.Add(this._position.Y, length),
                        Compute<T>.Add(this._position.Z, length)));
            }
        }

        public T Volume
        {
            get
            {
                // volume of a cube = length ^ 3
                return Compute<T>.Power(Compute<T>.Multiply(this._halfLength, Compute<T>.FromInt32(2)), Compute<T>.FromInt32(3));
            }
        }

        private Vector<T> GetMinimumVector(Vector<T>[] corners)
        {
            return new Vector<T>(
                Compute<T>.Minimum(step => corners.Stepper()(vector => step(vector.X))),
                Compute<T>.Minimum(step => corners.Stepper()(vector => step(vector.Y))),
                Compute<T>.Minimum(step => corners.Stepper()(vector => step(vector.Z))));
        }

        private Vector<T> GetMaximumVector(Vector<T>[] corners)
        {
            return new Vector<T>(
                Compute<T>.Maximum(step => corners.Stepper()(vector => step(vector.X))),
                Compute<T>.Maximum(step => corners.Stepper()(vector => step(vector.Y))),
                Compute<T>.Maximum(step => corners.Stepper()(vector => step(vector.Z))));
        }

        public Vector<T> XenoScan(Vector<T> direction)
        {            
            return new Vector<T>(
                ZeroOrNegateCheck(direction.X),
                ZeroOrNegateCheck(direction.Y),
                ZeroOrNegateCheck(direction.Z));
        }

        private T ZeroOrNegateCheck(T value)
        {
            // Notes: helper function for "Cube.SupportMapping". just
            // adjusts the "_halfLength" based on the direction value

            if (Compute<T>.Equals(value, Compute<T>.Zero))
            {
                return Compute<T>.Zero;
            }
            else if (Compute<T>.LessThan(value, Compute<T>.Zero))
            {
                return Compute<T>.Negate(this._halfLength);
            }
            else
            {
                return this._halfLength;
            }
        }
    }
}
