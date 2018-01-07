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
                test.Run(30.0, 0.0);
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
            
            Vector<float> camera_pos = new Vector<float>(0, 0, -10);
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
            Model model = Collada.Parse(xml_file_contents);
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

            float aspect_ratio = Width / (float)Height;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            var keyboard = OpenTK.Input.Keyboard.GetState();
            if (keyboard[OpenTK.Input.Key.Escape])
                this.Exit();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.ClearColor(System.Drawing.Color.DarkKhaki);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            Render.Model(animatedModelShader, loadedModel, camera, lightDirection);

            SwapBuffers();
        }
    }
}
