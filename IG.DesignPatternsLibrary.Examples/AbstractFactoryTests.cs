using System.Linq;
using IG.DesignPatternsLibrary.Patterns.AbstractFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IG.DesignPatternsLibrary.Examples
{
    [TestClass]
    public class AbstractFactoryTests
    {

        private class Book : AbstractProduct { }

        private class Apple : AbstractProduct { }
 
        private class Author : AbstractProducer<Book> { }

        private class AppleTree : AbstractProducer<Apple> { }

        private class AppleEater : AbstractConsumer<Apple>
        {
            public override string Processor => "Eat";
        }

        private class Reader : AbstractConsumer<Book>
        {
            public override string Processor => "Read";
        }

        private class GrowerReaderFactory : AbstractFactory<AppleTree, AppleEater, Apple> { }

        private class ReaderWriterFactory : AbstractFactory<Author, Reader, Book> { }

        [TestMethod]
        public void CreateFactory()
        {
            var growerEater = new GrowerReaderFactory();

            var grower = growerEater.Producer;
            var eater = growerEater.Consumer;
            var apple1 = grower.Create;
            Assert.AreEqual("Eating Apple1", eater.Consume(apple1));

            var apple2 = growerEater.Create;
            Assert.AreEqual("Eating Apple2", growerEater.Consume(apple2));



            var readerWriter = new ReaderWriterFactory();
            var book1 = readerWriter.Create;
            Assert.AreEqual("Reading Book1", readerWriter.Consume(book1));

            var author = readerWriter.Producer;
            var reader = readerWriter.Consumer;

            var books = Enumerable.Range(1, 5).Select(x => author.Create).ToList();

            books.ForEach(x => Assert.AreEqual($"Reading Book{books.IndexOf(x)+2}", reader.Consume(x)));
        }
    }
}
