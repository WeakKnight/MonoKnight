using System;
using System.Collections.Generic;
namespace MonoKnight
{
	public class Entity:Object
	{
		public Transform transform;

		public Entity()
		{
			transform = AddComponent<Transform>();
		}

		public Component AddComponent(Type type)  
		{
			Component component = Activator.CreateInstance(type) as Component;
			component.entity = this;
			componentContainer.Add(component);
			ComponentManager.GetInstance().Add(component, type);
			return component;
		}

		public T AddComponent<T>() where T:Component, new()
		{
			T component = new T();
			component.entity = this;
			componentContainer.Add(component);
			ComponentManager.GetInstance().Add(component, typeof(T));
			return component;
		}

		public Component[] GetAllComponents()
		{
			return componentContainer.ToArray();
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

		public T GetComponent<T>() where T : Component, new()
		{
			foreach (var item in componentContainer)
			{
				if (item.GetType() == typeof(T))
				{
					return item as T;
				}
			}
			return null;
		}

		public void RemoveComponent(Component com)
		{
			componentContainer.RemoveAll(delegate(Component target)
			{
				if (com == target)
				{
					ComponentManager.GetInstance().RemoveComponent(target);
					return true;
				}
				else
				{
					return false;
				}
			});
		}

		public void RemoveAllComponent()
		{
			componentContainer.RemoveAll(delegate(Component com) 
			{
				ComponentManager.GetInstance().RemoveComponent(com);
				return true;
			});
		}

		private List<Component> componentContainer = new List<Component>();
	}
}
