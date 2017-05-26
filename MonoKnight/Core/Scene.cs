using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using OpenTK.Graphics;

namespace MonoKnight
{
	public class Scene : Object
	{
		public Scene()
		{
		}

		public void UpdateTransform()
		{
			UpdateTransformInternal(root);
		}

		private void UpdateTransformInternal(Transform transform)
		{
			transform.UpdateWorld();
			foreach (var child in transform._children)
			{
				UpdateTransformInternal(child);
			}
		}

		public void AddItem(Entity go)
		{
			AddItem(go.GetComponent<Transform>() as Transform);
		}

		public void AddItem(Transform transform)
		{
			transform.parent = root;
		}

		public void Awake()
		{
			foreach (var comListPair in ComponentManager.GetInstance().componentPool)
			{
				foreach (var com in comListPair.Value)
				{
					com.Awake();
				}
			}
		}

		public void Start()
		{
			if (ComponentManager.GetInstance().componentPool.ContainsKey(typeof(Script)))
			{
				ComponentManager.GetInstance().componentPool[typeof(Script)].ForEach(
					delegate (Component script)
					{
						(script as Script).Start();
					}
				);
			}
			//for (int i = 0; i<ComponentManager.GetInstance().ScriptPool.Count; i++)
			//{
			//	var script = ComponentManager.GetInstance().ScriptPool[i];
			//	script.Start();
			//}
		}

		public void Update()
		{
			if (ComponentManager.GetInstance().componentPool.ContainsKey(typeof(Script)))
			{
				ComponentManager.GetInstance().componentPool[typeof(Script)].ForEach(
					delegate (Component script)
					{
						(script as Script).Update();
					}
				);
			}
			//for (int i = 0; i < ComponentManager.GetInstance().ScriptPool.Count; i++)
			//{
			//	var script = ComponentManager.GetInstance().ScriptPool[i];
			//	script.Update();
			//}

			UpdateTransform();
		}

		public void Render()
		{
			GL.Viewport(0, 0, Window.W, Window.H);
         	GL.Enable(EnableCap.DepthTest);
         	GL.ClearColor(new Color4(49.0f / 255.0f, 77.0f / 255.0f, 121.0f / 255.0f, 1.0f));
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			RenderInternal(root);
		}

		public void OnDestroy()
		{
			Object.DestroySet.RemoveWhere(delegate(Object obj) 
			{
				Object.ForceDestroy(obj);
				return true;
			});
		}

		private void RenderInternal(Transform transform)
		{
			if (ComponentManager.GetInstance().componentPool.ContainsKey(typeof(MeshRenderer)))
			{
				ComponentManager.GetInstance().componentPool[typeof(MeshRenderer)].ForEach(
					delegate (Component renderer)
					{
						(renderer as MeshRenderer).Render();
					}
				);
			}
			//ComponentManager.GetInstance().MeshRendererPool.ForEach(
			//	delegate (MeshRenderer renderer)
			//	{
			//		renderer.Render();
			//	}
			//);
		}

		Transform root = new Transform();
	}
}
