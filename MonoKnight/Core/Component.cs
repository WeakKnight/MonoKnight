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
			return entity.AddComponent(type);
		}

		protected T AddComponent<T>() where T : Component, new()
		{
			return entity.AddComponent<T>();
		}

		protected Component[] GetComponents(Type type)
		{
			return entity.GetComponents(type);
		}

		protected Component[] GetComponents<T>() where T : Component, new()
		{
			return entity.GetComponents<T>();
		}

		protected Component GetComponent(Type type)
		{
			return entity.GetComponent(type);
		}

		protected T GetComponent<T>() where T : Component, new()
		{
			return entity.GetComponent<T>();
		}

		public Transform transform
		{
			get
			{
				return entity.GetComponent<Transform>() as Transform;
			}
		}

		public Entity entity = null;
	}
}
