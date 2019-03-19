using System;
using System.Collections.Generic;
using System.Text;

namespace TestingStuff.EFCore
{
    public class TestModel
    {
        public Guid Id { get; set; }

        public int Value { get; set; }

        public TestModel(Guid id, int value)
        {
            Id = id;
            Value = value;
        }
    }
}
