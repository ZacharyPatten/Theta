using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using Theta.Graphics;
using System.Reflection;
using System.IO;
using Theta.Graphics.Formats;
using Theta.Graphics.OpenGL;
using Theta.Mathematics;

namespace GraphicsWindow
{
    public class AnimationTest : GameWindow
    {
        [STAThread]
        public static void Main()
        {
            System.Windows.Forms.MessageBox.Show("NOTE! The GraphicsWindow demo is still in major development. It only opens a blank OpenGL window at the moment.");

            using (AnimationTest test = new AnimationTest())
            {
                test.Run();
            }
        }
        
        LoadedModel loadedModel;
        Camera camera;
        Vector<float> lightDirection;
        Render.AnimatedModelShader animatedModelShader;
        
        public AnimationTest() : base(800, 600) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Version version = new Version(GL.GetString(StringName.Version).Substring(0, 3));
            Version target = new Version(3, 0);
            if (version < target)
            {
                throw new NotSupportedException(String.Format("OpenGL {0} is required (you only have {1}).", target, version));
            }
            
            Vector<float> camera_pos = new Vector<float>(0, 0, -20);
            Vector<float> camera_forward = new Vector<float>(0, 0, 1);
            Vector<float> camera_up = new Vector<float>(0, 1, 0);

            camera = new Camera(camera_pos, camera_forward, camera_up);
            
            string xml_file_contents;
            var assembly = Assembly.GetExecutingAssembly();
            var modelFile = "GraphicsWindow.Resources.model.dae";
            using (Stream stream = assembly.GetManifestResourceStream(modelFile))
            using (StreamReader reader = new StreamReader(stream))
            {
                xml_file_contents = reader.ReadToEnd();
            }



            Collada_OLD.AnimatedModelData data = Collada_OLD.Collada.LoadModel(xml_file_contents, 3);
            Collada_OLD.AnimationData animdata = Collada_OLD.Collada.LoadAnimation(xml_file_contents);
            
            Model model = Collada.Parse(xml_file_contents);

            if (!model._positions.ValuesAreEqual(data.mesh.vertices))
                throw new Exception("positions");
            if (!model._normals.ValuesAreEqual(data.mesh.normals))
                throw new Exception("normals");
            if (!model._indices.ValuesAreEqual(data.mesh.indices))
                throw new Exception("indices");
            if (!model._textureCoordinates.ValuesAreEqual(data.mesh.textureCoords))
                throw new Exception("textureCoordinates");

            if (!Collada.JOINTIDSDATA.ValuesAreEqual(Collada_OLD.JOINTIDSDATA))
                throw new Exception("JOINTIDSDATA");
            if (!Collada.JOINTWEIGHTSDATA.ValuesAreEqual(Collada_OLD.JOINTWEIGHTSDATA))
                throw new Exception("JOINTIDSDATA");

            if (!model._jointWeights.ValuesAreEqual(data.mesh.vertexWeights))
                throw new Exception("jointWeights");
            if (!model._jointIds.ValuesAreEqual(data.mesh.jointIds))
                throw new Exception("jointIds");


            //if (!Collada.JOINTIDSDATA.ValuesAreEqual(Collada_OLD.JOINTIDSDATA))
            //    throw new Exception("JOINTIDSDATA");
            //if (!Collada.JOINTWEIGHTSDATA.ValuesAreEqual(Collada_OLD.JOINTWEIGHTSDATA))
            //    throw new Exception("JOINTIDSDATA");

            //model._jointIds = data.mesh.jointIds;
            //model._jointWeights = data.mesh.vertexWeights;

            var textureFile = "GraphicsWindow.Resources.diffuse.png";
            Bitmap texture = new Bitmap(assembly.GetManifestResourceStream(textureFile));
            model._texture = texture;

            loadedModel = Theta.Graphics.OpenGL.Load.Model(model);
            loadedModel.ActivateAnimation("base animation");

            animatedModelShader = new Render.AnimatedModelShader();

            lightDirection = new Vector<float>(0.2f, -0.3f, -0.8f);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            //float aspect_ratio = Width / (float)Height;
            //Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadMatrix(ref perpective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var keyboard = OpenTK.Input.Keyboard.GetState();
            if (keyboard[OpenTK.Input.Key.Escape])
                this.Exit();

            #region camera controls

            bool super_speed = false;
            if (keyboard[OpenTK.Input.Key.ShiftLeft] || keyboard[OpenTK.Input.Key.ShiftRight])
                super_speed = true;

            float speed = 5f;

            // Camera position movement
            if (keyboard[OpenTK.Input.Key.Q])
                if (super_speed)
                    camera.Move(camera.Down, speed * 100);
                else
                    camera.Move(camera.Down, speed);

            if (keyboard[OpenTK.Input.Key.E])
                if (super_speed)
                    camera.Move(camera.Up, speed * 100);
                else
                    camera.Move(camera.Up, speed);

            if (keyboard[OpenTK.Input.Key.A])
                if (super_speed)
                    camera.Move(camera.Left, speed * 100);
                else
                    camera.Move(camera.Left, speed);

            if (keyboard[OpenTK.Input.Key.W])
                if (super_speed)
                    camera.Move(camera.Forward, speed * 100);
                else
                    camera.Move(camera.Forward, speed);

            if (keyboard[OpenTK.Input.Key.S])
                if (super_speed)
                    camera.Move(camera.Backward, speed * 100);
                else
                    camera.Move(camera.Backward, speed);

            if (keyboard[OpenTK.Input.Key.D])
                if (super_speed)
                    camera.Move(camera.Right, speed * 100);
                else
                    camera.Move(camera.Right, speed);

            // Camera look angle adjustment
            if (keyboard[OpenTK.Input.Key.K])
                camera.RotateX(.01f);
            if (keyboard[OpenTK.Input.Key.I])
                camera.RotateX(-.01f);
            if (keyboard[OpenTK.Input.Key.J])
                camera.RotateY(.01f);
            if (keyboard[OpenTK.Input.Key.L])
                camera.RotateY(-.01f);

            #endregion
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            //GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadIdentity();
            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadIdentity();


            GL.ClearColor(System.Drawing.Color.DarkKhaki);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            Render.Model(animatedModelShader, loadedModel, camera, lightDirection);

            SwapBuffers();
        }
    }
}
