using System;
namespace IMGUI
{
    public static class IMGUI
    {
        public static void Init()
        {
            DrawCommandRenderer.Init();    
        }

        public static void AddRect(float x, float y, float width, float height)
        {
            DrawCommandRenderer.AddRect(x, y, width, height);
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

        public static bool CheckUpdate()
        {
            return !DrawCommandRenderer.needRender;
        }
    }
}
