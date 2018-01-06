using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theta.Mathematics;

namespace Theta.Physics
{
    public class Material<T>
    {
        private T _density = Compute<T>.One;
        private T _kineticFriction = Compute<T>.Divide(Compute<T>.FromInt32(3), Compute<T>.FromInt32(10));
        private T _staticFriction = Compute<T>.Divide(Compute<T>.FromInt32(6), Compute<T>.FromInt32(10));
        private T _restitution = Compute<T>.Zero;

        public Material(
            T density,
            T kineticFriction,
            T staticFriction,
            T restitution)
        {
            this._density = density;
            this._kineticFriction = kineticFriction;
            this._staticFriction = staticFriction;
            this._restitution = restitution;
        }

        public T Density { get { return _density; } set { _density = value; } }
        public T Restitution { get { return _restitution; } set { _restitution = value; } }
        public T StaticFriction { get { return _staticFriction; } set { _staticFriction = value; } }
        public T KineticFriction { get { return _kineticFriction; } set { _kineticFriction = value; } }
    }
}
