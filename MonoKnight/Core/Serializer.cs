using System;
using OpenTK;
using ProtoBuf;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MonoKnight
{
	[ProtoContract]
	public class SceneInfo
	{
		[ProtoMember(1)]
		public List<GameObjectInfo> ItemList = new List<GameObjectInfo>();
	}

	[ProtoContract]
	public class GameObjectInfo
	{
		[ProtoMember(1)]
		public List<ComponentInfo> ComInfoList = new List<ComponentInfo>();
		[ProtoMember(2)]
		public List<GameObjectInfo> ChildrenList = new List<GameObjectInfo>();
	}

	[ProtoContract]
	public class ComponentInfo
	{
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

	public static class Serializer
	{
		static public T DeserializeValue<T>(string str)
		{
			return (T)DeserializeValue(typeof(T), str);
		}

		static public object DeserializeValue(Type type, string str)
		{
			if (type == typeof(Vector3))
			{
				return DeserializeVector3(str);
			}

			if (type == typeof(Vector4))
			{
				return DeserializeVector4(str);
			}

			if (type == typeof(Vector2))
			{
				return DeserializeVector2(str);
			}

			if (type == typeof(Quaternion))
			{
				return DeserializeQuaternion(str);
			}

			if (type == typeof(Matrix4))
			{
				return DeserializeMatrix4(str);
			}

			if (type == typeof(float))
			{
				return float.Parse(str);
			}

			if (type == typeof(double))
			{
				return double.Parse(str);
			}

			if (type == typeof(int))
			{
				return int.Parse(str);
			}

			if (type == typeof(string))
			{
				return str;
			}

			return default(object);
		}

		static public SceneInfo SerializeScene(Scene scene)
		{
			SceneInfo result = new SceneInfo();
			foreach (var item in scene.ItemList)
			{
				var itemInfo = SerializeEntity(item);
				result.ItemList.Add(itemInfo);
			}
			return result;
		}

		static public Scene DeserializeScene(SceneInfo sceneInfo)
		{
			Scene result = new Scene();
			foreach (var itemInfo in sceneInfo.ItemList)
			{
				var item = DeserializeEntity(itemInfo);
				result.AddItem(item);
			}
			return result;
		}

		static public GameObjectInfo SerializeEntity(Entity entity)
		{
			var result = new GameObjectInfo();

			foreach (var com in entity.GetAllComponents())
			{
				result.ComInfoList.Add(SerializeComponent(com));
			}

			if (entity.transform != null)
			{
				foreach (var childTransform in entity.transform._children)
				{
					var child = childTransform.entity;
					result.ChildrenList.Add(SerializeEntity(child));
				}
			}

			return result;
		}

		static public Entity DeserializeEntity(GameObjectInfo entityInfo)
		{
			var result = new Entity();

			foreach (var comInfo in entityInfo.ComInfoList)
			{
				var com = DeserializeComponent(comInfo);
				result.AddComponent(com);
			}

			foreach (var childInfo in entityInfo.ChildrenList)
			{
				var child = DeserializeEntity(childInfo);
				child.GetComponent<Transform>().parent = result.GetComponent<Transform>();
			}

			return result;
		}

		static public ComponentInfo SerializeComponent(Component com)
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
						var assemblyQualifiedName = field.FieldType.AssemblyQualifiedName.ToString();
						memberInfo.TypeName = assemblyQualifiedName;//field.FieldType.ToString()+","+assemblyQualifiedName;
						memberInfo.ValueString = field.GetValue(com).ToString();
						comInfo.Props[field.Name] = memberInfo;
					}
				}
			}
			return comInfo;
		}

		static public Component DeserializeComponent(ComponentInfo cominfo)
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

		static string TrimString(string str)
		{
			var result = Regex.Replace(str, @"\:+|\(+|\)+|V+|W+|\s+", "");
			return result;
		}

		static Matrix4 DeserializeMatrix4(string str)
		{
			//V: (0, 0, 0), W: 1
			str = TrimString(str);
			string[] nums = str.Split(',');
			return new Matrix4(
				float.Parse(nums[0]), float.Parse(nums[1]), float.Parse(nums[2]), float.Parse(nums[3]),
				float.Parse(nums[4]), float.Parse(nums[5]), float.Parse(nums[6]), float.Parse(nums[7]),
				float.Parse(nums[8]), float.Parse(nums[9]), float.Parse(nums[10]), float.Parse(nums[11]),
				float.Parse(nums[12]), float.Parse(nums[13]), float.Parse(nums[14]), float.Parse(nums[15])
			);
		}

		static Quaternion DeserializeQuaternion(string str)
		{
			//V: (0, 0, 0), W: 1
			str = TrimString(str);
			string[] nums = str.Split(',');
			return new Quaternion(float.Parse(nums[0]), float.Parse(nums[1]), float.Parse(nums[2]), float.Parse(nums[3]));
		}

		static Vector3 DeserializeVector3(string str)
		{
			//(0, 0, -10)
			str = TrimString(str);
			string[] nums = str.Split(',');
			return new Vector3(float.Parse(nums[0]), float.Parse(nums[1]), float.Parse(nums[2]));
		}

		static Vector4 DeserializeVector4(string str)
		{
			//(0, 0, -10)
			str = TrimString(str);
			string[] nums = str.Split(',');
			return new Vector4(float.Parse(nums[0]), float.Parse(nums[1]), float.Parse(nums[2]), float.Parse(nums[3]));
		}

		static Vector2 DeserializeVector2(string str)
		{
			//(0, 0, -10)
			str = TrimString(str);
			string[] nums = str.Split(',');
			return new Vector2(float.Parse(nums[0]), float.Parse(nums[1]));
		}
	}
}
