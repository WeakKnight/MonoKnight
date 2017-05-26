using System;
using System.Collections.Generic;

namespace MonoKnight
{
	public class ComponentManager:Object
	{
		public ComponentManager()
		{
		}

		public void Add(Component com, Type type)
		{
			if (type.IsSubclassOf(typeof(Script)))
			{
				if (componentPool.ContainsKey(typeof(Script)))
				{
					componentPool[typeof(Script)].Add(com);
				}
				else
				{
					var list = new List<Component>();
					list.Add(com);
					componentPool[typeof(Script)] = list;
				}
			}
			else
			{
				if (componentPool.ContainsKey(type))
				{
					componentPool[type].Add(com);
				}
				else
				{
					var list = new List<Component>();
					list.Add(com);
					componentPool[type] = list;
				}
			}
		}

		public void RemoveComponent(Component com)
		{
			if (com.GetType().IsSubclassOf(typeof(Script)))
			{
				if (componentPool.ContainsKey(typeof(Script)))
				{
					componentPool[typeof(Script)].Remove(com);
				}
			}
			else
			{
				if (componentPool.ContainsKey(com.GetType()))
				{
					componentPool[com.GetType()].Remove(com);
				}
			}

			com = null;
		}

		public SortedDictionary<Type, List<Component>> componentPool = new SortedDictionary<Type, List<Component>>(new ComponentComparer());

		static public ComponentManager GetInstance()
		{
			if (_instance == null)
			{
				_instance = new ComponentManager();
				return _instance;
			}
			else
			{
				return _instance;
			}
		}

		static private ComponentManager _instance = null;

		private class ComponentComparer : IComparer<Type>
		{
			public int Compare(Type a, Type b)
			{
				var aOrder = (Attribute.GetCustomAttribute(a, typeof(ComponentOrder)) as ComponentOrder).RunningOrder;
				var bOrder = (Attribute.GetCustomAttribute(b, typeof(ComponentOrder)) as ComponentOrder).RunningOrder;
				return aOrder.CompareTo(bOrder);
			}
		}
	}
}
