using System;
using System.Collections.Generic;
namespace MonoKnight
{
	public class Entity:Object
	{
		public Entity()
		{
			
		}

		public Component AddComponent(Type type)  
		{
			Component component = Activator.CreateInstance(type) as Component;
			component.parent = this;
			componentContainer.Add(component);
			return component;
		}

		public T AddComponent<T>() where T:Component, new()
		{
			T component = new T();
			component.parent = this;
			componentContainer.Add(component);
			return component;
		}

		public Component[] GetComponents(Type type)
		{
			Component[] result = { };
			foreach (var item in componentContainer)
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
			foreach (var item in componentContainer)
			{
				if (item.GetType() == typeof(T))
				{
					result[result.Length] = item;
				}
			}
			return result;
		}

		public Component GetComponent(Type type)
		{
			foreach (var item in componentContainer)
			{
				if (item.GetType() == type)
				{
					return item;
				}
			}
			return null;
		}

		public Component GetComponent<T>() where T : Component, new()
		{
			foreach (var item in componentContainer)
			{
				if (item.GetType() == typeof(T))
				{
					return item;
				}
			}
			return null;
		}

		static void Destroy(Component component) 
		{
		}

		private List<Component> componentContainer = new List<Component>();
	}
}
