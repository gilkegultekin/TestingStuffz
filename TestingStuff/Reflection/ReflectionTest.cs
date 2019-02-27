using System;
using System.Linq;
using System.Reflection;

namespace TestingStuff.Reflection
{
    public class ReflectionTest
    {
        public static void Test()
        {
            var derivedInterfaceList = typeof(Derived).GetInterfaces();
            var derivedImplementsInterface = derivedInterfaceList.Contains(typeof(IImplement));

            var declaringTypeInterfaceList = typeof(Derived).DeclaringType.GetInterfaces();
            var declaringTypeImplementsInterface = declaringTypeInterfaceList.Contains(typeof(IImplement));

            var baseTypeInterfaceList = typeof(Derived).BaseType.GetInterfaces();
            var baseTypeImplementsInterface = baseTypeInterfaceList.Contains(typeof(IImplement));
        }

        public static void Test2()
        {
            var test1 = new PropertyGetterTest(3);

            var test2 = new PropertyGetterTest(5);

            var propertyInfo = typeof(PropertyGetterTest).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.Name == "Value");

            var propertyGetterDelegate1 = (Func<int>)Delegate.CreateDelegate(typeof(Func<int>), test1, propertyInfo.GetMethod);
            var propertyGetterDelegate2 = (Func<int>)Delegate.CreateDelegate(typeof(Func<int>), test2, propertyInfo.GetMethod);
            

            var result1 = propertyGetterDelegate1();
            var result2 = propertyGetterDelegate2();

            try
            {
                var propertyGetterDelegate3 =
                    (Func<PropertyGetterTest, int>)Delegate.CreateDelegate(typeof(Func<PropertyGetterTest, int>), null, propertyInfo.GetMethod);
                var result3 = propertyGetterDelegate3(test1);
                var result4 = propertyGetterDelegate3(test2);
            }
            catch (Exception e)
            {
            }
        }

        public static void Test3()
        {
            var test1 = new PropertyGetterTest(3);

            var test2 = new PropertyGetterTest(5);

            var propertyInfo = typeof(PropertyGetterTest).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.Name == "ConstantValue");

            var propertyGetterDelegate1 = (Func<int>)Delegate.CreateDelegate(typeof(Func<int>), test1, propertyInfo.GetMethod);
            var propertyGetterDelegate2 = (Func<int>)Delegate.CreateDelegate(typeof(Func<int>), test2, propertyInfo.GetMethod);

            var result1 = propertyGetterDelegate1();
            var result2 = propertyGetterDelegate2();
        }

        public static void Test4()
        {
            var test1 = new PropertyGetterTest(3);

            var test2 = new PropertyGetterTest(5);

            var propertyInfo = typeof(PropertyGetterTest).GetProperties(BindingFlags.Static | BindingFlags.Public)
                .FirstOrDefault(p => p.Name == "StaticValue");

            var propertyGetterDelegate1 = (Func<int>)Delegate.CreateDelegate(typeof(Func<int>), null, propertyInfo.GetMethod);
            var result1 = propertyGetterDelegate1();

            try
            {
                var propertyGetterDelegate2 = (Func<PropertyGetterTest, int>)Delegate.CreateDelegate(typeof(Func<PropertyGetterTest, int>), test1, propertyInfo.GetMethod);
                var result2 = propertyGetterDelegate2(test1);
                var result3 = propertyGetterDelegate2(test2);
            }
            catch (Exception e)
            {
            }
        }

        public static void Test5()
        {
            var test1 = new PropertyGetterTest(3);

            var methodInfo =
                typeof(PropertyGetterTest).GetMethod("MyMethod", BindingFlags.Instance | BindingFlags.Public);

            var methodDelegate1 = (Func<int, int>) Delegate.CreateDelegate(typeof(Func<int, int>), test1, methodInfo);
            var result1 = methodDelegate1(10);
            try
            {
                var methodDelegate2 = (Func<PropertyGetterTest, int, int>)Delegate.CreateDelegate(typeof(Func<PropertyGetterTest, int, int>), null, methodInfo);
                var result2 = methodDelegate2(test1, 12);

                var methodDelegate3 = (Func<int, int>)Delegate.CreateDelegate(typeof(Func<int, int>), null, methodInfo);
                var result3 = methodDelegate3(13);
            }
            catch (Exception e)
            {
                
            }
        }

        private class PropertyGetterTest
        {
            private int _value;

            public PropertyGetterTest(int value)
            {
                _value = value;
            }

            public int Value
            {
                get
                {
                    return _value;
                }
            }

            public int ConstantValue
            {
                get
                {
                    return 10;
                }
            }

            public static int StaticValue
            {
                get
                {
                    return 15;
                }
            }

            public int MyMethod(int x)
            {
                return x + _value;
            }
        }

        private class Derived : Base
        {

        }

        private class Base : IImplement
        {
            public void NoOp()
            {
                
            }
        }

        private interface IImplement
        {
            void NoOp();
        }
    }


}
