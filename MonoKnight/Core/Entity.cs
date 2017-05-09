using System;
using System.Collections.Generic;
namespace MonoKnight
{
	public class Entity:Object
	{
		public Entity():base()
		{
			
		}

		public Component AddComponent(Type type)  
		{
			Component component = Activator.CreateInstance(type) as Component;
			ComponentPool.Add(component);
			return component;
		}

		public T AddComponent<T>() where T:Component, new()
		{
			T component = new T();
			ComponentPool.Add(component);
			return component;
		}

		public Component[] GetComponents(Type type)
		{
			Component[] result = { };
			foreach (var item in ComponentPool)
			{
				if (item.GetType() == type)
				{
					result[result.Length] = item;
				}
			}
			return result;
		}

		public Component[] GetComponents<T>() where T:Component, new()
		{
			Component[] result = { };
			foreach (var item in ComponentPool)
			{
				if (item.GetType() == typeof(T))
				{
					result[result.Length] = item;
				}
			}
			return result;
		}

		static void Destroy(Component component) 
		{
		}

		private List<Component> ComponentPool = new List<Component>();
	}
}
