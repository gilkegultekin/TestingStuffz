using System;

namespace TestingStuff.Reflection
{
	public class MethodTest
	{
		delegate string StringToString(string s);

		delegate bool StringToBool(string source, string s);

		public static void Test1()
		{
			var trimMethod = typeof(string).GetMethod("Trim", new Type[0]);
			var trimDelegate = (StringToString)Delegate.CreateDelegate(typeof(StringToString), trimMethod);

			for (int i = 0; i < 10; i++)
			{
				var trimmed = trimDelegate(" test ");
			}
		}

		public static void Test2()
		{
			var containsMethod = typeof(string).GetMethod("Contains", new [] { typeof(string) });
			var containsDelegate = (StringToBool)Delegate.CreateDelegate(typeof(StringToBool), containsMethod);

			for (int i = 0; i < 10; i++)
			{
				var contains = containsDelegate("test", "es");
			}
		}
	}
}
