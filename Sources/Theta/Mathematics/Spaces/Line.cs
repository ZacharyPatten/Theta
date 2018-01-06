using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theta.Mathematics.Spaces
{
    public class Line<T>
    {
        internal Vector<T> _a;
        internal Vector<T> _b;

        public Line(Vector<T> a, Vector<T> b)
        {
            this._a = new Vector<T>(a);
            this._b = new Vector<T>(b);
        }

        public Line(Line<T> line)
        {
            this._a = line._a;
            this._b = line._b;
        }

        public static bool operator ==(Line<T> a, Line<T> b)
        {
            return a._a == b._a && a._b == b._b;
        }

        public static bool operator !=(Line<T> a, Line<T> b)
        {
            return a._a != b._a || a._b != b._b;
        }

        public override bool Equals(object obj)
        {
            if (obj is Line<T>)
                return this == obj as Line<T>;
            return base.Equals(obj);
        }
    }
}
