﻿using System;
using System.Collections.Generic;

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

		public void SetTag(string tag) 
		{
			if (tag != null)
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
		}

		public static Object[] FindObject(string tag) 
		{
			return tagDictionary[tag].ToArray();
		}

		private static Dictionary<string, List<Object>> tagDictionary = new Dictionary<string, List<Object>>();
		private string tag = null;

		private static long _count = 0;
		private long _id = 0;
	}
}
