using IG.DesignPatternsLibrary.Patterns.AbstractFactory;

namespace IG.DesignPatternsLibrary.Examples.AbstractFactory
{
    internal class AppleEater : AbstractConsumer<Apple>
    {
        public override string Processor => "Eat";
    }
}