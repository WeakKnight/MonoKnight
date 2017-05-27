using System;
using System.Collections.Generic;
using System.Collections;
namespace MonoKnight
{
	public class Object
	{
		public Object()
		{
			_count += 1;
			_id = _count;
		}

		private bool willDestroy = false;

		public long GetInstanceID() 
		{
			return _id;
		}

		//public static void Destroy(Entity obj)
		//{
		//	obj.transform.parentTransform = null;
		//	obj.RemoveAllComponent();
		//	obj = null;
		//}

		public static void Destroy(Object obj) 
		{
			if (obj != null)
			{
				if (!obj.willDestroy)
				{
					obj.willDestroy = true;
					DestroySet.Add(obj);
				}
			}
		}

		public static void ForceDestroy(Object obj)
		{
			if (obj.GetType().IsSubclassOf(typeof(Component)))
			{
				if ((obj as Component).entity != null)
				{
					(obj as Component).entity.RemoveComponent(obj as Component);
				}
				Debug.Log("Component Destroy");
			}
			else if (obj.GetType().IsAssignableFrom(typeof(Entity)))
			{
				Debug.Log("Entity Destroy");
				if ((obj as Entity).transform.parent != null)
				{
					(obj as Entity).transform.parent.RemoveChild((obj as Entity).transform);
				}
				(obj as Entity).RemoveAllComponent();
				obj = null;
			}
			else 
			{
				Debug.Log("Object Destroy");
			}

			obj = null;
		}

		public static HashSet<Object> DestroySet = new HashSet<Object>();
		//public static Stack DestroyStack = new Stack();
		//public static List<Object> DestroyList = new List<Object>();

		public void SetTag(string tag) 
		{
			if (this._tag != null)
			{
				if (tagDictionary.ContainsKey(tag))
				{
					tagDictionary[tag].Remove(this);
				}
			}
			else
			{
				if (tagDictionary.ContainsKey(tag))
				{
					tagDictionary[tag].Add(this);
				}
				else
				{
					tagDictionary[tag] = new List<Object>();
					tagDictionary[tag].Add(this);
				}
			}
			_tag = tag;
		}

		public string GetTag()
		{
			return _tag;
		}

		public static Object[] FindObject(string tag) 
		{
			return tagDictionary[tag].ToArray();
		}

		private static Dictionary<string, List<Object>> tagDictionary = new Dictionary<string, List<Object>>();
		private string _tag = null;

		private static long _count = 0;
		private long _id = 0;
	}
}
