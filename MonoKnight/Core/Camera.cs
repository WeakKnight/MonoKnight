using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace MonoKnight
{
	class Camera:GameObject
    {
		public Camera()
        {
			SetTag("camera");
        }

		public Matrix4 ViewMatrix
		{
			get 
			{
				return Matrix4.LookAt(transform.position, transform.position + transform.forward, transform.up);
			}
		}

		public Matrix4 ProjectMatrix
		{
			get 
			{
				return Matrix4.CreateOrthographic(Window.W / 100.0f, Window.H / 100.0f, 0.1f, 100.0f);
			}
		}
    }
}
