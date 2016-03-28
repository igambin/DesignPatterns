namespace IG.DesignPatternsLibrary.Patterns.AbstractFactory
{

    public enum ObjectType
    {
        Car, House, Dog, Laptop
    }

    public interface IObject
    {
        string Name { get; }
    }
    public abstract class AbstractObject : IObject
    {
        public virtual string Name => GetType().Name;

    }

    public class Car : AbstractObject { }
    public class Dog : AbstractObject { }
    public class House : AbstractObject { }
    public class Laptop : AbstractObject { }


    public interface ICreator
    {
        IObject Create(ObjectType type);
    }


    public abstract class AbstractCreator
    {
        private ICreator Creator;

        public AbstractCreator(ICreator creator)
        {
            Creator = creator;
        }

        public abstract IObject Create();

    }
}