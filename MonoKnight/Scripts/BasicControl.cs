using System;
using OpenTK.Input;
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
		}
	}
}
