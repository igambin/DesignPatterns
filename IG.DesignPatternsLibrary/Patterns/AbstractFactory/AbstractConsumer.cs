using System;

namespace IG.DesignPatternsLibrary.Patterns.AbstractFactory
{
    public abstract class AbstractConsumer<TProduct> : IConsumer<TProduct>
        where TProduct : IProduct
    {

        public abstract string Processor { get; }           

        public virtual string Consume(TProduct product)
        {
            var result = $"{Processor}ing {product.Name}";
            Console.WriteLine(result);
            return result;
        }
    }
}