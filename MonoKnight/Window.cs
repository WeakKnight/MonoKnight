using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Input;

namespace MonoKnight
{
	public class Window : GameWindow
	{
		public Window() :
		base
		(
		800, // initial width
		600, // initial height
		GraphicsMode.Default,
		"dreamstatecoding",  // initial title
		GameWindowFlags.Default,
		DisplayDevice.Default,
		4, // OpenGL major version
		0, // OpenGL minor version
		GraphicsContextFlags.ForwardCompatible)
		{
			Title += ": OpenGL Version: " + GL.GetString(StringName.Version);
			W = Width;
			H = Height;
			camera.transform.position = new Vector3(0.0f, 0.0f, -3.0f);
		}

		public Scene _scene = new Scene();

		protected override void OnLoad(EventArgs e)
		{
            CursorVisible = true;
			var meshFilter = go.AddComponent<MeshFilter>();
			var meshRenderer = go.AddComponent<MeshRenderer>();
			meshFilter.LoadFromFile(@"Resources/Blonde Elexis - nude.obj");
			meshRenderer.Init();
			camera.AddComponent<BasicControl>();

			_scene.AddItem(go);
			_scene.AddItem(camera);

        }

		public static int W = 0;
		public static int H = 0;

		protected override void OnResize(EventArgs e)
		{
			W = Width;
			H = Height;
            //GL.Viewport(0, 0, this.ClientSize.Width, this.ClientSize.Height);
			Render();
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			Render();
			_scene.Render();
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			Time.DeltaTime = e.Time;
			_scene.Update();
		}
		//private float rotate = 0;
		private GameObject go = new GameObject();
		private GameObject go1 = new GameObject();
        private Camera camera = new Camera();

		private void Render()
		{
			_scene.Update();
			_scene.UpdateTransform();
			_scene.Render();
			//if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Right))
			//{
			//	camera.transform.position.X += 0.1f;
			//}
			//if(OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Left))
			//{
			//	camera.transform.position.X -= 0.1f;
			//}
			//if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Up))
			//{
			//	camera.transform.position.Y += 0.1f;
			//}
			//if(OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Down))
			//{
			//	camera.transform.position.Y -= 0.1f;
			//}

			//camera.transform.UpdateTransform();

   //         GL.Viewport(0, 0, Width, Height);
   //      	GL.Enable(EnableCap.DepthTest);
   //      	GL.ClearColor(new Color4(49.0f / 255.0f, 77.0f / 255.0f, 121.0f / 255.0f, 1.0f));
			//GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			//var mr = go.GetComponent<MeshRenderer>() as MeshRenderer;
			//mr.Render();

			SwapBuffers();
		}
	}
}
