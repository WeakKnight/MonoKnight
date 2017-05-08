using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using StbSharp;
using System.IO;

namespace MonoKnight
{
	public class Texture
	{
		public Texture(string texPath)
		{
            ImageReader loader = new ImageReader();
            using (System.IO.Stream stream = File.Open(texPath, FileMode.Open))
            {
                StbSharp.Image image = loader.Read(stream, Stb.STBI_rgb_alpha);
                Tex = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, Tex);
                GL.TexImage2D(TextureTarget.Texture2D,
                          0,
                          PixelInternalFormat.Rgba,
                          image.Width,
                          image.Height,
                          0,
                          OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                          PixelType.UnsignedByte,
                          image.Data);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                //GL.TexImage2D(TextureTarget.Texture2D,0,PixelInternalFormat.Rgba,
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
            //
            /*
            Bitmap bitmap = new Bitmap(texPath);
			Tex = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, Tex);
			BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
			ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			GL.TexImage2D(TextureTarget.Texture2D, 
			              0, 
			              PixelInternalFormat.Rgba,
			              data.Width, 
			              data.Height, 
			              0, 
			              OpenTK.Graphics.OpenGL.PixelFormat.Bgra, 
			              PixelType.UnsignedByte, 
			              data.Scan0);
    		bitmap.UnlockBits(data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest);
    		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Nearest);
    		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
    		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
            //GL.TexImage2D(TextureTarget.Texture2D,0,PixelInternalFormat.Rgba,
            GL.BindTexture(TextureTarget.Texture2D, 0);
            */
        }
		public void Bind()
		{
			GL.BindTexture(TextureTarget.Texture2D, Tex);
		}
		private int Tex;
	}
}
