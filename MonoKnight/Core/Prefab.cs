using System;
namespace MonoKnight
{
	public class Prefab:Resource
	{
		public Prefab()
		{
		}

		static public Prefab Create(Entity entity)
		{
			var prefab = new Prefab();
			return prefab;
		}
	}
}
