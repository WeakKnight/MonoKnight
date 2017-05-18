using System;
using System.Collections.Generic;

namespace MonoKnight
{
	public class ComponentPool
	{
		public ComponentPool()
		{
		}

		public void Add(Component com)
		{
			if (com.GetType().IsAssignableFrom(typeof(MeshRenderer)))
			{
				MeshRendererPool.Add(com as MeshRenderer);
			}
		}

		public List<MeshRenderer> MeshRendererPool = new List<MeshRenderer>();

		static public ComponentPool GetInstance()
		{
			if (_instance == null)
			{
				return new ComponentPool();
			}
			else
			{
				return _instance;
			}
		}

		static private ComponentPool _instance = null;
	}
}
