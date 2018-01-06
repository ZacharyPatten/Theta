#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Theta.Mathematics;
using Theta.Physics;
using Theta.Physics.Shapes;
//using Jitter.LinearMath;
//using Jitter.Collision;
//using Jitter;
//using Jitter.Dynamics;
//using Jitter.Collision.Shapes;
#endregion

namespace JitterOpenGLDemo
{
    public class Program : DrawStuffOtk, IDisposable
    {
        private PhysicsSystem<float> physicsSystem;
        private bool initFrame = true;

        private const string title = "Jitter OpenGL - Press 'Space' to shoot a sphere, 'R' to Reset";

        public Program()
            : base(800, 600)
        {
            physicsSystem = new PhysicsSystem<float>();
            physicsSystem.Gravity = new Vector<float>(0, 0, -10);

            dsSetSphereQuality(2);

            this.VSync = OpenTK.VSyncMode.Off;
            this.Title = title;

            Keyboard.KeyDown += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs>(Keyboard_KeyDown);

            BuildScene();
        }

        void Keyboard_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == OpenTK.Input.Key.Space) ShootSphere();
            if (e.Key == OpenTK.Input.Key.R) BuildScene();
        }

        private void BuildScene()
        {
            physicsSystem.ClearObjects();

            Material<float> material = new Material<float>(1, 0.3f, 0.6f, 0f);

            RigidPhysicsObject<float> ground = new RigidPhysicsObject<float>(new Cuboid<float>(10, 1, 20, new Vector<float>(0, 10, -.9f), Quaternion<float>.Identity), material, isStatic: true);
            physicsSystem.AddBody(ground);

            for (int i = 0; i < 20; i++)
            {
                for (int j = i; j < 20; j++)
                {
                    float x = 0.0f;
                    float y = (j - i * 0.5f) * 1.01f;
                    float z = 0.5f + i * 1.0f;

                    z = z + .1f;

                    Vector<float> position = new Vector<float>(x, y, z);

                    RigidPhysicsObject<float> box = new RigidPhysicsObject<float>(
                        new Cube<float>(.5f, position, Quaternion<float>.Identity),
                        material);

                    physicsSystem.AddBody(box);
                }
            }
        }

        private void ShootSphere()
        {
            Vector<float> pos, ang;
            dsGetViewPoint(out pos, out ang);

            Material<float> material = new Material<float>(3, 0.3f, 0.6f, 0f);

            RigidPhysicsObject<float> obj = new RigidPhysicsObject<float>(
                new Sphere<float>(.5f),
                material);

            obj.Velocity = new Vector<float>(
                (float)Math.Cos(ang.X / 180.0f * System.Math.PI),
                (float)Math.Sin(ang.X / 180.0f * System.Math.PI),
                (float)Math.Sin(ang.Y / 180.0f * System.Math.PI)) * 50f;

            physicsSystem.AddBody(obj);
        }

        protected override void OnBeginRender(double elapsedTime)
        {
            if (initFrame)
            {
                dsSetViewpoint(new float[] { 18, 10, 8 }, new float[] { 190, -10, 0 });
                initFrame = false;
            }

            RenderAll();

            base.OnBeginRender(elapsedTime);
        }

        private void RenderAll()
        {
            //dsSetTexture(DS_TEXTURE_NUMBER.DS_WOOD);

            physicsSystem.PhysicsObjects.Stepper(x =>
            {
                dsSetTexture(DS_TEXTURE_NUMBER.DS_WOOD);

                if (x.Shape is Cube<float>)
                {
                    Cube<float> cube = x.Shape as Cube<float>;

                    if (x.IsActive)
                        dsSetColor(1, 1, 1);
                    else
                        dsSetColor(0.5f, 0.5f, 1);

                    dsDrawBox(cube.Position, To3x4Matrix(cube.Orientation), new Vector<float>(cube.HalfLength * 2, cube.HalfLength * 2, cube.HalfLength * 2));
                }
                else if (x.Shape is Sphere<float>)
                {
                    Sphere<float> sphere = x.Shape as Sphere<float>;

                    if (x.IsActive)
                        dsSetColor(1, 1, 0);
                    else
                        dsSetColor(0.5f, 0.5f, 1);

                    dsDrawSphere(sphere.Position, To3x4Matrix(sphere.Orientation), sphere.Radius);
                }
                else if (x.Shape is Cuboid<float>)
                {
                    dsSetTexture(DS_TEXTURE_NUMBER.DS_GROUND);

                    Cuboid<float> cuboid = x.Shape as Cuboid<float>;

                    if (x.IsActive)
                        dsSetColor(.5, .5, .5);
                    else
                        dsSetColor(.5, .5, .5);

                    dsDrawBox(cuboid.Position, To3x4Matrix(cuboid.Orientation), new Vector<float>(cuboid.HalfLength * 2, cuboid.HalfHeight * 2, cuboid.HalfWidth * 2));
                }
            });
        }

        float accTime = 0.0f;

        protected override void OnUpdateFrame(OpenTK.FrameEventArgs e)
        {
            accTime += 1.0f / (float)RenderFrequency;

            if (accTime > 1.0f)
            {
                this.Title = title + " " + RenderFrequency.ToString("##.#") + " fps";
                accTime = 0.0f;
            }

            float step = 1.0f / (float)RenderFrequency;
            if (step > 1.0f / 100.0f) step = 1.0f / 100.0f;
            //physicsSystem.Step(step);

            base.OnUpdateFrame(e);
        }

        static void Main(string[] args)
        {
            using (Program p = new Program())
            {
                p.Run();
            }
        }

        public static Matrix<float> To3x4Matrix(Quaternion<float> quaternion)
        {
            Matrix<float> matrix = Quaternion<float>.ToMatrix3x3(quaternion);
            Matrix<float> zero_zero_one = new Matrix<float>(new float[,] { { 0 }, { 0 }, { 1 }, });
            return Matrix<float>.ConcatenateRowWise(matrix, zero_zero_one);

            //return new Matrix<float>(new float[,]
            //{
            //    { quaternion.W * quaternion.W + quaternion.X * quaternion.X - quaternion.Y * quaternion.Y - quaternion.Z * quaternion.Z, 2 * quaternion.X * quaternion.Y - 2 * quaternion.W * quaternion.Z, 2 * quaternion.X * quaternion.Z + 2 * quaternion.W * quaternion.Y, 0 },
            //    { 2 * quaternion.X * quaternion.Y + 2 * quaternion.W * quaternion.Z, quaternion.W * quaternion.W - quaternion.X * quaternion.X + quaternion.Y * quaternion.Y - quaternion.Z * quaternion.Z, 2 * quaternion.Y * quaternion.Z + 2 * quaternion.W * quaternion.X, 0 },
            //    { 2 * quaternion.X * quaternion.Z - 2 * quaternion.W * quaternion.Y, 2 * quaternion.Y * quaternion.Z - 2 * quaternion.W * quaternion.X, quaternion.W * quaternion.W - quaternion.X * quaternion.X - quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z, 1 },
            //});
        }
    }
}
