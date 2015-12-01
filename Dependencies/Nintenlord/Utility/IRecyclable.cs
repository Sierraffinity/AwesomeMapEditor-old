using System;
using System.Collections.Generic;
using System.Text;

namespace Nintenlord
{
    public interface IRecyclable
    {
        bool Used
        {
            get;
        }
    }

    public interface IRecycler
    {
        void Recycle(IRecyclable item);
    }
}
