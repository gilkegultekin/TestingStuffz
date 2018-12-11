using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace TestingStuff
{
    public class ImmutableStackPlayground
    {
        public void RandomStuff()
        {
            var stack = ImmutableStack.Create<string>();
            var stackAfterFirstPush = stack.Push("1");
            Console.WriteLine(stack.ConvertContentsToString());
            Console.WriteLine(stackAfterFirstPush.ConvertContentsToString());
            var stackAfterSecondPush = stack.Push("2");
            Console.WriteLine(stack.ConvertContentsToString());
            Console.WriteLine(stackAfterFirstPush.ConvertContentsToString());
            Console.WriteLine(stackAfterSecondPush.ConvertContentsToString());
        }
    }

    public static class ImmutableStackExtensions
    {
        public static string ConvertContentsToString<T>(this ImmutableStack<T> stack)
        {
            var contents = string.Join(" ", stack);
            return string.IsNullOrWhiteSpace(contents) ? "Empty" : contents;
        }
    }
}
