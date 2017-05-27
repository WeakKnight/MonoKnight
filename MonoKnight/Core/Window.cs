using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK.Input;
using ProtoBuf;
using System.IO;
using System.Xml;

namespace MonoKnight
{
	public delegate void WindowDelegate();

	public class Window : GameWindow
	{
		
		public Window() :
		base
		(
			800, // initial width
			600, // initial height
			GraphicsMode.Default,
			"MonoKnight",  // initial title
			GameWindowFlags.Default,
			DisplayDevice.Default,
			4, // OpenGL major version
			0, // OpenGL minor version
			GraphicsContextFlags.ForwardCompatible
		)
		{
			Title += ": OpenGL Version: " + GL.GetString(StringName.Version);
			W = Width;
			H = Height;
			//camera.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
		}

		//public Scene _scene;

		public WindowDelegate LoadDelegate;
		public WindowDelegate UpdateDelegate;
		public WindowDelegate ResizeDelegate;
		public WindowDelegate RenderDelegate;

		protected override void OnLoad(EventArgs e)
		{
            CursorVisible = true;
			if (LoadDelegate != null)
			{
				LoadDelegate();
			}
			//SceneInfo sceneInfo;
			//using (var file = File.OpenRead("Resources/main.scene")) {
   // 			sceneInfo = ProtoBuf.Serializer.Deserialize<SceneInfo>(file);
			//}

			//var scene = Serializer.DeserializeScene(sceneInfo);
			//_scene = scene;
			//_scene.Awake();
			//_scene.Start();
			//_scene.OnDestroy();
        }

		public static int W = 0;
		public static int H = 0;

		protected override void OnResize(EventArgs e)
		{
			W = Width;
			H = Height;
			if (RenderDelegate != null)
			{
				RenderDelegate();
			}
            SwapBuffers();
			//Render();
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			if (RenderDelegate != null)
			{
				RenderDelegate();
			}
            SwapBuffers();
			//Render();
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			//TODO Time Manager
			Time.DeltaTime = (float)e.Time;
			if (UpdateDelegate != null)
			{
                UpdateDelegate();
			}
			//_scene.Awake();
			//_scene.Start();
			//_scene.Update();
			//_scene.OnDestroy();
			//_scene.UpdateTransform();
		}

		//private void Render()
		//{
		//	_scene.Render();
		//	SwapBuffers();
		//}
	}
}
