using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace MonoKnight
{
	public class Window : GameWindow
	{
		public Window() :
		base
		(
		1280, // initial width
		720, // initial height
		GraphicsMode.Default,
		"dreamstatecoding",  // initial title
		GameWindowFlags.Default,
		DisplayDevice.Default,
		4, // OpenGL major version
		0, // OpenGL minor version
		GraphicsContextFlags.ForwardCompatible)
		{
			Title += ": OpenGL Version: " + GL.GetString(StringName.Version);
		}

		protected override void OnLoad(EventArgs e)
		{
			CursorVisible = true;
			GL.GenBuffers(2, VBO);
			EBO = GL.GenBuffer();
			GL.GenVertexArrays(2, VAO);
			GL.BindVertexArray(VAO[0]);
				GL.BindBuffer(BufferTarget.ArrayBuffer, VBO[0]);
				GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertice.Length, vertice, BufferUsageHint.DynamicDraw);
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
				GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * indices.Length, indices, BufferUsageHint.DynamicDraw);
				GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
				GL.EnableVertexAttribArray(0);
			GL.BindVertexArray(0);
			//
			GL.BindVertexArray(VAO[1]);
				GL.BindBuffer(BufferTarget.ArrayBuffer, VBO[1]);
				GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * otherVertice.Length, otherVertice, BufferUsageHint.DynamicDraw);
				GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
				GL.EnableVertexAttribArray(0);
			GL.BindVertexArray(0);
			//
			shader = new Shader(@"Resources/sprite.vert", @"Resources/sprite.frag");
		}

		protected override void OnResize(EventArgs e)
		{
			Render();
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			Time.DeltaTime = e.Time;
			Render();
		}

		private int[] VBO = new int[2];
		private int[] VAO = new int[2];
		private int EBO = 0;
		private Shader shader = null;
		private Vector4 myColor = new Vector4(1.0f, 0.5f, 0.2f, 1.0f);
		private readonly float[] vertice = 
		{      
			0.5f, 0.5f, 0.0f, // Top Right
     		0.5f, -0.5f, 0.0f, // Bottom Right
   			-0.5f, 0.5f, 0.0f, // Top Left
    		-0.5f, -0.5f, 0.0f, // Bottom Left
		};
		private readonly float[] otherVertice =
		{
			0.8f, 0.5f, 0.0f, // Top Right
     		0.5f, -0.9f, 0.0f, // Bottom Right
   			-0.8f, 0.5f, 0.0f, // Top Left
    		//-0.5f, -0.5f, 0.0f, // Bottom Left
		};
		private readonly int[] indices =
		{
			0,1,2,
			1,2,3
		};
		private void Render()
		{
			Color4 backColor = new Color4(49.0f/255.0f, 77.0f/255.0f, 121.0f/255.0f, 1.0f);
			GL.ClearColor(backColor);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			int myColorUniform = shader.GetUniformLocation(@"myColor");
			GL.Uniform4(myColorUniform, myColor);

			shader.Use();

			GL.BindVertexArray(VAO[0]);
				GL.DrawElements(BeginMode.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
			GL.BindVertexArray(0);

			GL.BindVertexArray(VAO[1]);
				GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
			GL.BindVertexArray(0);

			SwapBuffers();
		}
	}
}
