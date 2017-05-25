using System;
using OpenTK;

namespace MonoKnight
{
	public class Serializer
	{
		static public T Deserialize<T>(string str)
		{
			if (typeof(T) == typeof(Vector3))
			{
				return (T)(object)DeserializeVector3(str);
			}

			if (typeof(T) == typeof(Vector4))
			{
				return (T)(object)DeserializeVector4(str);
			}

			if (typeof(T) == typeof(Vector2))
			{
				return (T)(object)DeserializeVector2(str);
			}

			if (typeof(T) == typeof(Quaternion))
			{
				return (T)(object)DeserializeQuaternion(str);
			}

			if (typeof(T) == typeof(Matrix4))
			{
				return (T)(object)DeserializeMatrix4(str);
			}
			return default(T);
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
