using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Input;

namespace MonoKnight
{
	public class Window : GameWindow
	{
		public Window() :
		base
		(
		800, // initial width
		600, // initial height
		GraphicsMode.Default,
		"dreamstatecoding",  // initial title
		GameWindowFlags.Default,
		DisplayDevice.Default,
		4, // OpenGL major version
		0, // OpenGL minor version
		GraphicsContextFlags.ForwardCompatible)
		{
			Title += ": OpenGL Version: " + GL.GetString(StringName.Version);
			camera.transform.position = new Vector3(0.0f, 0.0f, -3.0f);
		}

		protected override void OnLoad(EventArgs e)
		{
            GL.Enable(EnableCap.Texture2D);
            CursorVisible = true;
			GL.GenBuffers(2, VBO);
			//EBO = GL.GenBuffer();
			GL.GenVertexArrays(2, VAO);
			GL.BindVertexArray(VAO[0]);
				GL.BindBuffer(BufferTarget.ArrayBuffer, VBO[0]);
				GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertice.Length, vertice, BufferUsageHint.DynamicDraw);
				GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
				GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(1);
            GL.BindVertexArray(0);
			//
			shader = new Shader(@"Resources/sprite.vert", @"Resources/sprite.frag");
            //
            texture = new Texture(@"Resources/wall.jpg");
        }

		protected override void OnResize(EventArgs e)
		{
            //GL.Viewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
			Render();
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			Time.DeltaTime = e.Time;
			Render();
		}

		private int[] VBO = new int[2];
		private int[] VAO = new int[2];
		//private int EBO = 0;
		private Shader shader = null;
        private Texture texture = null;
        private OpenTK.Matrix4 model = new Matrix4();
        private OpenTK.Matrix4 view = new Matrix4();
        private OpenTK.Matrix4 project = new Matrix4();
        //private float rotate = 0;
        private Camera camera = new Camera();
		private readonly float[] vertice = 
		{
         -1.0f, -1.0f, -1.0f,  0.0f, 0.0f,
         1.0f, -1.0f, -1.0f,  1.0f, 0.0f,
         1.0f,  1.0f, -1.0f,  1.0f, 1.0f,
         1.0f,  1.0f, -1.0f,  1.0f, 1.0f,
        -1.0f,  1.0f, -1.0f,  0.0f, 1.0f,
        -1.0f, -1.0f, -1.0f,  0.0f, 0.0f,

        -1.0f, -1.0f,  1.0f,  0.0f, 0.0f,
         1.0f, -1.0f,  1.0f,  1.0f, 0.0f,
         1.0f,  1.0f,  1.0f,  1.0f, 1.0f,
         1.0f,  1.0f,  1.0f,  1.0f, 1.0f,
        -1.0f,  1.0f,  1.0f,  0.0f, 1.0f,
        -1.0f, -1.0f,  1.0f,  0.0f, 0.0f,

        -1.0f,  1.0f,  1.0f,  1.0f, 0.0f,
        -1.0f,  1.0f, -1.0f,  1.0f, 1.0f,
        -1.0f, -1.0f, -1.0f,  0.0f, 1.0f,
        -1.0f, -1.0f, -1.0f,  0.0f, 1.0f,
        -1.0f, -1.0f,  1.0f,  0.0f, 0.0f,
        -1.0f,  1.0f,  1.0f,  1.0f, 0.0f,

         1.0f,  1.0f,  1.0f,  1.0f, 0.0f,
         1.0f,  1.0f, -1.0f,  1.0f, 1.0f,
         1.0f, -1.0f, -1.0f,  0.0f, 1.0f,
         1.0f, -1.0f, -1.0f,  0.0f, 1.0f,
         1.0f, -1.0f,  1.0f,  0.0f, 0.0f,
         1.0f,  1.0f,  1.0f,  1.0f, 0.0f,

        -1.0f, -1.0f, -1.0f,  0.0f, 1.0f,
         1.0f, -1.0f, -1.0f,  1.0f, 1.0f,
         1.0f, -1.0f,  1.0f,  1.0f, 0.0f,
         1.0f, -1.0f,  1.0f,  1.0f, 0.0f,
        -1.0f, -1.0f,  1.0f,  0.0f, 0.0f,
        -1.0f, -1.0f, -1.0f,  0.0f, 1.0f,

        -1.0f,  1.0f, -1.0f,  0.0f, 1.0f,
         1.0f,  1.0f, -1.0f,  1.0f, 1.0f,
         1.0f,  1.0f,  1.0f,  1.0f, 0.0f,
         1.0f,  1.0f,  1.0f,  1.0f, 0.0f,
        -1.0f,  1.0f,  1.0f,  0.0f, 0.0f,
        -1.0f,  1.0f, -1.0f,  0.0f, 1.0f
        };
		private float xpos = 0.0f;
		private void Render()
		{
			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Right))
			{
				camera.transform.position.X += 0.1f;
			}
			if(OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Left))
			{
				camera.transform.position.X -= 0.1f;
			}
			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Up))
			{
				camera.transform.position.Y += 0.1f;
			}
			if(OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Down))
			{
				camera.transform.position.Y -= 0.1f;
			}

			//camera.transform.LookAt(new Vector3(xpos, 0.0f, 0.0f));
			camera.transform.UpdateTransform();

            GL.Viewport(0, 0, Width, Height);
            //rotate += 0.03f;
            Matrix4.CreateTranslation(xpos, 0.0f, 0.0f, out model);
            //model = Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.3f, 0.5f), 20.0f)* model

			//view = Matrix4.LookAt(camera.transform.position, new Vector3(xpos, 0.0f, 0.0f), camera.transform.up);
			view = camera.ViewMatrix;
			//view = Matrix4.CreateFromQuaternion(camera.transform.rotation) * Matrix4.CreateTranslation(-1 * camera.transform.position);

            project = Matrix4.CreateOrthographic(Width/100.0f, Height/100.0f, 0.1f, 100.0f);
            //project = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(75.0f), Width / (float)Height, 0.1f, 80.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(new Color4(49.0f / 255.0f, 77.0f / 255.0f, 121.0f / 255.0f, 1.0f));
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            int myModelUniform = shader.GetUniformLocation(@"model");
            GL.UniformMatrix4(myModelUniform, false, ref model);
            int myViewUniform = shader.GetUniformLocation(@"view");
            GL.UniformMatrix4(myViewUniform, false, ref view);
            int myProjectUniform = shader.GetUniformLocation(@"project");
            GL.UniformMatrix4(myProjectUniform, false, ref project);
            texture.Bind();
            shader.Use();
            GL.BindVertexArray(VAO[0]);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
				//GL.DrawElements(BeginMode.Triangles, 36, DrawElementsType.UnsignedInt, 0);
			GL.BindVertexArray(0);

			SwapBuffers();
		}
	}
}
