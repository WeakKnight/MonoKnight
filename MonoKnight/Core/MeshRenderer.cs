using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace MonoKnight
{
	[ComponentOrder(50)]
	public class MeshRenderer : Component
	{
		public MeshRenderer()
		{
		}
	
		private MeshFilter meshFilter = null;

		public override void Awake()
		{
			base.Awake();
			Init();
		}

		private void Init() 
		{
			meshFilter = GetComponent<MeshFilter>();

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
