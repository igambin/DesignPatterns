using System.Collections.Generic;

namespace IG.DesignPatternsLibrary.Patterns.Builder
{
    public interface IProduct
    {
        string Name { get; }

        List<string> Components { get; set; }

        Dictionary<string, object> Configurations { get; set; } 
    }
}