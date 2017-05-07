using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace MonoKnight
{
    class Transform
    {
        public Transform()
        {
            rotatin = new Quaternion(0.0f,0.0f,0.0f);
            position = new Vector3(0.0f,0.0f,0.0f);
            scale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        public Matrix4 localToWorldMatrix
        {
            get
            {
                return Matrix4.CreateScale(scale) * Matrix4.CreateFromQuaternion(rotatin) * Matrix4.CreateTranslation(position);
            }
        }
        public Quaternion rotatin;
        public Vector3 position;
        public Vector3 scale;
    }
}
