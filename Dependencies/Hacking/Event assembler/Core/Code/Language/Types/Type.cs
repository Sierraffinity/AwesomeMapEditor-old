// -----------------------------------------------------------------------
// <copyright file="Type.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.Event_Assembler.Core.Code.Language.Types
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nintenlord.Collections;

    /// <summary>
    /// Class representing type information.
    /// </summary>
    public class Type
    {
        public readonly MetaType type;
        public readonly MetaType[] vectorParameterTypes;

        public int ParameterCount
        {
            get { return vectorParameterTypes.Length; }
        }

        private Type(MetaType type)
        {
            this.type = type;
            this.vectorParameterTypes = null;
        }

        private Type(IEnumerable<MetaType> parameters)
        {
            this.type = MetaType.Vector;
            this.vectorParameterTypes = parameters.ToArray();
        }

        public static readonly Type Atom = new Type(MetaType.Atom);

        private static Dictionary<int, Type> vectorTypes = new Dictionary<int,Type>();
        public static Type Vector(int paramCount)
        {
            Type result;
            if (!vectorTypes.TryGetValue(paramCount, out result))
            {
                result = vectorTypes[paramCount] = new Type(Repeat(paramCount, MetaType.Atom));
            }
            return result;
        }

        public static IEnumerable<MetaType> Repeat(int count, MetaType toRepeat)
        {
            for (int i = 0; i < count; i++)
            {
                yield return toRepeat;
            }
        }

        public override string ToString()
        {
            if (vectorParameterTypes  != null)
            {
                return vectorParameterTypes.ToElementWiseString(", ", "[", "]");
            }
            else
            {
                return type.ToString();
            }
        }
    }
}
