using IG.DesignPatternsLibrary.Patterns.Builder;

namespace IG.DesignPatternsLibrary.Examples.Builder
{
    public class RoadsterBuilder : AbstractBuilder<Car>
    {
        public override void AddComponents()
        {
            Product.Components.AddRange(new[] { "Small Wheels", "Powerful Engine" });
        }

        public override void ConfigureProduct()
        {
            Product.Configurations["Color"] = "red";
        }
    }
}