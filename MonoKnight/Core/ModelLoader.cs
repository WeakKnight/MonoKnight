using System;
using OpenTK;
using Assimp;
using Assimp.Configs;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MonoKnight
{
	public class ModelLoader:Object
	{

		private List<Mesh> _meshes = null;

		public Model model = null;

		public ModelLoader()
		{

		}

		public Model LoadFromFile(string path)
		{
			AssimpContext Importer = new AssimpContext();
			Importer.SetConfig(new NormalSmoothingAngleConfig(66.0f));
			String fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path);
			modelDirPath = Path.GetDirectoryName(fileName);
			//Scene scene = Importer.ImportFileFromStream(stream);
			Assimp.Scene scene = Importer.ImportFile(
				fileName,
				PostProcessSteps.FindDegenerates |
				PostProcessSteps.FindInvalidData |
				PostProcessSteps.FlipUVs |              // Required for Direct3D
				PostProcessSteps.FlipWindingOrder |     // Required for Direct3D
				PostProcessSteps.JoinIdenticalVertices |
				PostProcessSteps.ImproveCacheLocality |
				PostProcessSteps.OptimizeMeshes |
				PostProcessSteps.Triangulate
			);
			_meshes = new List<Mesh>();
			ProcessNode(scene.RootNode, scene);
			return new Model(path, ref _meshes);
		}

		private string modelDirPath = @"";

		private void ProcessNode(Node node, Assimp.Scene scene)
		{
			foreach (int index in node.MeshIndices)
			{
				Assimp.Mesh mesh = scene.Meshes[index];
				_meshes.Add(ProcessMesh(mesh, scene));
			}

			for (int i = 0; i < node.Children.Count; i++)
			{
				var child = node.Children[i];
				ProcessNode(child, scene);
			}
		}

		private MonoKnight.Mesh ProcessMesh(Assimp.Mesh mesh, Assimp.Scene scene)
		{
			List<Vertex> vertices = new List<Vertex>();
			List<int> indices = new List<int>();
			List<Texture> textures = new List<Texture>();

			for (int i = 0; i < mesh.VertexCount; i++)
			{
				Vertex vertex = new Vertex();
				Vector3 vector = new Vector3();
				vector.X = mesh.Vertices[i].X;
				vector.Y = mesh.Vertices[i].Y;
				vector.Z = mesh.Vertices[i].Z;
				vertex.position = vector;
				//
				if (mesh.HasNormals)
				{
					vector.X = mesh.Normals[i].X;
					vector.Y = mesh.Normals[i].Y;
					vector.Z = mesh.Normals[i].Z;
					vertex.normal = vector;
				}
				else
				{
					vertex.normal = Vector3.Zero;
				}
				//
				if (mesh.HasTextureCoords(0))
				{
					Vector2 vec = new Vector2();
					vec.X = mesh.TextureCoordinateChannels[0][i].X;
					vec.Y = mesh.TextureCoordinateChannels[0][i].Y;
					vertex.texCoord = vec;
				}
				else
				{
					vertex.texCoord = new Vector2(0.0f, 0.0f);
				}
				vertices.Add(vertex);
			}

			for (int i = 0; i < mesh.FaceCount; i++)
			{
				Face face = mesh.Faces[i];
				for (int j = 0; j < face.IndexCount; j++)
				{
					indices.Add(face.Indices[j]);
				}
			}

			if (mesh.MaterialIndex >= 0)
			{
				Assimp.Material material = scene.Materials[mesh.MaterialIndex];

				for (int i = 0; i < material.GetMaterialTextures(TextureType.Diffuse).Length; i++)
				{
					var texSlot = material.GetMaterialTextures(TextureType.Diffuse)[i];
					Texture texture = new Texture();
					texture.LoadFromPath(Path.Combine(modelDirPath, texSlot.FilePath));
					textures.Add(texture);
				}
				//TODO other type texture
			}
			return new Mesh(vertices, indices, textures);		
		}

	}
}
