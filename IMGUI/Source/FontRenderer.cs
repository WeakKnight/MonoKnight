using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SharpFont;

namespace IMGUI.Source
{
    public static class FontRenderer
    {
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
                //GL.TexParameterI(TextureTarget.Texture2D,TextureParameterName.TextureWrapS,CLAMP)
			//    int texture = 0;
			//       glGenTextures(1, &texture);
			//       glBindTexture(GL_TEXTURE_2D, texture);
			//       glTexImage2D(
			//           GL_TEXTURE_2D,
			//           0,
			//           GL_RED,
			//           face->glyph->bitmap.width,
			//           face->glyph->bitmap.rows,
			//           0,
			//           GL_RED,
			//           GL_UNSIGNED_BYTE,
			//           face->glyph->bitmap.buffer
			//);
			       //// Set texture options
			       //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
			       //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
			       //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
			       //glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
                //

            }


        }
    }
}
