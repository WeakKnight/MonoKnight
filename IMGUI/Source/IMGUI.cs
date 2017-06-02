using System;
namespace IMGUI
{
    public static class IMGUI
    {
        public static void AddRect(float x, float y, float width, float height)
        {
            DrawCommandRenderer.AddRect(x, y, width, height);
        }

        public static void Render()
        {
            DrawCommandRenderer.Render();
        }
    }
}
