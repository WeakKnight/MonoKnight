using System;
namespace MonoKnight
{
	public class GameObject : Entity
	{
		public GameObject()
		{
			transform = AddComponent<Transform>();
		}

		public Transform transform;
	}
}
