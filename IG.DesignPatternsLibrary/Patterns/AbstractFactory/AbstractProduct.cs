using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IG.DesignPatternsLibrary.Patterns.AbstractFactory
{
    public abstract class AbstractProduct : IProduct
    {
        private static readonly Dictionary<string, int> Counters = new Dictionary<string, int>();

        private string ObjectName => this.GetType().Name;

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
