using System;
using OpenTK.Graphics;
using SharpFont;

namespace IMGUI
{
    public static class IMGUI
    {
        public static void Init()
        {
            FontRenderer.Init();
            DrawCommandRenderer.Init();
        }

        public static bool Button()
        {
            DrawCommandRenderer.AddRect(0.0f, 0.0f, 150.0f, 50.0f, Color4.White, true);
            return true;
        }

        public static void Render()
        {
            if(DrawCommandRenderer.needRender)
            {
                DrawCommandRenderer.Render();
            }
        }

        public static void End()
        {
            DrawCommandRenderer.needRender = true;
        }

        public static void ProcessInput(float winx, float winy)
        {
            NativeWindowX = winx;
            NativeWindowY = winy;
            CursorX = OpenTK.Input.Mouse.GetCursorState().X - NativeWindowX;
            CursorY = OpenTK.Input.Mouse.GetCursorState().Y - NativeWindowY;
            Console.WriteLine("x is" + CursorX);
            Console.WriteLine("y is" + CursorY);
        }

        static float NativeWindowX = 0.0f;
        static float NativeWindowY = 0.0f;
        static float CursorX = 0.0f;
        static float CursorY = 0.0f;

        public static bool CheckUpdate()
        {
            return !DrawCommandRenderer.needRender;
        }
    }
}
