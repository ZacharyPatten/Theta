﻿using System;
using Theta.Mathematics;

namespace Theta.Physics.Shapes
{
    public class Capsule<T> : XenoScan<T>
    {
        private Vector<T> _position;
        private Quaternion<T> _orientation;
        private T _length;
        private T _radius;

        public Vector<T> Min
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Vector<T> Max
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public T Volume
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Vector<T> XenoScan(Vector<T> direction)
        {
            throw new NotImplementedException();
        }

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
            get { return new Bounds<T>(this.Min, this.Max); }
        }

        public Bounds<T> RoughBounds
        {
            get { return this.Bounds; }
        }
    }
}
