using System;
using System.Collections.Generic;
namespace MonoKnight
{
	public class SystemScheduler
	{
		public SystemScheduler()
		{
			
		}

		public void AddComponent() 
		{
	
		}

		SortedDictionary<Type, List<Component>> ComponentPool = new SortedDictionary<Type, List<Component>>();
	}

	public class ComponentComparer : IComparer<Component>
	{
		public int Compare(Component a, Component b)
		{
			return a.runOrder.CompareTo(b.runOrder);
		}
	}
}
