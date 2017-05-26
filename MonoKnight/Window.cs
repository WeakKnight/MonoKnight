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
			camera.transform.position = new Vector3(0.0f, 0.0f, -10.0f);
		}

		public Scene _scene = new Scene();

		protected override void OnLoad(EventArgs e)
		{
            CursorVisible = true;
			//var model1 = ResourceManager.GetInstance().LoadResource<Model>(@"Resources/nanomodel/nanosuit.obj");
			//var model2 = ResourceManager.GetInstance().LoadResource<Model>(@"Resources/Blonde Elexis - nude.obj");
			//var model3 = ResourceManager.GetInstance().LoadResource<Model>(@"Resources/animodel/boblampclean.md5mesh");

			//var meshFilter = go.AddComponent<MeshFilter>();
			//var meshRenderer = go.AddComponent<MeshRenderer>();
			//meshFilter.modelPath = @"Resources/nanomodel/nanosuit.obj";
			////meshFilter.LoadFromFile(@"Resources/Blonde Elexis - nude.obj");
			//go.AddComponent<BasicControl>();
			//go.transform.position = new Vector3(2.0f, 1.0f, 1.0f);
			////go.AddComponent<RotateBehavior>();

			//var meshFilter1 = go1.AddComponent<MeshFilter>();
			//var meshRenderer1 = go1.AddComponent<MeshRenderer>();
			//go1.AddComponent<RotateBehavior>();
			////meshFilter1.LoadFromFile(@"Resources/nanomodel/nanosuit.obj");
			//meshFilter1.modelPath = @"Resources/Blonde Elexis - nude.obj";

			////go1.AddComponent<RotateBehavior>();
			//go1.SetTag("go1");
			//go1.transform.position = new Vector3(6.0f, 1.0f, 1.0f);
			//go1.transform.parent = go.transform;

			//var meshFilter2 = go2.AddComponent<MeshFilter>();
			//var meshRenderer2 = go2.AddComponent<MeshRenderer>();
			//meshFilter2.modelPath = @"Resources/Blonde Elexis - nude.obj";

			//go2.AddComponent<RotateBehavior>();
			//go2.transform.position = new Vector3(3.0f, 1.0f, 1.0f);
			//go2.transform.parent = go1.transform;

			//var goInfo = Serializer.SerializeEntity(go);
			//using (var file = File.Create("person.bin")) {
			//	ProtoBuf.Serializer.Serialize(file, goInfo);
			//}
			GameObjectInfo deInfo;
			using (var file = File.OpenRead("person.bin")) {
    			deInfo = ProtoBuf.Serializer.Deserialize<GameObjectInfo>(file);
			}
			var goo = Serializer.DeserializeEntity(deInfo);
			//var pre = Prefab.Create(go2);
			camera.AddComponent<Camera>();

			_scene.AddItem(goo);
			//_scene.AddItem(go1);
			//_scene.AddItem(go2);
			//_scene.AddItem(go);
			_scene.AddItem(camera);

			_scene.Awake();

			_scene.Start();

			_scene.OnDestroy();
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
			_scene.Awake();
			_scene.Start();
			_scene.Update();
			_scene.OnDestroy();
			//_scene.UpdateTransform();
		}

		private Entity go = new Entity(0.0f, 0.0f, 0.0f);
		private Entity go1 = new Entity(0.0f, 0.0f, 0.0f);
		private Entity go2 = new Entity(0.0f, 0.0f, 0.0f);
		private Entity camera = new Entity(0.0f, 0.0f, 0.0f);

		private void Render()
		{
			_scene.Render();
			SwapBuffers();
		}
	}
}
