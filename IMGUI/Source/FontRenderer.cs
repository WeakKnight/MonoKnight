using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SharpFont;
using System.Collections.Generic;

namespace IMGUI
{
    public struct CharacterInfo
    {
        public int TextureId;
        public Vector2 Size;
        public Vector2 Pos;
        public Vector2 Advance;
    }

    public static class FontRenderer
    {
        public static int VAO;
        public static int VBO;

        public static void Init()
        {
			//OpenSans - Light.ttf
			Library library = new Library();
			Face face = new Face(library, @"OpenSans-Light.ttf");
			face.SetPixelSizes(0, 48);
            //
            GL.PixelStore(PixelStoreParameter.PackAlignment, 1);
            for (int c = 0; c < 128; c++)
            {
                face.LoadChar((char)c, LoadFlags.Default, LoadTarget.Mono);
                var bitmap = face.Glyph.Bitmap;
                //
                int tex = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, tex);
                GL.TexImage2D(TextureTarget.Texture2D,
                              0,
                              PixelInternalFormat.Rgba,
                              bitmap.Width,bitmap.Rows,
                              0,
                              PixelFormat.Rgba,
                              PixelType.UnsignedByte,
                              bitmap.Buffer);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
                CharacterInfo charInfo;
                charInfo.TextureId = tex;
                charInfo.Size = new Vector2(bitmap.Width, bitmap.Rows);
                charInfo.Pos = new Vector2(face.Glyph.BitmapLeft, face.Glyph.BitmapTop);
                charInfo.Advance = new Vector2(face.Glyph.Advance.X.ToSingle(), face.Glyph.Advance.Y.ToSingle());
                CharacterDictionary[(char)c] = charInfo;
            }
            //
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            //
            GL.BindVertexArray(VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * 6 * 4, new float[]{}, BufferUsageHint.DynamicDraw);

			GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }
        public static Dictionary<char, CharacterInfo> CharacterDictionary = new Dictionary<char, CharacterInfo>();
    }
}
