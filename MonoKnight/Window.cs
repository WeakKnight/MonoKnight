﻿using System;
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

		protected override void OnLoad(EventArgs e)
		{
            CursorVisible = true;
			var meshFilter = go.AddComponent<MeshFilter>();
			var meshRenderer = go.AddComponent<MeshRenderer>();
			meshFilter.LoadFromFile(@"Resources/Blonde Elexis - nude.obj");
			meshRenderer.Init();
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
			Time.DeltaTime = e.Time;
			Render();
		}

		//private float rotate = 0;
		private GameObject go = new GameObject();
        private Camera camera = new Camera();

		private void Render()
		{
			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Right))
			{
				camera.transform.position.X += 0.1f;
			}
			if(OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Left))
			{
				camera.transform.position.X -= 0.1f;
			}
			if (OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Up))
			{
				camera.transform.position.Y += 0.1f;
			}
			if(OpenTK.Input.Keyboard.GetState().IsKeyDown(Key.Down))
			{
				camera.transform.position.Y -= 0.1f;
			}

			camera.transform.UpdateTransform();

            GL.Viewport(0, 0, Width, Height);
         	GL.Enable(EnableCap.DepthTest);
         	GL.ClearColor(new Color4(49.0f / 255.0f, 77.0f / 255.0f, 121.0f / 255.0f, 1.0f));
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			var mr = go.GetComponent<MeshRenderer>() as MeshRenderer;
			mr.Render();

			SwapBuffers();
		}
	}
}
