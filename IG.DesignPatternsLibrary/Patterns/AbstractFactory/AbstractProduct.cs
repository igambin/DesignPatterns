using System.Collections.Generic;

namespace IG.DesignPatternsLibrary.Patterns.AbstractFactory
{
    public abstract class AbstractProduct : IProduct
    {
        private static readonly Dictionary<string, int> Counters = new Dictionary<string, int>();

        private string ObjectName => GetType().Name;

        public int Counter { get; }

        protected AbstractProduct()
        {
            if (!Counters.ContainsKey(ObjectName))
            {
                Counters[ObjectName] = 0;
            }
            Counter = ++Counters[ObjectName];
        }

        public virtual string Name => $"{ObjectName}{Counter}";
    }
}
