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
			UpdateTransformInternal(rootTransform);
		}

		private void UpdateTransformInternal(Transform transform)
		{
			transform.UpdateWorld();
			foreach (var child in transform._children)
			{
				UpdateTransformInternal(child);
			}
		}

		public void AddItem(GameObject go)
		{
			AddItem(go.GetComponent<Transform>() as Transform);
		}

		public void AddItem(Transform transform)
		{
			transform.parentTransform = rootTransform;
		}

		public void Start()
		{
			foreach (var script in ComponentPool.GetInstance().ScriptPool)
			{
				script.Start();
			}
		}

		public void Update()
		{
			foreach (var script in ComponentPool.GetInstance().ScriptPool)
			{
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

			foreach (var renderer in ComponentPool.GetInstance().MeshRendererPool)
			{
				if (renderer.transform.root != rootTransform)
				{
					continue;
				}
				else
				{
					renderer.Render();
				}
			}
		}

		Transform rootTransform = new Transform();

		//Color4 ambientLightColor = Color4.White;
	}
}
