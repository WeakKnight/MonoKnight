using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
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
			VBO = GL.GenBuffer();
			VAO = GL.GenVertexArray();
			GL.BindVertexArray(VAO);
				GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
				GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertice.Length, vertice, BufferUsageHint.DynamicDraw);
				GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
				GL.EnableVertexAttribArray(0);
			GL.BindVertexArray(0);
			vertexShader = GL.CreateShader(ShaderType.VertexShader);
			fragShader = GL.CreateShader(ShaderType.FragmentShader);
			string vertShaderSource = System.IO.File.ReadAllText(@"sprite.vert");
			string fragShaderSource = System.IO.File.ReadAllText(@"sprite.frag");
			GL.ShaderSource(vertexShader, vertShaderSource);
			GL.ShaderSource(fragShader, fragShaderSource);
			GL.CompileShader(vertexShader);
			GL.CompileShader(fragShader);
			program = GL.CreateProgram();
			GL.AttachShader(program, vertexShader);
			GL.AttachShader(program, fragShader);
			GL.LinkProgram(program);
			GL.DeleteShader(vertexShader);
			GL.DeleteShader(fragShader);
		}

		protected override void OnResize(EventArgs e)
		{
			Render();
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			Render();
		}

		private int VBO = 0;
		private int VAO = 0;
		private int vertexShader = 0;
		private int fragShader = 0;
		private int program = 0;
		private float[] vertice = { -0.5f, -0.5f, 0.0f,
     								0.5f, -0.5f, 0.0f,
     								0.0f,  0.5f, 0.0f };
		private void Render()
		{
			Color4 backColor = new Color4(49.0f/255.0f, 77.0f/255.0f, 121.0f/255.0f, 1.0f);
			GL.ClearColor(backColor);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			GL.UseProgram(program);
			GL.BindVertexArray(VAO);
			GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
			GL.BindVertexArray(0);
			SwapBuffers();
		}
	}
}
