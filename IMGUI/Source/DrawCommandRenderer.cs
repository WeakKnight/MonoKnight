using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;

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

    public static class PrimitiveMode
    {
        public const float Triangles = 0.0f;
        public const float Lines = 1.0f;
        public const float Points = 2.0f;

        public static PrimitiveType GetPrimitiveType(float pm)
        {
            switch (pm)
            {
                case Triangles:
                    return PrimitiveType.Triangles;
                case Lines:
                    return PrimitiveType.Lines;
                case Points:
                    return PrimitiveType.Points;
                default:
                    System.Diagnostics.Debug.Assert(true);
                    return PrimitiveType.Triangles;
            }
        }
    }

    public static class DrawCommandRenderer
    {
        public static void Init()
        {
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
        }
        public static void Render()
        {
            buffer = DataBuffer.ToArray();

            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * buffer.Length, buffer, BufferUsageHint.DynamicDraw);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(0);

            int dataPos = 0;
            while(DrawCommandList.Count != 0)
            {
                var cmd = DrawCommandList.Dequeue();
                dataPos += DrawByCommand(cmd, dataPos);
            }    
        }

        static int VBO = 0;
        static int VAO = 0;

        static float[] buffer = null;

        static Shader solidShader = new Shader(@"Shaders/solidcolor.vert", @"Shaders/solidcolor.frag");

        static int DrawByCommand(DrawCommand cmd, int dataPos)
        {
            switch (cmd)
            {
                //data length is 5
                case DrawCommand.Rectangle:
                    DrawRect(dataPos);
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
            DataBuffer.Enqueue(x + width);
			DataBuffer.Enqueue(y);
			DataBuffer.Enqueue(x + width);
			DataBuffer.Enqueue(y + height);
            //triangle 2
			DataBuffer.Enqueue(x);
			DataBuffer.Enqueue(y);
			DataBuffer.Enqueue(x);
            DataBuffer.Enqueue(y + height);
			DataBuffer.Enqueue(x + width);
			DataBuffer.Enqueue(y + height);
            //primitive type
            DataBuffer.Enqueue(PrimitiveMode.Triangles);
        }

		private static void DrawRect(int dataPos)
		{
            solidShader.Use();

            GL.BindVertexArray(VAO);
                var primitiveType = PrimitiveMode.GetPrimitiveType(buffer[dataPos + 12]);
                GL.DrawArrays(primitiveType, dataPos, 12);
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
