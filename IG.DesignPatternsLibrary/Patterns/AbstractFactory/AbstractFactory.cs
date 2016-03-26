namespace IG.DesignPatternsLibrary.Patterns.AbstractFactory
{
    public abstract class AbstractFactory<TProducer, TConsumer, TProduct>
        where TProducer : IProducer<TProduct>, new() 
        where TConsumer : IConsumer<TProduct>, new() 
        where TProduct  : IProduct
    {

        protected AbstractFactory()
        {
            Producer = new TProducer();
            Consumer = new TConsumer();
        }

        public TProducer Producer { get; }

        public TConsumer Consumer { get; }

        public TProduct Create => Producer.Create;

        public string Consume(TProduct product) => Consumer.Consume(product);

    }

 }
