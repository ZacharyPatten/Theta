using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theta.Mathematics.Spaces
{
    public class Rectangle<T>
    {
        internal Vector<T> _a;
        internal Vector<T> _b;

        public Vector<T> A { get { return this._a; } }
        public Vector<T> B { get { return this._b; } }

        public Rectangle(Vector<T> a, Vector<T> b)
        {
            this._a = a;
        }
    }
}
