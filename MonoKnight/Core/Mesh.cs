using System;
using OpenTK;
using System.Collections.Generic;

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
		public Mesh(List<Vertex> vertices, List<int> indices, List<TextureInfo> textures)
		{
			_vertices = vertices;
			_indices = indices;
			_textures = textures;
		}

		public List<Vertex> _vertices;
		public List<int> _indices;
		public List<TextureInfo> _textures;
	}
}
