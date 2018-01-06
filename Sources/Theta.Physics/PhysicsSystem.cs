using Theta;
using Theta.Structures;
using Theta.Mathematics;

namespace Theta.Physics
{
    public class PhysicsSystem<T>
    {
        public static Vector<T> DefaultGravity { get { return new Vector<T>(Compute<T>.Zero, Compute<T>.Divide(Compute<T>.FromInt32(-981), Compute<T>.FromInt32(100)), Compute<T>.Zero); } }

        public Vector<T> _gravity;
        public OmnitreePoints<RigidPhysicsObject<T>, T, T, T> _rigidPhysicsObjects;

        public PhysicsSystem()
        {
            this._gravity = DefaultGravity;
            this._rigidPhysicsObjects = new OmnitreePointsLinked<RigidPhysicsObject<T>, T, T, T>(
                (RigidPhysicsObject<T> value, out T x, out T y, out T z) =>
                {
                    x = value.Shape.Position.X;
                    y = value.Shape.Position.Y;
                    z = value.Shape.Position.Z;
                });
        }

        public Vector<T> Gravity { get { return this._gravity; } set { this._gravity = value; } }

        public OmnitreePoints<RigidPhysicsObject<T>, T, T, T> PhysicsObjects
        {
            get
            {
                return this._rigidPhysicsObjects;
            }
        }

        public void AddBody(RigidPhysicsObject<T> rigidPhysicsObject)
        {
            Code.AssertArgNonNull(rigidPhysicsObject, "rigidPhysicsObject");
            this._rigidPhysicsObjects.Add(rigidPhysicsObject);
        }

        public void Run(float timeStep)
        {
            Code.Assert<System.ArgumentException>(timeStep >= 0, "The timestep can't be negative.");

            if (timeStep == 0.0f)
                return;


        }

        public void ClearObjects()
        {
            this._rigidPhysicsObjects.Clear();
        }
    }
}
