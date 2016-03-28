using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IG.DesignPatternsLibrary.Examples.AbstractFactory
{
    [TestClass]
    public class AbstractFactoryExamples
    {

        [TestMethod]
        public void CreateGrowerEaterFactoryExample()
        {
            //Arrange
            var growerEater = new GrowerEaterFactory();
            var grower = growerEater.Producer;
            var eater = growerEater.Consumer;

            //Act
            var apple1 = grower.Create;
            var apple2 = growerEater.Create;
            var result1 = eater.Consume(apple1);
            var result2 = growerEater.Consume(apple2);
            
            //Assert
            Assert.AreEqual("Eating Apple1", result1);
            Assert.AreEqual("Eating Apple2", result2);
        }

        [TestMethod]
        public void CreateAuthorReaderFactoryExample()
        {
            //Arrange
            var authorReader = new AuthorReaderFactory();
            var author = authorReader.Producer;
            var reader = authorReader.Consumer;

            //Act
            var book1 = authorReader.Create;
            var books2Thru6 = Enumerable.Range(1, 5).Select(x => author.Create).ToList();
            var result1 = authorReader.Consume(book1);
            var result2 = books2Thru6.Select(x => reader.Consume(x));
            var expected = Enumerable.Range(0, 5).Select(x => $"Reading Book{x + 2}");

            //Assert
            Assert.AreEqual("Reading Book1", result1);
            CollectionAssert.AreEquivalent(expected.ToList(), result2.ToList());
        }
    }
}
