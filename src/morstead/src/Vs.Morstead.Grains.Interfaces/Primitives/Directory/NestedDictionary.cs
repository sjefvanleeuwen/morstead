using System.Collections.Generic;
using System.Linq;

namespace Vs.Morstead.Grains.Interfaces.Primitives.Directory
{
    public class NestedDictionary<K, V> : Dictionary<K, NestedDictionary<K, V>>
    {
        public V Value { set; get; }

        public new NestedDictionary<K, V> this[K key]
        {
            set { base[key] = value; }

            get
            {
                if (!Keys.Contains(key))
                {
                    base[key] = new NestedDictionary<K, V>();
                }
                return base[key];
            }
        }
    }
}
