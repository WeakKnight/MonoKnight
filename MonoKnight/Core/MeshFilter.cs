using System;
using OpenTK;
using Assimp;
using Assimp.Configs;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MonoKnight
{
	[ComponentOrder(40)]
	public class MeshFilter : Component
	{
		public Model model = null;

		[DataMember]
		public string modelPath = null;

		public override void Awake()
		{
			base.Awake();
			if (modelPath != null)
			{
				model = ResourceManager.GetInstance().LoadResource<Model>(modelPath);
			}
		}

		public MeshFilter()
		{
			
		}
	}
}
