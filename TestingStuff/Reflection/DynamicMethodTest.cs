using System;
using System.Reflection.Emit;

namespace TestingStuff.Reflection
{
    class DynamicMethodTest
    {
        public static void Test1()
        {
            var dynamicMethod = new DynamicMethod("Foo", null, null, typeof(DynamicMethodTest));
            var generator = dynamicMethod.GetILGenerator();
            var consoleWriteLine = typeof(Console).GetMethod("WriteLine", new[] { typeof(string) });
            generator.Emit(OpCodes.Ldstr, "Wazzzzuuuupppp");
            generator.Emit(OpCodes.Call, consoleWriteLine);
            generator.Emit(OpCodes.Ret);
            dynamicMethod.Invoke(null, null);
        }

        public static void Test2()
        {
            var consoleWriteLine = typeof(Console).GetMethod("WriteLine", new[] { typeof(int) });
            var dynamicMethod = new DynamicMethod("Bar", null, null, typeof(void));
            var generator = dynamicMethod.GetILGenerator();
            generator.Emit(OpCodes.Ldc_I4, 1);
            generator.Emit(OpCodes.Ldc_I4, 10);
            generator.Emit(OpCodes.Ldc_I4, 2);
            generator.Emit(OpCodes.Div);
            generator.Emit(OpCodes.Add);
            generator.Emit(OpCodes.Call, consoleWriteLine);
            generator.Emit(OpCodes.Ret);
            dynamicMethod.Invoke(null, null);
        }

        public static void Test3()
        {
            var dynamicMethod = new DynamicMethod("Baz", typeof(int), new []{typeof(int), typeof(int)}, typeof(void));
            var generator = dynamicMethod.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Add);
            generator.Emit(OpCodes.Ret);

            var func = (AddDelegate)dynamicMethod.CreateDelegate(typeof(AddDelegate));
            Console.WriteLine(func(5, 8));
        }

        delegate int AddDelegate(int x, int y);
    }
}
