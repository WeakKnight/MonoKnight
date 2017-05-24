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
		public Mesh(List<Vertex> vertices, List<int> indices, List<Texture> textures)
		{
			_vertices = vertices;
			_indices = indices;
			_textures = textures;
		}

		public List<Vertex> _vertices;
		public List<int> _indices;
		public List<Texture> _textures;
	}
}
