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
			camera.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
		}

		public Scene _scene = new Scene();

		protected override void OnLoad(EventArgs e)
		{
            CursorVisible = true;
			var model1 = ResourceManager.GetInstance().LoadResource<Model>(@"Resources/nanomodel/nanosuit.obj");
			var model2 = ResourceManager.GetInstance().LoadResource<Model>(@"Resources/Blonde Elexis - nude.obj");
			var meshFilter = go.AddComponent<MeshFilter>();
			var meshRenderer = go.AddComponent<MeshRenderer>();
			meshFilter.model = model1;
			//meshFilter.LoadFromFile(@"Resources/Blonde Elexis - nude.obj");
			meshRenderer.Init();
			//go.AddComponent<BasicControl>();
			//go.AddComponent<RotateBehavior>();

			var meshFilter1 = go1.AddComponent<MeshFilter>();
			var meshRenderer1 = go1.AddComponent<MeshRenderer>();
			go1.AddComponent<BasicControl>();
			//meshFilter1.LoadFromFile(@"Resources/nanomodel/nanosuit.obj");
			meshFilter1.model = model2;
			meshRenderer1.Init();

			//go1.AddComponent<RotateBehavior>();
			go1.transform.position = new Vector3(3.0f, 1.0f, 1.0f);
			//go1.transform.parentTransform = go.transform;

			//var meshFilter2 = go2.AddComponent<MeshFilter>();
			//var meshRenderer2 = go2.AddComponent<MeshRenderer>();
			//meshFilter2.LoadFromFile(@"Resources/Blonde Elexis - nude.obj");
			//meshRenderer2.Init();

			//go2.AddComponent<RotateBehavior>();
			//go2.transform.position = new Vector3(3.0f, 1.0f, 1.0f);
			//go2.transform.parentTransform = go1.transform;

			_scene.AddItem(go1);
			_scene.AddItem(go);
			_scene.AddItem(camera);

			_scene.Start();

        }

		public static int W = 0;
		public static int H = 0;

		protected override void OnResize(EventArgs e)
		{
			W = Width;
			H = Height;
			Render();
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			Render();
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			Time.DeltaTime = (float)e.Time;
			_scene.Update();
			//_scene.UpdateTransform();
		}

		private GameObject go = new GameObject();
		private GameObject go1 = new GameObject();
		private GameObject go2 = new GameObject();
        private Camera camera = new Camera();

		private void Render()
		{
			_scene.Render();
			SwapBuffers();
		}
	}
}
