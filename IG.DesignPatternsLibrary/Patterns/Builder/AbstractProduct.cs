using System.Collections.Generic;

namespace IG.DesignPatternsLibrary.Patterns.Builder
{
    public abstract class AbstractProduct : IProduct
    {
        private static readonly Dictionary<string, int> Counters = new Dictionary<string, int>();

        private string ObjectName => GetType().Name;

        public int Counter { get; }

        public virtual string Name => $"{ObjectName}{Counter}";

        protected AbstractProduct()
        {
            if (!Counters.ContainsKey(ObjectName))
            {
                Counters[ObjectName] = 0;
            }
            Counter = ++Counters[ObjectName];

            Components = new List<string>();
            Configurations = new Dictionary<string, object>();
        }

        public List<string> Components { get; set; }

        public Dictionary<string, object> Configurations { get; set; }
    }
}
