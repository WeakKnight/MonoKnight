using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace MonoKnight
{
	public class MeshRenderer : Component
	{
		public MeshRenderer()
		{
		}

		private int[] VAOs = null;
		private int[] VBOs = null;
		private int[] EBOs = null;

		private MeshFilter meshFilter = null;
		private Shader shader = new Shader(@"Resources/mesh.vert", @"Resources/mesh.frag");
		private Texture defaultTex = null;

		public void Init() 
		{
			meshFilter = GetComponent<MeshFilter>() as MeshFilter;

			int count = meshFilter._meshes.Count;

			var tvaos = new int[count];
			GL.GenVertexArrays(count, tvaos);
			VAOs = tvaos;

			var tvbos = new int[count];
			GL.GenBuffers(count, tvbos);
			VBOs = tvbos;

			var tebos = new int[count];
			GL.GenBuffers(count, tebos);
			EBOs = tebos;

			for (int index = 0; index < count; index++)
			{
				Mesh mesh = meshFilter._meshes[index];
				int length = 8;
				float[] Vertices = null;
				int[] Indices = null;

				Vertices = new float[mesh._vertices.Count * length];

				for (int i = 0; i<mesh._vertices.Count; i++) 
				{
					Vertices[i * length + 0] = mesh._vertices[i].position.X;
					Vertices[i * length + 1] = mesh._vertices[i].position.Y;
					Vertices[i * length + 2] = mesh._vertices[i].position.Z;
					Vertices[i * length + 3] = mesh._vertices[i].normal.X;
					Vertices[i * length + 4] = mesh._vertices[i].normal.Y;
					Vertices[i * length + 5] = mesh._vertices[i].normal.Z;
					Vertices[i * length + 6] = mesh._vertices[i].texCoord.X;
					Vertices[i * length + 7] = mesh._vertices[i].texCoord.Y;
				}

				Indices = new int[mesh._indices.Count];

				for (int i = 0; i<mesh._indices.Count; i++)
				{
					Indices[i] = mesh._indices[i];
				}


				GL.BindVertexArray(VAOs[index]);
					GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[index]);
					GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * Vertices.Length, Vertices, BufferUsageHint.DynamicDraw);
					GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, length* sizeof(float), 0);
					GL.EnableVertexAttribArray(0);
					GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, length* sizeof(float), 3);
					GL.EnableVertexAttribArray(1);
	                GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, length* sizeof(float), 6 * sizeof(float));
	                GL.EnableVertexAttribArray(2);
					//
					GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOs[index]);
					GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * Indices.Length, Indices, BufferUsageHint.DynamicDraw);
				GL.BindVertexArray(0);	
			}
		}

		public void Render()
		{
			int count = meshFilter._meshes.Count;
			Camera camera = Object.FindObject("camera")[0] as Camera;
			Matrix4 project = camera.ProjectMatrix;
			Matrix4 view = camera.ViewMatrix;
			Matrix4 model = transform.localToWorldMatrix;
			for (int index = 0; index < count; index++)
			{

				int myModelUniform = shader.GetUniformLocation(@"model");
				GL.UniformMatrix4(myModelUniform, false, ref model);

            	int myViewUniform = shader.GetUniformLocation(@"view");
				GL.UniformMatrix4(myViewUniform, false, ref view);

				int myProjectUniform = shader.GetUniformLocation(@"project");
				GL.UniformMatrix4(myProjectUniform, false, ref project);

				if (meshFilter._meshes[index]._textures.Count == 0)
				{
					defaultTex = new Texture(@"Resources/chalet.jpg");
					meshFilter._meshes[index]._textures.Add(defaultTex);
				}

				int myDiffuseUniform = shader.GetUniformLocation(@"diffuseTex");
				GL.Uniform1(myDiffuseUniform, 0);

				GL.ActiveTexture(TextureUnit.Texture0);
				meshFilter._meshes[index]._textures[0].Bind();

				shader.Use();
				//GL.CullFace(CullFaceMode.FrontAndBack);
				GL.BindVertexArray(VAOs[index]);
				GL.DrawElements(BeginMode.Triangles, meshFilter._meshes[index]._indices.Count, DrawElementsType.UnsignedInt, 0);
				GL.BindVertexArray(0);
			}
		}
	}
}
