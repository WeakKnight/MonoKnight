using System;
using System.Collections.Generic;

namespace MonoKnight
{
	public class ResourceManager:Object
	{
		public ResourceManager()
		{
		}

		public Model LoadModel(string path)
		{
			var model = modelLoader.LoadFromFile(path);
			_resourceDic[path] = model;
			return model;
		}

		public Texture LoadTexture(string path)
		{
			var texture = new Texture();
			texture.LoadFromPath(path);
			_resourceDic[path] = texture;
			return texture;
		}

		public Material LoadMaterial(string path)
		{
			return null;
		}

		public T LoadResource<T>(string path) where T : Resource
		{
			if (_resourceDic.ContainsKey(path))
			{
				return _resourceDic[path] as T;
			}
			else
			{
				if (typeof(T).Equals(typeof(Model)))
				{
					return LoadModel(path) as T;
				}
				else 
				{
					return LoadTexture(path) as T;
				}
			}
		}

		private Dictionary<string, Resource> _resourceDic = new Dictionary<string, Resource>();

		private ModelLoader modelLoader = new ModelLoader();

		static public ResourceManager GetInstance()
		{
			if (_instance == null)
			{
				_instance = new ResourceManager();
				return _instance;
			}
			else
			{
				return _instance;
			}
		}

		static private ResourceManager _instance = null;
	}
}
