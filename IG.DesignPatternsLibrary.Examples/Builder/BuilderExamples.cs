using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IG.DesignPatternsLibrary.Examples.Builder
{
    /// <summary>
    /// Summary description for BuilderExamples
    /// </summary>
    [TestClass]
    public class BuilderExamples
    {
        [TestMethod]
        public void CreateSuvUsingABuilder()
        {
            var builder = new SuVBuilder();
            builder.BuildProduct();
            builder.AddComponents();
            builder.ConfigureProduct();
            var suv = builder.GetProduct();

            Assert.AreEqual("Car1", suv.Name);
            CollectionAssert.AreEquivalent(new[] { "Big Wheels", "Heavy Engine" }, suv.Components);
            Assert.AreEqual("blue", suv.Configurations["Color"]);
        }

        [TestMethod]
        public void CreateRoadsterUsingABuilder()
        {
            var builder = new RoadsterBuilder();
            builder.BuildProduct();
            builder.AddComponents();
            builder.ConfigureProduct();
            var roadster = builder.GetProduct();

            Assert.AreEqual("Car2", roadster.Name);
            CollectionAssert.AreEquivalent(new[] { "Small Wheels", "Powerful Engine" }, roadster.Components);
            Assert.AreEqual("red", roadster.Configurations["Color"]);
        }

    }
}
