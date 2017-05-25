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
        
		[DataMember]
		public Quaternion rotation;
		[DataMember]
		public Vector3 position;
		[DataMember]
		public Vector3 scale;

		public Matrix4 localMatrix
		{
			get
			{
				return _localMatrix;
			}
		}

		//public void Test()
		//{
		//	var fields = typeof(Transform).GetFields();
		//	foreach (var field in fields)
		//	{ 
		//		var attrs = Attribute.GetCustomAttributes(field);
		//		foreach (var attr in attrs)
		//		{
		//			if (attr is DataMember)
		//			{
						
		//			}
		//		}
		//	}
		//}

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
			var righthandPos = new Vector3(-1.0f * position.X, position.Y, position.Z);
			_localMatrix = Matrix4.CreateScale(scale) * Matrix4.CreateFromQuaternion(rotation) * Matrix4.CreateTranslation(righthandPos);
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

		public Transform parent
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

		public void RemoveChild(Transform transform)
		{
			_children.Remove(transform);
		}

		public Vector3 forward 
		{
			get
			{
				return (Matrix3.CreateFromQuaternion(rotation) * _forward).Normalized();
			}
		}

		public Vector3 up
		{
			get
			{
				return (Matrix3.CreateFromQuaternion(rotation) * _up).Normalized();
			}
		}

		public Vector3 right
		{
			get
			{
				return (Matrix3.CreateFromQuaternion(rotation) * _right).Normalized();
			}
		}

		////TODO correct look at
		public void LookAt(Vector3 target) 
		{
			rotation = rotation * Matrix4.LookAt(position, target, up).Inverted().ExtractRotation();
		}

		public void Translate(Vector3 translation)
		{
			position += translation;
		}

		public Transform root
		{
			get
			{
				if (parent != null)
				{
					return parent.root;
				}
				else
				{
					return this;
				}
			}
		}

		private Vector3 _up = new Vector3(0.0f, 1.0f, 0.0f);
		//TODO Left or right handed coordinate
		private Vector3 _right = new Vector3(1.0f, 0.0f, 0.0f);
		private Vector3 _forward = new Vector3(0.0f, 0.0f, 1.0f);

		private Transform _parent = null;
		public List<Transform> _children = new List<Transform>();

		private Matrix4 _localMatrix = Matrix4.Identity;
		private Matrix4 _worldMatrix = Matrix4.Identity;
    }
}
