using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace MonoKnight
{
    class Camera
    {
        public Camera()
        {
            transform = new Transform();
        }
    
        public Transform transform;
    }
}
