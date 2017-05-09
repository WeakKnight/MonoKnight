using System;

namespace MonoKnight
{
	public class Object
	{
		public Object()
		{
			_count += 1;
			_id = _count;
		}

		public long GetInstanceID() 
		{
			return _id;
		}

		public static void Destroy(Object obj) 
		{
			if (obj.GetType().IsAssignableFrom(typeof(Component)))
			{
				Debug.Log("Component Destroy");
			}
			else if (obj.GetType().IsAssignableFrom(typeof(Entity)))
			{
				Debug.Log("Entity Destroy");
			}
			else 
			{
				Debug.Log("Object Destroy");
			}
		}

		public string name = "";

		private static long _count = 0;
		private long _id = 0;
	}
}
