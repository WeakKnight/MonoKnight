using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace MonoKnight
{
	//public delegate void TransformChangedEventHandler(object sender, EventArgs e);

	public class Transform : Component
    {
        public Transform()
        {
            rotation = Quaternion.Identity;
            position = new Vector3(0.0f,0.0f,0.0f);
			scale = Vector3.One;
        }
        
		//public event TransformChangedEventHandler Changed;

		//private void onChanged(EventArgs e)
		//{
		//	if (Changed != null)
		//	{
		//		//Changed(this, e);
		//		foreach (var item in _children)
		//		{
		//			item.Changed(item, e);
		//		}
		//	}
		//}

		public Matrix4 localMatrix
		{
			get
			{
				return _localMatrix;
			}
		}

		//TODO
		public Matrix4 localToWorldMatrix
        {
            get
            {
				return _worldMatrix;
                //return Matrix4.CreateScale(scale) * Matrix4.CreateFromQuaternion(rotation) * Matrix4.CreateTranslation(position);
            }
        }

		//TODO
		public Matrix4 worldToLocalMatrix
		{
			get
			{
				return _worldMatrix.Inverted();
				//return localToWorldMatrix.Inverted();
			}
		}

		public void UpdateLocal() 
		{
			_localMatrix = Matrix4.CreateScale(scale) * Matrix4.CreateFromQuaternion(rotation) * Matrix4.CreateTranslation(position);
		}

		public void UpdateWorld()
		{
			UpdateLocal();
			UpdateWorldInternal();
		}

		private void UpdateWorldInternal()
		{
			if (_parent != null)
			{
				_parent.UpdateWorld();
				Matrix4.Mult(ref _localMatrix, ref _parent._worldMatrix, out _worldMatrix);
			}
			else
			{
				_worldMatrix = _localMatrix;
			}
		}

		public Transform parentTransform 
		{
			get 
			{
				return _parent;
			}
			set
			{
				if (_parent == value)
				{
					return;
				}
				else if (_parent == null)
				{
					_parent = value;
					_parent._children.Add(this);
				}
				else
				{
					_parent._children.Remove(this);
					_parent = value;
					_parent._children.Add(this);
				}
			}
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
			rotation = rotation * Matrix4.LookAt(position, target, up).Inverted().ExtractRotation();
			UpdateTransform();
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

		public Transform root
		{
			get
			{
				if (parentTransform != null)
				{
					return parentTransform.root;
				}
				else
				{
					return this;
				}
			}
		}

		private Vector3 _up = new Vector3(0.0f, 1.0f, 0.0f);
		private Vector3 _right = new Vector3(1.0f, 0.0f, 0.0f);
		private Vector3 _forward = new Vector3(0.0f, 0.0f, 1.0f);

		private Transform _parent = null;
		public List<Transform> _children = new List<Transform>();

		private Matrix4 _localMatrix = Matrix4.Identity;
		private Matrix4 _worldMatrix = Matrix4.Identity;
    }
}
