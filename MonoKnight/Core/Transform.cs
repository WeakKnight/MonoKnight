using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace MonoKnight
{
	public class Transform : Component
    {
        public Transform()
        {
            rotation = new Quaternion(0.0f,0.0f,0.0f);
            position = new Vector3(0.0f,0.0f,0.0f);
            scale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        
		public Matrix4 localToWorldMatrix
        {
            get
            {
                return Matrix4.CreateScale(scale) * Matrix4.CreateFromQuaternion(rotation) * Matrix4.CreateTranslation(position);
            }
        }

		public Matrix4 worldToLocalMatrix
		{
			get
			{
				return localToWorldMatrix.Inverted();
			}
		}

		public Transform parentTransform 
		{
			get 
			{
				return _parent;
			}
		}

		public void SetParent(Transform transform) 
		{
			_parent = transform;
		}

		public int childCount 
		{
			get 
			{
				return _children.Count;
			}
		}

		public Transform GetChild(int index) 
		{
			return _children[index];
		}

		public Vector3 forward 
		{
			get
			{
				return _forward;
			}
		}

		public Vector3 up
		{
			get
			{
				return _up;
			}
		}

		public Vector3 right
		{
			get
			{
				return _right;
			}
		}

		//TODO correct look at
		public void LookAt(Vector3 target) 
		{
			//rotation = Matrix4.LookAt(position, target, up).ExtractRotation();
		}

		public void Translate(Vector3 translation)
		{
			position += translation;
		}

		public void UpdateTransform() 
		{
			_forward =  (Matrix3.CreateFromQuaternion(rotation) * new Vector3(0.0f, 0.0f, 1.0f)).Normalized();
			_right = (Matrix3.CreateFromQuaternion(rotation) * new Vector3(1.0f, 0.0f, 0.0f)).Normalized();
			_up = (Matrix3.CreateFromQuaternion(rotation) * new Vector3(0.0f, 1.0f, 0.0f)).Normalized();
		}

		public Quaternion rotation;
		public Vector3 position;
		public Vector3 scale;

		private Vector3 _up = new Vector3(0.0f, 1.0f, 0.0f);
		private Vector3 _right = new Vector3(1.0f, 0.0f, 0.0f);
		private Vector3 _forward = new Vector3(0.0f, 0.0f, 1.0f);

		private Transform _parent = null;
		private List<Transform> _children = new List<Transform>();
    }
}
