using System;
namespace MonoKnight
{
	public class Component:Object
	{
		public Component() 
		{
			
		}

		protected Component AddComponent(Type type)
		{
			return parent.AddComponent(type);
		}

		protected T AddComponent<T>() where T : Component, new()
		{
			return parent.AddComponent<T>();
		}

		protected Component[] GetComponents(Type type)
		{
			return parent.GetComponents(type);
		}

		protected Component[] GetComponents<T>() where T : Component, new()
		{
			return parent.GetComponents<T>();
		}

		protected Component GetComponent(Type type)
		{
			return parent.GetComponent(type);
		}

		protected Component GetComponent<T>() where T : Component, new()
		{
			return parent.GetComponent<T>();
		}

		public Transform transform
		{
			get
			{
				return parent.GetComponent<Transform>() as Transform;
			}
		}

		public Entity parent = null;
	}
}
