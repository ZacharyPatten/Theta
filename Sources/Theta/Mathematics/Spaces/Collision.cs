using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theta.Mathematics.Spaces
{
    public static class Collision
    {
        private static bool GetIntersection<T>(T fDst1, T fDst2, Vector<T> min, Vector<T> max, out Vector<T> hit)
        {
            hit = null;
            if (Compute<T>.GreaterThanOrEqualTo(Compute<T>.Multiply(fDst1, fDst2), Compute<T>.FromInt32(0)))
                return false;
            if (Compute<T>.Equate(fDst1, fDst2))
                return false; 
            hit = min + (max - min) * Compute<T>.Divide(Compute<T>.Negate(fDst1), (Compute<T>.Subtract(fDst2, fDst1)));
            return true;
        }

        private static bool InBox<T>(Vector<T> hit, Vector<T> min, Vector<T> max, int axis)
        {
            if (min.Dimensions != max.Dimensions)
                throw new Exception();

            int dimensions = min.Dimensions;

            for (int i = 0; i < dimensions; i++)
            {
                if (axis == 1 && Compute<T>.GreaterThan(hit[2], min[2]) && Compute<T>.LessThan(hit[2], max[2]) && Compute<T>.GreaterThan(hit[1], min[1]) && Compute<T>.LessThan(hit[1], max[1]))
                    return true;
            }

            if ( axis == 1 && Compute<T>.GreaterThan(hit[2], min[2]) && Compute<T>.LessThan(hit[2], max[2]) && Compute<T>.GreaterThan(hit[1], min[1]) && Compute<T>.LessThan(hit[1], max[1]))
                return true;
            if ( axis == 2 && Compute<T>.GreaterThan(hit[2], min[2]) && Compute<T>.LessThan(hit[2], max[2]) && Compute<T>.GreaterThan(hit[0], min[0]) && Compute<T>.LessThan(hit[0], max[0]))
                return true;
            if ( axis == 3 && Compute<T>.GreaterThan(hit[0], min[0]) && Compute<T>.LessThan(hit[0], max[0]) && Compute<T>.GreaterThan(hit[1], min[1]) && Compute<T>.LessThan(hit[1], max[1]))
                return true;
            return false;
        }

        public static bool CheckLineBox<T>(Vector<T> min, Vector<T> max, Vector<T> a, Vector<T> b, out Vector<T> hit)
        {
            hit = null;
            int dimensions = min.Dimensions;
            for (int i = 0; i < dimensions; i++)
            {
                if (Compute<T>.LessThan(b[i], min[i]) && Compute<T>.LessThan(a[i], min[i]))
                    return false;
                if (Compute<T>.GreaterThan(b[i], max[i]) && Compute<T>.GreaterThan(a[i], max[i]))
                    return false;
            }

            bool temp = true;
            for (int i = 0; i < dimensions; i++)
            {
                if (!(Compute<T>.GreaterThan(a[i], min[i]) && Compute<T>.LessThan(a[i], max[i])))
                {
                    temp = false;
                    break;
                }
            }
            if (temp == true)
            {
                hit = a; 
                return true;
            }


            if ( (GetIntersection(Compute<T>.Subtract(a[0], min[0]), Compute<T>.Subtract(b[0], min[0]), a, b, out hit) && InBox(hit, min, max, 1))
              || (GetIntersection(Compute<T>.Subtract(a[1], min[1]), Compute<T>.Subtract(b[1], min[1]), a, b, out hit) && InBox(hit, min, max, 2)) 
              || (GetIntersection(Compute<T>.Subtract(a[2], min[2]), Compute<T>.Subtract(b[2], min[2]), a, b, out hit) && InBox(hit, min, max, 3)) 
              || (GetIntersection(Compute<T>.Subtract(a[0], max[0]), Compute<T>.Subtract(b[0], max[0]), a, b, out hit) && InBox(hit, min, max, 1)) 
              || (GetIntersection(Compute<T>.Subtract(a[1], max[1]), Compute<T>.Subtract(b[1], max[1]), a, b, out hit) && InBox(hit, min, max, 2)) 
              || (GetIntersection(Compute<T>.Subtract(a[2], max[2]), Compute<T>.Subtract(b[2], max[2]), a, b, out hit) && InBox(hit, min, max, 3)))
            	return true;
            
            return false;
        }
    }
}
