// -----------------------------------------------------------------------
// <copyright file="SpriteHelpers.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Nintenlord.ROMHacking.GBA.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class SpriteHelpers
    {
        private static IEnumerable<T> Enumerate<T>(T[,] items, Rectangle rect)
        {
            for (int i = rect.Left; i < rect.Right; i++)
            {
                for (int j = rect.Top; j < rect.Bottom; j++)
                {
                    yield return items[i, j];
                }
            }
        }
        
        private static IEnumerable<KeyValuePair<Point, T>> EnumeratePoints<T>(T[,] items, Rectangle rect)
        {
            for (int i = rect.Left; i < rect.Right; i++)
            {
                for (int j = rect.Top; j < rect.Bottom; j++)
                {
                    yield return new KeyValuePair<Point, T>(new Point(i, j), items[i, j]);
                }
            }
        }
    }
}
/*          Size  Square   Horizontal  Vertical
          0     8x8      16x8        8x16
          1     16x16    32x8        8x32
          2     32x32    32x16       16x32
          3     64x64    64x32       32x64
 */