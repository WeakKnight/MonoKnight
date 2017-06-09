using System;
namespace MonoKnight
{
	public class Game
	{
		public Game(GameConfiguration config)
		{
			CurrentScene = ResourceManager.GetInstance().LoadResource<Scene>(config.DefaultScenePath);
			Config = config;
		}

		public GameConfiguration Config;
		public Scene CurrentScene;

		public virtual void Start()
		{
            var entity = new Entity(1.0f, 1.0f, 1.0f);
            entity.AddComponent<MeshRenderer>();
            var meshFilter = entity.AddComponent<MeshFilter>();
            meshFilter.model = ResourceManager.GetInstance().LoadResource<Model>(@"Resources/animodel/boblampclean.md5mesh");
            entity.transform.scale = OpenTK.Vector3.One * 0.1f;
            CurrentScene.AddItem(entity);
            //
			CurrentScene.Awake();
			CurrentScene.Start();
			CurrentScene.OnDestroy();
		}

		public virtual void Update()
		{
			CurrentScene.Awake();
			CurrentScene.Start();
			CurrentScene.Update();
			CurrentScene.OnDestroy();
		}

		public virtual void Render()
		{
			CurrentScene.Render();
		}
	}
}
