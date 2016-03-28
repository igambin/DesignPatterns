namespace IG.DesignPatternsLibrary.Patterns.Builder
{
    public abstract class AbstractBuilder<TProduct> : IBuilder<TProduct>
        where TProduct : IProduct, new()
    {
        protected TProduct Product;

        public void BuildProduct() {
            Product = new TProduct();
        }

        public TProduct GetProduct() => Product;


        public abstract void AddComponents();

        public abstract void ConfigureProduct();


    }
}