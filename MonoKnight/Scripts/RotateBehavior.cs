using System;
using OpenTK;
namespace MonoKnight
{
	public class RotateBehavior:Script
	{
		public RotateBehavior()
		{
		}

		public int rotateAxis = 0;// 0 is y , 1 is x
		public float rotateSpeed = 10.0f;

		public override void Update()
		{
			base.Update();
			if (rotateAxis == 0)
			{
				transform.rotation = (Matrix4.CreateRotationY(rotateSpeed * Time.DeltaTime) * Matrix4.CreateFromQuaternion(transform.rotation)).ExtractRotation();
			}
			else
			{ 
				transform.rotation = (Matrix4.CreateRotationX(rotateSpeed* Time.DeltaTime) * Matrix4.CreateFromQuaternion(transform.rotation)).ExtractRotation();
			}
		}
	}
}
