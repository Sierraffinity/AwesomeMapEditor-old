using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Event_Assembler.Core.Collections;
using Nintenlord.Collections;

namespace Nintenlord.Event_Assembler.Core.Code.Language
{
    public class Context
    {
        private List<Dictionary<string, int>> allScopes;
        private Stack<Dictionary<string, int>> currentScopes;
        private int labelScode;
        private int offset;

        public Context()
        {
            allScopes = new List<Dictionary<string, int>>();
            currentScopes = new Stack<Dictionary<string, int>>();
            offset = 0;
            labelScode = 0;
        }

        public int Offset
        {
            get { return offset; }
            set
            {
                offset = value;
            }
        }
        public int ScopesOnStack
        {
            get { return currentScopes.Count; }
        }

        public void AddLabel(string name)
        {
            currentScopes.Peek().Add(name, offset);
        }

        public bool TryGetLabelOffset(string name, out int offset)
        {
            return currentScopes.TryGetKey(name, out offset);
        }

        public void AddNewScope()
        {
            var newScope = new Dictionary<string, int>();
            currentScopes.Push(newScope);
            allScopes.Add(newScope);
        }

        public void EndScope()
        {
            currentScopes.Pop();
        }


        public void StartFromFirstScope()
        {
            labelScode = 0;
        }

        public bool MoveToNextScope()
        {
            labelScode++;
            if (labelScode < allScopes.Count)
            {
                currentScopes.Push(allScopes[labelScode]);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
