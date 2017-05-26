using System;
using OpenTK;
using ProtoBuf;
using System.Collections.Generic;

namespace MonoKnight
{
	[ProtoContract]
	public class ComponentInfo
	{
		public ComponentInfo()
		{
		}

		[ProtoMember(1)]
		public string Name = "";

		[ProtoMember(2)]
		public Dictionary<string, MemberInfo> Props = new Dictionary<string, MemberInfo>();
	}

	[ProtoContract]
	public class MemberInfo
	{
		[ProtoMember(1)]
		public string TypeName = "";
		[ProtoMember(2)]
		public string ValueString = "";	
	}

	public class Serializer
	{
		static public T DeserializeValue<T>(string str)
		{
			return (T)DeserializeValue(typeof(T), str);
		}

		static public object DeserializeValue(Type type, string str)
		{
			if (type == typeof(Vector3))
			{
				return (object)DeserializeVector3(str);
			}

			if (type == typeof(Vector4))
			{
				return (object)DeserializeVector4(str);
			}

			if (type == typeof(Vector2))
			{
				return (object)DeserializeVector2(str);
			}

			if (type == typeof(Quaternion))
			{
				return (object)DeserializeQuaternion(str);
			}

			if (type == typeof(Matrix4))
			{
				return (object)DeserializeMatrix4(str);
			}

			if (type == typeof(float))
			{
				return (object)float.Parse(str);
			}

			if (type == typeof(double))
			{
				return (object)double.Parse(str);
			}

			if (type == typeof(int))
			{
				return (object)int.Parse(str);
			}

			if (type == typeof(string))
			{
				return (object)str;
			}

			return default(object);
		}

		static private ComponentInfo SerializeComponent(Component com)
		{
			var comInfo = new ComponentInfo();

			var type = com.GetType();
			comInfo.Name = type.ToString();

			var fields = type.GetFields();
			foreach (var field in fields)
			{
				var atrrs = Attribute.GetCustomAttributes(field);
				foreach (var atrr in atrrs)
				{
					if (atrr is DataMember)
					{
						var memberInfo = new MemberInfo();
						memberInfo.TypeName = field.FieldType.ToString();
						memberInfo.ValueString = field.GetValue(com).ToString();
						comInfo.Props[field.Name] = memberInfo;
					}
				}
			}
			return comInfo;
		}

		static private Component DeserializeComponent(ComponentInfo cominfo)
		{
			Type type = Type.GetType(cominfo.Name);
			Component component = Activator.CreateInstance(type) as Component;
			foreach (var pair in cominfo.Props)
			{
				var field = type.GetField(pair.Key);
				var valueType = Type.GetType(pair.Value.TypeName);
				field.SetValue(component, DeserializeValue(valueType, pair.Value.ValueString));
			}
			return component;
		}

		static private Matrix4 DeserializeMatrix4(string str)
		{
			//V: (0, 0, 0), W: 1
			str = str.Trim(new char[] { '(', ')', ' ' });
			string[] nums = str.Split(',');
			return new Matrix4(
				float.Parse(nums[0]), float.Parse(nums[1]), float.Parse(nums[2]), float.Parse(nums[3]),
				float.Parse(nums[4]), float.Parse(nums[5]), float.Parse(nums[6]), float.Parse(nums[7]),
				float.Parse(nums[8]), float.Parse(nums[9]), float.Parse(nums[10]), float.Parse(nums[11]),
				float.Parse(nums[12]), float.Parse(nums[13]), float.Parse(nums[14]), float.Parse(nums[15])
			);
		}

		static private Quaternion DeserializeQuaternion(string str)
		{
			//V: (0, 0, 0), W: 1
			str = str.Trim(new char[] { '(', ')', ' ', 'V', 'W', ':' });
			string[] nums = str.Split(',');
			return new Quaternion(float.Parse(nums[0]), float.Parse(nums[1]), float.Parse(nums[2]), float.Parse(nums[3]));
		}

		static private Vector3 DeserializeVector3(string str)
		{
			//(0, 0, -10)
			str = str.Trim(new char[] { '(', ')', ' ' });
			string[] nums = str.Split(',');
			return new Vector3(float.Parse(nums[0]), float.Parse(nums[1]), float.Parse(nums[2]));
		}

		static private Vector4 DeserializeVector4(string str)
		{
			//(0, 0, -10)
			str = str.Trim(new char[] { '(', ')', ' ' });
			string[] nums = str.Split(',');
			return new Vector4(float.Parse(nums[0]), float.Parse(nums[1]), float.Parse(nums[2]), float.Parse(nums[3]));
		}

		static private Vector2 DeserializeVector2(string str)
		{
			//(0, 0, -10)
			str = str.Trim(new char[] { '(', ')', ' ' });
			string[] nums = str.Split(',');
			return new Vector2(float.Parse(nums[0]), float.Parse(nums[1]));
		}
	}
}
