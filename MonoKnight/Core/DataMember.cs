using System;
namespace MonoKnight
{
	[System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class DataMember : Attribute
	{
		public DataMember()
		{
		}
	}
}
