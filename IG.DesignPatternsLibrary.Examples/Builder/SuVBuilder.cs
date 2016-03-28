using IG.DesignPatternsLibrary.Patterns.Builder;

namespace IG.DesignPatternsLibrary.Examples.Builder
{
    public class SuVBuilder : AbstractBuilder<Car>
    {
        public override void AddComponents()
        {
            Product.Components.AddRange(new [] { "Big Wheels", "Heavy Engine"});
        }

        public override void ConfigureProduct()
        {
            Product.Configurations["Color"] = "blue";
        }
    }
}