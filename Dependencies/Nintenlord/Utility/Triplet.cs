using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Utility
{
    public class Triplet<TKey1,TKey2,TValue>
    {
        public TKey1 Key1
        {
            get;
            private set;
        }
        public TKey2 Key2
        {
            get;
            private set;
        }
        public TValue Value
        {
            get;
            private set;
        }

        public Triplet()
        {
            this.Key1 = default(TKey1);
            this.Key2 = default(TKey2);
            this.Value = default(TValue);
        }
        public Triplet(TKey1 key1, TKey2 key2, TValue value)
        {
            this.Key1 = key1;
            this.Key2 = key2;
            this.Value = value;
        }

        public Triplet(KeyValuePair<TKey1, TKey2> key, TValue value)
        {
            this.Key1 = key.Key;
            this.Key2 = key.Value;
            this.Value = value;
        }

        public static implicit operator KeyValuePair<KeyValuePair<TKey1,TKey2>,TValue>
            (Triplet<TKey1,TKey2,TValue> toConvert)
        {
            return new KeyValuePair<KeyValuePair<TKey1, TKey2>, TValue>(
                       new KeyValuePair<TKey1, TKey2>(toConvert.Key1, toConvert.Key2), 
                       toConvert.Value
                       );
        }

        public static implicit operator Triplet<TKey1, TKey2, TValue>
            (KeyValuePair<KeyValuePair<TKey1, TKey2>, TValue> toConvert)
        {
            return new Triplet<TKey1, TKey2, TValue>(toConvert.Key, toConvert.Value);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", Key1, Key2, Value);
        }
    }
}
