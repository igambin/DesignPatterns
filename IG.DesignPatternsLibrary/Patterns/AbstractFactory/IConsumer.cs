namespace IG.DesignPatternsLibrary.Patterns.AbstractFactory
{
    public interface IConsumer<TProduct>
        where TProduct : IProduct
    {
        string Consume(TProduct product);
    }
}