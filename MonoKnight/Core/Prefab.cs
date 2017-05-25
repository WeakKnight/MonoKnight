using System;
using System.Collections.Generic;
using ProtoBuf;

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

	public class Prefab:Resource
	{
		public Prefab()
		{
		}

		public List<ComponentInfo> componentInfos= new List<ComponentInfo>();

		static public Prefab Create(Entity entity)
		{
			var prefab = new Prefab();

			var coms = entity.GetAllComponents();
			foreach (var com in coms)
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

				prefab.componentInfos.Add(comInfo);
			}

			return prefab;
		}
	}
}
