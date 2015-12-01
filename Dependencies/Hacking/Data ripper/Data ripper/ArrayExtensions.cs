using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_ripper
{
    public static class ArrayExtensions
    {
        public static int AmountOfSame<T>(T[] array1, int index1, T[] array2, int index2)
        {
            int length = Math.Min(array1.Length - index1, array2.Length - index2);
            int i;
            for (i = 0; i < length; i++)
            {
                if (!array1[index1 + i].Equals(array2[index2 + i]))
                {
                    return i;
                }
            }
            return i;
        }
    }
}
