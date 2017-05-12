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
        }

		public Matrix4 ViewMatrix
		{
			get 
			{
				return Matrix4.LookAt(transform.position, transform.position + transform.forward, transform.up);
			}
		}
    }
}
