using System;
using System.Collections.Generic;
using OpenTK;

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

    public static class DrawCommandRenderer
    {
        static void DrawCommand(DrawCommand cmd)
        {
            
        }

        static void DrawRect(float x, float y, float width, float height)
        {
            
        }

        static void DrawTriangle(Vector2 a, Vector2 b, Vector2 c)
        {
            
        }

        static void DrawCircle(Vector2 center, float radius)
        {
            
        }

        static void DrawLine(Vector2 a, Vector2 b)
        {
            
        }

        static void DrawText(string text, Vector2 pos)
        {
            
        }

        static List<DrawCommand> DrawCommandList = new List<DrawCommand>();
    }
}
