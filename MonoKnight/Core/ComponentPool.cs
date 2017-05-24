using System;
using System.Collections.Generic;

namespace MonoKnight
{
	public class ComponentPool:Object
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
			else if (type.IsSubclassOf(typeof(Script)))
			{
				ScriptPool.Add(com as Script);
			}
		}

		public void RemoveComponent(Component com)
		{
			if (com.GetType().IsSubclassOf(typeof(Script)))
			{
				ScriptPool.Remove(com as Script);
			}
			else if (com.GetType().IsAssignableFrom(typeof(MeshRenderer)))
			{
				MeshRendererPool.Remove(com as MeshRenderer);	
			}
			com = null;
		}

		public List<MeshRenderer> MeshRendererPool = new List<MeshRenderer>();

		public List<Script> ScriptPool = new List<Script>();

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
