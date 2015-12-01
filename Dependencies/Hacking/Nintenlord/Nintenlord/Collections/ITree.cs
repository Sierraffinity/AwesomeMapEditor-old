using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord.Collections
{
    interface ITree<T>
    {
        int Heigth
        {
            get;
        }


        void ForEach(Action<T> action);
    }
}
