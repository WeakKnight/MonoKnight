using System;
using System.Collections.Generic;

namespace MonoKnight
{
	public class Model:Resource
	{
		public Model(string a_path, ref List<Mesh> a_meshes)
		{
			path = a_path;
			meshes = a_meshes;
		}
		
		public string path = null;
		public List<Mesh> meshes = null;
        public Skeleton skeleton = null;
	}
}
