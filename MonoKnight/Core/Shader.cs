using System;
using OpenTK.Graphics.OpenGL;
namespace MonoKnight
{
	public class Shader
	{
		public Shader(string vertPath, string fragPath)
		{
			Vertex = GL.CreateShader(ShaderType.VertexShader);
			Fragment = GL.CreateShader(ShaderType.FragmentShader);
			string vertShaderSource = System.IO.File.ReadAllText(vertPath);
			string fragShaderSource = System.IO.File.ReadAllText(fragPath);
			GL.ShaderSource(Vertex, vertShaderSource);
			GL.ShaderSource(Fragment, fragShaderSource);
			GL.CompileShader(Vertex);
			GL.CompileShader(Fragment);
			Program = GL.CreateProgram();
			GL.AttachShader(Program, Vertex);
			GL.AttachShader(Program, Fragment);
			GL.LinkProgram(Program);
			Console.WriteLine(GL.GetProgramInfoLog(Program));
			//
			GL.DeleteShader(Vertex);
			GL.DeleteShader(Fragment);
		}

		public void Use()
		{
			GL.UseProgram(Program);
		}

		public int GetUniformLocation(string name)
		{
			return GL.GetUniformLocation(Program, name);
		}

		protected int Vertex = 0;
		protected int Fragment = 0;
		protected int Program = 0;
	}
}
