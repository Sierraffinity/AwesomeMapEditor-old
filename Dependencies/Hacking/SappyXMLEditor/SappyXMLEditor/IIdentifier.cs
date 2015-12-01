using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SappyXMLEditor
{
    public interface IIdentifiable<T>
    {
        T Identifier
        {
            get;
        }
    }

    static public class IdentifierHelper
    {
        static public V GetMathingID<V,T>(this IEnumerable<V> emurable,
            T identifier) where V : IIdentifiable<T>
        {
            foreach (V item in emurable)
            {
                if (item.Identifier.Equals(identifier))
                {
                    return item;
                }
            }
            throw new KeyNotFoundException();
        }

        static public bool ContainsMathingID<V, T>(this IEnumerable<V> emurable,
            T identifier) where V : IIdentifiable<T>
        {
            foreach (V item in emurable)
            {
                if (item.Identifier.Equals(identifier))
                {
                    return true;
                }
            }
            return false;
        }

        static public bool TryGetMathingID<V,T>(this IEnumerable<V> emurable,
            T identifier, out V result) where V : IIdentifiable<T>
        {
            result = default(V);

            foreach (V item in emurable)
            {
                if (item.Identifier.Equals(identifier))
                {
                    result = item;
                    return true;
                }
            }
            return false;
        }
    }
}
