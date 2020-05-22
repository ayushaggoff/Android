using System;
using System.Collections.Generic;

namespace Training.Activity
{
    internal class ArrayList<T>
    {
        public ArrayList()
        {
        }

        public static implicit operator List<T>(ArrayList<T> v)
        {
            throw new NotImplementedException();
        }
    }
}