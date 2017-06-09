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

	public class Mesh:Object
	{
		public Mesh(ref List<Vertex> vertices, ref List<int> indices, ref List<Texture> textures)
		{
			_vertices = vertices;
			_indices = indices;
			_textures = textures;
		}

  //      public Mesh(ref List<Vertex> vertices, ref List<int> indices, ref List<Texture> textures, ref Dictionary<string, Bone> boneDic, ref Matrix4 globalInverseTransform, ref Assimp.Scene scene)
		//{
		//	_vertices = vertices;
		//	_indices = indices;
		//	_textures = textures;
  //          if(boneDic.Count > 0)
  //          {
  //              hasBone = true;
  //          }
  //          _boneDic = boneDic;
  //          _globalInverseTransform = globalInverseTransform;
  //          _scene = scene;
		//}

		public int vbo;
		public int vao;
		public int ebo;

        public bool hasBone = false;
        //public Assimp.Scene _scene;

		public void SetUp()
		{
			vao = GL.GenVertexArray();
			vbo = GL.GenBuffer();
			ebo = GL.GenBuffer();

			int length = 8;
			float[] Vertices = null;
			int[] Indices = null;

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

			Indices = _indices.ToArray();

			GL.BindVertexArray(vao);

			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
			GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * Vertices.Length, Vertices, BufferUsageHint.DynamicDraw);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
			GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * Indices.Length, Indices, BufferUsageHint.DynamicDraw);

			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, length* sizeof(float), 0);
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, length* sizeof(float), 3);
			GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, length* sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);

			GL.BindVertexArray(0);	
		}

		public void Render(Transform transform)
		{
			Camera camera = null;
			if (ComponentManager.GetInstance().componentPool.ContainsKey(typeof(Camera)))
			{
				if (ComponentManager.GetInstance().componentPool[typeof(Camera)][0] != null)
				{
					camera = ComponentManager.GetInstance().componentPool[typeof(Camera)][0] as Camera;
				}
			}

			if (camera == null)
			{
				return;
			}

			Matrix4 project = camera.ProjectMatrix;
			Matrix4 view = camera.ViewMatrix;
			Matrix4 model = transform.localToWorldMatrix;
			var shader = ResourceManager.GetInstance().meshShader;

			int myModelUniform = shader.GetUniformLocation(@"model");
			GL.UniformMatrix4(myModelUniform, false, ref model);

        	int myViewUniform = shader.GetUniformLocation(@"view");
			GL.UniformMatrix4(myViewUniform, false, ref view);

			int myProjectUniform = shader.GetUniformLocation(@"project");
			GL.UniformMatrix4(myProjectUniform, false, ref project);

			int myDiffuseUniform = shader.GetUniformLocation(@"diffuseTex");
			GL.Uniform1(myDiffuseUniform, 0);

			GL.ActiveTexture(TextureUnit.Texture0);
			_textures[0].Bind();

			shader.Use();
			GL.BindVertexArray(vao);
				GL.DrawElements(BeginMode.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
			GL.BindVertexArray(0);

			GL.ActiveTexture(TextureUnit.Texture0);
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}

		public List<Vertex> _vertices;
		public List<int> _indices;
		public List<Texture> _textures;
        public Dictionary<string,Bone> _boneDic;
        public Matrix4 _globalInverseTransform;
	}
}
