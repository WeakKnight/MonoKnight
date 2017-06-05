using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
            RectVBO = GL.GenVertexArray();
            RectVAO = GL.GenBuffer();
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

            //buffer = DataBuffer.ToArray();
			//GCHandle pinnedArray = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			//IntPtr pointer = pinnedArray.AddrOfPinnedObject();
			//// Do your stuff...
			//pinnedArray.Free();
            //GL.BindVertexArray(VAO);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            //GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * buffer.Length, buffer, BufferUsageHint.DynamicDraw);

            //GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            //GL.EnableVertexAttribArray(0);

            //GL.BindVertexArray(0);

            //int dataPos = 0;
            //while(DrawCommandList.Count != 0)
            //{
            //    var cmd = DrawCommandList.Dequeue();
            //    dataPos += DrawByCommand(cmd, dataPos);
            //}
            //DataBuffer.Clear();
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

        static int DrawByCommand(DrawCommand cmd, int dataPos)
        {
            switch (cmd)
            {
                //data length is 5
                case DrawCommand.Rectangle:
                    //DrawRect(dataPos);
                    return 13;
                default:
                    return 0;
            }
        }

        //maybe indices is useful
        public static void AddRect(float x, float y, float width, float height)
        {
            //draw command
            DrawCommandList.Enqueue(DrawCommand.Rectangle);
            //triangle 1
            DataBuffer.Enqueue(x);
            DataBuffer.Enqueue(y);
            DataBuffer.Enqueue(width);
            DataBuffer.Enqueue(height);
        }

		static public void DrawRect(float x, float y, float width, float height)
		{
            var model = Matrix4.CreateScale(width, height, 1.0f) * Matrix4.CreateTranslation(x, y, 0.0f) * Matrix4.CreateTranslation(-400.0f, -300.0f, 0.0f);
            var modelLoc = solidShader.GetUniformLocation(@"model");
            var projectLoc = solidShader.GetUniformLocation(@"projection");
            GL.UniformMatrix4(modelLoc, false, ref model);
            GL.UniformMatrix4(projectLoc, false, ref projection);
            solidShader.Use();

            GL.BindVertexArray(RectVAO);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.BindVertexArray(0);
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

        static Queue<float> DataBuffer = new Queue<float>();
        static Queue<DrawCommand> DrawCommandList = new Queue<DrawCommand>();
    }
}
