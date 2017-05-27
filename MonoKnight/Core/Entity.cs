using System;
using System.Collections.Generic;
namespace MonoKnight
{
	public class Entity:Object
	{
		public Transform transform
		{
			get 
			{
				return GetComponent<Transform>();
			}
		}

		public Entity()
		{
		}

		public Entity(float x, float y, float z)
		{
			AddComponent<Transform>();
			transform.position = new OpenTK.Vector3(x, y, z);
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
			return AddComponent(typeof(T)) as T;
		}

		public void AddComponent(Component component)
		{
			component.entity = this;
			componentContainer.Add(component);
			ComponentManager.GetInstance().Add(component, component.GetType());
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
			return GetComponents(typeof(T));
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
			return GetComponent(typeof(T)) as T;
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
