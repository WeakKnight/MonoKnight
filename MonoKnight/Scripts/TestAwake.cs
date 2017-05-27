using System;
namespace MonoKnight
{
	public class TestAwake:Script
	{
		public TestAwake()
		{
		}

		public override void Update()
		{
			base.Update();
			Debug.Log("Haha Update");
		}
	}
}
