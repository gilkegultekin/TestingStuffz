using System.Linq;

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
