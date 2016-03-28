namespace IG.DesignPatternsLibrary.Patterns.Builder
{
    public interface IBuilder<out TProduct>
        where TProduct : IProduct
    {
        void BuildProduct();

        void AddComponents();

        void ConfigureProduct();

        TProduct GetProduct();

    }
}