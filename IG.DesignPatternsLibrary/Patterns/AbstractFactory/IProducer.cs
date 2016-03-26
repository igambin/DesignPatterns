namespace IG.DesignPatternsLibrary.Patterns.AbstractFactory
{
    public interface IProducer<out TProduct>
        where TProduct : IProduct
    {
        TProduct Create { get; }
    }
}