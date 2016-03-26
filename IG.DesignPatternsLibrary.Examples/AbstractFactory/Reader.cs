using IG.DesignPatternsLibrary.Patterns.AbstractFactory;

namespace IG.DesignPatternsLibrary.Examples.AbstractFactory
{
    internal class Reader : AbstractConsumer<Book>
    {
        public override string Processor => "Read";
    }
}