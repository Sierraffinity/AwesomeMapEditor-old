using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FETextEditor
{
    public interface IUserInterface
    {
        int MaxIndex { get; set; }
        void SetDisplayText(string text);
    }
}
