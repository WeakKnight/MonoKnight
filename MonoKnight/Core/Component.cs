using System;
namespace MonoKnight
{
	public class Component:Object
	{
		public Component() 
		{
			
		}

		public virtual void Update() 
		{
			
		}

		public Entity parent = null;

		public int runOrder = 0;
	}
}
