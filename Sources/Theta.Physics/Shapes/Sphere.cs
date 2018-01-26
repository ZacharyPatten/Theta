using System;
using Theta.Mathematics;

namespace Theta.Physics.Shapes
{
    public class Sphere<T> : XenoScan<T>
    {
        private Vector<T> _position;
        private Quaternion<T> _orientation;
        private T _radius;

        public Sphere() : base()
        {
            this._radius = Compute<T>.One;
        }

        public Sphere(T radius)
        {
            this._position = Vector<T>.FactoryZero(3);
            this._orientation = Quaternion<T>.Identity;
            this._radius = radius;
        }

        public Sphere(Vector<T> position, Quaternion<T> orientation, T radius)
        {
            Code.Assert<ArgumentException>(position.Dimensions == 3, "The position vector privided was not 3 dimensional.");

            this._position = position;
            this._orientation = orientation;
            this._radius = radius;
        }

        public Vector<T> Min
        {
            get
            {
                return new Vector<T>(
                    Compute<T>.Subtract(this._position.X, this._radius),
                    Compute<T>.Subtract(this._position.Y, this._radius),
                    Compute<T>.Subtract(this._position.Z, this._radius));
            }
        }

        public Vector<T> Max
        {
            get
            {
                return new Vector<T>(
                    Compute<T>.Add(this._position.X, this._radius),
                    Compute<T>.Add(this._position.Y, this._radius),
                    Compute<T>.Add(this._position.Z, this._radius));
            }
        }

        public T Radius
        {
            get { return this._radius; }
        }

        public Vector<T> Position
        {
            get { return this._position; }
        }

        public Quaternion<T> Orientation
        {
            get { return this._orientation; }
        }

        public T Volume
        {
            get
            {
                // volume of a sphere = (4/3)pi * radius ^ 3
                T radiusCubed = Compute<T>.Power(this._radius, Compute<T>.FromInt32(3));
                return Compute<T>.Multiply(Sphere<T>.FourThirdsPi, radiusCubed);
            }
        }

        public Bounds<T> Bounds
        {
            get { return new Bounds<T>(this.Min, this.Max); }
        }

        public Bounds<T> RoughBounds
        {
            get { return this.Bounds; }
        }

        private static bool _fourThirdsPiComputed = false;
        private static T _fourThirdsPi;
        private static T FourThirdsPi
        {
            get
            {
                if (_fourThirdsPiComputed)
                    return _fourThirdsPi;
                T fourThirds = Compute<T>.Divide(Compute<T>.FromInt32(4), Compute<T>.FromInt32(3));
                Sphere<T>._fourThirdsPi = Compute<T>.Multiply(fourThirds, Compute<T>.Pi);
                Sphere<T>._fourThirdsPiComputed = true;
                return Sphere<T>.FourThirdsPi;
            }
        }

        public Vector<T> XenoScan(Vector<T> direction)
        {
            return direction.Normalize() * _radius;
        }
    }
}
