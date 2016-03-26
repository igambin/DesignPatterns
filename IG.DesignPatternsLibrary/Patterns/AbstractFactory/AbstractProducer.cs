namespace IG.DesignPatternsLibrary.Patterns.AbstractFactory
{
    public abstract class AbstractProducer<TProduct> : IProducer<TProduct>
        where TProduct : IProduct, new()
    {
        public virtual TProduct Create => new TProduct();

    }
}