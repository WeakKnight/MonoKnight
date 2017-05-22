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
				transform.position.X += 0.1f;
			}
			if(OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Left))
			{
				transform.position.X -= 0.1f;
			}
			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Up))
			{
				transform.position.Y += 0.1f;
			}
			if(OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Down))
			{
				transform.position.Y -= 0.1f;			
			}
		}
	}
}
