using System;
using System.Collections.Generic;
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
			foreach (var entity in ItemList)
			{	
				UpdateTransformInternal(entity.transform);
			}
		}

		private void UpdateTransformInternal(Transform transform)
		{
			transform.UpdateWorld();
			foreach (var child in transform._children)
			{
				UpdateTransformInternal(child);
			}
		}

		public void AddItem(Entity entity)
		{
			ItemList.Add(entity);
			InternalAddItem(entity);
		}

		public void InternalAddItem(Entity entity)
		{
			foreach (var com in entity.GetAllComponents())
			{
				ComponentManager.GetInstance().AddingList.Add(com);
			}
			if (entity.transform != null)
			{
				foreach (var childTransform in entity.transform._children)
				{
					InternalAddItem(childTransform.entity);
				}
			}
		}

		public void AddItem(Transform transform)
		{
			if (transform.entity == null)
			{
				Debug.Assert(true);
				return;
			}
			ItemList.Add(transform.entity);
		}

		public void Awake()
		{
			foreach (var com in ComponentManager.GetInstance().AddingList)
			{
				com.Awake();
			}
		}

		public void Start()
		{
			ComponentManager.GetInstance().AddingList.RemoveWhere(delegate(Component com)
			{
				com.Start();
				return true;
			});
		}

		public void Update()
		{
			if (ComponentManager.GetInstance().componentPool.ContainsKey(typeof(Script)))
			{
				ComponentManager.GetInstance().componentPool[typeof(Script)].ForEach(
					delegate (Component script)
					{
						if (script.isAwake)
						{ 
							(script as Script).Update();
						}
					}
				);
			}

			UpdateTransform();
		}

		public void Render()
		{
			GL.Viewport(0, 0, Window.W, Window.H);
         	GL.Enable(EnableCap.DepthTest);
         	GL.ClearColor(new Color4(49.0f / 255.0f, 77.0f / 255.0f, 121.0f / 255.0f, 1.0f));
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			foreach (var entity in ItemList)
			{
				RenderInternal(entity.transform);
			}
		}

		public void OnDestroy()
		{
			Object.DestroySet.RemoveWhere(delegate(Object obj) 
			{
				Object.ForceDestroy(obj);
				return true;
			});
		}

		//TODO Root Changed Listener
		private void RenderInternal(Transform transform)
		{
			if (transform.entity != null)
			{
				var renderer = transform.entity.GetComponent<MeshRenderer>();
				if (renderer != null)
				{
					renderer.Render();
				}
			}

			foreach (var childTransform in transform._children)
			{
				RenderInternal(childTransform);	
			}

			//TODO Root Changed Listener
			//if (ComponentManager.GetInstance().componentPool.ContainsKey(typeof(MeshRenderer)))
			//{
			//	ComponentManager.GetInstance().componentPool[typeof(MeshRenderer)].ForEach(
			//		delegate (Component renderer)
			//		{
			//			(renderer as MeshRenderer).Render();
			//		}
			//	);
			//}
		}

		public List<Entity> ItemList = new List<Entity>();
		//Transform root = new Transform();
	}
}
