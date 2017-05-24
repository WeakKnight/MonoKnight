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
	
		private MeshFilter meshFilter = null;

		public void Init() 
		{
			meshFilter = GetComponent<MeshFilter>() as MeshFilter;

			int count = meshFilter.model.meshes.Count;
			for (int index = 0; index<count; index++)
			{
				Mesh mesh = meshFilter.model.meshes[index];
				mesh.SetUp();
			}
		}

		public void Render()
		{
			int count = meshFilter.model.meshes.Count;
			for (int index = 0; index<count; index++)
			{
				Mesh mesh = meshFilter.model.meshes[index];
				mesh.Render(transform);
			}
		}
	}
}
