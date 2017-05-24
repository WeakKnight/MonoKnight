using System;
using OpenTK.Input;
using OpenTK;

namespace MonoKnight
{
	public class BasicControl:Script
	{
		public BasicControl()
		{
		}

		public override void Update()
		{
			base.Update();

			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Right))
			{
				transform.position += 0.1f * transform.right;
			}

			if(OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Left))
			{
				transform.position -= 0.1f * transform.right;
			}

			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Up))
			{
				transform.position += 0.1f * transform.up;
			}

			if(OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Down))
			{
				transform.position -= 0.1f * transform.up;			
			}

			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.J))
			{
				transform.rotation = (Matrix4.CreateRotationX(10.0f * Time.DeltaTime) * Matrix4.CreateFromQuaternion(transform.rotation)).ExtractRotation();
			}

			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.K))
			{
				transform.rotation = (Matrix4.CreateRotationY(10.0f * Time.DeltaTime) * Matrix4.CreateFromQuaternion(transform.rotation)).ExtractRotation();
			}

			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.L))
			{
				transform.rotation = (Matrix4.CreateRotationZ(10.0f * Time.DeltaTime) * Matrix4.CreateFromQuaternion(transform.rotation)).ExtractRotation();
			}

			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Q))
			{
				transform.scale += Vector3.One * 0.1f;
			}

			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.E))
			{
				transform.scale -= Vector3.One * 0.1f;
			}
			//
			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.B))
			{
				Object.Destroy(entity);
			}
		}
	}
}
