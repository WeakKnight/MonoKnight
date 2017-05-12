using System;
namespace MonoKnight
{
	public class Component:Object
	{
		public Component() :base()
		{
			
		}

		public virtual void Update() 
		{
			
		}

		public Entity parent = null;

		public int runOrder = 0;
	}
}
