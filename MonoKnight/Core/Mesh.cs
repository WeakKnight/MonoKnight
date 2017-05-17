using System;
using OpenTK;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace MonoKnight
{
	
	public struct Vertex
	{
		public Vector3 position;
		public Vector3 normal;
		public Vector2 texCoord;
	}

	public struct TextureInfo
	{
		public Texture texture;
		public string type;	
	}
	public class Mesh
	{
		public Mesh(List<Vertex> vertices, List<int> indices, List<Texture> textures)
		{
			_vertices = vertices;
			_indices = indices;
			_textures = textures;
		}

		public List<Vertex> _vertices;
		public List<int> _indices;
		public List<Texture> _textures;

		private float[] Vertices = null;
		private int[] Indices = null;
		public Shader shader = new Shader(@"Resources/mesh.vert",@"Resources/mesh.frag");
		private int VAO = 0;
		private int VBO = 0;
		private int EBO = 0;
		private Texture defaultTex = null;

		public void Init()
		{
			
			int length = 8;
			Vertices = new float[_vertices.Count * length];

			for (int i = 0; i<_vertices.Count; i++) 
			{
				Vertices[i * length + 0] = _vertices[i].position.X;
				Vertices[i * length + 1] = _vertices[i].position.Y;
				Vertices[i * length + 2] = _vertices[i].position.Z;
				Vertices[i * length + 3] = _vertices[i].normal.X;
				Vertices[i * length + 4] = _vertices[i].normal.Y;
				Vertices[i * length + 5] = _vertices[i].normal.Z;
				Vertices[i * length + 6] = _vertices[i].texCoord.X;
				Vertices[i * length + 7] = _vertices[i].texCoord.Y;
			}

			Indices = new int[_indices.Count];

			for (int i = 0; i<_indices.Count; i++)
			{
				Indices[i] = _indices[i];
			}

			VBO = GL.GenBuffer();
			VAO = GL.GenVertexArray();
			EBO = GL.GenBuffer();

			GL.BindVertexArray(VAO);
				GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
				GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * Vertices.Length, Vertices, BufferUsageHint.DynamicDraw);
				GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, length * sizeof(float), 0);
				GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, length * sizeof(float), 3 * sizeof(float));
                GL.EnableVertexAttribArray(1);
				//
				GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
				GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * Indices.Length, Indices, BufferUsageHint.DynamicDraw);
			GL.BindVertexArray(0);	
		}

		public void Draw() 
		{
			Matrix4 project = Matrix4.CreateOrthographic(800.0f/100.0f, 600.0f/100.0f, 0.1f, 100.0f);
			int myProjectUniform = shader.GetUniformLocation(@"project");
			GL.UniformMatrix4(myProjectUniform, false, ref project);

			if (_textures.Count == 0)
			{
				defaultTex = new Texture(@"Resources/chalet.jpg");
				_textures.Add(defaultTex);
			}

			_textures[0].Bind();

			shader.Use();
			//GL.CullFace(CullFaceMode.FrontAndBack);
			GL.BindVertexArray(VAO);
			GL.DrawElements(BeginMode.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);
			GL.BindVertexArray(0);
		}
	}
}
