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

		public void Start()
		{
			for (int i = 0; i<ComponentPool.GetInstance().ScriptPool.Count; i++)
			{
				var script = ComponentPool.GetInstance().ScriptPool[i];
				script.Start();
			}
		}

		public void Update()
		{
			for (int i = 0; i < ComponentPool.GetInstance().ScriptPool.Count; i++)
			{
				var script = ComponentPool.GetInstance().ScriptPool[i];
				script.Update();
			}

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
			ComponentPool.GetInstance().MeshRendererPool.ForEach(
				delegate (MeshRenderer renderer)
				{
					renderer.Render();
				}
			);
		}

		Transform root = new Transform();
	}
}
