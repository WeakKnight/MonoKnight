using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SharpFont;

namespace IMGUI
{
	enum DrawCommand
	{
		Rectangle,
		Triangle,
		Text,
		Circle,
		Line
	}

    public static class PrimitiveData
    {
		public static float[] Rect = 
        {
            // Pos      // Tex
            0.0f, 1.0f, 0.0f, 1.0f,
		    1.0f, 0.0f, 1.0f, 0.0f,
		    0.0f, 0.0f, 0.0f, 0.0f,

		    0.0f, 1.0f, 0.0f, 1.0f,
		    1.0f, 1.0f, 1.0f, 1.0f,
		    1.0f, 0.0f, 1.0f, 0.0f
	    };
    }

    public static class DrawCommandRenderer
    {
        public static void Init()
        {
            //VAO = GL.GenVertexArray();
            //VBO = GL.GenBuffer();
            RectVAO = GL.GenVertexArray();
            RectVBO = GL.GenBuffer();
            projection = Matrix4.CreateOrthographic(800.0f / 1.0f, 600.0f / 1.0f, 0.2f, 100.0f);

			GL.BindVertexArray(RectVAO);
				GL.BindBuffer(BufferTarget.ArrayBuffer, RectVBO);
	            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * PrimitiveData.Rect.Length, PrimitiveData.Rect, BufferUsageHint.DynamicDraw);

				GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
			    GL.EnableVertexAttribArray(0);
			GL.BindVertexArray(0);
		}
        public static void Render()
        {
            if(!needRender)
            {
                return;
            }

            while(DrawCommandList.Count != 0)
            {
                var cmd = DrawCommandList.Dequeue();
                DrawByCommand(cmd);
            }
            DataBuffer.Clear();
            needRender = false;
        }

        static int RectVBO = 0;
        static int RectVAO = 0;
        //static int VBO = 0;
        //static int VAO = 0;

        static Matrix4 projection = Matrix4.Identity;
        //static float[] buffer = null;

        public static bool needRender = true;

        static Shader solidShader = new Shader(@"Shaders/solidcolor.vert", @"Shaders/solidcolor.frag");

        static void DrawByCommand(DrawCommand cmd)
        {
            switch (cmd)
            {
                case DrawCommand.Rectangle:
                    ExtractRect();
                    return;
                default:
                    return;
            }
        }

        //maybe indices is useful
        public static void AddRect(float x, float y, float width, float height, Color4 color, bool filled = true)
        {
            //draw command
            DrawCommandList.Enqueue(DrawCommand.Rectangle);
            //triangle 1
            DataBuffer.Enqueue(x);
            DataBuffer.Enqueue(y);
            DataBuffer.Enqueue(width);
            DataBuffer.Enqueue(height);
            DataBuffer.Enqueue(color);
            DataBuffer.Enqueue(filled);
        }

        public static void ExtractRect()
        {
            var x = (float)DataBuffer.Dequeue();
            var y = (float)DataBuffer.Dequeue();
            var w = (float)DataBuffer.Dequeue();
            var h = (float)DataBuffer.Dequeue();
            var color = (Color4)DataBuffer.Dequeue();
            var filled = (bool)DataBuffer.Dequeue();

            DrawRect(x, y, w, h, color, filled);
        }

        static public void DrawRect(float x, float y, float width, float height, Color4 color, bool filled = true)
		{
            var model = Matrix4.CreateScale(width, height, 1.0f) * Matrix4.CreateTranslation(x, y, 0.0f) * Matrix4.CreateTranslation(-400.0f, -300.0f, 0.0f);
            var modelLoc = solidShader.GetUniformLocation(@"model");
            var projectLoc = solidShader.GetUniformLocation(@"projection");
            var colorLoc = solidShader.GetUniformLocation(@"maskColor");

            GL.UniformMatrix4(modelLoc, false, ref model);
            GL.UniformMatrix4(projectLoc, false, ref projection);
            GL.Uniform4(colorLoc, color);

            solidShader.Use();

            GL.BindVertexArray(RectVAO);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindVertexArray(0);
		}

        static public void DrawText(float x, float y, string text)
        {
            
        }

        public static void AddTriangle(Vector2 a, Vector2 b, Vector2 c)
        {
            
        }

        public static void AddCircle(Vector2 center, float radius)
        {
            
        }

        public static void AddLine(Vector2 a, Vector2 b)
        {
            
        }

        public static void AddText(string text, Vector2 pos)
        {
            
        }

        static Queue<object> DataBuffer = new Queue<object>();
        static Queue<DrawCommand> DrawCommandList = new Queue<DrawCommand>();
    }
}
