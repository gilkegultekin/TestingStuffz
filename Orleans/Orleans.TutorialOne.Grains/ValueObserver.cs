using System;
using System.Collections.Generic;
using System.Text;
using Orleans.TutorialOne.GrainInterfaces;

namespace Orleans.TutorialOne.Grains
{
    public class ValueObserver : IValueObserver
    {
        public void ReceiveValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
