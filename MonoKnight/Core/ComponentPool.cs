using System;
using System.Collections.Generic;

namespace MonoKnight
{
	public class ComponentPool
	{
		public ComponentPool()
		{
		}

		public void Add(Component com, Type type)
		{
			if (type.IsAssignableFrom(typeof(MeshRenderer)))
			{
				MeshRendererPool.Add(com as MeshRenderer);
			}
		}

		public List<MeshRenderer> MeshRendererPool = new List<MeshRenderer>();

		static public ComponentPool GetInstance()
		{
			if (_instance == null)
			{
				_instance = new ComponentPool();
				return _instance;
			}
			else
			{
				return _instance;
			}
		}

		static private ComponentPool _instance = null;
	}
}
