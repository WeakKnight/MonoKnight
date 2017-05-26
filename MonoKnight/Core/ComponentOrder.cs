using System;
namespace MonoKnight
{
	[System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class ComponentOrder : Attribute
	{
		readonly int runningOrder;
		public int RunningOrder
		{
			get
			{
				return runningOrder;
			}
		}

		public ComponentOrder(int runningOrder)
		{
            this.runningOrder = runningOrder;
		}
	}
}
