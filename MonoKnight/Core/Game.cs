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
